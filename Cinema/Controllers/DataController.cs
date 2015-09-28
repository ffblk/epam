using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cinema.Models;
using System.IO;

namespace Cinema.Controllers
{
    public class DataController : Controller
    {
        //
        // GET: /Data/
        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
            Session["Culture"] = new CultureInfo(lang);
            return Redirect(returnUrl);
        }
        public ActionResult Index(int? id)
        {           
            ViewBag.Plays = Plays.PlList(id);
            ViewBag.SelPurch = Plays.SelPur();
            ViewBag.Detail = Detail.Plist();
            return View();
        }
        public ActionResult Details(int id)
        {
            ViewBag.Detail = Detail.FindId(id);
            if (Request.IsAjaxRequest())
            {
                return PartialView("Details/_Dialog");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Add(string pName, int pPrice, int pCount)
        {          
            return RedirectToAction("Index");
        }
        public ActionResult Del(int id)
        {
            if ((Session["role"] as int?) == 1)
            {
                return RedirectToAction("Index");
            }
            else return RedirectToAction("Index", "Home");
        }
        public ActionResult Buy(int Id, DateTime Date)
        {
            ViewBag.Play = Plays.SelectPlay(Id);
            ViewBag.Date = Date;
            ViewBag.DateID = Plays.DateId(Date);
            ViewBag.Detail = Detail.FindId(Id);            
            ViewBag.Hall = Hall.ListOrders(Date);
            return View();
        }
        public ActionResult Edit(int pId, string pName, int pPrice, int pCount)
        {
            if ((Session["role"] as int?) == 1)
            {
                return RedirectToAction("Index");
            }
            else return RedirectToAction("Index", "Home");
        }
        public ActionResult ConfirmBuy(int Date, string ID)
        {
            if (Session["userID"] != null && Session["userID"] != "" && (int)Session["userID"] != -1)
            {
                int Price;
                ViewBag.ListOrder = Funcs.RequestConfirm(Date, ID, out Price);
                ViewBag.Price = Price;
                ViewBag.OrderString = ID;
                ViewBag.IdDate = Date;
                return View();
            }
            else
                return RedirectToAction("Login", "Account");
        }
        public ActionResult BuyID(int Date, string ID)
        {
            int uname;
            if (Session["userID"] != null && Session["userID"] != "" && (int)Session["userID"] != -1)
            {
                uname = (int)Session["userID"];       
                ViewBag.ListOrder = Funcs.InsertOrders(Date, ID, uname);
                return View();
            }
            else
                return RedirectToAction("Login", "Account");
        }
        public ActionResult ShowOrders(int ? id)
        {
            if (id != null) AllOrders.CancelOrder(id);
            ViewBag.Orders = AllOrders.Show();
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Data");
        }
        public ActionResult confirmOrder(int id)
        {
            return View();
        }
        public ActionResult GetTxtReport()
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=myfile.txt");
            Response.ContentType = "text/txt";
            List<AllOrders> p = AllOrders.Show();
            // Write all my data
            foreach (AllOrders e in p)
            {
                Response.Write(Resources.Resource.User);
                Response.Write(": ");
                Response.Write(e.NameUser);
                Response.Write("\r\n");
                Response.Write(Resources.Resource.Phone);
                Response.Write(": ");
                Response.Write(e.Phone);
                Response.Write("\r\n");
                Response.Write(Resources.Resource.Mail);
                Response.Write(": ");
                Response.Write(e.Mail);
                Response.Write("\r\n");
                Response.Write(Resources.Resource.Film);
                Response.Write(": ");
                Response.Write(e.NamePlays);
                Response.Write("\r\n");
                Response.Write(Resources.Resource.Dates);
                Response.Write(": ");
                Response.Write(e.Date.ToString("d"));
                Response.Write("\r\n");
                Response.Write(Resources.Resource.Row);
                Response.Write(": ");
                Response.Write(e.Row);
                Response.Write("  ");
                Response.Write(Resources.Resource.Seat);
                Response.Write(": ");
                Response.Write(e.Seat);
                Response.Write("\r\n");
                Response.Write(Resources.Resource.Price);
                Response.Write(": ");
                Response.Write(e.Price);
                Response.Write("\r\n");
                Response.Write(Resources.Resource.Status);
                Response.Write(": ");
                    Response.Write(Resources.Resource.NotPaid);
            }
            Response.End();

            // Not sure what else to do here
            return Content(String.Empty);
        }
    }
}
