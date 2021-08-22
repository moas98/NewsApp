using Microsoft.AspNetCore.Http;
using NewsApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.ViewModel
{
    public class SectionInfoModel
    {
       
        public int InfoId { get; set; }
        public string Title { get; set; }
        public string Discription { get; set; }
        public string ImageURL { get; set; }
        public IFormFile File { get; set; }
        public DateTime Date { get; set; }
        public List<Sections> Sections { get; set; }
        public int SecId { get; set; }
        public string SectionName { get; set; }

    }
}
