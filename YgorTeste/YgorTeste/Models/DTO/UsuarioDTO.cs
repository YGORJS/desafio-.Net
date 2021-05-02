using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YgorTeste.Models.DTO
{
    public class UsuarioDTO
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public List<phones> phones { get; set; }
        public string created_at { get; set; }
        public string last_login { get; set; }
    }
}
