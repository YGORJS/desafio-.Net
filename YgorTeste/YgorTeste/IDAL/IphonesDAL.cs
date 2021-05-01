using System.Collections.Generic;
using YgorTeste.Models;

namespace YgorTeste.DAL
{
    public interface IphonesDAL
    {
        List<phones> ObterFonesUsuario(int UsuarioId);
    }
}