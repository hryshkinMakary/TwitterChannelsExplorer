using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitterChannelsExplorer.Web.Models;
using TwitterChannelsExplorer.Core.Factories;


namespace TwitterChannelsExplorer.Web.Controllers
{
    public class TweetsController : Controller
    {
		// GET: Tweets
		[HttpGet]
		public ViewResult GetTweets(int id)
        {
			try
			{
				var service = ServicesFactory.GetTweetsService();
				var model = service.GetTweetsModel(id);
				return View(model);
			}
			catch(Exception exception)
			{
				return View();
			}
		}
		[HttpPost]
		public JsonResult GetPartialModel(ChannelInfoModel channelInfo)
		{
			
			var service = ServicesFactory.GetTweetsService();
			var model = service.GetPartialTweetsModel(channelInfo.ChannelId,channelInfo.ChannelName);

			return Json(new { status = "ok", model = model });
		}
	}
}