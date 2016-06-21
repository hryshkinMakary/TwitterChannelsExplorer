using System.Web.Mvc;
using TwitterChannelsExplorer.Core;
using System;
using TwitterChannelsExplorer.TwitterService.Factories;


namespace TwitterChannelsExplorer.Core
{
	public class ChannelsController : Controller
	{
		#region Fields & Constants

		#endregion

		#region Public methods
		[HttpGet]
		public ViewResult GetChannels()
		{
			var service = ServicesFactory.GetChannelsService();
			var model = service.GetChannelsModel();

			return View(model);
		}
		[HttpPost]
		public JsonResult AddChannel(string channel)
		{
			string status = "ok";
			int id = 0;
			try
			{
				var service = ServicesFactory.GetChannelsService();
				id = service.AddChannel(channel);
			}
			catch (Exception exception)
			{
				status = "false";
			}

			return Json(new { id = id, status = status });
		}
		[HttpPost]
		public JsonResult DeleteChannel(int id)
		{
			string status = string.Empty;
			var service = ServicesFactory.GetChannelsService(); ;
			if (service.DeleteChannel(id))
			{
				status = "ok";
			}
			else
			{
				status = "fail";
			}


			return Json(new { status = status });
		}
		#endregion
	}
}