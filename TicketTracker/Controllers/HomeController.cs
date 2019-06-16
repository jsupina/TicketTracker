using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TicketTracker.Models;

namespace TicketTracker.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private Entities db = new Entities();

        public ActionResult Index()
        {
            List<Ticket> openTickets = new List<Ticket>();
            List<Ticket> closedTickets = new List<Ticket>();
            List<Ticket> allTickets = db.Tickets.ToList();

            foreach (var t in allTickets)
            {
                if (t.Status.Equals("Open") == true)
                {
                    openTickets.Add(t);
                }

                else
                {
                    closedTickets.Add(t);
                }
            }

            var model = new IndexPostModel
            {
                Open = openTickets,
                Closed = closedTickets
            };

            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ticket ticket = db.Tickets.Find(id);

            if (ticket == null)
            {
                return HttpNotFound();
            }

            return View(ticket);
        }

        [HttpPost]
        public ActionResult Details(int? id, int oid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ticket ticket = db.Tickets.Find(id);

            if (ticket == null)
            {
                return HttpNotFound();
            }

            ticket.Status = "Closed";

            db.Entry(ticket).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Name,Severity,Status,User,Description")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ticket);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Users()
        {
            List<ExpandedUserDTO> col_UserDTO = new List<ExpandedUserDTO>();

            var result = UserManager.Users;

            foreach(var item in result)
            {
                ExpandedUserDTO objUserDTO = new ExpandedUserDTO();
                objUserDTO.UserName = item.UserName;
                objUserDTO.Email = item.Email;
                objUserDTO.LockoutEndDateUTC = item.LockoutEndDateUtc;
                col_UserDTO.Add(objUserDTO);
            }

            var model = new UserViewModel
            {
                users = col_UserDTO
            };

            return View(model);

        }

        // Utility
        #region public ApplicationUserManager UserManager
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ??
                    HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        #endregion
        #region public ApplicationRoleManager RoleManager
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ??
                    HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        #endregion
    }

}
