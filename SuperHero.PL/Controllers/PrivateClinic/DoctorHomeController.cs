using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Interface;

namespace SuperHero.PL.Controllers.PrivateClinic
{
    public class DoctorHomeController : Controller
    {
        #region Prop
        private readonly IMapper mapper;
        private readonly IServiesRep servies;
        private readonly SignInManager<Person> signInManager;
        #endregion


        #region ctor
        public DoctorHomeController( IMapper mapper, IServiesRep servies, SignInManager<Person> signInManager)
        {
           
          
            this.mapper = mapper;
            this.servies = servies;
            this.signInManager = signInManager;
        }
        #endregion
        [HttpGet]
        public async Task<IActionResult>  AllRecorder()
        {
            var PersonProfile = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
            var patient = await servies.GetAllPatientRecord(PersonProfile.Id);
            var patientVM = mapper.Map<IEnumerable<RecorderVM>>(patient);
            return View(patientVM);
        }

        [HttpGet]
        public async Task<IActionResult> PatientRecord(string id)
        {

            var patient = await servies.GetPatientRecord(id);
            return View(patient);
        }
    }
}
