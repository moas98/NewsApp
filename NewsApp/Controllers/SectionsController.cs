using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Models;
using NewsApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Controllers
{
    [Authorize]
    public class SectionsController : Controller
        
    {
        private readonly iNewsAppRepositories<Sections> _secRepository;

        public SectionsController(iNewsAppRepositories<Sections> SecRepository)
        {
            _secRepository = SecRepository;
        }
        // GET: SectionsController
        public ActionResult Index()
        {
            var sec = _secRepository.List();

            return View(sec);
        }

        // GET: SectionsController/Details/5
        public ActionResult Details(int id)
        {
            var sec = _secRepository.Find(id);

            return View(sec);
        }

        // GET: SectionsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SectionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Sections sections)
        {
            try
            {
                _secRepository.Add(sections);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SectionsController/Edit/5
        public ActionResult Edit(int id)
        {
            var sec = _secRepository.Find(id);
            return View(sec);
        }

        // POST: SectionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Sections sec)
        {
            try
            {
                _secRepository.Update(id,sec);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            var sec = _secRepository.Find(id);

            return View(sec);
        }

        // POST: Author/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Sections sec)
        {
            try
            {
                _secRepository.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
