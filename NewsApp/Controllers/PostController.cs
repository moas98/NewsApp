using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsApp.Models;
using NewsApp.Repository;
using NewsApp.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly iNewsAppRepositories<Informations> _info;
        private readonly iNewsAppRepositories<Sections> _secRepository;
        [Obsolete]
        private readonly IHostingEnvironment hosting;
        private readonly ApplicationDbContext _db;

        [Obsolete]
        public PostController(iNewsAppRepositories<Informations> info,
            iNewsAppRepositories<Sections> SecRepository,
            IHostingEnvironment hosting,
            ApplicationDbContext db)
        {
            _db = db;
            _info = info;
            _secRepository = SecRepository;
            this.hosting = hosting;
        }

        // GET: PostController
        public ActionResult Index()
        {
            var Show_Info = _info.List();
            return View(Show_Info);
        }
        [AllowAnonymous]
        public ActionResult OnePostDetails( int id)
        {
            ViewBag.Sections = _db.sections.ToList();
            ViewBag.InformationDetails =_info.List().ToList().Where(x=>x.id==id);
            var postdetailId = _db.informations.Find(id);
            return View(postdetailId);
        }
        [AllowAnonymous]

        public ActionResult UserIndex(int? id)
        {
            ViewBag.Sections = _db.sections.ToList();
            var OneSection = _db.sections.Include(x => x.Informations).Where(x=>x.id==id).ToList();
            var listSections = _db.sections.Include(x=> x.Informations).ToList();
            if (id !=null)
            {
                return View(OneSection);
            }
            else
            {
                return View(listSections);
            }
            
        }
        

        // GET: PostController/Details/5
        public ActionResult Details(int id)
        {
            var detail = _info.Find(id);
            return View(detail);
        }

        // GET: PostController/Create
        public ActionResult Create()

        {
            var model = new SectionInfoModel
            {
                Sections = FillSelectList()
            };
            return View(model);
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SectionInfoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = UploadFile(model.File) ?? string.Empty;

                    if (model.SecId == -1)
                    {
                        ViewBag.Message = "Please select an author from the list!";

                        return View(GetAllsections());
                    }

                    var sec = _secRepository.Find(model.SecId);
                    Informations info = new Informations
                    {
                        id = model.InfoId,
                        Title = model.Title,
                        Discription = model.Discription,
                        Section = sec,
                        ImageURL = fileName,
                        Date = model.Date

                    };

                    _info.Add(info);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }


            ModelState.AddModelError("", "You have to fill all the required fields!");
            return View(GetAllsections());
        }


        // GET: PostController/Edit/5
        public ActionResult Edit(int id)
        {
            var post = _info.Find(id);
            var secid = post.Section == null ? post.Section.id = 0 : post.Section.id;

            var viewModel = new SectionInfoModel
            {
                InfoId = post.id,
                Title = post.Title,
                Discription = post.Discription,
                SecId = secid,
                Sections = _secRepository.List().ToList(),
                ImageURL = post.ImageURL,
                Date = post.Date,
            };

            return View(viewModel);
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SectionInfoModel viewModel)
        {
            try
            {
                // TODO: Add update logic here
                string fileName = UploadFile(viewModel.File, viewModel.ImageURL);

                var sec = _secRepository.Find(viewModel.SecId);
                Informations info = new Informations
                {
                    id = viewModel.InfoId,
                    Title = viewModel.Title,
                    Discription = viewModel.Discription,
                    Date=viewModel.Date,
                    Section=sec,
                    ImageURL = fileName,
                };

                _info.Update(viewModel.InfoId, info);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: PostController/Delete/5
        public ActionResult Delete(int id)
        {
            var DelInfo = _info.Find(id);
            return View(DelInfo);
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                _info.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        List<Sections> FillSelectList()
        {
            var section = _secRepository.List().ToList();
            section.Insert(0, new Sections { id = -1, SectionName = "Enter Section Name !" });

            return section;
        }
        SectionInfoModel GetAllsections()
        {
            var vmodel = new SectionInfoModel
            {
                Sections = FillSelectList()
            };

            return vmodel;
        }
        string UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "UploadImages");
                string fullPath = Path.Combine(uploads, file.FileName);
                file.CopyTo(new FileStream(fullPath, FileMode.Create));

                return file.FileName;
            }

            return null;
        }

        string UploadFile(IFormFile file, string imageUrl)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "UploadImages");

                string newPath = Path.Combine(uploads, file.FileName);
                string oldPath = Path.Combine(uploads, imageUrl);

                if (newPath != oldPath)
                {
                    System.IO.File.Delete(oldPath);
                    file.CopyTo(new FileStream(newPath, FileMode.Create));
                }

                return file.FileName;
            }

            return imageUrl;
        }
    }
}

