using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Api.Helpers
{
    public class MySettingModel
    {
        static string netTestConecction;

        /// <summary>
        /// Este metodo asigna el valor del string de conexión de la aplicación.
        /// NO cambiar las propiedades de acceso a la propiedad netTestConecction,
        /// de cambiarlas queda la aplicación expuesta a errores
        /// </summary>
        /// <param name="Connection"></param>
        public static void SetNetTestConecction(string Connection)
        {
            if (netTestConecction == null) { netTestConecction = Connection; return; }
            throw new System.ArgumentException("No se puede cambiar el valor del string de conexión");
        }

        /// <summary>
        /// Obtiene el valor del string de conexión
        /// </summary>
        /// <returns>String de Conexión</returns>
        public static string GetNetTestConecction() { return netTestConecction; }
    }
}
