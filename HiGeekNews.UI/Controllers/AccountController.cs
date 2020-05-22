using HiGeekNews.Entity.Entities;
using HiGeekNews.Entity.Enums;
using HiGeekNews.Service.Repositories;
using HiGeekNews.UI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HiGeekNews.UI.Controllers
{
    public class AccountController : Controller
    {
        AppUserRepository _appUserRepository;

        public AccountController()
        {
            _appUserRepository = new AppUserRepository();
        }
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(AppUser data, HttpPostedFileBase Image)
        {
            _appUserRepository.Add(data);
            return View();
        }
        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser user = _appUserRepository.FindByUserName(User.Identity.Name);
                if (user.Status != Status.Passive)
                {
                    if (user.Role == Role.Admin)
                    {
                        string cookie = user.UserName;
                        FormsAuthentication.SetAuthCookie(cookie, true);
                        Session["UserName"] = user.UserName;
                        Session["ImagePath"] = user.UserImage;
                        return Redirect("/Admin/Home/Index");
                    }
                    else if (user.Role == Role.Author)
                    {
                        string cookie = user.UserName;
                        FormsAuthentication.SetAuthCookie(cookie, true);
                        Session["UserName"] = user.UserName;
                        Session["ImagePath"] = user.UserImage;
                        return Redirect("/Author/Home/Index");
                    }
                    else
                    {
                        string cookie = user.UserName;
                        FormsAuthentication.SetAuthCookie(cookie, true);
                        Session["UserName"] = user.UserName;
                        Session["ImagePath"] = user.UserImage;
                        return Redirect("/Member/Home/Index");
                    }
                }
                else
                {
                    ViewData["Error"] = "Username  or password are wrong..1";
                    return View();
                }
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(LoginDTO credential)
        {
            if (ModelState.IsValid)
            {
                if (_appUserRepository.CheckCredentials(credential.UserName, credential.Password))
                {
                    AppUser user = _appUserRepository.FindByUserName(credential.UserName);
                    if (user.Status != Status.Passive)
                    {
                        if (user.Role == Role.Admin)
                        {
                            string cookie = user.UserName;
                            FormsAuthentication.SetAuthCookie(cookie, true);
                            Session["UserName"] = user.UserName;
                            Session["ImagePath"] = user.UserImage;
                            return Redirect("/Admin/Home/Index");
                        }
                        else if (user.Role == Role.Author)
                        {
                            string cookie = user.UserName;
                            FormsAuthentication.SetAuthCookie(cookie, true);
                            Session["UserName"] = user.UserName;
                            Session["ImagePath"] = user.UserImage;
                            return Redirect("/Author/Home/Index");
                        }
                        else
                        {
                            string cookie = user.UserName;
                            FormsAuthentication.SetAuthCookie(cookie, true);
                            Session["UserName"] = user.UserName;
                            Session["ImagePath"] = user.UserImage;
                            return Redirect("/Member/Home/Index");
                        }
                    }
                    else
                    {
                        ViewData["error"] = "Username or password are wrong..!";
                        return View();
                    }
                }
                else
                {
                    ViewData["error"] = "Username or password are wrong..!";
                    return View();
                }
            }
            else
            {
                TempData["class"] = "custom-hide";
                return View();
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Account/Login");
        }
    }
}