using ApplicationLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.Abstract
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(string userId, string username, int minute);
        string CreateRefreshToken();
    }
}
