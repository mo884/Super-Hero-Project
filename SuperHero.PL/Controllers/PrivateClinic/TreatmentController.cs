using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Interface;
using SuperHero.DAL.Entities;

namespace SuperHero.PL.Controllers.PrivateClinic
{
    public class TreatmentController : Controller
    {
        #region Prop
        private readonly IMapper mapper;
        private readonly IBaseRepsoratory<Treatment> treatment;
        private readonly IServiesRep servies;
        private readonly SignInManager<Person> signInManager;
        #endregion


        #region ctor
        public TreatmentController(IMapper mapper, IBaseRepsoratory<Treatment> treatment, IServiesRep servies, SignInManager<Person> signInManager)
        {


            this.mapper = mapper;
            this.treatment = treatment;
            this.servies = servies;
            this.signInManager = signInManager;
        }
        #endregion
        public async Task<IActionResult> PatientTreatment(int id)
        {
            if (signInManager.IsSignedIn(User))
            {
                var Treatment = await servies.GetAllTreatmentbyId(id);
                var TreatmentVM = mapper.Map<List<TreatmentVM>>(Treatment);

                return PartialView(TreatmentVM);
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            if (signInManager.IsSignedIn(User))
            {
                TempData["PatientId"] = id;
                return PartialView();
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> Create(DoctorTreatment treatment)
        {
            if (signInManager.IsSignedIn(User))
            {
                treatment.personID = (int)TempData["PatientId"];
                var user = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
                await servies.CreateTreatment(treatment, user.Id);
                return RedirectToAction("PatientRecord", "DoctorHome", new { id = treatment.patient.UserID });
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> AddByPatient(int id)
        {
            if (signInManager.IsSignedIn(User))
            {
                var data = await treatment.GetByID(id);
                data.IsAdd = true;
                await treatment.Update(data);
                return Ok();
            }
            return RedirectToAction("AccessDenied", "Account");
        }
    }
}
