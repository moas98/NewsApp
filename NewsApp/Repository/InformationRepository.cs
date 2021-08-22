using Microsoft.EntityFrameworkCore;
using NewsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Repository
{
    public class InformationRepository : iNewsAppRepositories<Informations>
    {
        private readonly ApplicationDbContext db;

        public InformationRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void Add(Informations entity)
        {
            db.informations.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var info = Find(id);

            db.informations.Remove(info);
            db.SaveChanges();
        }

        public Informations Find(int id)
        {
            var info = db.informations.Include(a => a.Section).SingleOrDefault(b => b.id == id);

            return info;
        }

        public IList<Informations> List()
        {
            return db.informations.Include(a => a.Section).ToList();
        }

        public void Update(int id, Informations entity)
        {

            db.Update(entity);
            db.SaveChanges();
        }
    }
}
