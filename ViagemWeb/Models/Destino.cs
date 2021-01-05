using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ViagemWeb.Models
{
    public class Destino
    {
        public int DestinoId { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Pais { get; set; }
        [Required]
        public string  Cidade { get; set; }
        [Required]
        public string Foto { get; set; }
    }
}