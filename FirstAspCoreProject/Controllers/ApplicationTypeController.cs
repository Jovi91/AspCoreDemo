using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstAspCoreProject.Data;
using FirstAspCoreProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstAspCoreProject.Controllers
{
    public class ApplicationTypeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ApplicationTypeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<ApplicationType> applicationTypes = _db.ApplicationType.ToList();
            return View(applicationTypes);
        }
        //GET-Create
        public IActionResult Create()
        {
            return View();
        }

        //POST -Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationType appTypeObj)
        {
            //server-side validation (-walidacja dla tego medelu to sprawdzenie czy użytkownik wprowadził imie)
            if (ModelState.IsValid)
            {
                _db.ApplicationType.Add(appTypeObj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appTypeObj);
        }

        public IActionResult Edit(int Id)
        {
            if (Id == 0)
                return NotFound();

            var obj = _db.ApplicationType.Find(Id);
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationType obj)
        {
            if(ModelState.IsValid)
            {
                _db.ApplicationType.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
        }


        public IActionResult Delete(int Id)
        {
            if (Id == 0)
                return NotFound();

            var obj = _db.ApplicationType.Find(Id);
            return View(obj);
        }

        public IActionResult DeletePost(int Id)
        {
            var obj = _db.ApplicationType.Find(Id);
            _db.ApplicationType.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
           
        }
    }
}
