using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Models
{
    public class Informations
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Discription { get; set; }
        public string ImageURL { get; set; }
        public DateTime Date { get; set; }
        public int Sectionid { get; set; }
        public Sections Section { get; set; }
    }
}
