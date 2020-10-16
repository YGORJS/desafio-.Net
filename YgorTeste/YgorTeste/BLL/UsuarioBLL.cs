using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YgorTeste.Context;
using Microsoft.EntityFrameworkCore;
using YgorTeste.DAL;
using YgorTeste.Context;


namespace YgorTeste.BLL
{
    public class UsuarioBLL
    {

        public bool EmailExiste(string email, ApiContext context)
        {
            UsuarioDAL usuDAL = new UsuarioDAL(context);

            var existe = usuDAL.EmailExiste(email);

            if (existe.Count() > 0)
                return true;
            else
                return false;
            
        }
    }

}
