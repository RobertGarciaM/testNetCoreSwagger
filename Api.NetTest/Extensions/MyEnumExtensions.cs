using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Api.NetTest.Extensions
{
    public static class MyEnumExtensions
    {
        /// <summary>
        /// Este metodo permite obtener la descripción de un enumerador de la aplicación
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToDescriptionString(this Enum val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
