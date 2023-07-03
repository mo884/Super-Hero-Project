using AutoMapper;
using FRYMA_SuperHero.BL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SuperHero.BL.DomainModelVM;
using SuperHero.BL.Helper;
using SuperHero.BL.Interface;
using SuperHero.DAL.Database;
using SuperHero.DAL.Entities;
using SuperHero.DAL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SuperHero.BL.Reposoratory
{
    public class ServiesRep : IServiesRep
    {
        #region Prop
        protected ApplicationContext Db;
        private readonly IBaseRepsoratory<Comment> comment;
        private readonly IBaseRepsoratory<Person> person;
        private readonly IBaseRepsoratory<Friends> allfriends;
        private readonly IBaseRepsoratory<UserInfo> user;
        private readonly IBaseRepsoratory<DonnerInfo> donner;
        private readonly IBaseRepsoratory<DoctorInfo> doctor;
        private readonly IMapper mapper;
        private readonly IBaseRepsoratory<TrainerInfo> trainer;
        #endregion

        #region Ctor
        public ServiesRep(ApplicationContext Db, IBaseRepsoratory<Comment> comment, IBaseRepsoratory<Person> person, IBaseRepsoratory<Friends> allfriends, IBaseRepsoratory<UserInfo> user, IBaseRepsoratory<DonnerInfo> donner, IBaseRepsoratory<DoctorInfo> doctor, IMapper mapper, IBaseRepsoratory<TrainerInfo> trainer)
        {
            this.Db = Db;
            this.comment = comment;
            this.person = person;
            this.allfriends = allfriends;
            this.user = user;
            this.donner = donner;
            this.doctor = doctor;
            this.mapper = mapper;
            this.trainer = trainer;
        }
        #endregion

        #region update Person(Patien - Doctor - Trainer - Admin - Donner)
        public async Task Update(PersonVM obj)
        {
            
            var OldData = await person.GetByID(obj.Id);

            OldData.FullName = obj.FullName;
            OldData.Email = obj.Email;
            OldData.gender = (obj.gender == 0) ? Gender.Male : Gender.Female;
            OldData.districtID = obj.districtID;
            if (obj.Image != null)
            {
                if (OldData.PersonType == PersonType.User)
                {
                    var olduserData = await GetPatientBYID(OldData.Id);
                    OldData.patient = new UserInfo()
                    {
                        ID = olduserData.ID,
                        Reason = obj.patient.Reason
                    };

                }
                else if (OldData.PersonType == PersonType.Doctor)
                {
                    if (obj.doctor.Cv_Name != null)
                    {
                        var olduserData = await GetDoctorBYID(OldData.Id);
                        DoctorInfo doctorInfo = new DoctorInfo()
                        {

                            CV = FileUploader.UploadFile("CVDoctors", obj.doctor.Cv_Name),
                            ClinicAdress = obj.doctor.ClinicAdress,
                            ClinicName = obj.doctor.ClinicName,
                            DectorID = obj.Id
                        };
                        await doctor.Delete(olduserData);
                        await doctor.Create(doctorInfo);
                    }

                }
                else if (OldData.PersonType == PersonType.Trainer)
                {
                    if (obj.trainer.Cv_Name != null)
                    {
                        var olduserData = await GetTrainerBYID(OldData.Id);

                        var Trainer = new TrainerInfo()
                        {

                            CV = FileUploader.UploadFile("CVDoctors", obj.trainer.Cv_Name),
                            Graduation = obj.trainer.Graduation,
                            TrainerID = OldData.Id,
                            Person = OldData,
                        };

                        await trainer.Create(Trainer);
                        await trainer.Delete(olduserData.ID);
                    }
                  

                }
                else if (OldData.PersonType == PersonType.Doner)
                {

                    if (obj.doner != null)
                    {
                        var olduserData = await GetDonnerBYID(OldData.Id);
                        OldData.donner = new DonnerInfo()
                        {

                            DonationType = obj.doner.DonationType,
                            DonnerID = obj.Id

                        };

                    }

                }
                OldData.Image = obj.Image;
            }


            Db.SaveChanges();
        }
        #endregion

        #region Get By Id => Person(Patien - Doctor - Trainer - Donner -  Person)
        public async Task<UserInfo> GetPatientBYID(string id)
        {
            var user = Db.UserInfos.Where(a => a.UserID == id).FirstOrDefaultAsync();
            return await user;
        }
        public async Task<DoctorInfo> GetDoctorBYID(string id)
        {
            var doctor = Db.DoctorsInfos.Where(a => a.DectorID == id).FirstOrDefaultAsync();
            return await doctor;
        }
        public async Task<IEnumerable<Person>> GetDoctor(int Districtid, int cityid, int GovernorateID)
        {
            var data = await Db.Persons.Where(doctor => doctor.PersonType == PersonType.Doctor && (doctor.districtID == Districtid || doctor.district.CityId == cityid || doctor.district.City.GovernorateID == GovernorateID)).Include("district").Include("friends").ToListAsync();
            if (data.Count() != 0)
                return data;
            return await Db.Persons.Where(doctor => doctor.PersonType == PersonType.Doctor).Include("district").Include("friends").ToListAsync();
        }
        public async Task<TrainerInfo> GetTrainerBYID(string id)
        {

            var Trainer = Db.TrainerInfos.Where(a => a.TrainerID == id).FirstOrDefaultAsync();
            return await Trainer;
        }


        public async Task<DonnerInfo> GetDonnerBYID(string id)
        {

            var donner = Db.DonnerInfos.Where(a => a.DonnerID == id).FirstOrDefaultAsync();
            return await donner;
        }

        public async Task<Person> GetPersonBYID(string id)
        {
            var user = Db.Persons.Where(a => a.Id == id).FirstOrDefaultAsync();
            return await user;
        }
        public async Task<Person> GetPatientProfile(string id)
        {
            var user = await Db.Persons.Where(a => a.Id == id).Include("district").Include("friends").Include("patient").FirstOrDefaultAsync();
            user.district.City = await Db.Cities.Where(a => a.ID == user.district.CityId).SingleOrDefaultAsync();
            user.patient.Treatments = await GetAllTreatmentbyId(user.patient.ID);
            user.patient.Analyses = await GetAllAnalysisbyId(user.patient.ID);
            user.patient.radiologies = await GetAllRadiologybyId(user.patient.ID);
            return user;
        }
        #endregion

        #region Get Person By UserName
        public async Task<Person> GetBYUserName(string Name)
        {
            var user = Db.Persons.Where(a => a.UserName == Name).FirstOrDefaultAsync();
            return await user;
        }
        public async Task<Person> GetBYEmail(string Name)
        {
            var user = Db.Persons.Where(a => a.Email == Name).FirstOrDefaultAsync();
            return await user;
        }
        #endregion

        #region Get Person and PersonFriends and Person Comments and ReactPost By Id and use Include 

        public async Task<Person> GetPersonInclud(string include, string Id)
        {
            var user = await Db.Persons.Where(p => p.Id == Id).Include(include).Include("friends").SingleOrDefaultAsync();
            user.district.City = await Db.Cities.Where(a => a.ID == user.district.CityId).SingleOrDefaultAsync();
            user.Posts = await Db.Posts.Where(post => post.PersonID == Id).Include("Comments").Include("ReactPosts").ToListAsync();

            return user;
        }

        #endregion

        #region Post => GetAllPost  by three include and Descending and by id

        public async Task<IEnumerable<Post>> GetALlPost(string include, string include1, string include2)
        {
            var post = await Db.Posts.Include(include).Include(include1).Include(include2).OrderByDescending(d => d.CreatedTime).ToListAsync();
            return post;
        }
        public async Task<Post> GetPostById(int id, string include1, string include2, string include3)
        {
            var post = await Db.Posts.Where(p => p.ID == id).Include(include1).Include(include2).Include(include3).FirstOrDefaultAsync();
            var Comments = await Db.Comments.Where(c => c.PostID == id).Include("person").ToListAsync();
            post.Comments = Comments;
            return post;
        }
        public async Task EditPost(PostVM postVM)
        {
            var data = await Db.Posts.Where(post => post.ID == postVM.ID).SingleOrDefaultAsync();
            var Person = await Db.Persons.Where(person => person.Id == postVM.PersonID).SingleOrDefaultAsync();
            data.Image = postVM.Image;
            data.Body = postVM.Body;
            data.person = Person;
            data.PersonID = postVM.PersonID;
            Db.SaveChanges();
        }
        #endregion

        #region GetAll Comment and Include
        public async Task<IEnumerable<Comment>> GetAll(int id, string include, string include1)
        {
            var comments = await Db.Comments.Where(c => c.PostID == id).Include(include).Include(include1).ToListAsync();
            return comments;
        }
        public async Task<IEnumerable<CoursesComment>> GetAllCoursesComment(int id, string include, string include1)
        {
            var comments = await Db.coursesComments.Where(c => c.courseId == id).Include(include).Include(include1).ToListAsync();
            return comments;
        }
        #endregion

        #region Get Person React with Post in table React Post

        public async Task<ReactPost> GetBYUserAndPost(string id, int postid)
        {
            var react = await Db.ReactPosts.Where(c => c.PostID == postid && c.PersonID == id).SingleOrDefaultAsync();
            return react;
        }

        #endregion

        #region Group ❤ Youmna
        public async Task<bool> GetAll(int id, string personId)
        {
            var data = await Db.personGroups.Where(g => g.PersonId == personId && g.Group == id).SingleOrDefaultAsync();
            if (data is null)
                return false;
            return true;
        }
        public async Task<bool> Create(int id, string personId)
        {
            var data = await Db.personGroups.Where(g => g.Group == id && g.PersonId == personId).SingleOrDefaultAsync();
            if (data is null)
                return true;
            return false;

        }
        public async Task<bool> Delete(int id, string personId)
        {
            var data = await Db.personGroups.Where(g => g.Group == id && g.PersonId == personId).SingleOrDefaultAsync();
            if (data is null)
                return false;
            return true;
        }

        public async Task<PersonGroup> FindById(string personId, int groupId)
        {

            var data = await Db.personGroups.Where(a => a.PersonId == personId && a.Group == groupId).FirstOrDefaultAsync();
            return data;
        }
        public async Task<IEnumerable<PersonGroup>> FindAllGroupById(string personId)
        {

            var data = await Db.personGroups.Where(a => a.PersonId == personId).Include("Person").Include("group").ToListAsync();
            return data;
        }
        #region Delete Group
        public async Task DeletePersonGroup(int id)
        {
            var persongroup = await Db.personGroups
                .Where(a => a.Group == id).ExecuteDeleteAsync();


            Db.SaveChanges();
        }
        #endregion
        #endregion

        #region Friends
        public async Task<Friends> GetBYUserFriends(string id, string personid)
        {
            var friend = await Db.Friends.Where(friend => friend.personId == id && friend.FriendId == personid).SingleOrDefaultAsync();
            return friend;
        }



        public async Task<IEnumerable<Friends>> GetFollower(string id)
        {
            var follwers = await Db.Friends.Where(a => a.FriendId == id && a.IsFriend).ToListAsync();
            return follwers;
        }

        public async Task<IEnumerable<Friends>> GetBYUserFriends(string personid)
        {
            var data = await Db.Friends.Where(a => a.personId == personid && a.IsFriend == true).Include("person").ToListAsync();
            return data;
        }
        #endregion

        #region  filter address ❤ fatma 
        public async Task<IEnumerable<Governorate>> GetGovAsync(Expression<Func<Governorate, bool>> filter = null)
        {
            if (filter != null)
                return
                      await Db.Governorates.Where(filter).ToListAsync();
            else
                return await Db.Governorates.ToListAsync();
        }
        public async Task<IEnumerable<City>> GetCityAsync(Expression<Func<City, bool>> filter = null)
        {
            if (filter != null)
            {
                return
                  await Db.Cities.Where(filter)
                               .ToListAsync();
            }
            else
                return
                    await Db.Cities
                                 .ToListAsync();
        }
        public async Task<IEnumerable<District>> GetDistAsync(Expression<Func<District, bool>> filter = null)
        {
            if (filter != null)
            {
                return
                  await Db.District.Where(filter)
                               .ToListAsync();
            }
            else
                return
                    await Db.District
                                 .ToListAsync();
        }
        public async Task<IEnumerable<Person>> GetPersonAsync(Expression<Func<Person, bool>> filter = null)
        {
            if (filter != null)
            {
                return
                  await Db.Persons.Where(filter)
                               .ToListAsync();
            }
            else
                return
                    await Db.Persons
                                    .ToListAsync();
        }


        #endregion

        #region Get Lessons by Id
        public async Task<IEnumerable<Lesson>> GetLessonByID(int id)
        {
            var lessons = await Db.Lessons
                .Where(a => a.CourseID == id).ToListAsync();
            return lessons;
        }

        #endregion

        #region Add Doctor Reating ❤ Radwa
        public async Task<DoctorRatingVM> AddDoctorReating(DoctorRatingVM doctorRating, string PersonId, string DoctorId, float reating)
        {

            DoctorRatingVM rate = new DoctorRatingVM()
            {
                DoctorId = (string)DoctorId,
                PersonID = PersonId,
                IsReating = true,
                reating = reating,
                description = doctorRating.description

            };
            return rate;
        }
        #endregion

        #region Add Doctor Reating ❤ Ameen
        public async Task<DoctorRatingVM> DoctorRatingISTrue(string PersonId, string DoctorId)
        {

            var DoctorRating = await Db.DoctorRating.Where(a => a.DoctorId == DoctorId && a.PersonID == PersonId).FirstOrDefaultAsync();
            var IsReat = mapper.Map<DoctorRatingVM>(DoctorRating);
            if (IsReat != null)
            {
                IsReat.PersonIsReating = true;
                return IsReat;
            }

            return null;




        }


        #endregion

        #region Get analysis
        public async Task<List<Analysis>> GetAllAnalysisbyId(int userinfo)
        {
            var data = await Db.Analyses.Where(a => a.personID == userinfo).Include("patient").ToListAsync();
            return data;
        }
        public async Task Create(DoctorAnalysis analysis, string DoctorId)
        {
            analysis.patient = await Db.UserInfos.Where(a => a.ID == analysis.personID).FirstOrDefaultAsync();

            var data = new Analysis()
            {
                Name = analysis.Name,
                personID = analysis.personID,
                patient = analysis.patient,
                DoctorID = DoctorId,
                IsAdd = false
            };

            Db.Analyses.Add(data);
            Db.SaveChanges();
        }
        public async Task CreateBYUser(DoctorAnalysis analysis)
        {
            analysis.patient = await Db.UserInfos.Where(a => a.ID == analysis.personID).FirstOrDefaultAsync();

            var data = new Analysis()
            {
                Name = analysis.Name,
                personID = analysis.personID,
                patient = analysis.patient,
                AnalysisPDF = analysis.AnalysisPDF,
                IsAdd = true
            };

            Db.Analyses.Add(data);
            Db.SaveChanges();
        }
        #endregion

        #region Get Treatment
        public async Task<List<Treatment>> GetAllTreatmentbyId(int userinfo)
        {
            var data = await Db.Treatments.Where(a => a.personID == userinfo).ToListAsync();
            return data;
        }
        public async Task CreateTreatment(DoctorTreatment Treatment, string DoctorId)
        {
            Treatment.patient = await Db.UserInfos.Where(a => a.ID == Treatment.personID).FirstOrDefaultAsync();

            var data = new Treatment()
            {
                Name = Treatment.Name,
                Description = Treatment.Description,
                personID = Treatment.personID,
                patient = Treatment.patient,
                DoctorID = DoctorId,
                IsAdd = false,
            };

            Db.Treatments.Add(data);
            Db.SaveChanges();
        }
        #endregion

        #region Get Radiology
        public async Task<List<Radiology>> GetAllRadiologybyId(int userinfo)
        {
            var data = await Db.Radiologies.Where(a => a.personID == userinfo).ToListAsync();
            return data;
        }
        public async Task CreateRadiology(DoctorRadiology Radiology, string DoctorId)
        {
            Radiology.patient = await Db.UserInfos.Where(a => a.ID == Radiology.personID).FirstOrDefaultAsync();

            var data = new Radiology()
            {
                Name = Radiology.Name,
                personID = Radiology.personID,
                patient = Radiology.patient,
                DoctorID = DoctorId,
                IsAdd = false,
            };

            Db.Radiologies.Add(data);
            Db.SaveChanges();
        }
        public async Task CreateRadiologyBYPatient(DoctorRadiology Radiology)
        {
            Radiology.patient = await Db.UserInfos.Where(a => a.ID == Radiology.personID).FirstOrDefaultAsync();

            var data = new Radiology()
            {
                Name = Radiology.Name,
                personID = Radiology.personID,
                patient = Radiology.patient,
                XRay = Radiology.XRay,
                IsAdd = true,
            };

            Db.Radiologies.Add(data);
            Db.SaveChanges();
        }
        #endregion


        #region Record && Clicnic 
        public async Task SaveRecord(string PersonId, string DoctorId, Recorder record)
        {
            var Record = await Db.Recorders.Where(a => a.DoctorID == DoctorId && PersonId == a.PatientID).FirstOrDefaultAsync();
            var person = await Db.Persons.Where(a => a.Id == PersonId).FirstOrDefaultAsync();
            if (Record == null)
            {
                var data = new Recorder()
                {
                    PatientID = PersonId,
                    DoctorID = DoctorId,
                    IsCheck = false,
                    Patient = person,
                    RecodDate = record.RecodDate,
                };

                Db.Recorders.Add(data);
                Db.SaveChanges();
            }
            else
            {
                if (Record.IsCheck)
                {
                    Record.IsCheck = false;
                    Record.RecodDate = record.RecodDate;

                }
                else
                {
                    Record.IsCheck = true;
                    Record.RecodDate = record.RecodDate;

                }
                Db.Recorders.Update(Record);
                Db.SaveChanges();
            }


        }

        //Get All Patient Record
        public async Task<IEnumerable<Recorder>> GetAllPatientRecord(string DoctorId)
        {
            var data = await Db.Recorders.Where(a => a.DoctorID == DoctorId).Include("Patient").ToListAsync();
            return data;
        }
        //Get Patient
        public async Task<Recorder> GetPatientRecordBYID(string DoctorId, string PatientId)
        {
            var data = await Db.Recorders.Where(a => a.DoctorID == DoctorId && a.PatientID == PatientId).Include("Patient").FirstOrDefaultAsync();
            return data;
        }
        //Get Patient with info
        public async Task<Person> GetPatientRecord(string PatientId)
        {

            var data = await Db.Persons.Where(a => a.Id == PatientId).Include("patient").Include("district").Include("Recorder").FirstOrDefaultAsync();
            data.patient.Analyses = await GetAllAnalysisbyId(data.patient.ID);
            data.patient.Treatments = await GetAllTreatmentbyId(data.patient.ID);
            data.patient.radiologies = await GetAllRadiologybyId(data.patient.ID);

            return data;
        }
        #endregion

        #region Edite Lesson
        public async Task EditeLessonByID(LessonVM lessonVM)
        {
            var lessons = await Db.Lessons
                .Where(a => a.ID == lessonVM.ID).SingleOrDefaultAsync();
            lessons.Num = lessonVM.Num;
            lessons.Name = lessonVM.Name;

            Db.SaveChanges();
        }


        #endregion

        #region GetAllSocial
        public async Task<AuditViewModel> GetAllSocial(Person PersonProfile)
        {
            var data = await GetALlPost("person", "Comments", "ReactPosts");
            var post = mapper.Map<IEnumerable<PostVM>>(data);
            var dataDoctor = await GetPersonInclud("district", PersonProfile.Id);
            var Patient = mapper.Map<CreatePerson>(dataDoctor);
            var Doctor = await GetDoctor(Patient.districtID, Patient.district.CityId, Patient.district.City.GovernorateID);
            var Doctorvm = mapper.Map<IEnumerable<CreatePerson>>(Doctor);
            var Friends = await GetBYUserFriends(PersonProfile.Id);

            var p = new AuditViewModel { post = post, nearDoctor = Doctorvm, friends = Friends, Allfriends = await allfriends.GetAll() };
            if (PersonProfile.PersonType == 0)
            {
                var patient = await GetPersonInclud("patient", PersonProfile.Id);
                p.person = patient;
            }
            else
            {
                p.person = PersonProfile;
            }
            return p;
        }
        #endregion

        #region GetMedicalSyndicate
        public async Task<bool> GetMedicalSyndicate(string NummberSyndicate)
        {
            var ISFound = await Db.MedicalSyndicates.Where(a => a.NUMMBERSYNDICATE == NummberSyndicate).FirstOrDefaultAsync();
            if (ISFound == null)
                return false;
            return true;
        }
        #endregion

        #region Chat
        public async Task<IEnumerable<ChatGroup>> GetAllChatGroup(int GroupId)
        {
            var Chat = await Db.ChatGroups.Where(a => a.groupId == GroupId).Include("Person").Include("group").ToListAsync();
            return Chat;
        }
        public async Task<IEnumerable<PrivateChat>> GetAllPrivateChat(string SenderID, string ReciverID)
        {
            var Chat = await Db.PrivateChats.Where(a => (a.SenderID == SenderID && a.RecivierID == ReciverID) || (a.SenderID == ReciverID && a.RecivierID == SenderID)).Include("Sender").ToListAsync();
            return Chat;
        }
        #endregion

        #region Search
        public async Task<IEnumerable<Person>> Search(string query)
        {
            var searchResults = await Db.Persons
             .Where(p => p.FullName.Contains(query))
            .ToListAsync();
            return searchResults;
        }

        #endregion
        #region Notification
        public async Task<List<NotificationMessage>> GetNotiFications(Expression<Func<NotificationMessage, bool>> filter = null)
        {
            if (filter != null)
            {
                var data = await Db.NotificationMessages.Where(filter).ToListAsync();
                return data;

            }
            else
            {
                return await Db.NotificationMessages.ToListAsync();
            }
        }

        public async Task<bool> IsRead(string UserId)
        {
            var data = await Db.NotificationMessages.Where(x => x.ReciverID == UserId).ToListAsync();
            foreach (var item in data)
            {
                item.Show = true;
            }

            try
            {

                await Db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return
                    false;
                throw ex;
            }


        }


        #endregion
        #region Best 5 Doctor
        public async Task<float> AVG(string Id)
        {
            var data = await Db.DoctorRating.Where(a => a.DoctorId == Id).ToListAsync();
            float? sum = 0;
            for (int i = 0; i < data.Count(); i++)
            {
                sum = data[i].reating;
            }
            float? rating = sum/data.Count();
            return (float)rating;
        }
        public async Task<SecondPage> BestDoctor()
        {
            IEnumerable<Person> AllDoctor = await Db.Persons.Where(doctor => doctor.PersonType == PersonType.Doctor).Include("DoctorRating").Include("doctor").ToListAsync();
            
            foreach (var item in AllDoctor)
            {
                float Rating =await AVG(item.Id);
                item.doctor.Rating = Rating;
                
            }
            SecondPage All = new SecondPage()
            {
                Doctor = AllDoctor.OrderByDescending(a => a.doctor.Rating).Take(5),
                DoctorCount = await Db.Persons.Where(a => a.PersonType == PersonType.Doctor).CountAsync(),
                DonnerCount = await Db.Persons.Where(a => a.PersonType == PersonType.Doner).CountAsync(),
                TrainnerCount = await Db.Persons.Where(a => a.PersonType == PersonType.Trainer).CountAsync(),
                UserCount = await Db.Persons.Where(a=>a.PersonType == PersonType.User).CountAsync(),
            };
            return All;
        }

        #endregion
    }
}
