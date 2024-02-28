using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TownBuilder.Helppers
{
    internal class DiasHelper
    {
        internal static string GetDiaSemanaTexto(int dia)
        {
            switch (dia)
            {
                case 1:
                    return "Lunes";
                case 2:
                    return "Martes";
                case 3:
                    return "Miercoles";
                case 4:
                    return "Jueves";
                case 5:
                    return "Viernes";
                case 6:
                    return "Sabado";
                case 7:
                    return "Domingo";

            }

            return "";
        }
    }
}
