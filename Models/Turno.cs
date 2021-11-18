using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace CapsuleCorp.Models
{
    public class Turno
    {
        // PROPIEDADES.

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int turnoID { get; set; }

        [Required(ErrorMessage = "Por favor, ingrese una fecha")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime fecha { get; set; }

        [Required(ErrorMessage = "Por favor, ingrese una especialidad")]
        [Display(Name = "Especialidad")]
        [EnumDataType(typeof(Especialidad))]
        public Especialidad especialidad { get; set; }

        [Display(Name = "Paciente")]
        public int pacienteID { get; set; }

        [Display(Name = "Paciente")]
        public Paciente paciente { get; set; }
    }
}