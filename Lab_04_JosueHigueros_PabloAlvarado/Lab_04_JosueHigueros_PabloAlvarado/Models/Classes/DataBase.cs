using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab_04_JosueHigueros_PabloAlvarado.Models.Classes
{
    public class DataBase
    {
        private static DataBase instance;
        public static DataBase Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataBase();
                return instance;
            }
        }

        public Dictionary<string, Pagina> Diccionario1;
        public Dictionary<string,Estampa> Diccionario2;

        public DataBase()
        {
            Diccionario1 = new Dictionary<string, Pagina>();
            Diccionario2 = new Dictionary<string, Estampa>();
        }
    }
}