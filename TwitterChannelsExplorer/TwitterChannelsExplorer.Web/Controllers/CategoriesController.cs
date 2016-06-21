using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitterChannelsExplorer.Web;
using TwitterChannelsExplorer.TwitterService.Factories;

namespace TwitterChannelsExplorer.Web.Web.Controllers
{
	public class CategoriesController : Controller
	{
		[HttpGet]
		public ViewResult GetCategories()
		{
			var service = ServicesFactory.GetCategoriesService();
			var model = service.GetCategoriesModel();

			return View(model);
		}
		[HttpPost]
		public JsonResult AddCategory(string nameCategory)
		{
			string status = string.Empty;
			var service = ServicesFactory.GetCategoriesService();
			int id = service.AddCategory(nameCategory);
			if (!id.Equals(-1))
			{
				status = "ok";
			}
			else
			{
				status = "false";
			}
			return Json(new { id = id, status = status });
		}
		[HttpPost]
		public JsonResult DeleteCategory(int id)
		{
			string status = string.Empty;
			var service = ServicesFactory.GetCategoriesService();
			if (service.DeleteCategory(id))
			{
				status = "ok";
			}
			else
			{
				status = "fail";
			}

			return Json(new { status = status });
		}

		public JsonResult GetCategoryAutoComplete(string term)
		{
			var service = ServicesFactory.GetCategoriesService();
			var categoriesModel =  service.GetCategoriesAutoCompeteModel(term);
			return Json(categoriesModel,JsonRequestBehavior.AllowGet);
		}
	}
}