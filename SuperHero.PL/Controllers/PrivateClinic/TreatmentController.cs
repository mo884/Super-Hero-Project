using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Interface;

namespace SuperHero.PL.Controllers.PrivateClinic
{
    public class TreatmentController : Controller
    {
        #region Prop
        private readonly IMapper mapper;
        private readonly IServiesRep servies;
        private readonly SignInManager<Person> signInManager;
        #endregion


        #region ctor
        public TreatmentController(IMapper mapper, IServiesRep servies, SignInManager<Person> signInManager)
        {


            this.mapper = mapper;
            this.servies = servies;
            this.signInManager = signInManager;
        }
        #endregion
        public async Task<IActionResult> PatientTreatment(int id)
        {
            var Treatment = await servies.GetAllTreatmentbyId(id);
            var TreatmentVM = mapper.Map<List<TreatmentVM>>(Treatment);
            return PartialView(TreatmentVM);
        }
    }
}
