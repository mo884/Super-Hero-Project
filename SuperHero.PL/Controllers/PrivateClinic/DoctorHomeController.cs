using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Interface;
using SuperHero.DAL.Entities;

namespace SuperHero.PL.Controllers.PrivateClinic
{
    public class DoctorHomeController : Controller
    {
        private readonly IBaseRepsoratory<Recorder> recoder;
        #region Prop
        private readonly IMapper mapper;
        private readonly IServiesRep servies;
        private readonly SignInManager<Person> signInManager;
        #endregion


        #region ctor
        public DoctorHomeController( IBaseRepsoratory<Recorder> recoder,IMapper mapper, IServiesRep servies, SignInManager<Person> signInManager)
        {
            this.recoder = recoder;
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
        #region Check Patient
        [HttpPost]
        public async Task<IActionResult> IsCheck(string id)
        {
            var PersonProfile = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);

            var PatientRecord = await servies.GetPatientRecordBYID(PersonProfile.Id, id);

           
            
              
            if (PatientRecord.IsCheck == false)
            {
                PatientRecord.IsCheck = true;
                await recoder.Update(PatientRecord);
              
                return Ok();
            }
            PatientRecord.IsCheck = false;
            await recoder.Update(PatientRecord);

            return Ok();

        }
        #endregion
    }
}
