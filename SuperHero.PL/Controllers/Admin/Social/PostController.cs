using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.DomainModelVM;
using SuperHero.BL.Helper;
using SuperHero.BL.Interface;
using SuperHero.DAL.Entities;

namespace SuperHero.PL.Controllers.Admin.Social
{
    public class PostController : Controller
    {
        #region Prop
        private readonly IBaseRepsoratory<Person> person;
        private readonly IBaseRepsoratory<ReactPost> react;
        private readonly IBaseRepsoratory<Post> post;
        private readonly IMapper mapper;
        private readonly IServiesRep servies;
        private readonly SignInManager<Person> signInManager;
        #endregion

        #region ctor
        public PostController(IBaseRepsoratory<Person> person, IBaseRepsoratory<ReactPost> react, IBaseRepsoratory<Post> post, IMapper mapper, IServiesRep servies, SignInManager<Person> signInManager)
        {
            this.person = person;
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
            var p = new AuditViewModel { post = post, nearDoctor = Doctorvm  };
            return View(p);
        }
        #endregion

        #region Create Post
        [HttpPost]
        public async Task<IActionResult> Create(AuditViewModel postvm)
        {
            try
            {
                var PersonProfile = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
                var posts = await Service.CreatePost(postvm, PersonProfile);

                if (ModelState.IsValid)
                {
                    var result = mapper.Map<Post>(posts);
                    await post.Create(result);
                    TempData["Message"] = "saved Successfuly";
                    return RedirectToAction("GetALL");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            TempData["Message"] = null;
            return View("GetAll", postvm);
        }
        #endregion

        #region Delete Post
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    await post.Delete(id);
                    return RedirectToAction("GetAll");
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return View("GetAll");

        }
        #endregion

        #region Edite Post
        [HttpGet]
        public async Task<IActionResult> Edite(int id)
        {

            var data = await post.GetByID(id);
            data.person = await person.GetByID(data.PersonID);
            var result = mapper.Map<PostVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edite(PostVM Post)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    Post.Image = FileUploader.UploadFile("Imgs", Post.ImageName);
                    var result = mapper.Map<Post>(Post);
                    await post.Update(result);
                    return RedirectToAction("GetAll");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return View("Edite", Post);
        }

        #endregion

        #region Add React
        [HttpPost]
        public async Task<IActionResult> AddLike(int id)
        {

            var Post = await post.GetByID(id);
            if (Post is null)
                return NotFound();
            var PersonProfile = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
            var React = await servies.GetBYUserAndPost(PersonProfile.Id, id);
            if (React is null)
            {
                var AddReact = new ReactPost()
                {
                    IsLike = true,
                    PersonID = PersonProfile.Id,
                    PostID = id,
                    Post = Post
                };

                Post.Like += 1;
                await post.Update(Post);
                await react.Create(AddReact);
                return Ok();
            }

            if (React.IsLike == false)
            {
                React.IsLike = true;
                await react.Update(React);
                Post.Like += 1;
                await post.Update(Post);
                return Ok();
            }
            React.IsLike = false;
            await react.Update(React);
            Post.Like -= 1;
            await post.Update(Post);
            return Ok();

        }
        #endregion

    }
}
