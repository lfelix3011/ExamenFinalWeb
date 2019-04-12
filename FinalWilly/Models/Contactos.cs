namespace FinalWilly.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Contactos
    {
        public int Id { get; set; }

        public int? IdUsuario { get; set; }

        [Required(ErrorMessage = "El campo Telefono de Contacto no puede estar vacio")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Telefono de Contacto no puede estar vacio")]
        [StringLength(15)]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El campo Correo de Contacto no puede estar vacio")]
        [StringLength(50)]
        public string Correo { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
