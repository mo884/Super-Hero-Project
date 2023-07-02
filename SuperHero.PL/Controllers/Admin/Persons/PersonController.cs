
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SuperHero.BL.Enum;
using SuperHero.BL.Helper;
using SuperHero.BL.Interface;
using SuperHero.BL.Seeds;
using SuperHero.DAL.Database;
using SuperHero.DAL.Entities;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;


namespace SuperHero.PL.Controllers.Admin.Persons
{
    

    public class PersonController : Controller
    {

        #region Prop
        private readonly IBaseRepsoratory<Person> person;
        private readonly UserManager<Person> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IBaseRepsoratory<District> district;
        private readonly IBaseRepsoratory<UserInfo> user;
        private readonly IMapper mapper;
        private readonly IServiesRep servis;

        #endregion

        #region Ctor
        public PersonController(IBaseRepsoratory<Person> person, UserManager<Person> userManager, RoleManager<IdentityRole> roleManager, IBaseRepsoratory<District> district, IBaseRepsoratory<UserInfo> user, IMapper mapper, IServiesRep servis)
        {
            this.person = person;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.district = district;
            this.user = user;
            this.mapper = mapper;
            this.servis = servis;
        }
        #endregion

        #region Get All Persons
        [Authorize(Roles = $"{AppRoles.Admin}")]
        public async Task<IActionResult> GetAll()
        {
            var data = await person.FindAsync("district");
            var result = mapper.Map<IEnumerable<CreatePerson>>(data);
            return View(result);
        }
        #endregion

        #region Edite
        public async Task<IActionResult> Edite(string id)
        {
            try
            {

                var data = await person.GetByID(id);
                if (data.PersonType.ToString().Equals(PersonType.User.ToString()))
                {
                    return RedirectToAction("Edite", "Patient", new { data.Id });
                }
                else if (data.PersonType.ToString().Equals(PersonType.Trainer.ToString()))
                {
                    return RedirectToAction("Edite", "Trainer", new { data.Id });
                }
                else if (data.PersonType.ToString().Equals(PersonType.Doner.ToString()))
                {
                    return RedirectToAction("Edite", "Donner", new { data.Id });
                }
                else if (data.PersonType.ToString().Equals(PersonType.Doctor.ToString()))
                {
                    return RedirectToAction("Edite", "Doctor", new { data.Id });
                }
                return RedirectToAction("GetAll");

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                TempData["Message"] = null;
                ViewBag.districtList = new SelectList(await district.GetAll(), "Id", "Name");
                ViewBag.ID = "Edite";
                return RedirectToAction("GetAll");
            }
        }

        #endregion

        #region Delete

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var data = await person.GetByID(id);

            if (data is null)
                return NotFound();
           var Person = await Service.Delete(data);
            await person.Update(Person);
            return Ok();
           
            
        }


        #endregion

        #region Create
        #region Create Person
        public async Task<IActionResult> CreateUser()
        {

            var Alldistrict = await district.GetAll();

            ViewBag.districtList = new SelectList(Alldistrict, "Id", "Name");
            ViewBag.ID = null;
            ViewBag.Type = "Patient";
            TempData["Message"] = null;
            return View("Form"); ;
        }
        [HttpPost, ValidateAntiForgeryToken]


        #endregion

