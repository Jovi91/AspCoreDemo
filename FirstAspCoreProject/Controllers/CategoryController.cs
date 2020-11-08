using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FirstAspCoreProject.Data;
using FirstAspCoreProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstAspCoreProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        //Dzięki dodaniu ApplicationDbContext w AppStart.cs jako usługi do dependency injection container
        //Możemy jej teraz używać w całym projekcie i poprzez nią odwoływać się do obiektu bazy danych
        //Poniżej zwracamy się do kontenera o instancję bazy (poprzez stworzenie zmiennej referencyjnej 
        //ApplicationDbContext) i przekazujemy ją do pola _db
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }   
        public IActionResult Index()
        {

            List<Category> objList = _db.Category.ToList(); ;
            return View(objList);
        }
        //GET-Create
        public IActionResult Create()
        {
            return View();
        }

        //POST-Create
        [HttpPost]
        //poniższe to zabezpieczenie dodawane przy wpisywaniu prez użytkownika jakiś danych
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {   
            if(ModelState.IsValid)
            {
                _db.Category.Add(obj); //znowu używamy _db czyli korzystamy z dependency injection container
                _db.SaveChanges(); //dodaje obiekt do bazy danych
                return RedirectToAction("Index");
            }

            return View(obj);

        }

        //EDIT
        public IActionResult Edit(int? Id)
        {
            if(Id==null||Id==0)
                return NotFound();

            Category obj = _db.Category.Find(Id);

            if (obj == null)
                return NotFound();

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            //Server-side validation
            if(ModelState.IsValid)
            {
                _db.Category.Update(obj);
                _db.SaveChanges();

               return RedirectToAction("Index");
            }

            return View();
            
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
                return NotFound();

            var obj = _db.Category.Find(Id);

            if (obj == null)
                return NotFound();

            return View(obj);
        }

        public IActionResult DeletePost(int? Id)
        {
            var obj = _db.Category.Find(Id);

            if (obj == null)
                return NotFound();

            _db.Category.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}
