using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ObuvashkaWebAPI.Modules
{
    public class AuthOptions
    {
        public const string ISSUER = "ObuvashkaWebAPI";
        public const string AUDIENCE = "ObuvashkaClient";
        const string KEY = "F3v7sZ=34Xw]jhMmj*_n=J^G%HTJPW?c,fA#ZJA@v,sZ9N_J@qo_Pf4?fcxJ8sW})q57>_VEh?U^GsR0pUr7-n]c7yWrWj78D:6=";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
