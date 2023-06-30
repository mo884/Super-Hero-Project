using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Interface;

namespace SuperHero.PL.Controllers
{
    public class NotififcationController : Controller
    {
        private readonly IServiesRep servies;

        public NotififcationController(IServiesRep servies)
        {
            this.servies = servies;
        }

        public async Task<IActionResult> GetNotiy(string? UserId)
        {
            if (UserId != null)
            {
                var Notiy = await servies.GetNotiFications(x => x.ReciverID == UserId && x.Show == false);
                var Count = Notiy.Count;
                return Json(new { Notiy= Notiy,Count=Count });
            }
            else
            {
                return Json("No Data");
            }


        }
        public async Task<IActionResult>ReadNotiy(string UserId)
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
    }
}
