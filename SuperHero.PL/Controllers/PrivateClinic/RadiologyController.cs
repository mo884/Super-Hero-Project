using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.DomainModelVM;
using SuperHero.BL.Interface;
using SuperHero.DAL.Entities;

namespace SuperHero.PL.Controllers.PrivateClinic
{
    public class RadiologyController : Controller
    {

        #region Prop
        private readonly IMapper mapper;
        private readonly IBaseRepsoratory<Radiology> radiology;
        private readonly IServiesRep servies;
        private readonly SignInManager<Person> signInManager;
        #endregion


        #region ctor
        public RadiologyController(IMapper mapper, IBaseRepsoratory<Radiology> radiology, IServiesRep servies, SignInManager<Person> signInManager)
        {


            this.mapper = mapper;
            this.radiology = radiology;
            this.servies = servies;
            this.signInManager = signInManager;
        }
        #endregion
        public async Task<IActionResult> PatientRadiology(int id)
        {
            if (signInManager.IsSignedIn(User))
            {
                var Radiology = await servies.GetAllRadiologybyId(id);
                var RadiologyVM = mapper.Map<List<RadiologyVM>>(Radiology);
                var userinfo = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
                var patient = await servies.GetPatientBYID(userinfo.Id);
                var data = new PatientInfo()
                {
                    patient = patient,
                    User = userinfo,
                    RadiologyVMs = RadiologyVM
                };
                return PartialView(data);
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> AddByPatient(int id)
        {
            if (signInManager.IsSignedIn(User))
            {
                var data = await radiology.GetByID(id);
                data.IsAdd = true;
                await radiology.Update(data);
                return Ok();
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
        public async Task<IActionResult> Create(DoctorRadiology Radiology)
        {
            if (signInManager.IsSignedIn(User))
            {
                Radiology.personID = (int)TempData["PatientId"];
                var user = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
                await servies.CreateRadiology(Radiology, user.Id);
                return RedirectToAction("PatientRecord", "DoctorHome", new { id = Radiology.patient.UserID });
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> CreateBYUser(PatientInfo analysisVM)
        {
            if (signInManager.IsSignedIn(User))
            {
                var user = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
                var Patient = await servies.GetPatientBYID(user.Id);
                var Data = new DoctorRadiology
                {
                    XRay = FileUploader.UploadFile("PDF", analysisVM.uploade),
                    Name = analysisVM.Name,
                    personID = Patient.ID
                };

                await servies.CreateRadiologyBYPatient(Data);
                return RedirectToAction("Profile", "MyProfile", new { id = user.Id });
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PatientInfo patientInfo)
        {
            if (signInManager.IsSignedIn(User))
            {
                var data = await radiology.GetByID(patientInfo.ID);
                data.XRay = FileUploader.UploadFile("PDF", patientInfo.uploade);
                await radiology.Update(data);
                var user = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
                return RedirectToAction("Profile", "MyProfile", new { Id = user.Id });
            }
            return RedirectToAction("AccessDenied", "Account");
        }
    }
}
