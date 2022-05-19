using Crud_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Person> model;
            using (var db = new USERSContext())
            {
                model = (from d in db.People
                         select new Person { 
                            Id = d.Id,
                            PersonName = d.PersonName,
                            PersonLastName = d.PersonLastName,
                            Profesion = d.Profesion,
                            Img = d.Img
                         }).ToList();
            }
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PersonForm model, List<IFormFile> image)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    foreach (var file in image)
                    {
                        if (file.Length > 0)
                        {
                            using (var stream = new MemoryStream())
                            {
                                await file.CopyToAsync(stream);
                                model.Img = stream.ToArray();
                            }
                        }
                    }

                    using (var db = new USERSContext())
                    {
                        Person toAdd = new();
                        toAdd.PersonName = model.PersonName;
                        toAdd.PersonLastName = model.PersonLastName;
                        toAdd.Profesion = model.Profession;
                        toAdd.Img = model.Img;

                        db.People.Add(toAdd);
                        db.SaveChanges();
                    }

                    return Redirect("~/Home/Index");

                }
                return View(model);

            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
            
        }


        public IActionResult Edit(int id)
        {
            Person model = new Person();

            using(var db = new USERSContext())
            {
                var data = db.People.Find(id);
                model.Id = data.Id;
                model.PersonName = data.PersonName;
                model.PersonLastName = data.PersonLastName;
                model.Profesion = data.Profesion;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Person model, List<IFormFile> image)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    foreach (var file in image)
                    {
                        using (MemoryStream stream = new())
                        {
                            await file.CopyToAsync(stream);
                            model.Img = stream.ToArray();
                        }
                    }

                        using (USERSContext db = new())
                        {
                            var registro = db.People.Find(model.Id);
                            registro.PersonName = model.PersonName;
                            registro.PersonLastName = model.PersonLastName;
                            registro.Profesion = model.Profesion;
                            registro.Img = model.Img;

                            db.Entry(registro).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            db.SaveChanges();
                        }

                        return Redirect("~/Home/Index");
                    
                }
                return View();
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                using(var db = new USERSContext())
                {
                    var toRemove = db.People.Find(id);
                    db.People.Remove(toRemove);
                    db.SaveChanges();
                }

                return Redirect("~/Home/Index");

            }catch(Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
