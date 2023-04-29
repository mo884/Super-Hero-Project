using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Helper;
using SuperHero.BL.Interface;
using SuperHero.DAL.Entities;

namespace SuperHero.PL.Controllers.PatientProfile
{
    public class SocialPatientController : Controller
    {
        #region Prop
        private readonly IBaseRepsoratory<Person> person;
        private readonly IBaseRepsoratory<Comment> comment;
        private readonly IBaseRepsoratory<ReactPost> react;
        private readonly IBaseRepsoratory<Post> post;
        private readonly IMapper mapper;
        private readonly IServiesRep servies;
        private readonly SignInManager<Person> signInManager;
        #endregion


        #region ctor
        public SocialPatientController(IBaseRepsoratory<Person> person, IBaseRepsoratory<Comment> comment, IBaseRepsoratory<ReactPost> react, IBaseRepsoratory<Post> post, IMapper mapper, IServiesRep servies, SignInManager<Person> signInManager)
        {
            this.person = person;
            this.comment = comment;
            this.react = react;
            this.post = post;
            this.mapper = mapper;
            this.servies = servies;
            this.signInManager = signInManager;
        }
        #endregion

        #region GetAll
        public async Task<IActionResult> GetALL()
        {
            var data = await servies.GetALlPost("person", "Comments", "ReactPosts");
            var post = mapper.Map<IEnumerable<PostVM>>(data);
            var dataDoctor = await servies.GetPersonInclud("district", (await signInManager.UserManager.FindByNameAsync(User.Identity.Name)).Id);
            var Patient = mapper.Map<CreatePerson>(dataDoctor);
            var Doctor = await servies.GetDoctor(Patient.districtID, Patient.district.CityId, Patient.district.City.GovernorateID);
            var Doctorvm = mapper.Map<IEnumerable<CreatePerson>>(Doctor);
            var p = new AuditViewModel { post = post, nearDoctor = Doctorvm };
            return View(p);
        }
        #endregion

        #region GetComment
        [HttpGet]
        public async Task<IActionResult> Comments(int id)
        {
            //Get Comment With Include Person And Post
            var comment = await servies.GetAll(id, "person", "post");
            //Mapper
            var comments = mapper.Map<IEnumerable<CommentVM>>(comment);
            //Use Class To Send The PostId and The Comments To send to Partial View 
            return PartialView("GetComments", new CommentServise
            {
                PostID = id,
                comment = comments
            });
        }
        #endregion

        #region Add Comment
        public async Task<IActionResult> Create(CommentServise model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var PersonProfile = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
                    var Comment = await Service.Createcomment(model, PersonProfile);
                    if (ModelState.IsValid)
                    {
                        var result = mapper.Map<Comment>(Comment);
                        await comment.Create(result);
                        return RedirectToAction("GetAll");
                    }
                }
                
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return View("GetComments", comment);
        }
        #endregion


    }
}
