
using NewsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Repository
{
    public class SectionRepository : iNewsAppRepositories<Sections>
    {
        private readonly ApplicationDbContext db;

        public SectionRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void Add(Sections entity)
        {
            db.sections.Add(entity);
            db.SaveChanges(); ;
        }

        public void Delete(int id)
        {
            var sec = Find(id);

            db.sections.Remove(sec);
            db.SaveChanges(); ;
        }

        public Sections Find(int id)
        {
            var sec = db.sections.SingleOrDefault(a => a.id == id);

            return sec; 
        }

        public IList<Sections> List()
        {
            return db.sections.ToList();
        }

        public void Update(int id, Sections entity)
        {
            db.Update(entity);
            db.SaveChanges(); ;
        }
    }
}
