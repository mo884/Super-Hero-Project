using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.DomainModelVM;
using SuperHero.BL.Seeds;
using SuperHero.DAL.Entities;

namespace SuperHero.PL.Controllers.Admin
{
    //[Authorize(Roles = AppRoles.Admin)]
    public class RolesController : Controller
    {
        #region prop

        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<Person> userManager;
        private readonly IBaseRepsoratory<Person> person;
        #endregion

        #region Ctor
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<Person> userManager, IBaseRepsoratory<Person> person)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.person = person;
        }
        #endregion

        #region GetAll Roles
        public IActionResult GetAll()
        {
            var data = roleManager.Roles;
            return View(data);
        }

        #endregion

        #region Create Roles
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {

            var result = await roleManager.CreateAsync(model);

            if (result.Succeeded)
            {
                return RedirectToAction("GetAll");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View();
        }

        #endregion

        #region Update Roles
        public async Task<IActionResult> Update(string id)
        {
            var data = await roleManager.FindByIdAsync(id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(IdentityRole model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await roleManager.UpdateAsync(model);
                    return RedirectToAction("GetAll");
                }
                else
                {
                    TempData["Message"] = null;
                    return View("Form", model);
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

        #region Delete Roles
        public async Task<IActionResult> Delete(string id)
        {
            var data = await roleManager.FindByIdAsync(id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(IdentityRole model)
        {

            var data = await roleManager.FindByIdAsync(model.Id);

            if (data != null)
            {
                var result = await roleManager.DeleteAsync(data);


                if (result.Succeeded)
                {
                    return RedirectToAction("GetAll");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return View(model);
        }


        #endregion

        #region Add and Remover Person Group
        public async Task<IActionResult> AddOrRemoveUsers(string RoleId)
        {

            ViewBag.roleId = RoleId;

            var role = await roleManager.FindByIdAsync(RoleId);

            var model = new List<UserInRoleVM>();

            foreach (var user in await person.GetAll())
            {
                var userInRole = new UserInRoleVM()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelected = true;
                }
                else
                {
                    userInRole.IsSelected = false;
                }

                model.Add(userInRole);
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(List<UserInRoleVM> model, string RoleId)
        {

            var role = await roleManager.FindByIdAsync(RoleId);

            for (int i = 0; i < model.Count; i++)
            {

                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {

                    result = await userManager.AddToRoleAsync(user, role.Name);

                }
                else if (!model[i].IsSelected && (await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

            }

            return RedirectToAction("GetAll");
        }
        #endregion


    }
}
