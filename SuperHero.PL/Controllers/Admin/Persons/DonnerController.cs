using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Helper;
using SuperHero.BL.Interface;
using SuperHero.BL.Seeds;
using SuperHero.DAL.Entities;

namespace SuperHero.PL.Controllers.Admin.Persons
{
    //[Authorize(Roles = AppRoles.Admin)]
    public class DonnerController : Controller
    {
        #region Prop
        private readonly UserManager<Person> userManager;
        private readonly IBaseRepsoratory<Person> person;
        private readonly IMapper mapper;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IServiesRep servis;
        private readonly IBaseRepsoratory<District> district;
        #endregion

        #region Ctor
        public DonnerController(UserManager<Person> userManager, IBaseRepsoratory<Person> person, IMapper mapper, RoleManager<IdentityRole> roleManager, IServiesRep servis, IBaseRepsoratory<District> district)
        {
            this.userManager = userManager;
            this.person = person;
            this.mapper = mapper;
            this.roleManager = roleManager;
            this.servis = servis;
            this.district = district;
        }
        #endregion

        #region Create Donner
        public async Task<IActionResult> CreateDonner()
        { 
            TempData["Message"] = null;
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDonner(PersonVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Map PersonVM to Class CreatePerson
                    var donner = mapper.Map<CreatePerson>(model);
                    //Create Donner 
                    var result = await userManager.CreateAsync(await Service.Add(donner, 2), model.PasswordHash);
                    //Get Donner By UserName
                    var Donner = await servis.GetBYUserName(model.UserName);
                    //Get Role Donner
                    var role = await roleManager.FindByNameAsync(AppRoles.Donner);
                    //Add Donner in table Role
                    var result1 = await userManager.AddToRoleAsync(Donner, role.Name);
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
                    return View("CreateDonner", model);
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
           
            TempData["Message"] = null;
            return View("CreateDonner", model);
        }


        #endregion

        #region Edite Donner
        [HttpGet]
        public async Task<IActionResult> Edite(string ID)
        {
            //Get Donner By Id
            var data = await person.GetByID(ID);
            //Map Donner to PersonVM
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
                    //Call Function To Update Donner 
                    await servis.Update(model);
                    //send Message Sucess
                    TempData["Message"] = "saved Successfuly";
                    return RedirectToAction("GetAll", "Person");
                }

                else
                {
                    TempData["Message"] = null;
                    return View(model);
                }


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
                //Add Donor Image
                model.Image = FileUploader.UploadFile("Imgs", model.ImageName);
                //Add Donor
                var result = await userManager.CreateAsync(await Service.Add(model, 2), model.PasswordHash);
                //Get Donor By Name
                var donner = await servis.GetBYUserName(model.UserName);
                //Get Role Donor
                var role = await roleManager.FindByNameAsync(AppRoles.Donner);
                //Add Donor in table Role
                var result1 = await userManager.AddToRoleAsync(donner, role.Name);
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
