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
        Task<IEnumerable<Post>> GetALlPost(string include,string include1, string include2);
        Task<IEnumerable<Comment>> GetAll(int id, string include, string include1);
        Task<Person> GetPersonInclud(string includ, string id);
        Task<ReactPost> GetBYUserAndPost(string id,int postid);
        Task<Friends> GetBYUserFriends(string id, string personid);
        Task<IEnumerable<Friends>> GetBYUserFriends( string personid);
        Task<bool> GetAll(int id, string personId);
        //Task<bool> FindByIdAsync(string personId, int groupId);
        Task<PersonGroup> FindById(string personId, int groupId);
        Task<bool> Create(int id, string personId);
        Task<bool> Delete(int id, string personId);
        Task<IEnumerable<Person>> GetDoctor(int Districtid, int cityid, int GovernorateID);
        Task<IEnumerable<City>> GetCityAsync(Expression<Func<City, bool>> filter = null);
        Task<IEnumerable<District>> GetDistAsync(Expression<Func<District, bool>> filter = null);
        Task<IEnumerable<Governorate>> GetGovAsync(Expression<Func<Governorate, bool>> filter = null);
        Task<IEnumerable<Lesson>> GetLessonByID(int id);
        Task<IEnumerable<CoursesComment>> GetAllCoursesComment(int id, string include, string include1);
    }
}
