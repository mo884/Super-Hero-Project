using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.DomainModelVM;
using SuperHero.BL.Helper;
using SuperHero.BL.Interface;
using SuperHero.BL.Seeds;
using SuperHero.DAL.Entities;

namespace SuperHero.PL.Controllers.Admin.Persons
{
    [Authorize(Roles = AppRoles.Admin)]
    public class PatientController : Controller
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
        public PatientController(UserManager<Person> userManager, IBaseRepsoratory<Person> person, IMapper mapper, RoleManager<IdentityRole> roleManager, IServiesRep servis, IBaseRepsoratory<District> district)
        {
            this.userManager = userManager;
            this.person = person;
            this.mapper = mapper;
            this.roleManager = roleManager;
            this.servis = servis;
            this.district = district;
        }
        #endregion

        #region Create Person
        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            ViewBag.districtList = new SelectList(await district.GetAll(), "Id", "Name");
            TempData["Message"] = null;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreatePerson model)
        {
            try
            {
                model.Image = FileUploader.UploadFile("Imgs", model.ImageName);

                if (ModelState.IsValid)
                {
                    var result = await userManager.CreateAsync(await Service.Add(model, 0), model.PasswordHash);
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

        #endregion

        #region Edite Patient
        [HttpGet]
        public async Task<IActionResult> Edite(string ID)
        {
            var data = await userManager.FindByIdAsync(ID);
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
    }
}
