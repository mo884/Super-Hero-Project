using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Interface;
using SuperHero.BL.Seeds;
using System.Data;

namespace SuperHero.PL.Controllers.Admin.Social
{
   [Authorize(Roles = @$"{AppRoles.Admin},{AppRoles.Doctor}")]
    public class GroupController : Controller
    {
        #region Pop
        private readonly IBaseRepsoratory<Group> groups;
        private readonly IMapper mapper;
        private readonly IBaseRepsoratory<Person> person;
        private readonly IBaseRepsoratory<PersonGroup> personGroup;
        private readonly IServiesRep servis;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<Person> userManager;
        private readonly SignInManager<Person> signInManager;
        #endregion

        #region Ctor
        public GroupController(SignInManager<Person> signInManager, IBaseRepsoratory<Group> groups, IMapper mapper, IBaseRepsoratory<Person> person, IBaseRepsoratory<PersonGroup> personGroup, IServiesRep servis, RoleManager<IdentityRole> roleManager, UserManager<Person> userManager)
        {
            this.groups = groups;
            this.mapper = mapper;
            this.person = person;
            this.personGroup = personGroup;
            this.servis = servis;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        #endregion

        #region GetAll Groups
        public async Task<IActionResult> GetAll()
        {
            if (signInManager.IsSignedIn(User))
            {
                var data = await groups.GetAll();
                var result = mapper.Map<IEnumerable<GroupVM>>(data);
                return View(result);
            }
            return RedirectToAction("AccessDenied", "Account");
        }

        #endregion

        #region Create Groups
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (signInManager.IsSignedIn(User))
            {
                ViewBag.ID = null;
                TempData["Message"] = null;
                return PartialView("Form");
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        public async Task<IActionResult> Create(GroupVM groupVM)
        {
            if (signInManager.IsSignedIn(User))
            {
                try
                {

                    if (ModelState.IsValid)
                    {
                        var result = mapper.Map<Group>(groupVM);
                        result.CreatedTime = DateTime.Now.Date;
                        await groups.Create(result);

                        TempData["Message"] = "saved Successfuly";
                        return RedirectToAction("GetAll");
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                }

                //ModelState.Clear();
                TempData["Message"] = null;


                ViewBag.ID = null;
                return PartialView("Form", groupVM);
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        #endregion

        #region Edite Groups
        [HttpGet]
        public async Task<IActionResult> Edite(int id)
        {
            if (signInManager.IsSignedIn(User))
            {
                var data = await groups.GetByID(id);
                var result = mapper.Map<GroupVM>(data);
                TempData["Message"] = null;
                return PartialView("Form", result);
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        public async Task<IActionResult> Edite(GroupVM groupVM)
        {
            if (signInManager.IsSignedIn(User))
            {
                try
                {

                    if (ModelState.IsValid)
                    {
                        groupVM.CreatedTime = DateTime.Now;
                        var result = mapper.Map<Group>(groupVM);
                        await groups.Update(result);
                        TempData["Message"] = "saved Successfuly";
                        return RedirectToAction("GetAll");
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                }

                //ModelState.Clear();
                TempData["Message"] = null;

                ViewBag.ID = "Edite";
                return View("Form", groupVM);
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        #endregion

        #region Delete Groups

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (signInManager.IsSignedIn(User))
            {
                var data = await groups.GetByID(id);
                var result = mapper.Map<GroupVM>(data);
                TempData["Message"] = null;
                return PartialView("Delete", result);
            }
            return RedirectToAction("AccessDenied", "Account");
        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(GroupVM obj)
        {
            if (signInManager.IsSignedIn(User))
            {

                try
                {
                    if (ModelState.IsValid)
                    {
                        var result = mapper.Map<Group>(obj);
                        await servis.DeletePersonGroup(result.ID);
                        await groups.Delete(result.ID);
                        return RedirectToAction("GetAll");
                    }

                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                }
                return PartialView("Delete", obj);
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        #endregion

        #region AddOrRemoveGroups
        public async Task<IActionResult> AddOrRemoveGroups(int id)
        {
            if (signInManager.IsSignedIn(User))
            {
                ViewBag.Id = id;

                var DataGroup = await groups.GetByID(id);

                var model = new List<GroupInRoleVM>();

                foreach (var use in await person.GetAll())
                {
                    var groupInRole = new GroupInRoleVM()
                    {
                        Id = use.Id,
                        Name = use.UserName,
                        FullName = use.FullName,
                        Image = use.Image,
                        gender = use.gender
                    };

                    if (await servis.GetAll(id, use.Id))
                    {
                        groupInRole.IsSelected = true;
                    }
                    else
                    {
                        groupInRole.IsSelected = false;
                    }

                    model.Add(groupInRole);
                }

                return View(model);
            }
            return RedirectToAction("AccessDenied", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveGroups(List<GroupInRoleVM> model, int id)
        {
            if (signInManager.IsSignedIn(User))
            {
                var group = await groups.GetByID(id);

                for (int i = 0; i < model.Count; i++)
                {

                    var user = await userManager.FindByIdAsync(model[i].Id);
                    if (user != null)
                    {
                        if (model[i].IsSelected && !await servis.GetAll(group.ID, user.Id))
                        {
                            var result = new PersonGroup()
                            {
                                PersonId = user.Id,
                                Group = group.ID

                            };

                            await personGroup.Create(result);

                        }
                        else if (!model[i].IsSelected && await servis.GetAll(group.ID, user.Id))
                        {
                            var result = await servis.FindById(user.Id, group.ID);

                            await personGroup.Delete(result.ID);
                        }
                    }
                    else
                    {
                        continue;
                    }

                }

                return RedirectToAction("GetAll");
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        #endregion
    }
}
