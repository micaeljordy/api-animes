using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animes.Application.Configurations
{
    public class JwtSettings
    {
        public string SecretKey { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public int Expires { get; set; }
    }
}