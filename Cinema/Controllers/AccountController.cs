using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cinema.Models;

namespace Cinema.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {               
                Users.AddUser(user);      
                return RedirectToAction("Index", "Data");
            }
            else
                return View(user);
        }
        public ActionResult Login(string uname, string upass="")
        {         
            int role;
            int idUser;
            if (uname != null && uname != "")
            {
                ViewBag.uName = uname;                
                if (Users.IsLogin(uname, upass, out idUser, out role))
                {
                    Session["userID"] = idUser;                   
                    Session["role"] = role;
                    Session["User"] = uname;
                    return RedirectToAction("Index", "Data");
                }                
            }
            return View();
        }
        public ActionResult MyOrders(int? id)
        {
            if (Session["userID"] != null && Session["userID"] != "" && (int)Session["userID"] != -1)
            {
                if(id != null) ListMyOrder.CancelMyOrder(id);
                ViewBag.MyOrders = ListMyOrder.Show((int)Session["userID"]);                
            }
            return View();
        }
    }
}
