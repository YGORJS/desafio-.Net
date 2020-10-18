using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace YgorTeste.Models
{
    public class Fone
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [StringLength(9, ErrorMessage = "Invalid fields", MinimumLength = 9)]
        public string numero { get; set; }

        [StringLength(2, ErrorMessage = "Invalid fields", MinimumLength = 2)]
        public string Codigoarea { get; set; }

        [StringLength(3, ErrorMessage = "Invalid fields", MinimumLength = 3)]
        public string CodigoPais { get; set; }

        public int usuarioid   { get; set; }



    }
}
