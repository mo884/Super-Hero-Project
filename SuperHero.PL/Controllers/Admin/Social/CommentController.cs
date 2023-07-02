using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.DomainModelVM;
using SuperHero.BL.Helper;
using SuperHero.BL.Interface;

namespace SuperHero.PL.Controllers.Admin.Social
{
    public class CommentController : Controller
    {
        #region Prop
        private readonly IBaseRepsoratory<Person> person;
        private readonly IBaseRepsoratory<Comment> comment;
        private readonly IBaseRepsoratory<Post> post;
        private readonly IMapper mapper;
        private readonly IServiesRep servies;
        private readonly SignInManager<Person> signInManager;

        #endregion

        #region ctor
        public CommentController(IBaseRepsoratory<Person> person, IBaseRepsoratory<Comment> comment, IBaseRepsoratory<Post> post, IMapper mapper, IServiesRep servies, SignInManager<Person> signInManager)
        {
            this.person = person;
            this.comment = comment;
            this.post = post;
            this.mapper = mapper;
            this.servies = servies;
            this.signInManager = signInManager;
        }
        #endregion

        #region GetComment
        [HttpGet]
        public async Task<IActionResult> Comments(int id)
        {
            if (signInManager.IsSignedIn(User))
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
            return RedirectToAction("AccessDenied", "Account");
        }
        #endregion

        #region Add Comment
        public async Task<IActionResult> Create(CommentServise model)
        {
            if (signInManager.IsSignedIn(User))
            {
                try
                {
                    var PersonProfile = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
                    var Comment = await Service.Createcomment(model, PersonProfile);
                    if (ModelState.IsValid)
                    {
                        var result = mapper.Map<Comment>(Comment);
                        await comment.Create(result);
                        return RedirectToAction("Detailes", "Post", new { id = model.PostID });
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                }

                return View("GetComments", comment);
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        #endregion


    }
}