        #region Create Doctor
        public async Task<IActionResult> CreateDoctor()
        {

            var Alldistrict = await district.GetAll();

            ViewBag.districtList = new SelectList(Alldistrict, "Id", "Name");
            ViewBag.ID = null;
            ViewBag.Type = "Doctor";
            TempData["Message"] = null;
            return View("Form");
        }
        [HttpPost, ValidateAntiForgeryToken]
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
                        return RedirectToAction(nameof(GetAll));
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }

                    ViewBag.Type = "Doctor";
                    ViewBag.districtList = new SelectList(Alldistrict, "Id", "Name");
                    TempData["Message"] = null;
                    return View("Form", model);
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            //ModelState.Clear();
            ViewBag.Type = "Doctor";
            ViewBag.districtList = new SelectList(Alldistrict, "Id", "Name");
            TempData["Message"] = null;
            return View("Form", model);
        }


        #endregion

        #region Create Trainer
        public async Task<IActionResult> CreateTrainer()
        {

            var Alldistrict = await district.GetAll();

            ViewBag.districtList = new SelectList(Alldistrict, "Id", "Name");
            ViewBag.ID = null;
            ViewBag.Type = "Trainer";
            TempData["Message"] = null;
            return View("Form");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTrainer(CreatePerson model)
        {
            var Alldistrict = await district.GetAll();
            try
            {
                model.Image = FileUploader.UploadFile("Imgs", model.ImageName);
                if (ModelState.IsValid)
                {
                    var result = await userManager.CreateAsync(await Service.Add(model, 3), model.PasswordHash);
                    var Trainer = await servis.GetBYUserName(model.UserName);
                    var role = await roleManager.FindByNameAsync(AppRoles.Trainer);
                    var result1 = await userManager.AddToRoleAsync(Trainer, role.Name);
                    if (result.Succeeded && result1.Succeeded)
                    {
                        TempData["Message"] = "saved Successfuly";
                        return RedirectToAction(nameof(GetAll));
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }

                    ViewBag.Type = "Trainer";
                    ViewBag.districtList = new SelectList(Alldistrict, "Id", "Name");
                    TempData["Message"] = null;
                    return View("Form", model);
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            //ModelState.Clear();
            ViewBag.Type = "Trainer";
            ViewBag.districtList = new SelectList(Alldistrict, "Id", "Name");
            TempData["Message"] = null;
            return View("Form", model);
        }


        #endregion

        #region Create Donner
        public async Task<IActionResult> CreateDonner()
        {

            var Alldistrict = await district.GetAll();

            ViewBag.districtList = new SelectList(Alldistrict, "Id", "Name");
            ViewBag.ID = null;
            ViewBag.Type = "Donner";
            TempData["Message"] = null;
            return View("Form");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDonner(CreatePerson model)
        {
            var Alldistrict = await district.GetAll();
            try
            {
                model.Image = FileUploader.UploadFile("Imgs", model.ImageName);
                if (ModelState.IsValid)
                {
                    var result = await userManager.CreateAsync(await Service.Add(model, 2), model.PasswordHash);
                    var Donner = await servis.GetBYUserName(model.UserName);
                    var role = await roleManager.FindByNameAsync(AppRoles.Donner);
                    var result1 = await userManager.AddToRoleAsync(Donner, role.Name);
                    if (result.Succeeded && result1.Succeeded)
                    {
                        TempData["Message"] = "saved Successfuly";
                        return RedirectToAction(nameof(GetAll));
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }

                    ViewBag.Type = "Donner";
                    ViewBag.districtList = new SelectList(Alldistrict, "Id", "Name");
                    TempData["Message"] = null;
                    return View("Form", model);
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            //ModelState.Clear();
            ViewBag.Type = "Donner";
            ViewBag.districtList = new SelectList(Alldistrict, "Id", "Name");
            TempData["Message"] = null;
            return View("Form", model);
        }


        #endregion
        #endregion

        #region ajax call
        [HttpPost]
        public async Task<JsonResult> GetCityBtGonvId(int? goverId)
        {
            if (goverId != null)
            {
                var data = await servis.GetCityAsync(a => a.GovernorateID == goverId);

                return Json(data);
            }
            else
            {
                string message = "Data Not Found";
                return Json(message);
            }

        }
        [HttpPost]
        public async Task<JsonResult> GetDistricByIdCity(int? CityId)
        {
            if (CityId != null)
            {
                var data = await servis.GetDistAsync(a => a.CityId == CityId);
                return Json(data);
            }
            else
            {
                string message = "Data Not Found";
                return Json(message);
            }

        }
        #endregion

    }
}
