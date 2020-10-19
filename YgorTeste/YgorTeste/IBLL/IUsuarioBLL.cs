using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YgorTeste.Models;

namespace YgorTeste.IBLL
{
    public interface IUsuarioBLL
    {

               
        bool EmailExiste(string email);

        Usuario ObterUsuario(string email, string password);

        bool AtualizarUsuario(Usuario usuario);

        bool CadastrarUsuario(Usuario usuario, List<Fone> fone);

    }
}
