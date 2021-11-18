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

        [Required]
        public string mail { get; set; }

        [Required]
        public string contrasenia { get; set; }
    }
}