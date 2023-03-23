using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Helper;
using SuperHero.BL.Interface;
using SuperHero.BL.Seeds;

namespace SuperHero.PL.Controllers.Admin.Persons
{
    [Authorize(Roles = AppRoles.Admin)]
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
            ViewBag.districtList = new SelectList(await district.GetAll(), "Id", "Name");
            TempData["Message"] = null;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateDoctor(CreatePerson model)
        {
            var Alldistrict = await district.GetAll();
            try
            {
                model.Image = FileUploader.UploadFile("Imgs", model.ImageName);
                if (ModelState.IsValid)
                {
                    var result = await userManager.CreateAsync(await Service.Add(model, 1), model.PasswordHash);
                    var Doctor = await servis.GetBYUserName(model.UserName);
                    var role = await roleManager.FindByNameAsync(AppRoles.Doctor);
                    var result1 = await userManager.AddToRoleAsync(Doctor, role.Name);
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
                    ViewBag.districtList = new SelectList(Alldistrict, "Id", "Name");
                    TempData["Message"] = null;
                    return View("CreateDoctor", model);
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            ViewBag.districtList = new SelectList(Alldistrict, "Id", "Name");
            TempData["Message"] = null;
            return View("CreateDoctor", model);
        }

        #endregion

        #region Edite Doctor
        [HttpGet]
        public async Task<IActionResult> Edite(string ID)
        {
            var data = await person.GetByID(ID);
            var result = mapper.Map<CreatePerson>(data);
            ViewBag.districtList = new SelectList(await district.GetAll(), "Id", "Name", data.districtID);
            TempData["Message"] = null;
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edite(CreatePerson model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = await person.GetByID(model.Id);
                    model.Image = FileUploader.UploadFile("Imgs", model.ImageName);
                    await servis.Update(model);
                    TempData["Message"] = "saved Successfuly";
                    return RedirectToAction("GetAll", "Person");
                }
                else
                {
                    ViewBag.districtList = new SelectList(await district.GetAll(), "Id", "Name");
                    TempData["Message"] = null;
                    return View("Edite", model);
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            TempData["Message"] = null;
            ViewBag.districtList = new SelectList(await district.GetAll(), "Id", "Name");
            return View("Edite", model);
        }
        #endregion

        #region Get Better Doctor
        public async Task<IActionResult> nearDoctor()
        {
            
            var data = await servis.GetPersonInclud("district",(await signInManager.UserManager.FindByNameAsync(User.Identity.Name)).Id);
            var Patient = mapper.Map<CreatePerson>(data);
            var Doctor = await servis.GetDoctor(Patient.districtID, Patient.district.CityId, Patient.district.City.GovernorateID);
            var Doctorvm = mapper.Map<IEnumerable<CreatePerson>>(Doctor);
            return PartialView("nearDoctor", Doctorvm);
        }
        #endregion
    }
}
