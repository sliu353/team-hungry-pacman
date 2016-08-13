using IAmHungry.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace IAmHungry.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(double latitude, double longitude, string time)
        {
            string nearbyRestaurantJSON = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + latitude + "," + longitude + "&radius=500&type=restaurant&key=AIzaSyCHCEe0fd5bCiRC1q83p4T0AuGPC7g4PIw";

            var json = new WebClient().DownloadString(nearbyRestaurantJSON);
            var stringList = json.Split("\n".ToArray());
            List<string> restaurantList = new List<string>();
            foreach(var str in stringList)
            {
                if (str.Contains("\"place_id\""))
                {
                    restaurantList.Add((new WebClient().DownloadString("https://maps.googleapis.com/maps/api/place/details/json?key=AIzaSyCHCEe0fd5bCiRC1q83p4T0AuGPC7g4PIw&placeid=" + str.Substring(23, str.Length - 25))));
                }
            }

            int currentDay = (int) DateTime.Now.DayOfWeek;
            foreach (var restaurantString in restaurantList)
            {
                //if(restaurantString.Contains(""))
                var detailList = restaurantString.Split("\n".ToArray());
                var checkCurrentDayString = "\"day\" : " + currentDay;
                if (detailList.Any(s => s.Contains(checkCurrentDayString)))
                {
                   // detailList.Where(d => d.Contains(checkCurrentDayString))
                    //detailList.Where()
                }
               // restaurantString.Split("\n".ToArray()).Where(x => x.Contains());
            }
            RestaurantInfo restaurantJson = new RestaurantInfo() { RestaurantJson = restaurantList };
            return (View("AskForTime", restaurantJson));
  
            //foreach(var restaurant in dynObj.)
            //Console.WriteLine("{0} {1}", dynObj.plugname, dynObj.link);
            //foreach (var version in dynObj.versions)
            //{
            //    var dt = new DateTime(1970, 1, 1).AddSeconds((int)version.date);
            //    Console.WriteLine("\t{0} {1} {2}", version.version, version.download, dt);
            //}
        }

    }
}