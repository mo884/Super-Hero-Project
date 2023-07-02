using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Interface;

namespace SuperHero.PL.Controllers
{
    public class NotififcationController : Controller
    {
        private readonly IServiesRep servies;
        private readonly SignInManager<Person> signInManager;
        public NotififcationController(IServiesRep servies, SignInManager<Person> signInManager)
        {
            this.servies = servies;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> GetNotiy(string? UserId)
        {
            if (signInManager.IsSignedIn(User))
            {
                if (UserId != null)
                {
                    var Notiy = await servies.GetNotiFications(x => x.ReciverID == UserId && x.Show == false);
                    var Count = Notiy.Count;
                    return Json(new { Notiy = Notiy, Count = Count });
                }
                else
                {
                    return Json("No Data");
                }
            }
            return RedirectToAction("AccessDenied", "Account");

        }
        public async Task<IActionResult> ReadNotiy(string UserId)
        {
            if (signInManager.IsSignedIn(User))
            {
                if (UserId != null)
                {
                    if (await servies.IsRead(UserId))
                    {
                        return Json("Done");
                    }
                    else
                    {
                        return Json("Error");
                    }

                }
                else
                {
                    return Json("Id Null");
                }
            }
            return RedirectToAction("AccessDenied", "Account");
        }
    }
}
