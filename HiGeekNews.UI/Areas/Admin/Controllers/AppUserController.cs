using HiGeekNews.Entity.Entities;
using HiGeekNews.Service.Repositories;
using HiGeekNews.UI.Areas.Admin.Data.DTO;
using HiGeekNews.Utility.ImageProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiGeekNews.UI.Areas.Admin.Controllers
{
    public class AppUserController : Controller
    {
        AppUserRepository _appUserRepository;
        public AppUserController()
        {
            _appUserRepository = new AppUserRepository();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(AppUser data, HttpPostedFileBase Image)
        {
            List<string> UploadImagePaths = new List<string>();
            UploadImagePaths = ImageUploader.UploadSingleImage(ImageUploader.OriginalProfileImagePath, Image, 1);
            data.UserImage = UploadImagePaths[0];
            if (data.UserImage == "0" || data.UserImage == "1" | data.UserImage == "2")
            {
                data.UserImage = ImageUploader.DefaultProfileImagePath;
                data.XSmallUserImage = ImageUploader.DefaultXSmallProfileImage;
                data.CruptedUserImage = ImageUploader.DefaulCruptedProfileImage;
            }
            else
            {
                data.XSmallUserImage = UploadImagePaths[1];
                data.CruptedUserImage = UploadImagePaths[2];
            }
            _appUserRepository.Add(data);
            return View();
        }
        public ActionResult Update(int id)
        {
            AppUser appUser = _appUserRepository.GetById(id);
            AppUserDTO model = new AppUserDTO();
            model.Id = appUser.Id;
            model.FirstName = appUser.FirstName;
            model.LastName = appUser.LastName;
            model.UserName = appUser.UserName;
            model.Email = appUser.Email;
            model.PhoneNumber = appUser.PhoneNumber;
            model.Password = appUser.Password;
            model.Role = appUser.Role;
            model.UserImage = appUser.UserImage;
            model.XSmallUserImage = appUser.XSmallUserImage;
            model.CruptedUserImage = appUser.CruptedUserImage;
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(AppUserDTO model, HttpPostedFileBase Image)
        {
            List<string> UploadedImagePaths = new List<string>();

            UploadedImagePaths = ImageUploader.UploadSingleImage(ImageUploader.OriginalProfileImagePath, Image, 1);

            model.UserImage = UploadedImagePaths[0];

            AppUser appUser = _appUserRepository.GetById(model.Id);

            if (model.UserImage == "0" || model.UserImage == "1" || model.UserImage == "2")
            {
                if (appUser.UserImage == null || appUser.UserImage == ImageUploader.DefaultProfileImagePath)
                {
                    appUser.UserImage = ImageUploader.DefaultProfileImagePath;
                    appUser.XSmallUserImage = ImageUploader.DefaultXSmallProfileImage;
                    appUser.CruptedUserImage = ImageUploader.DefaulCruptedProfileImage;
                }
                else
                {
                    appUser.UserImage = appUser.UserImage;
                    appUser.XSmallUserImage = appUser.XSmallUserImage;
                    appUser.CruptedUserImage = appUser.CruptedUserImage;
                }
            }
            else
            {
                appUser.UserImage = UploadedImagePaths[0];
                appUser.XSmallUserImage = UploadedImagePaths[1];
                appUser.CruptedUserImage = UploadedImagePaths[2];
            }

            appUser.FirstName = model.FirstName;
            appUser.LastName = model.LastName;
            appUser.UserName = model.UserName;
            appUser.Email = model.Email;
            appUser.PhoneNumber = model.PhoneNumber;
            appUser.Password = model.Password;
            appUser.Role = model.Role;
            appUser.ImagePath = model.ImagePath;
            _appUserRepository.Update(appUser);
            return Redirect("/Admin/AppUser/List");
        }

        public ActionResult List()
        {
            List<AppUser> model = _appUserRepository.GetActive();
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            _appUserRepository.Remove(id);
            return Redirect("/Admin/AppUser/List");
        }       
    }
}