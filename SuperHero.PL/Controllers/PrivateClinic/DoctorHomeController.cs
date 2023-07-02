using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Interface;
using SuperHero.BL.Seeds;
using SuperHero.DAL.Entities;
using System.Data;

namespace SuperHero.PL.Controllers.PrivateClinic
{
    [Authorize(Roles = AppRoles.Doctor)]
    public class DoctorHomeController : Controller
    {

        #region Prop
        private readonly IMapper mapper;
        private readonly IServiesRep servies;
        private readonly SignInManager<Person> signInManager;
        private readonly IBaseRepsoratory<Recorder> recoder;
        #endregion


        #region ctor
        public DoctorHomeController(IBaseRepsoratory<Recorder> recoder, IMapper mapper, IServiesRep servies, SignInManager<Person> signInManager)
        {
            this.recoder = recoder;
            this.mapper = mapper;
            this.servies = servies;
            this.signInManager = signInManager;
        }
        #endregion
        [HttpGet]
        public async Task<IActionResult> AllRecorder()
        {
            if (signInManager.IsSignedIn(User))
            {
                var PersonProfile = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
                var patient = await servies.GetAllPatientRecord(PersonProfile.Id);
                var patientVM = mapper.Map<IEnumerable<RecorderVM>>(patient);
                return View(patientVM);
            }
            return RedirectToAction("AccessDenied", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> PatientRecord(string id)
        {
            if (signInManager.IsSignedIn(User))
            {
                var patient = await servies.GetPatientRecord(id);
                return View(patient);
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        #region Check Patient
        [HttpPost]
        public async Task<IActionResult> IsCheck(string id)
        {
            if (signInManager.IsSignedIn(User))
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
            return RedirectToAction("AccessDenied", "Account");

        }
        #endregion
    }
}
