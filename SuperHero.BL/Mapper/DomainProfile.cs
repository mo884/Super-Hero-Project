using System;
using AutoMapper;
using SuperHero.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperHero.BL.DomainModelVM;

namespace SuperHero.BL.Mapper
{
    public class DomainProfile:Profile
    {
        public DomainProfile()
        {
          

            CreateMap<Person,CreatePerson>();
            CreateMap<CreatePerson, Person>();

            CreateMap<Person, PersonVM>();
            CreateMap<PersonVM, Person>();

            CreateMap<PersonVM, CreatePerson>();
            CreateMap<CreatePerson, PersonVM>();

            CreateMap<Catogery, CategoryVM>();
            CreateMap<CategoryVM, Catogery>();

            CreateMap<Course, CourseVM>();
            CreateMap<CourseVM, Course>();

            CreateMap<Course, Courseview>();
            CreateMap<Courseview, Course>();

            CreateMap<Lesson, LessonVM>();
            CreateMap<LessonVM, Lesson>();

            CreateMap<UserInfo, UserInfoVM>();
            CreateMap<UserInfoVM, UserInfo>();

            CreateMap<Post, PostVM>();
            CreateMap<PostVM, Post>();

            CreateMap<Post, AuditViewModel>();
            CreateMap<AuditViewModel, Post>();


            CreateMap<Comment, CommentVM>();
            CreateMap<CommentVM, Comment>();
           


            CreateMap<CommentServise, Comment>();
            CreateMap<Comment, CommentServise>();

            CreateMap<DoctorRatingVM, DoctorRating>();
            CreateMap<DoctorRating, DoctorRatingVM>();

            CreateMap<Group, GroupVM>();
            CreateMap<GroupVM, Group>();
            CreateMap<DoctorInfo, DoctorInfoVM>();
            CreateMap<DoctorInfoVM, DoctorInfo>();

            CreateMap<TrainerInfo, TrainerInfoVM>();
            CreateMap<DoctorInfoVM, TrainerInfo>();

            CreateMap<CityVM, City>();
            CreateMap<City, CityVM>();

            CreateMap<Analysis, AnalysisVM>();
            CreateMap<AnalysisVM, Analysis>();

            CreateMap<Analysis, DoctorAnalysis>();
            CreateMap<DoctorAnalysis, Analysis>();
            CreateMap<Radiology, RadiologyVM>();
            CreateMap<RadiologyVM, Radiology>();

            CreateMap<Treatment, TreatmentVM>();
            CreateMap<TreatmentVM, Treatment>();

            CreateMap<Recorder, RecorderVM>();
            CreateMap<RecorderVM, Recorder>();

            CreateMap<Problem, ProblemVM>();
            CreateMap<ProblemVM, Problem>();
        }
    }
}
