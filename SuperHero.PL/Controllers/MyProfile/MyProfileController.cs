using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Interface;
using System.Data;

namespace SuperHero.PL.Controllers.MyProfile
{
    //[Route("/api/Profile")]
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
            if (signInManager.IsSignedIn(User))
            {
                var Profile = await person.GetByID(id);
                var PersonProfile = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
                if (Profile.PersonType == 0)
                {
                    var Patient = await servies.GetPatientProfile(id);
                    var Patientresult = mapper.Map<CreatePerson>(Patient);

                    Patientresult.doctorRating = await servies.DoctorRatingISTrue(PersonProfile.Id, id);
                    var PatientFriends = await servies.GetBYUserFriends(id);
                    Patientresult.Friends = PatientFriends;
                    Patientresult.Allfriends = await allfriends.GetAll();
                    return View(Patientresult);
                }
                else
                {
                    var data = await servies.GetPersonInclud("district", id);
                    var result = mapper.Map<CreatePerson>(data);

                    result.doctorRating = await servies.DoctorRatingISTrue(PersonProfile.Id, id);
                    var Friends = await servies.GetBYUserFriends(id);
                    result.Friends = Friends;
                    result.Allfriends = await allfriends.GetAll();
                    return View(result);
                }



            }
            return RedirectToAction("AccessDenied", "Account");

        }

        #endregion

        #region Record
        [HttpGet]
        public async Task<IActionResult> Record(string id)
        {
            if (signInManager.IsSignedIn(User))
            {
                var data = new Recorder()
                {
                    DoctorID = id
                };
                return PartialView("Record", data);
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        public async Task<IActionResult> Record(Recorder record)
        {
            if (signInManager.IsSignedIn(User))
            {
                var PersonProfile = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);

                await servies.SaveRecord(PersonProfile.Id, record.DoctorID, record);
                return RedirectToAction("Profile", "MyProfile", new { id = record.DoctorID });
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        #endregion
    }
}
