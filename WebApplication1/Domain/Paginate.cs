using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Atcom.Domain
{
    public class Paginate
    {
        public Paginate() { 
            Page = 1;
            Limit = 10;
        }

        [Required(ErrorMessage = "El campo 'Page' es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El valor mínimo para 'Page' es 1.")]
        [DefaultValue(1)]
        public int Page { get; set; }

        [Required(ErrorMessage = "El campo 'Limit' es obligatorio.")]
        [Range(5, int.MaxValue, ErrorMessage = "El valor mínimo para 'Limit' es 5.")]
        [DefaultValue(10)]
        public int Limit { get; set; }
    }
}
