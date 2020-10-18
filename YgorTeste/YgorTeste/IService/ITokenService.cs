using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YgorTeste.Models;

namespace YgorTeste.IService
{
    public interface ITokenService
    {
        UserToken BuildToken(UserInfoToken userInfo);
    }
}
