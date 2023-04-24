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
        private readonly ApplicationContext db;
        private readonly IMapper mapper;
        private readonly SignInManager<Person> signInManager;

        #endregion

        #region ctor
        public ReatingController(IBaseRepsoratory<DoctorRating> doctor, IServiesRep servies, ApplicationContext Db, IMapper mapper,SignInManager<Person> signInManager)
        {
            this.doctor = doctor;
            this.servies = servies;
            db = Db;
            this.mapper = mapper;
            this.signInManager = signInManager;
        }
        #endregion

        public async Task< IActionResult> Reating(string id)
        {
            TempData["doctorId"] = id;
            var PersonProfile = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
            //var Istrue = await servies.DoctorRatingISTrue(PersonProfile.Id, id);
            //if (Istrue != null)
            //    return PartialView("Done");
            return PartialView("Reating");  
        }
        public async Task< IActionResult > ReatingStar(DoctorRatingVM model )
        {
            var PersonProfile = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
            var rate = await servies.AddDoctorReating(model, PersonProfile.Id, (string)TempData["doctorId"], Service.Calc(model.star));
            var DoctorReating = mapper.Map<DoctorRating>(rate);
            await doctor.Create(DoctorReating);
            return RedirectToAction("Profile", "Profile", new {id= DoctorReating.DoctorId });
        }
    }
}
