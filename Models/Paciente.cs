using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace CapsuleCorp.Models
{
    public class Paciente
    {
        // PROPIEDADES.

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int pacienteID { get; set; }

        [Required(ErrorMessage = "Por favor, ingrese un DNI")]
        [Range(10000000, 99999999, ErrorMessage = "Por favor, ingrese valores entre 10000000 y 99999999")]
        public int DNI { get; set; }

        [Required(ErrorMessage = "Por favor, ingrese un nombre")]
        [MinLength(2), MaxLength(20)]
        [DataType(DataType.Text)]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Por favor, ingrese un apellido")]
        [MinLength(2), MaxLength(20)]
        [DataType(DataType.Text)]
        [Display(Name = "Apellido")]
        public string apellido { get; set; }

        [Required(ErrorMessage = "Por favor, ingrese un e-mail")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "El e-mail ingresado es incorrecto")]
        [Display(Name = "E-mail")]
        public string mail { get; set; }

        [Required(ErrorMessage = "Por favor, ingrese una contraseña")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "La contraseña debe contener al menos 8 caracteres, 1 mayúscula, 1 minúscula y 1 número")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string contrasenia { get; set; }

        public List<Turno> historial { get; set; }
    }
}