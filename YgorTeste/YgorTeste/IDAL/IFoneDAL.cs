using System.Collections.Generic;
using YgorTeste.Models;

namespace YgorTeste.DAL
{
    public interface IFoneDAL
    {
        List<Fone> ObterFonesUsuario(int UsuarioId);
    }
}