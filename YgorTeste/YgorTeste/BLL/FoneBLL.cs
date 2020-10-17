using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YgorTeste.Context;
using YgorTeste.DAL;
using YgorTeste.Models;

namespace YgorTeste.BLL
{
    public class FoneBLL
    {
        public List<Fone> OterFonesUsuario( int UsuarioId  , ApiContext context)
        {
            FoneDAL FoneDAL = new FoneDAL(context);

            List<Fone> Fones = FoneDAL.ObterFonesUsuario(UsuarioId);

            return Fones;

        }
    }
}
