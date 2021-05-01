using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YgorTeste.Context;
using YgorTeste.Models;

namespace YgorTeste.DAL
{
    public class PhonesDAL : IphonesDAL
    {
        private readonly ApiContext _context;

        public PhonesDAL(ApiContext context)
        {
            _context = context;
        }

        public List<phones> ObterFonesUsuario(int UsuarioId)
        {

            List<phones> Fones =  _context.Fone.Where(a => a.usuarioid == UsuarioId).ToList();

            return Fones;
        }


    }
}
