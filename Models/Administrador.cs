using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace CapsuleCorp.Models
{
    public class Administrador
    {
        // PROPIEDADES.

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

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
    }
}