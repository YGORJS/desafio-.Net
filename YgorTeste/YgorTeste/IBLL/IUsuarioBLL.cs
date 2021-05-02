using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YgorTeste.Models;
using YgorTeste.Models.DTO;

namespace YgorTeste.IBLL
{
    public interface IUsuarioBLL
    {

               
        bool EmailExiste(string email);

        Usuario ObterUsuario(string email, string password);
        Usuario ObterUsuario(int id);



        bool AtualizarUsuario(Usuario usuario);

        bool CadastrarUsuario(Usuario usuario, List<phones> fone);
        UsuarioDTO BuscarUsuario(Usuario usuario, UsuarioDTO usuarioFormatado);


    }
}
