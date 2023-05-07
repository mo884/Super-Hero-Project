using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;
using SuperHero.BL.Helper;
using SuperHero.BL.Interface;
using SuperHero.BL.Seeds;
using SuperHero.DAL.Entities;

namespace SuperHero.PL.Controllers.Admin.Persons
{
    //[Authorize(Roles = AppRoles.Admin)]
    public class TrainerController : Controller
    {
        #region Prop
        private readonly UserManager<Person> userManager;
        private readonly IMapper mapper;
        private readonly IBaseRepsoratory<Person> person;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IServiesRep servis;
        private readonly IBaseRepsoratory<District> district;
        #endregion

        #region Ctor
        public TrainerController(UserManager<Person> userManager, IMapper mapper, IBaseRepsoratory<Person> person, RoleManager<IdentityRole> roleManager, IServiesRep servis, IBaseRepsoratory<District> district)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.person = person;
            this.roleManager = roleManager;
            this.servis = servis;
            this.district = district;
        }
        #endregion

        #region Create Trainer
        public async Task<IActionResult> CreateTrainer()
        {
            ViewBag.districtList = new SelectList(await district.GetAll(), "Id", "Name");
            TempData["Message"] = null;
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTrainer(PersonVM model)
        {
            var Alldistrict = await district.GetAll();
            try
            {
                model.Image = FileUploader.UploadFile("Imgs", model.ImageName);

                if (ModelState.IsValid)
                {
                    var trainer = mapper.Map<CreatePerson>(model);
                    var result = await userManager.CreateAsync(await Service.Add(trainer, 3), model.PasswordHash);
                    var Trainer = await servis.GetBYUserName(model.UserName);
                    var role = await roleManager.FindByNameAsync(AppRoles.Trainer);
                    var result1 = await userManager.AddToRoleAsync(Trainer, role.Name);
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

                    ViewBag.Type = "Trainer";
                    ViewBag.districtList = new SelectList(Alldistrict, "Id", "Name");
                    TempData["Message"] = null;
                    return View("CreateTrainer", model);
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
            return View(model);
        }


        #endregion

        #region Edite Trainer
        [HttpGet]
        public async Task<IActionResult> Edite(string ID)
        {
            var data = await person.GetByID(ID);
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
                if (ModelState.IsValid)
                {
                    var data = await person.GetByID(model.Id);
                   
                    await servis.Update(model);
                    TempData["Message"] = "saved Successfuly";
                    return RedirectToAction("GetAll", "Person");
                }

                else
                {
                    TempData["Message"] = null;
                    ViewBag.districtList = new SelectList(await district.GetAll(), "Id", "Name");
                    return View(model);
                }


            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            TempData["Message"] = null;
            ViewBag.districtList = new SelectList(await district.GetAll(), "Id", "Name");
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
                //Add Trainer Image
                model.Image = FileUploader.UploadFile("Imgs", model.ImageName);
                //Add Trainer
                var result = await userManager.CreateAsync(await Service.Add(model, 3), model.PasswordHash);
                //Get Trainer By Name
                var trainer = await servis.GetBYUserName(model.UserName);
                //Add Trainer Role
                var role = await roleManager.FindByNameAsync(AppRoles.User);
                //Add Trainer in table Role
                var result1 = await userManager.AddToRoleAsync(trainer, role.Name);
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
