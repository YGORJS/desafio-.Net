using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace YgorTeste.Models
{
    public class LoginUsuario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }


        [Required(ErrorMessage = "Missing field")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Invalid fields")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Missing field")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
