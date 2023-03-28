
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    //[Authorize(Roles = $"{AppRoles.Doctor}, {AppRoles.Admin}")]

    public class PersonController : Controller
    {

        #region Prop
        private readonly IBaseRepsoratory<Person> person;
        private readonly UserManager<Person> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IBaseRepsoratory<District> district;
        private readonly IMapper mapper;
        private readonly IServiesRep servis;

        #endregion

        #region Ctor
        public PersonController(IBaseRepsoratory<Person> person, UserManager<Person> userManager, RoleManager<IdentityRole> roleManager, IBaseRepsoratory<District> district, IMapper mapper, IServiesRep servis)
        {
            this.person = person;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.district = district;
            this.mapper = mapper;
            this.servis = servis;
        }
        #endregion

        #region Get All Persons
        public async Task<IActionResult> GetAll()
        {
            var data = await person.FindAsync("district");
            var result = mapper.Map<IEnumerable<CreatePerson>>(data);
            return View(result);
        }
        #endregion

        #region
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

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var data = await person.GetByID(id);
            var result = mapper.Map<PersonVM>(data);
            ViewBag.districtList = new SelectList(await district.GetAll(), "Id", "Name", data.districtID);
            ViewBag.Delete = "Delete";
            ViewBag.ID = "Edite";
            return View("Form", result);
        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(PersonVM obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = await person.GetByID(obj.Id);

                    var result = mapper.Map<Person>(obj);
                    FileUploader.RemoveFile("Imgs", data.Image);
                    if (data.PersonType == DAL.Enum.PersonType.Doctor)
                    {
                        var Doctor = await servis.GetDoctorBYID(obj.Id);
                        data.doctor = Doctor;
                    }
                    else if (data.PersonType == DAL.Enum.PersonType.Trainer)
                    {
                        var Trainer = await servis.GetTrainerBYID(obj.Id);
                        data.trainer = Trainer;
                    }
                    else if (data.PersonType == DAL.Enum.PersonType.Doner)
                    {
                        var Trainer = await servis.GetDonnerBYID(obj.Id);
                        data.donner = Trainer;
                    }
                    else
                    {
                       
                    }
                    await person.Delete(data.Id);

                    return RedirectToAction("GetAll");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }



            var dist = await district.GetAll();
            ViewBag.districtList = new SelectList(dist, "Id", "Name", obj.districtID);
            ViewBag.Delete = "Delete";
            ViewBag.ID = "Edite";
            return View("Form", obj);

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

    }
}
