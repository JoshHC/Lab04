using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab_04_JosueHigueros_PabloAlvarado.Models.Classes
{
    public class Pagina
    {
        public List<int> faltantes { get; set; }
        public List<int> coleccionadas { get; set; }
        public List<int> cambios { get; set; }
    }
}