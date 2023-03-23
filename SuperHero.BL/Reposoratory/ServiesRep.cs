using FRYMA_SuperHero.BL.Interface;
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
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.Reposoratory
{
    public class ServiesRep : IServiesRep
    {
        #region Prop
        protected ApplicationContext Db;
        private readonly IBaseRepsoratory<Comment> comment;
        private readonly IBaseRepsoratory<Person> person;
        private readonly IBaseRepsoratory<UserInfo> user;
        private readonly IBaseRepsoratory<DonnerInfo> donner;
        private readonly IBaseRepsoratory<DoctorInfo> doctor;
        private readonly IBaseRepsoratory<TrainerInfo> trainer;
        #endregion

        #region Ctor
        public ServiesRep(ApplicationContext Db, IBaseRepsoratory<Comment> comment, IBaseRepsoratory<Person> person, IBaseRepsoratory<UserInfo> user, IBaseRepsoratory<DonnerInfo> donner, IBaseRepsoratory<DoctorInfo> doctor, IBaseRepsoratory<TrainerInfo> trainer)
        {
            this.Db = Db;
            this.comment = comment;
            this.person = person;
            this.user = user;
            this.donner = donner;
            this.doctor = doctor;
            this.trainer = trainer;
        }
        #endregion

        #region update Person(Patien - Doctor - Trainer - Admin - Donner)
        public async Task Update(CreatePerson obj)
        {
            var OldData = await person.GetByID(obj.Id);

            OldData.FullName = obj.FullName;
            OldData.Email = obj.Email;
            OldData.gender = (obj.gender == 0) ? Gender.Male : Gender.Female;

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
                        var olduserData = await GetTrainerBYID(OldData.Id);
                        OldData.doctor = new DoctorInfo()
                        {
                            ID = olduserData.ID,
                            CV = FileUploader.UploadFile("CVDoctors", obj.doctor.Cv_Name),
                            ClinicAdress = obj.doctor.ClinicAdress,
                            ClinicName = obj.doctor.ClinicName
                        };
                    }

                }
                else if (OldData.PersonType == PersonType.Trainer)
                {
                    if (obj.trainer.Cv_Name != null)
                    {
                        var olduserData = await GetTrainerBYID(OldData.Id);
                        OldData.trainer = new TrainerInfo()
                        {
                            ID = olduserData.ID,
                            CV = FileUploader.UploadFile("CVDoctors", obj.doctor.Cv_Name),

                        };

                    }

                }
                else if (OldData.PersonType == PersonType.Doner)
                {

                    if (obj.doner != null)
                    {
                        var olduserData = await GetDonnerBYID(OldData.Id);
                        OldData.donner = new DonnerInfo()
                        {
                            ID = olduserData.ID,
                            DonationType = obj.doner.DonationType,

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
        #endregion

        #region Get Person By UserName
        public async Task<Person> GetBYUserName(string Name)
        {
            var user = Db.Persons.Where(a => a.UserName == Name).FirstOrDefaultAsync();
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

        #region Post => GetAllPost  by three include and Descending

        public async Task<IEnumerable<Post>> GetALlPost(string include, string include1, string include2)
        {
            var post = await Db.Posts.Include(include).Include(include1).Include(include2).OrderByDescending(d => d.CreatedTime).ToListAsync();
            return post;
        }
        #endregion

        #region GetAll Comment and Include
        public async Task<IEnumerable<Comment>> GetAll(int id, string include, string include1)
        {
            var comments = await Db.Comments.Where(c => c.PostID == id).Include(include).Include(include1).ToListAsync();
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

        #region Group
        public async Task<bool> GetAll(int id, string personId)
        {
            var data = await Db.personGroups.Where(g => g.PersonId == personId && g.Group == id).FirstOrDefaultAsync();
            if (data is null)
                return false;
            return true;
        }
        public async Task<bool> Create(int id, string personId)
        {
            var data = await Db.personGroups.Where(g => g.Group == id && g.PersonId == personId).SingleOrDefaultAsync();
            if (data is null)
                return false;
            return true;
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
        #endregion

        #region Friends
        public async Task<Friends> GetBYUserFriends(string id, string personid)
        {
            var friend = await Db.Friends.Where(friend => friend.personId == id && friend.FriendId == personid).SingleOrDefaultAsync();
            return friend;
        }



        public async Task<IEnumerable<Friends>> GetBYUserFriends(string personid)
        {
            var data = await Db.Friends.Where(a => a.personId == personid && a.IsFriend == true).Include("person").ToListAsync();
            return data;
        }
        #endregion

    }
}
