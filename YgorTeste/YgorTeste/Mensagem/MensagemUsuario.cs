﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YgorTeste.Models;

namespace YgorTeste.Mensagem
{
    public class MensagemUsuario
    {

        public string Msg { get; set; }
        public string Codigo { get; set; }
        public Usuario usuario { get; set; }
    }
}