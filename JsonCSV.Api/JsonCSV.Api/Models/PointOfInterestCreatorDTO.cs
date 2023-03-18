using System.ComponentModel.DataAnnotations;

namespace JsonCSV.Api.Models
{
    public class PointOfInterestCreatorDTO
    {
        [Required(ErrorMessage = "Debes definirle un nombre a tu ciudad, animal")]
        [MaxLength(50, ErrorMessage = "El nombre no debe sobrepasar los 50 caracteres, abusador")]
        public string name { get; set; } = string.Empty;

        [MaxLength(200, ErrorMessage = "¿Era una breve explicacion o una dedicatoria para tu ex?")]
        public string description { get; set; } = string.Empty;
    }
}
