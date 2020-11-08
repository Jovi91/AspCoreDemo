using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAspCoreProject.Models
{
    public class ApplicationType
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is required.")]
        public string Name { get; set; }
    }
}
