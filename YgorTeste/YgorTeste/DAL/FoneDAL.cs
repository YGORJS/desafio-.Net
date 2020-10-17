using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YgorTeste.Context;
using YgorTeste.Models;

namespace YgorTeste.DAL
{
    public class FoneDAL : IFoneDAL
    {
        private readonly ApiContext _context;

        public FoneDAL(ApiContext context)
        {
            _context = context;
        }

        public List<Fone> ObterFonesUsuario(int UsuarioId)
        {

            List<Fone> Fones =  _context.Fone.Where(a => a.usuarioid == UsuarioId).ToList();

            return Fones;
        }


    }
}
