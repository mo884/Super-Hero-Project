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
    //[Authorize(Roles = AppRoles.Admin)]
    public class PatientController : Controller
    {
        #region Prop
        private readonly UserManager<Person> userManager;
        private readonly IBaseRepsoratory<Person> person;

        private readonly SignInManager<Person> signInManager;
        private readonly IMapper mapper;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IServiesRep servis;
        private readonly IBaseRepsoratory<District> district;

        #endregion
        private IConfiguration Configuration;

        #region Ctor
        public PatientController(UserManager<Person> userManager, SignInManager<Person> signInManager, IBaseRepsoratory<Person> person, IMapper mapper, RoleManager<IdentityRole> roleManager, IServiesRep servis, IBaseRepsoratory<District> district, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.person = person;
            this.mapper = mapper;
            this.roleManager = roleManager;
            this.servis = servis;
            this.district = district;
            Configuration = configuration;
        }
        #endregion


        #region Create Person
        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            if (signInManager.IsSignedIn(User))
            {
                ViewBag.districtList = new SelectList(await district.GetAll(), "Id", "Name");
                TempData["Message"] = null;
                return View();
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(PersonVM model)
        {
            if (signInManager.IsSignedIn(User))
            {
                try
                {
                    model.Image = FileUploader.UploadFile("Imgs", model.ImageName);

                    if (ModelState.IsValid)
                    {
                        var patient = mapper.Map<CreatePerson>(model);
                        var result = await userManager.CreateAsync(await Service.Add(patient, 0), model.PasswordHash);
                        var Patient = await servis.GetBYUserName(model.UserName);
                        var role = await roleManager.FindByNameAsync(AppRoles.User);
                        var result1 = await userManager.AddToRoleAsync(Patient, role.Name);
                        if (result.Succeeded)
                        {
                            TempData["Message"] = "saved Successfuly";
                            return RedirectToAction("GetAll", "Person");
                        }
                        else
                        {
                            foreach (var item in result.Errors)
                                ModelState.AddModelError("", item.Description);
                        }
                        ViewBag.districtList = new SelectList(await district.GetAll(), "Id", "Name");
                        TempData["Message"] = null;
                        return View("CreateUser", model);
                    }

                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                }
                ViewBag.districtList = new SelectList(await district.GetAll(), "Id", "Name");
                TempData["Message"] = null;
                return View("CreateUser", model);
            }
            return RedirectToAction("AccessDenied", "Account");
        }

        #endregion

        #region Edite Patient
        [HttpGet]
        public async Task<IActionResult> Edite(string ID)
        {
            var data = await userManager.FindByIdAsync(ID);
            var result = mapper.Map<PersonVM>(data);
            ViewBag.districtList = new SelectList(await district.GetAll(), "Id", "Name", data.districtID);
            TempData["Message"] = null;
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edite(PersonVM model)
        {
            try
            {
                
                    var data = await person.GetByID(model.Id);
                    if(model.ImageName != null)
                    {
                    model.Image = FileUploader.UploadFile("Imgs", model.ImageName);

                    }else
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
            
            return View(model);
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
                if (checkUserName == null && checkEmail==null)
                {
                    //Add Patient Image
                    model.Image = FileUploader.UploadFile("Imgs", model.ImageName);
                    //Add Patient
                    var result = await userManager.CreateAsync(await Service.Add(model, 0), model.PasswordHash);
                    //Add Patient By Name
                    var Patient = await servis.GetBYUserName(model.UserName);
                    //Add Patient Role
                    var role = await roleManager.FindByNameAsync(AppRoles.User);
                    //Add Patient
                    var result1 = await userManager.AddToRoleAsync(Patient, role.Name);
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


                TempData["UserName"] = "User Name or Email Found Please Try Another";


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
                var confiramtionLink = Url.Action(nameof(ConfirmEmail), "Patient", new { token, email = usr.Email }, Request.Scheme);
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
