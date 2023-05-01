using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Interface;

namespace SuperHero.PL.Controllers.MyProfile
{
    [Route("/api/Profile")]
    public class MyProfileController : Controller
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
        public MyProfileController(IBaseRepsoratory<Person> person, IBaseRepsoratory<Friends> allfriends, IBaseRepsoratory<Comment> comment, IBaseRepsoratory<ReactPost> react, IBaseRepsoratory<Post> post, IMapper mapper, IServiesRep servies, SignInManager<Person> signInManager)
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

        #region Get Profile By Id
        [HttpGet("profile")]
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
