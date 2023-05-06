using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Interface;

namespace SuperHero.PL.Controllers.PrivateClinic
{
    public class RadiologyController : Controller
    {

        #region Prop
        private readonly IMapper mapper;
        private readonly IServiesRep servies;
        private readonly SignInManager<Person> signInManager;
        #endregion


        #region ctor
        public RadiologyController(IMapper mapper, IServiesRep servies, SignInManager<Person> signInManager)
        {


            this.mapper = mapper;
            this.servies = servies;
            this.signInManager = signInManager;
        }
        #endregion
        public async Task<IActionResult> PatientRadiology(int id)
        {
            var Radiology = await servies.GetAllRadiologybyId(id);
            var RadiologyVM = mapper.Map<List<RadiologyVM>>(Radiology);
            return PartialView(RadiologyVM);
        }
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            TempData["PatientId"] = id;
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DoctorRadiology Radiology)
        {
            Radiology.personID = (int)TempData["PatientId"];
            var user = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
            await servies.CreateRadiology(Radiology,user.Id);
            return RedirectToAction("PatientRecord", "DoctorHome", new { id = Radiology.patient.UserID });
        }
    }
}
