using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Helper;

namespace SuperHero.PL.Controllers.SecondPage
{
    public class MailController : Controller
    {
        [HttpPost]

        public IActionResult SendMail(MailVM mail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["Msg"] = MailSender.SendEmail(mail);
                    return RedirectToAction("Index" , "BestDoctor");
                }

                return RedirectToAction("Index", "BestDoctor");
            }
            catch (Exception ex)
            {
                TempData["Msg"] = "Faild";
                return RedirectToAction("Index", "BestDoctor");
            }
        }
    }
}
