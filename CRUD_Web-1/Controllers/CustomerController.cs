using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_Web_1.Models;
using CRUD_Web_1.DataAccess;

namespace CRUD_Web_1.Controllers
{
    public class CustomerController : Controller
    {
        private DataAccessLayer objDB = new DataAccessLayer();

        #region Show All Customer
        [HttpGet]
        public ActionResult Index()
        {
            Customer objCustomer = new Customer
            {
                ShowallCustomer = objDB.Selectalldata()
            };

            return View(objCustomer);
        }
        #endregion

        #region Insert Customer
        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }

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
        #endregion

        #region Show Single Customer
        [HttpGet]
        public ActionResult Details(string ID)
        {
            Customer objCustomer = objDB.SelectDatabyID(ID);

            return View(objCustomer);
        }
        #endregion

        #region Update Customer
        [HttpGet]
        public ActionResult Edit(string ID)
        {
            Customer objCustomer = objDB.SelectDatabyID(ID);

            return View(objCustomer);
        }

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
        #endregion

        #region Delete Customer
        [HttpGet]
        public ActionResult Delete(String ID)
        {
            int res = objDB.DeleteData(ID);
            TempData["ForDelete"] = (res == 3) ?
                "Record Is Deleted Successfully" :
                "Error Occurred in Deleting Data";

            return RedirectToAction("Index");
        }
        #endregion
    }
}