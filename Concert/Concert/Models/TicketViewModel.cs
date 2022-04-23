using Concert.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Concert.Models
{
    public class TicketViewModel
    {


        public int Id { get; set; }


        public bool WasUsed { get; set; }
/*
        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]

*/
        public string? Document { get; set; }

    /*
        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]

        */
        public string? Name { get; set; }




        //public IEnumerable<SelectListItem>? EntranceId { get; set; }
        public Entrance? EntranceId { get; set; }

        public DateTime? Date { get; set; }
    }
}
