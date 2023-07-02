using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.DomainModelVM;
using SuperHero.BL.Interface;
using SuperHero.DAL.Entities;

namespace SuperHero.PL.Controllers.PrivateClinic
{
    public class AnalysisController : Controller
    {

        #region Prop
        private readonly IMapper mapper;
        private readonly IServiesRep servies;
        private readonly SignInManager<Person> signInManager;
        private readonly IBaseRepsoratory<UserInfo> patient;
        private readonly IBaseRepsoratory<Analysis> analysis;
        #endregion


        #region ctor
        public AnalysisController(IBaseRepsoratory<UserInfo> patient, IBaseRepsoratory<Analysis> analysis, IMapper mapper, IServiesRep servies, SignInManager<Person> signInManager)
        {
            this.patient = patient;
            this.analysis = analysis;
            this.mapper = mapper;
            this.servies = servies;
            this.signInManager = signInManager;
        }
        #endregion
        public async Task<IActionResult> PatientAnalysis(int id)
        {
            if (signInManager.IsSignedIn(User))
            {
                var Analysis = await servies.GetAllAnalysisbyId(id);
                var AnalysisVM = mapper.Map<List<AnalysisVM>>(Analysis);
                var userinfo = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
                var patient = await servies.GetPatientBYID(userinfo.Id);
                var data = new PatientInfo()
                {
                    patient = patient,
                    User = userinfo,
                    AnalysisVMs = AnalysisVM
                };
                return PartialView(data);
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            if (signInManager.IsSignedIn(User) || id == null)
            {
                TempData["PatientId"] = id;
                return PartialView();
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> Create(DoctorAnalysis analysisVM)
        {
            if (signInManager.IsSignedIn(User))
            {
                analysisVM.personID = (int)TempData["PatientId"];
                var user = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
                await servies.Create(analysisVM, user.Id);
                return RedirectToAction("PatientRecord", "DoctorHome", new { id = analysisVM.patient.UserID });
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
                var Data = new DoctorAnalysis
                {
                    AnalysisPDF = FileUploader.UploadFile("PDF", analysisVM.uploade),
                    Name = analysisVM.Name,
                    personID = Patient.ID
                };

                await servies.CreateBYUser(Data);
                return RedirectToAction("Profile", "MyProfile", new { id = user.Id });
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> AddByPatient(int id)
        {
            if (signInManager.IsSignedIn(User))
            {
                var data = await analysis.GetByID(id);
                data.IsAdd = true;
                await analysis.Update(data);
                return Ok();
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PatientInfo patientInfo)
        {
            if (signInManager.IsSignedIn(User))
            {
                var data = await analysis.GetByID(patientInfo.ID);
                data.AnalysisPDF = FileUploader.UploadFile("PDF", patientInfo.uploade);
                await analysis.Update(data);
                var user = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
                return RedirectToAction("Profile", "MyProfile", new { Id = user.Id });
            }
            return RedirectToAction("AccessDenied", "Account");
        }
    }
}
