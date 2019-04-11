using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace ETicketWebClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? i, string searchTerm = null)
        {
            using (ETicketService.EventServiceClient eventClient = new ETicketService.EventServiceClient())
            {
                eventClient.ClientCredentials.UserName.UserName = "ETicket";
                eventClient.ClientCredentials.UserName.Password = "ETicketPass";
                var events = eventClient.GetAllEvents().Where(e => searchTerm == null || e.Title.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase) || e.Title.Contains(searchTerm)).OrderBy(e => e.Date);
                return View(events.ToList().ToPagedList(i ?? 1, 6));
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}