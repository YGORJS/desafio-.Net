﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YgorTeste.DAL;
using YgorTeste.Models;

namespace YgorTeste.IBLL
{
    public interface IFoneBLL
    {

        List<Fone> OterFonesUsuario(int UsuarioId);
        
    }
}
