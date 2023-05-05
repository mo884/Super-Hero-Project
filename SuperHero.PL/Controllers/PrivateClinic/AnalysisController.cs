using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Interface;

namespace SuperHero.PL.Controllers.PrivateClinic
{
    public class AnalysisController : Controller
    {
        private readonly IBaseRepsoratory<UserInfo> patient;
        #region Prop
        private readonly IMapper mapper;
        private readonly IServiesRep servies;
        private readonly SignInManager<Person> signInManager;
        #endregion


        #region ctor
        public AnalysisController(IBaseRepsoratory<UserInfo> patient,IMapper mapper, IServiesRep servies, SignInManager<Person> signInManager)
        {
            this.patient = patient;
            this.mapper = mapper;
            this.servies = servies;
            this.signInManager = signInManager;
        }
        #endregion
        public async Task<IActionResult> PatientAnalysis(int id)
        {
            var Analysis = await servies.GetAllAnalysisbyId(id);
            var AnalysisVM = mapper.Map<List<AnalysisVM>>(Analysis);
            return PartialView(AnalysisVM);
        }
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            TempData["PatientId"] = id;
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DoctorAnalysis analysisVM)
        {
            analysisVM.personID = (int)TempData["PatientId"];
            await servies.Create(analysisVM);
            return RedirectToAction("PatientRecord", "DoctorHome", new { id = analysisVM.patient.UserID });
        }
    }
}
