using Microsoft.AspNetCore.Http;
using SuperHero.BL.DomainModelVM;
using SuperHero.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.Interface
{
    public interface IServiesRep
    {
        Task Update(PersonVM obj);
        Task<DoctorInfo> GetDoctorBYID(string id);
        Task<TrainerInfo> GetTrainerBYID(string id);
        Task<DonnerInfo> GetDonnerBYID(string id);
        Task<UserInfo> GetPatientBYID(string id);
        Task<Person> GetPersonBYID(string id);
        Task<Person> GetBYUserName(string Name);
        Task<Person> GetBYEmail(string Name);
        Task<IEnumerable<Post>> GetALlPost(string include,string include1, string include2);
        Task<Post> GetPostById(int id, string include1, string include2, string include3);
        Task<IEnumerable<Comment>> GetAll(int id, string include, string include1);
        Task<Person> GetPersonInclud(string includ, string id);
        Task<Person> GetPatientProfile(string id);
        Task<ReactPost> GetBYUserAndPost(string id,int postid);
        Task EditPost(PostVM postVM);
        Task<Friends> GetBYUserFriends(string id, string personid);
        Task<IEnumerable<Friends>> GetFollower(string id);
        Task<IEnumerable<Friends>> GetBYUserFriends( string personid);
        Task<bool> GetAll(int id, string personId);
        //Task<bool> FindByIdAsync(string personId, int groupId);
        Task<PersonGroup> FindById(string personId, int groupId);
        Task DeletePersonGroup(int id);
        Task<IEnumerable<PersonGroup>> FindAllGroupById(string PersonId);
        Task<bool> Create(int id, string personId);
        Task<bool> Delete(int id, string personId);
        Task<IEnumerable<Person>> GetDoctor(int Districtid, int cityid, int GovernorateID);
        Task<IEnumerable<City>> GetCityAsync(Expression<Func<City, bool>> filter = null);
        Task<IEnumerable<District>> GetDistAsync(Expression<Func<District, bool>> filter = null);
        Task<IEnumerable<Governorate>> GetGovAsync(Expression<Func<Governorate, bool>> filter = null);
        Task<IEnumerable<Lesson>> GetLessonByID(int id);
        Task<IEnumerable<CoursesComment>> GetAllCoursesComment(int id, string include, string include1);
        Task<DoctorRatingVM> AddDoctorReating(DoctorRatingVM doctorRating, string PersonId, string DoctorId, float reating);
        Task<DoctorRatingVM> DoctorRatingISTrue(string PersonId, string DoctorId);
        Task EditeLessonByID(LessonVM lessonVM);
        Task<AuditViewModel> GetAllSocial(Person PersonProfile);
        Task SaveRecord(string PersonId, string DoctorId, Recorder record);
        Task<Recorder> GetPatientRecordBYID(string DoctorId, string PatientId);
        Task<IEnumerable<Recorder>> GetAllPatientRecord(string DoctorId);
        Task<Person> GetPatientRecord(string PatientId);
        Task<List<Analysis>> GetAllAnalysisbyId(int userinfo);
        Task Create(DoctorAnalysis analysis, string DoctorId);
        Task CreateBYUser(DoctorAnalysis analysis);
        Task<List<Treatment>> GetAllTreatmentbyId(int userinfo);
        Task CreateTreatment(DoctorTreatment Treatment, string DoctorId);
        Task<List<Radiology>> GetAllRadiologybyId(int userinfo);

        Task CreateRadiology(DoctorRadiology Radiology, string DoctorId);
        Task CreateRadiologyBYPatient(DoctorRadiology Radiology);
        Task<bool> GetMedicalSyndicate(string NummberSyndicate);
        Task<IEnumerable<ChatGroup>> GetAllChatGroup(int GroupId);
        Task<IEnumerable<PrivateChat>> GetAllPrivateChat(string SenderID, string ReciverID);
        Task<IEnumerable<Person>> Search(string query);


        Task<List<NotificationMessage>> GetNotiFications(Expression<Func<NotificationMessage, bool>> filter = null);

        Task<bool> IsRead(string UserId);
         Task<SecondPage> BestDoctor();
    }
}
