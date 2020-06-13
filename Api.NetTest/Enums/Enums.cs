using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Api.NetTest.Enums
{
    public class Enums
    {
        public enum MensajesApplicacion
        {
            [Description("Estamos pasando por una nube turbulenta, sosténgase  del asiento e intente nuevamente en un momento.")]
            ErrorApplication = 1,
        }
    }
}
