﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class AdminController : Controller
    {
        DAL.IPostTag _PostTag;
        DAL.IKomentarz _Komentarz;

        public AdminController()
        {
            _PostTag = new DAL.PostTagDAL();
            _Komentarz = new DAL.KomentarzDAL();
        }

        //TODO
        // GET: /Admin/
        [Authorize(Roles = "Administracja")]
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Admin/Create
        [Authorize(Roles = "Administracja")]
        public ActionResult Create()
        {
            return View();
        } 

        // POST: /Admin/Create
        [HttpPost]
        [Authorize(Roles = "Administracja")]
        public ActionResult Create(Models.PostTagModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Id=_PostTag.dodajPost(model);
                    if (model.Id==0)
                    {
                        /*dodanie wpisu nie powiodło się*/
                        ViewData["result"] = "Dodanie nowego wpisu nie powiodło się. Spróbuj ponownie, jeśli problem będzie się powtarzać skontaktuj się z administratorem.";
                        return View(model);
                    }
                }
                else
                {
                    return View(model);
                }

                /*po prawidłowym wykonaniu:*/
                return RedirectToAction("Details", "Post", new { model.Id}); //Można pokombinowac żeby przejść do Post/Details/id< 
            }
            catch(Exception e)
            {
                ViewData["result"] = e.Message.ToString();
                return View(model);
            }
        }
        
        //TODO
        // GET: /Admin/Edit/5
        [Authorize(Roles = "Administracja")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        //TODO
        // POST: /Admin/Edit/5

        [HttpPost]
        [Authorize(Roles = "Administracja")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //TODO
        // GET: /Admin/Delete/5
        [Authorize(Roles = "Administracja")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        //TODO
        // POST: /Admin/Delete/5

        [HttpPost]
        [Authorize(Roles = "Administracja")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Administracja")]
        public ActionResult DeleteComment(int idPosta, int id)
        {
            try
            {
                if (_Komentarz.UsunKomentarz(id))
                {
                    return RedirectToAction("Details", "Post", new { id = idPosta });
                }
                else
                {
                    ViewData["error"] = "Nie ma komentarza o takim ID";
                    //TODO: Zrobic strone z bledami
                    return RedirectToAction("Details", "Post", new { id = idPosta });
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
