using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.DomainModelVM;
using SuperHero.BL.Helper;
using SuperHero.BL.Interface;
using SuperHero.BL.Seeds;
using SuperHero.DAL.Entities;
using System.Configuration;

namespace SuperHero.PL.Controllers.Admin.Persons
{
    //[Authorize(Roles = AppRoles.Doctor)]
    public class DoctorController : Controller
    {
        #region Prop
        private readonly UserManager<Person> userManager;
        private readonly IMapper mapper;
        private readonly SignInManager<Person> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IServiesRep servis;
        private readonly IBaseRepsoratory<Person> person;
        private readonly IBaseRepsoratory<District> district;
        private readonly IBaseRepsoratory<City> city;
        private readonly IBaseRepsoratory<Governorate> governorate;
        private IConfiguration Configuration;
        #endregion

        #region Ctor
        public DoctorController(UserManager<Person> userManager, IConfiguration Configuration, IMapper mapper, SignInManager<Person> signInManager, RoleManager<IdentityRole> roleManager, IServiesRep servis, IBaseRepsoratory<Person> person, IBaseRepsoratory<District> district, IBaseRepsoratory<City> city, IBaseRepsoratory<Governorate> governorate)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.servis = servis;
            this.person = person;
            this.district = district;
            this.city = city;
            this.governorate = governorate;
            this.Configuration = Configuration;
        }
        #endregion

