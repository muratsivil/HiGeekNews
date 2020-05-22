using HiGeekNews.Entity.Entities;
using HiGeekNews.Entity.Enums;
using HiGeekNews.Service.Repositories;
using HiGeekNews.UI.Areas.Admin.Data.DTO;
using HiGeekNews.UI.Areas.Admin.Data.VMs;
using HiGeekNews.Utility.ImageProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiGeekNews.UI.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        PostRepository _postRepository;
        CategoryRepository _categoryRepository;
        AppUserRepository _appUserRepository;

        public PostController()
        {
            _postRepository = new PostRepository();
            _categoryRepository = new CategoryRepository();
            _appUserRepository = new AppUserRepository();
        }
        // GET: Admin/Post
        public ActionResult Create()
        {
            CreatePostVM model = new CreatePostVM()
            {
                Categories = _categoryRepository.GetActive(),
                AppUsers = _appUserRepository.GetDefault(x => x.Role != Role.Member)
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(Post model, HttpPostedFileBase Image)
        {
            List<string> UploadImagePaths = new List<string>();

            UploadImagePaths = ImageUploader.UploadSingleImage(ImageUploader.OriginalProfileImagePath, Image, 1);

            model.ImagePath = UploadImagePaths[0];

            if (model.ImagePath == "0" || model.ImagePath == "1" || model.ImagePath == "2")
            {
                model.ImagePath = ImageUploader.DefaultProfileImagePath;
                model.ImagePath = ImageUploader.DefaultXSmallProfileImage;
                model.ImagePath = ImageUploader.DefaulCruptedProfileImage;
            }
            else
            {
                model.ImagePath = UploadImagePaths[1];
                model.ImagePath = UploadImagePaths[2];
            }

            model.PublishDate = DateTime.Now;

            _postRepository.Add(model);
            return Redirect("/Admin/Post/List");
        }

        public ActionResult List()
        {
            List<Post> model = _postRepository.GetActive();
            return View(model);
        }

        public ActionResult Detail(int id)
        {
            Post post = _postRepository.GetById(id);
            return View(post);
        }


        public ActionResult Update(int id)
        {
            Post post = _postRepository.GetById(id);
            UpdatePostVM model = new UpdatePostVM();
            model.PostDTO.Id = post.Id;
            model.PostDTO.Header = post.Header;
            model.PostDTO.Content = post.Content;
            model.PostDTO.PublishDate = post.PublishDate;
            model.PostDTO.ImagePath = post.ImagePath;

            List<Category> categories = _categoryRepository.GetActive();
            model.Categories = categories;

            List<AppUser> appUsers = _appUserRepository.GetActive();
            model.AppUsers = appUsers;

            return View(model);
        }

        [HttpPost]
        public ActionResult Update(PostDTO model, HttpPostedFileBase Image)
        {
            List<string> UploadImagePaths = new List<string>();

            UploadImagePaths = ImageUploader.UploadSingleImage(ImageUploader.OriginalProfileImagePath, Image, 1);

            model.ImagePath = UploadImagePaths[0];

            Post post = _postRepository.GetById(model.Id);

            if (model.ImagePath == "0" || model.ImagePath == "1" || model.ImagePath == "2")
            {
                if (post.ImagePath == null || post.ImagePath == ImageUploader.DefaultProfileImagePath)
                {
                    post.ImagePath = ImageUploader.DefaultProfileImagePath;
                    post.ImagePath = ImageUploader.DefaultXSmallProfileImage;
                    post.ImagePath = ImageUploader.DefaulCruptedProfileImage;
                }
                else
                {
                    post.ImagePath = post.ImagePath;
                }
            }
            else
            {
                post.ImagePath = UploadImagePaths[0];
                post.ImagePath = UploadImagePaths[1];
                post.ImagePath = UploadImagePaths[2];
            }

            post.Header = model.Header;
            post.Content = model.Content;
            post.PublishDate = DateTime.Now;
            post.Status = Status.Modified;
            post.UpdateDate = DateTime.Now;
            post.CategoryId = model.CategoryId;
            post.AppUserId = model.AppUserId;

            _postRepository.Update(post);
            return Redirect("/Admin/Post/List");
        }

        public ActionResult Delete(int id)
        {
            _postRepository.Remove(id);
            return Redirect("/Admin/Post/List");
        }
    }
}