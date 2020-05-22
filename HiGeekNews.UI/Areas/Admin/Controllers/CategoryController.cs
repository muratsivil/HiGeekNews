using HiGeekNews.Entity.Entities;
using HiGeekNews.Service.Repositories;
using HiGeekNews.UI.Areas.Admin.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiGeekNews.UI.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        CategoryRepository _categoryRepository;
        public CategoryController()
        {
            _categoryRepository = new CategoryRepository();
        }
        // GET: Admin/Category
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category data)
        {
            _categoryRepository.Add(data);
            return Redirect("/Admin/Category/List");
        }
        public ActionResult List()
        {
            List<Category> model = _categoryRepository.GetActive();
            return View(model);
        }
        public ActionResult Update(int id)
        {
            Category category = _categoryRepository.GetById(id);
            CategoryDTO model = new CategoryDTO();
            model.Id = category.Id;
            model.Name = category.Name;
            model.Description = category.Description;
            return View(model);
        }
        [HttpPost]
        public ActionResult Update(CategoryDTO model)
        {
            Category category = _categoryRepository.GetById(model.Id);
            category.Id = model.Id;
            category.Name = model.Name;
            category.Description = model.Description;
            _categoryRepository.Update(category);
            return Redirect("/Admin/Category/List");
        }
        public ActionResult Delete(int id)
        {
            _categoryRepository.Remove(id);
            return Redirect("/Admin/Category/List");
        }
    }
}