        #region Create Doctor
        [HttpGet]
        public async Task<IActionResult> CreateDoctor()
        {
            if (signInManager.IsSignedIn(User))
            {
                TempData["Message"] = null;
                return View();
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> CreateDoctor(PersonVM model)
        {
            if (signInManager.IsSignedIn(User))
            {
                try
                {
                    //Save Image Profile
                    model.Image = FileUploader.UploadFile("Imgs", model.ImageName);

                    if (ModelState.IsValid)
                    {
                        //Map PersonVM to Class CreatePerson
                        var doctor = mapper.Map<CreatePerson>(model);
                        //Create Doctor 
                        var result = await userManager.CreateAsync(await Service.Add(doctor, 1), model.PasswordHash);
                        //Get Doctor By Id
                        var Doctor = await servis.GetBYUserName(model.UserName);
                        //Get Role Doctor
                        var role = await roleManager.FindByNameAsync(AppRoles.Doctor);
                        //Add Doctor in table Role
                        var result1 = await userManager.AddToRoleAsync(Doctor, role.Name);
                        //Send Message Success
                        if (result.Succeeded && result1.Succeeded)
                        {
                            TempData["Message"] = "saved Successfuly";
                            return RedirectToAction("GetAll", "Person");
                        }
                        else
                        {
                            foreach (var item in result.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                        }
                        TempData["Message"] = null;
                        return View("CreateDoctor", model);
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                }
                TempData["Message"] = null;
                return View("CreateDoctor", model);
            }
            return RedirectToAction("AccessDenied", "Account");
        }

        #endregion

        #region Edite Doctor
        [HttpGet]
        public async Task<IActionResult> Edite(string ID)
        {
            if (signInManager.IsSignedIn(User))
            {
                //Get Doctor By Id
                var data = await person.GetByID(ID);
                data.doctor = await servis.GetDoctorBYID(ID);
                //Map Doctor to PersonVM
                var result = mapper.Map<PersonVM>(data);
                TempData["Message"] = null;
                return View(result);
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> Edite(PersonVM model)
        {
            if (signInManager.IsSignedIn(User))
            {
                try
                {
                    var data = await person.GetByID(model.Id);
                    data.doctor = await servis.GetDoctorBYID(model.Id);
                    if (model.ImageName != null)
                    {
                        model.Image = FileUploader.UploadFile("Imgs", model.ImageName);

                    }
                    if(model.doctor.Cv_Name != null)
                    {
                        model.doctor.CV = data.doctor.CV;
                    }
                    else
                    {
                        model.Image = data.Image;
                    }
                    if (model.districtID == null)
                    {
                        model.districtID = data.districtID;
                    }
                    await servis.Update(model);
                    TempData["Message"] = "saved Successfuly";
                    return RedirectToAction("GetAll", "Person");
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                }
                TempData["Message"] = null;
                return View("Edite", model);
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        #endregion

        #region Get near Doctor
        public async Task<IActionResult> nearDoctor()
        {
            if (signInManager.IsSignedIn(User))
            {
                //Get Person Profile By include Adress (District - City - Governate) 
                var data = await servis.GetPersonInclud("district", (await signInManager.UserManager.FindByNameAsync(User.Identity.Name)).Id);
                //Map Profile
                var Patient = mapper.Map<CreatePerson>(data);
                //Get Near Doctor BY Using Person Profile Adress
                var Doctor = await servis.GetDoctor(Patient.districtID, Patient.district.CityId, Patient.district.City.GovernorateID);
                //Map All Doctor
                var Doctorvm = mapper.Map<IEnumerable<CreatePerson>>(Doctor);
                return PartialView("nearDoctor", Doctorvm);
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        #endregion

        #region Registration 
        [HttpGet]
        public IActionResult Registration()
        {
            return PartialView("Registration");
        }

        [HttpPost]
        public async Task<IActionResult> Registration(CreatePerson model)
        {
            try
            {
                var checkUserName = await servis.GetBYUserName(model.UserName);
                var checkEmail = await servis.GetBYEmail(model.Email);
                if (checkUserName == null && checkEmail == null)
                {
                    if (await servis.GetMedicalSyndicate(model.doctor.MedicalSyndicate))
                    {
                        //Add Doctor Image
                        model.Image = FileUploader.UploadFile("Imgs", model.ImageName);
                        //Add Doctor
                        var result = await userManager.CreateAsync(await Service.Add(model, 1), model.PasswordHash);
                        //Get Doctor By Name
                        var Doctor = await servis.GetBYUserName(model.UserName);
                        //Get Role Doctor
                        var role = await roleManager.FindByNameAsync(AppRoles.Doctor);
                        //Add Doctor in table Role
                        var result1 = await userManager.AddToRoleAsync(Doctor, role.Name);


                        if (result.Succeeded)
                        {

                            if (await SendConfitmEmail(model.Email))
                            {
                                return RedirectToAction("SuccessRegistration");
                            }
                            return RedirectToAction("Login", "Account");


                        }

                        else
                        {
                            foreach (var item in result.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                        }
                    }
                }
                TempData["UserName"] = "User Name or Email Found Please Try again";
                return PartialView("Registration", model);

            }
            catch (Exception)
            {

                return PartialView("Registration", model);
            }

        }
        #endregion


        #region ConfirmEmail

        public IActionResult SuccessRegistration()
        {
            return PartialView("SuccessRegistration");
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {

                var res = await userManager.ConfirmEmailAsync(user, token);
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return RedirectToAction(nameof(AccessDenied));
            }


        }
        #endregion

        #region AccessDenied
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
        #endregion
        #region Private Methods

        private async Task<bool> SendConfitmEmail(string Email)
        {
            var usr = await userManager.FindByEmailAsync(Email);
            if (usr != null)
            {
                var host = Configuration.GetValue<string>("Smtp:Server");
                var Port = Configuration.GetValue<int>("Smtp:Port");
                var fromEmail = Configuration.GetValue<string>("Smtp:UserName");
                var Password = Configuration.GetValue<string>("Smtp:Password");


                var token = await userManager.GenerateEmailConfirmationTokenAsync(usr);
                var confiramtionLink = Url.Action(nameof(ConfirmEmail), "Doctor", new { token, email = usr.Email }, Request.Scheme);
                EmailSetting email = new EmailSetting
                {
                    ToEmail = usr.Email,
                    Name = usr.FullName,



                };
                var TempHtml = $"<a href='{confiramtionLink}'>ConfrmLink</a>";
                var res = MaullSetting.MailSender(host, Port, fromEmail, Password, email, TempHtml);
                if (res != null)
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion


    }
}
