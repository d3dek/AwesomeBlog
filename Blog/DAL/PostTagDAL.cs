﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.DAL
{
    public interface IPostTag
    {
        bool dodajPost(Models.PostTagModel model);
        List<Models.PostTagModel> pobierzPorcje(int ile, int offset);
    }

    public class PostTagDAL:IPostTag
    {
        public bool dodajPost(Models.PostTagModel model)
        {
            using (LinqTodbBlogDataContext db = new LinqTodbBlogDataContext())
            {
                try
                {
                    Posty p = new Posty
                    {
                        tytul = model.Tytul,
                        tresc = model.Tresc,
                        data_dodania = DateTime.Now,
                        data_modyfikacji = DateTime.Now,
                        status = model.Status
                    };

                    db.Posties.InsertOnSubmit(p);
                    db.SubmitChanges();

                    Tagi t = new Tagi
                    {
                        description = model.Desc,
                        keywords = model.Keywords,
                        id_posta = p.id
                    };

                    db.Tagis.InsertOnSubmit(t);
                    db.SubmitChanges();

                }
                catch (Exception)
                {
                    return false;
                    /*throw new Exception("Wystąpił błąd podczas dodawania nowego postu");*/
                }

                return true;
            }
        }
        public List<Models.PostTagModel> pobierzPorcje(int ile, int offset)
        {
            using (LinqTodbBlogDataContext db = new LinqTodbBlogDataContext())
            {
                try
                {
                    var lista = (from a in db.PostyTagis
                                 where a.status != 0
                                 orderby a.data_dodania descending
                                 select new Models.PostTagModel
                                 {
                                     Tytul = a.tytul,
                                     Tresc = a.tresc,
                                     Status = a.status,
                                     Keywords = a.keywords,
                                     Id = a.id,
                                     Desc = a.description,
                                     DataModyfikacji = a.data_modyfikacji,
                                     DataDodania = a.data_dodania,
                                 }).Skip(ile*offset).Take(ile).ToList();
                    return lista;
                }
                catch (Exception)
                {
                    throw new Exception("Wystąpił błąd podczas pobierania wpisów");
                }
            }
        }
    }
}