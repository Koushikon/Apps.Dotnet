using System;
using System.Web.Mvc;
using Web.App_Data;
using Web.Models;

namespace Web.Controllers
{
    public class CustomerController : Controller
    {
        private DataAccessLayer objDB = new DataAccessLayer();
                
        // GET: Customer/Index
        [HttpGet]
        public ActionResult Index()
        {
            Customer objCustomer = new Customer
            {
                ShowallCustomer = objDB.Selectalldata()
            };

            return View(objCustomer);
        }


        // GET: Customer/Insert
        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }

        // POST: Customer/Insert
        [HttpPost]
        public ActionResult Insert(Customer objCustomer)
        {
            objCustomer.Birthdate = Convert.ToDateTime(objCustomer.Birthdate);
            if (ModelState.IsValid)
            {
                int res = objDB.InsertData(objCustomer);

                TempData["ForCreate"] = (res == 1) ?
                    "Record Is Inserted Successfully" :
                    "Error Occurred in Inserting Data";

                ModelState.Clear();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Error in saving data");
                return View();
            }
        }


        // GET: Customer/Details/7
        [HttpGet]
        public ActionResult Details(string ID)
        {
            Customer objCustomer = objDB.SelectDatabyID(ID);

            return View(objCustomer);
        }


        // GET: Customer/Edit/7
        [HttpGet]
        public ActionResult Edit(string ID)
        {
            Customer objCustomer = objDB.SelectDatabyID(ID);

            return View(objCustomer);
        }


        // POST: Customer/Edit/7
        [HttpPost]
        public ActionResult Edit(Customer objCustomer)
        {
            //objCustomer.Birthdate = DateTime.Now;
            int res = objDB.UpdateData(objCustomer);

            TempData["ForUpdate"] = (res == 2) ?
                "Record Is Updated Successfully" :
                "Error Occurred in Updating Data";

            return RedirectToAction("Index");
        }


        // GET: Customer/Delete/7
        [HttpGet]
        public ActionResult Delete(String ID)
        {
            int res = objDB.DeleteData(ID);
            TempData["ForDelete"] = (res == 3) ?
                "Record Is Deleted Successfully" :
                "Error Occurred in Deleting Data";

            return RedirectToAction("Index");
        }
    }
}