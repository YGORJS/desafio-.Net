using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace YgorTeste.Models
{
    public class Usuario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Missing field")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Missing field")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Missing field")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Invalid fields")]
        public string email { get; set; }

        [Required(ErrorMessage = "Missing field")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Necessário ao menos uma letra, um número, um caractere especial, uma letra maiuscula e no mínimo 8 caracteres")]
        public string password { get; set; }

        [Required(ErrorMessage = "Missing field")]
        public List<phones> phones { get; set; }

        public DateTime createdAt { get; set; }

        public DateTime last_login  { get; set; }



    }
}
