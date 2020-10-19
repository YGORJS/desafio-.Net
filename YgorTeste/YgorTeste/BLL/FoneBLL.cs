using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YgorTeste.Context;
using YgorTeste.DAL;
using YgorTeste.IBLL;
using YgorTeste.Models;

namespace YgorTeste.BLL
{
    public class FoneBLL : IFoneBLL
    {
        private readonly IFoneDAL _foneDal;

        public FoneBLL( IFoneDAL foneDal)
        {
            _foneDal = foneDal;
        }

        public List<Fone> OterFonesUsuario( int UsuarioId)
        {

            List<Fone> Fones = _foneDal.ObterFonesUsuario(UsuarioId);

            return Fones;

        }
    }
}
