using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Crud_Project.Models
{
    public partial class Person
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string PersonName { get; set; }
        [Required]
        [StringLength(50)]
        public string PersonLastName { get; set; }
        [Required]
        [StringLength(50)]
        public string Profesion { get; set; }
        public byte[] Img { get; set; }
    }
}
