using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAspCoreProject.Models
{
    /*Zeby działała walidacja (czyli np Range lub Required) trzeba jeszcze dodać w widoku na początku formularza:
    <div asp-validation-summary="ModelOnly" class="text-danger"></div>
     oraz pod każdym ze sprawdzanych inputów:
    <span asp-validation-for="Name" class="text-danger"></span>  - przykład dla Name
    Patrz Category/Create.cshtml
    */

    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Display Order for category must be grater then 0")]
        public int DisplayOrder { get; set; }

    }
}
