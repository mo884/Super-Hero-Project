using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.DomainModelVM;
using SuperHero.BL.Interface;

namespace SuperHero.PL.Controllers
{
    public class ChatHubController : Controller
    {
        private readonly IServiesRep serviesRep;
        private readonly SignInManager<Person> signInManager;
        private readonly IBaseRepsoratory<Group> Group;
        public ChatHubController(IServiesRep serviesRep, SignInManager<Person> signInManager, IBaseRepsoratory<Group> Group) 
        { 
            this.serviesRep = serviesRep;
            this.signInManager = signInManager;
            this.Group = Group;
        }
        public async Task<IActionResult>Index()
        {
            Random randomNumber = new Random();
            int RnadomSession = randomNumber.Next(0, 955121135); 
            HttpContext.Session.SetInt32("UserId", RnadomSession);
            var PersonProfile = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
           
            var data = await serviesRep.FindAllGroupById(PersonProfile.Id);
            if (data.Count() != 0)
            {
                var FindIn = await serviesRep.FindById(PersonProfile.Id, Convert.ToInt32(data.FirstOrDefault().Group));
                if (FindIn != null)
                {
                    var Chat = await serviesRep.GetAllChatGroup(Convert.ToInt32(FindIn.Group));
                    var GroupName = await Group.GetByID(Convert.ToInt32(FindIn.Group));
                    TempData["GroupName"] = GroupName.Name;
                    TempData["GroupID"] = GroupName.ID;
                    ListGroupVM listGroupVM = new ListGroupVM()
                    {Chat = Chat,
                    Groups =data
                    };
                    return View(listGroupVM);
                }
            }
            return View(null);

        }
        public async Task<IActionResult> GetMessage(int id)
        {
            Random randomNumber = new Random();
            int RnadomSession = randomNumber.Next(0, 955121135);
            HttpContext.Session.SetInt32("UserId", RnadomSession);
            var PersonProfile = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
            var FindIn = await serviesRep.FindById(PersonProfile.Id, id);
            var data = await serviesRep.FindAllGroupById(PersonProfile.Id);
            if (data.Count() != 0)
            {
                if (FindIn != null)
                {
                    var Chat = await serviesRep.GetAllChatGroup(id);
                    var GroupName = await Group.GetByID(id);
                    TempData["GroupName"] = GroupName.Name;
                    TempData["GroupID"] = GroupName.ID;
                    ListGroupVM listGroupVM = new ListGroupVM()
                    {
                        Chat = Chat,
                        Groups = data
                    };
                    return View("Index", listGroupVM);
                }
            }
            return RedirectToAction("GetAll", "Person");
        }
        public IActionResult Index2(string Id)
        {
            TempData["PersonId"] = Id;
            return View();
        }
    }
}
