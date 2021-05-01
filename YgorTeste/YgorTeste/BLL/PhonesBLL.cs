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
    public class PhonesBLL : IphonesBLL
    {
        private readonly IphonesDAL _phonesDal;

        public PhonesBLL(IphonesDAL phonesDal)
        {
            _phonesDal = phonesDal;
        } 

        public List<phones> OterFonesUsuario( int UsuarioId)
        {

            List<phones> Fones = _phonesDal.ObterFonesUsuario(UsuarioId);

            return Fones;

        }
    }
}
