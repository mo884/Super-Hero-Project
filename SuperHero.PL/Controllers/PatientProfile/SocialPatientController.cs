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
        private readonly IBaseRepsoratory<Friends> allfriends;
        private readonly IBaseRepsoratory<Comment> comment;
        private readonly IBaseRepsoratory<ReactPost> react;
        private readonly IBaseRepsoratory<Post> post;
        private readonly IMapper mapper;
        private readonly IServiesRep servies;
        private readonly SignInManager<Person> signInManager;
        #endregion


        #region ctor
        public SocialPatientController(IBaseRepsoratory<Person> person, IBaseRepsoratory<Friends> allfriends, IBaseRepsoratory<Comment> comment, IBaseRepsoratory<ReactPost> react, IBaseRepsoratory<Post> post, IMapper mapper, IServiesRep servies, SignInManager<Person> signInManager)
        {
            this.person = person;
            this.allfriends = allfriends;
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
            var PersonProfile = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
            var data = await servies.GetALlPost("person", "Comments", "ReactPosts");
            var post = mapper.Map<IEnumerable<PostVM>>(data);
            var dataDoctor = await servies.GetPersonInclud("district", PersonProfile.Id);
            var Patient = mapper.Map<CreatePerson>(dataDoctor);
            var Doctor = await servies.GetDoctor(Patient.districtID, Patient.district.CityId, Patient.district.City.GovernorateID);
            var Doctorvm = mapper.Map<IEnumerable<CreatePerson>>(Doctor);
            var Friends = await servies.GetBYUserFriends(PersonProfile.Id);
            var patient = await  servies.GetPersonInclud("patient", PersonProfile.Id);
            var p = new AuditViewModel { post = post,person= patient, nearDoctor = Doctorvm, friends = Friends,Allfriends=await allfriends.GetAll() };
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

        #region Get Profile By Id
       
        public async Task<IActionResult> Profile(string id)

        {

            var data = await servies.GetPersonInclud("district", id);
            var PersonProfile = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
            if (data.PersonType == DAL.Enum.PersonType.User)
            {
                var Reason = await servies.GetPatientBYID(id);
                data.patient = Reason;
            }
            var result = mapper.Map<CreatePerson>(data);
           
            result.doctorRating = await servies.DoctorRatingISTrue(PersonProfile.Id, id);
            var Friends = await servies.GetBYUserFriends(id);
            result.Friends = Friends;
            result.Allfriends = await allfriends.GetAll();

            return View(result);
        }

        #endregion

    }
}
