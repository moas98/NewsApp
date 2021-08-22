using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.ViewModel
{
    public class CreateViewRoleModel
    {
        [Required]
        [Display(Name ="Role name")]
        [Remote(action: "IsRoleUse", controller: "Roule")]
        public string RoleName { get; set; }
    }
}
