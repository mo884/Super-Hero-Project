﻿using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Interface;

namespace SuperHero.PL.Controllers.PrivateClinic
{
    public class AnalysisController : Controller
    {
        #region Prop
        private readonly IMapper mapper;
        private readonly IServiesRep servies;
        private readonly SignInManager<Person> signInManager;
        #endregion


        #region ctor
        public AnalysisController(IMapper mapper, IServiesRep servies, SignInManager<Person> signInManager)
        {


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
    }
}
