using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Models
{
    public class Sections
    {
        public int id { get; set; }
        public string SectionName { get; set; }
        public ICollection<Informations> Informations { get; set; }
    }
}
