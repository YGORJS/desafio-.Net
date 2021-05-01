using System.Collections.Generic;
using YgorTeste.Models;

namespace YgorTeste.DAL
{
    public interface IUsuarioDAL
    {
        List<Usuario> EmailExiste(string email);

        bool AtualizarUsuario(Usuario usuario);

        bool UsuarioExists(int id);

        Usuario ObterUsuario(string email, string password);

        bool CriarUsuario(Usuario usuario, List<phones> fone);
    }
}