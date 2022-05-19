using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_Project.Models
{
    public class PersonForm
    {
        [StringLength(50)]
        [Required]
        [Display(Name = "Name")]
        public string PersonName { get; set; }
        [StringLength(50)]
        [Required]
        [Display(Name = "Last Name")]
        public string PersonLastName { get; set; }
        [StringLength(50)]
        [Required]
        [Display(Name = "Profession")]
        public string Profession { get; set; }
        public byte[] Img { get; set; }
    }
}
