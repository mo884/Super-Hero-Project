using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.DomainModelVM;
using SuperHero.BL.Helper;
using SuperHero.BL.Interface;
using SuperHero.DAL.Database;

namespace SuperHero.PL.Controllers.User.DoctorsRating
{
    public class ReatingController : Controller
    {
        #region prop


        private readonly IBaseRepsoratory<DoctorRating> doctor;
        private readonly IServiesRep servies;
        private readonly IMapper mapper;
        private readonly SignInManager<Person> signInManager;

        #endregion

        #region ctor
        public ReatingController(IBaseRepsoratory<DoctorRating> doctor, IServiesRep servies, IMapper mapper, SignInManager<Person> signInManager)
        {
            this.doctor = doctor;
            this.servies = servies;
            this.mapper = mapper;
            this.signInManager = signInManager;
        }
        #endregion

        #region Reating view
        public async Task<IActionResult> Reating(string id)
        {
            if (signInManager.IsSignedIn(User))
            {
                // save the Doctor Id
                TempData["doctorId"] = id;

                //Go to PartialView Reating
                return PartialView("Reating", new DoctorRatingVM());
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        #endregion

        #region Add Doctor Reating
        public async Task<IActionResult> ReatingStar(DoctorRatingVM model)
        {
            if (signInManager.IsSignedIn(User))
            {
                //Get All data of SignIn Use
                var PersonProfile = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
                //Use Method to DoctorReating by use UserId and DoctorID and Use Method To calc The average of reating
                var rate = await servies.AddDoctorReating(model, PersonProfile.Id, (string)TempData["doctorId"], Service.Calc(model.star));
                //Mapping
                var DoctorReating = mapper.Map<DoctorRating>(rate);
                //Add Doctor reating
                await doctor.Create(DoctorReating);
                return RedirectToAction("Profile", "MyProfile", new { id = DoctorReating.DoctorId });
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        #endregion
    }
}
