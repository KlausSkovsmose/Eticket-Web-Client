using ETicketWebClient.ETicketService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ETicketWebClient.Models;

namespace ETicketWebClient.Controllers
{
    public class OrderController : Controller
    {

        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }





        // GET: Order/Create
        [Authorize(Roles = "customer, admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderFormViewModel orderFormViewModel)
        {
            using (ETicketService.OrderServiceClient orderClient = new ETicketService.OrderServiceClient())
            {
                using (ETicketService.EventServiceClient eventClient = new ETicketService.EventServiceClient())
                {

                    orderClient.ClientCredentials.UserName.UserName = "ETicket";
                    orderClient.ClientCredentials.UserName.Password = "ETicketPass";
                    eventClient.ClientCredentials.UserName.UserName = "ETicket";
                    eventClient.ClientCredentials.UserName.Password = "ETicketPass";

                    string UserId = User.Identity.GetUserId();
                    int EventId = orderFormViewModel.EventId;
                    int Quantity = orderFormViewModel.Quantity;


                    Order order = new Order();
                    order.Quantity = Quantity;
                    order.CustomerId = UserId;
                    order.EventId = EventId;
                    order.Date = DateTime.Now.Date;
                    decimal ticketPrice = eventClient.GetEvent(EventId).TicketPrice;
                    decimal totalPrice = ticketPrice * Quantity;
                    order.TotalPrice = totalPrice;

                    var myOrder = orderClient.CreateOrder(order);
                    if (orderClient.GetOrder(myOrder) == null)
                    {
                        ViewBag.Message = "Order not placed, please try again!";
                    }
                    else
                    {
                        ViewBag.Message = "Order placed :)";
                    }
                    return View();
                }
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
