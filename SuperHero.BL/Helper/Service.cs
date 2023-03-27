﻿using FRYMA_SuperHero.BL.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using SuperHero.BL.DomainModelVM;
using SuperHero.BL.Interface;
using SuperHero.BL.Reposoratory;
using SuperHero.DAL.Database;
using SuperHero.DAL.Entities;
using SuperHero.DAL.Enum;
using SuperHero.DAL.Enum;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.Helper
{
    public class Service
    {
        #region Add Person
        public static async Task<Person> Add(CreatePerson model, int num)
        {
            try
            {
                if (num == 0)
                {
                    if(model.Image is null)
                    {
                        model.Image = null;
                    }
                    var user = new Person()
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        districtID = model.districtID,
                        Image = model.Image,
                        FullName = model.FullName,
                        gender = (model.gender == 0) ? Gender.Male : Gender.Female,
                        PersonType = PersonType.User,
                        patient = new UserInfo()
                        {
                            Reason = model.patient.Reason,
                        }
                    };
                    return user;
                }
                else if (num == 1)
                {
                    var user = new Person()
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        districtID = model.districtID,
                        //GroupID =model.GroupID,
                        Image = model.Image,
                        gender = (model.gender == 0) ? Gender.Male : Gender.Female,
                        FullName = model.FullName,
                        PersonType = PersonType.Doctor,
                        doctor = new DoctorInfo()
                        {
                            CV = FileUploader.UploadFile("CVDoctors", model.doctor.Cv_Name),
                            ClinicAdress = model.doctor.ClinicAdress,
                            ClinicName = model.doctor.ClinicName
                        }
                    };
                    return user;
                }
                else if (num == 2)
                {
                    var user = new Person()
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        districtID = model.districtID,
                        Image = model.Image,
                        gender = (model.gender == 0) ? Gender.Male : Gender.Female,
                        FullName = model.FullName,
                        PersonType = PersonType.Doner,
                        donner = new DonnerInfo()
                        {
                            DonationType = model.doner.DonationType,
                        }
                    };
                    return user;
                }
                else if (num == 3)
                {

                    var user = new Person()
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        districtID = model.districtID,
                        Image = model.Image,
                        gender = (model.gender == 0) ? Gender.Male : Gender.Female,
                        FullName = model.FullName,
                        PersonType = PersonType.Trainer,
                        trainer = new TrainerInfo()
                        {
                            CV = FileUploader.UploadFile("CVDoctors", model.trainer.Cv_Name),
                            Graduation = model.trainer.Graduation,

                        }
                    };
                    return user;
                }
                else
                {
                    var user = new Person()
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        districtID = model.districtID,
                        Image = model.Image,
                        gender = (model.gender == 0) ? Gender.Male : Gender.Female,
                        FullName = model.FullName,
                        PersonType = PersonType.Admin,

                    };
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }

        }

        #endregion

        #region Add Post
        public static async Task<AuditViewModel> CreatePost(AuditViewModel postvm, Person person)
        {
            string Image = FileUploader.UploadFile("Imgs", postvm.ImageName);
            if (Image == null)
                postvm.Image = null;
            else
                postvm.Image = Image;
            postvm.PersonID = person.Id;
            postvm.CreatedTime = DateTime.Now;
            postvm.person = person;
            return postvm;



        }
        #endregion

        #region Add Comment
        public static async Task<CommentServise> Createcomment(CommentServise comment, Person person)
        {
            comment.UserID = person.Id;
            comment.person = person;
            comment.createdOn = DateTime.Now;
            return comment;
        }
        #endregion


    }
}
