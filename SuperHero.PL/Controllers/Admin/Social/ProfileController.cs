using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Interface;

namespace SuperHero.PL.Controllers.Admin.Social
{

    public class ProfileController : Controller
    {
        #region Prop
        private readonly IBaseRepsoratory<Person> person;
        private readonly IBaseRepsoratory<Friends> friends;
        private readonly IBaseRepsoratory<Post> post;
        private readonly IBaseRepsoratory<Comment> comment;
        private readonly IMapper mapper;
        private readonly IServiesRep servies;
        private readonly SignInManager<Person> signInManager;
        #endregion

        #region Ctor
        public ProfileController(IBaseRepsoratory<Person> person, IBaseRepsoratory<Friends> friends, IBaseRepsoratory<Post> post, IBaseRepsoratory<Comment> comment, IMapper mapper, IServiesRep servies, SignInManager<Person> signInManager)
        {
            this.person = person;
            this.friends = friends;
            this.post = post;
            this.comment = comment;
            this.mapper = mapper;
            this.servies = servies;
            this.signInManager = signInManager;
        }
        #endregion

        #region Get Profile By Id
        [Route("Profile")]
        public async Task<IActionResult> Profile(string id)

        {
            if (signInManager.IsSignedIn(User))
            {
                var data = await servies.GetPersonInclud("district", id);
                var PersonProfile = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
                var result = mapper.Map<CreatePerson>(data);
                result.doctorRating = await servies.DoctorRatingISTrue(PersonProfile.Id, id);
                return PartialView(result);
            }
            return RedirectToAction("AccessDenied", "Account");
        }

        #endregion

        #region Add Friends
        [HttpPost]
        public async Task<IActionResult> AddFriends(string id)
        {
            if (signInManager.IsSignedIn(User))
            {
                var Person = await person.GetByID(id);
                if (Person is null)
                    return NotFound();
                var PersonProfile = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
                var Friend = await servies.GetBYUserFriends(PersonProfile.Id, id);
                if (Friend is null)
                {
                    var AddFriends = new Friends()
                    {
                        personId = PersonProfile.Id,
                        FriendId = id,
                        IsFriend = true,
                    };


                    await friends.Create(AddFriends);
                    return Ok();
                }

                if (Friend.IsFriend == false)
                {
                    Friend.IsFriend = true;
                    await friends.Update(Friend);
                    return Ok();
                }

                Friend.IsFriend = false;
                await friends.Update(Friend);

                return Ok();
            }
            return RedirectToAction("AccessDenied", "Account");

        }
        #endregion

        #region GetFriends
        [HttpGet]
        public async Task<IActionResult> Friend(string id)
        {
            if (signInManager.IsSignedIn(User))
            {
                var Friends = await servies.GetBYUserFriends(id);
                return PartialView("Friends", Friends);
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        public async Task<IActionResult> Follower(string id)
        {
            if (signInManager.IsSignedIn(User))
            {
                var Friends = await servies.GetFollower(id);
                return PartialView("Follower", Friends);
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        #endregion

    }
}
