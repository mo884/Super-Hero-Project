using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Helper;
using SuperHero.BL.Interface;
using SuperHero.BL.Seeds;

namespace SuperHero.PL.Controllers.Admin.Persons
{
    //[Authorize(Roles = AppRoles.Admin)]
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
        #endregion

        #region Ctor
        public DoctorController(UserManager<Person> userManager, IMapper mapper, SignInManager<Person> signInManager, RoleManager<IdentityRole> roleManager, IServiesRep servis, IBaseRepsoratory<Person> person, IBaseRepsoratory<District> district, IBaseRepsoratory<City> city, IBaseRepsoratory<Governorate> governorate)
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
        }
        #endregion

        #region Create Doctor
        [HttpGet]
        public async Task<IActionResult> CreateDoctor()
        {
            TempData["Message"] = null;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateDoctor(PersonVM model)
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

        #endregion

        #region Edite Doctor
        [HttpGet]
        public async Task<IActionResult> Edite(string ID)
        {
            //Get Doctor By Id
            var data = await person.GetByID(ID);
            //Map Doctor to PersonVM
            var result = mapper.Map<PersonVM>(data);
            TempData["Message"] = null;
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edite(PersonVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   //Call Function To Update Doctor 
                    await servis.Update(model);
                    //send Message Sucess
                    TempData["Message"] = "saved Successfuly";
                    return RedirectToAction("GetAll", "Person");
                }
                else
                {
                    TempData["Message"] = null;
                    return View("Edite", model);
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            TempData["Message"] = null;
            return View("Edite", model);
        }
        #endregion

        #region Get Better Doctor
        public async Task<IActionResult> nearDoctor()
        {
            //Get Person Profile By include Adress (District - City - Governate) 
            var data = await servis.GetPersonInclud("district",(await signInManager.UserManager.FindByNameAsync(User.Identity.Name)).Id);
            //Map Profile
            var Patient = mapper.Map<CreatePerson>(data);
            //Get Near Doctor BY Using Person Profile Adress
            var Doctor = await servis.GetDoctor(Patient.districtID, Patient.district.CityId, Patient.district.City.GovernorateID);
            //Map All Doctor
            var Doctorvm = mapper.Map<IEnumerable<CreatePerson>>(Doctor);
            return PartialView("nearDoctor", Doctorvm);
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
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

                return PartialView("Registration", model);

            }
            catch (Exception)
            {

                return PartialView("Registration", model);
            }

        }
        #endregion

    }
}
