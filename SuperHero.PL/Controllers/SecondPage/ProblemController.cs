using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.DomainModelVM;
using SuperHero.DAL.Entities;

namespace SuperHero.PL.Controllers.SecondPage
{
    public class ProblemController : Controller
    {

        private readonly IBaseRepsoratory<Problem> problem;
        private readonly IMapper mapper;
        public ProblemController(IBaseRepsoratory<Problem> problem, IMapper mapper)
        {
            this.problem = problem;
            this.mapper = mapper;

        } 
        public async Task<IActionResult>  Problem()
        {
            var Date = await problem.GetAll();
            var result = mapper.Map<IEnumerable<ProblemVM>>(Date);
            return View(result);
        }
        public async Task<IActionResult> GetAll()
        {
            var Date = await problem.GetAll();
            var result= mapper.Map< IEnumerable<ProblemVM>>(Date);
            return View(result);
        }
        public async Task<IActionResult> Delete(int id)
        {
             var Problem = await problem.GetByID(id);
             await problem.Delete(Problem);
             return RedirectToAction("Creat","Problem");
        }
        [HttpGet]
        public async Task<IActionResult> Creat()
        {
            return PartialView("Creat");
           

        }
        [HttpPost]
        public async Task<IActionResult> Creat(ProblemVM problems)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result =  mapper.Map<Problem>(problems);
                    result.PathImage = FileUploader.UploadFile("Courses", problems.Image);

                    await problem.Create(result);
                   
                    return RedirectToAction("GetAll");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return RedirectToAction("Creat",problems);

        }
    }
}
