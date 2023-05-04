using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Interface;

namespace SuperHero.PL.Controllers.Admin.Social
{
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
        #endregion

        #region Ctor
        public GroupController(IBaseRepsoratory<Group> groups, IMapper mapper, IBaseRepsoratory<Person> person, IBaseRepsoratory<PersonGroup> personGroup, IServiesRep servis, RoleManager<IdentityRole> roleManager, UserManager<Person> userManager)
        {
            this.groups = groups;
            this.mapper = mapper;
            this.person = person;
            this.personGroup = personGroup;
            this.servis = servis;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        #endregion

        #region GetAll Groups
        public async Task<IActionResult> GetAll()
        {
            var data = await groups.GetAll();
            var result = mapper.Map<IEnumerable<GroupVM>>(data);
            return View(result);
        }

        #endregion

        #region Create Groups
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.ID = null;
            TempData["Message"] = null;
            return PartialView("Form");
        }
        public async Task<IActionResult> Create(GroupVM groupVM)
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
        #endregion

        #region Edite Groups
        [HttpGet]
        public async Task<IActionResult> Edite(int id)
        {
            var data = await groups.GetByID(id);
            var result = mapper.Map<GroupVM>(data);
            TempData["Message"] = null;
            return PartialView("Form", result);
        }
        public async Task<IActionResult> Edite(GroupVM groupVM)
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
        #endregion

        #region Delete Groups

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await groups.GetByID(id);
            var result = mapper.Map<GroupVM>(data);
            TempData["Message"] = null;
            return PartialView("Delete", result);
        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(GroupVM obj)
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
        #endregion

        #region AddOrRemoveGroups
        public async Task<IActionResult> AddOrRemoveGroups(int id)
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
                    FullName =use.FullName,
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

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveGroups(List<GroupInRoleVM> model, int id)
        {

            var group = await groups.GetByID(id);

            for (int i = 0; i < model.Count; i++)
            {

                var user = await userManager.FindByIdAsync(model[i].Id);
                if(user != null)
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
        #endregion
    }
}
