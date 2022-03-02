using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Constants
{
    public class JwtExpireConstants
    {
        public const double EXPIRE_ACCESS_TOKEN= 15.0;  // minutes
        public const double EXPIRE_REFRESH_TOKEN = 7.0; // days
    }
}
