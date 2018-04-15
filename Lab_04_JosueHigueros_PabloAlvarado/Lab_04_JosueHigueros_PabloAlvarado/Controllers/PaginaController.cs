using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Lab_04_JosueHigueros_PabloAlvarado.Models.Classes
{
    public class PaginaController : Controller
    {
        // GET: Pagina
        public ActionResult Index(Pagina ValorBuscado)
        {
            List<Pagina> Lista = new List<Pagina>();
            List<string> Nombres = new List<string>();
            
            if(ValorBuscado.coleccionadas == null)
            {
                foreach (var item in DataBase.Instance.Diccionario1)
                {
                    Lista.Add(item.Value);
                    Nombres.Add(item.Key);
                }
                return View(Lista);
            }
            else
            {
                Lista.Add(ValorBuscado);
                return View(Lista);
            }
           
          
        }


        // GET: Pagina
        public ActionResult IndexEstampa(List<Estampa> Busqueda)
        {
            if(Busqueda == null)
            {
                List<Estampa> ListadeEstampas = new List<Estampa>();
                foreach (var item in DataBase.Instance.Diccionario2)
                {
                    ListadeEstampas.Add(item.Value);
                }
                return View(ListadeEstampas);
            }
            else
            {
                return View(Busqueda);
            }
            
        }
        

       // GET: Pagina
       public ActionResult Modificar(string Nombre, int Numero, bool Obtenida)
        {
            Estampa Auxiliar = new Estampa();
            Auxiliar.Nombre = Nombre;
            Auxiliar.Numero = Numero;
            Auxiliar.Obtenida = Obtenida;
            //Hola

            foreach (var item in DataBase.Instance.Diccionario2)
            {
                if(Auxiliar.Nombre == item.Value.Nombre)
                {
                    if(Auxiliar.Numero == item.Value.Numero)
                    {
                        item.Value.Obtenida = Obtenida;
                    }
                }
            }
            return View();
           
        }

        // GET: Pagina/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pagina/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pagina/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pagina/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pagina/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pagina/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pagina/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Busqueda(string Tipo, string Search)
        {

            Pagina ValorBuscado = new Pagina();
            if (Tipo == null && Search == null)
            {
                return View("Busqueda");
            }
            foreach (var item in DataBase.Instance.Diccionario1)
            {
                if (item.Key == Search)
                {
                    ValorBuscado.cambios = item.Value.cambios;
                    ValorBuscado.coleccionadas = item.Value.coleccionadas;
                    ValorBuscado.faltantes = item.Value.faltantes;
                    ViewBag.Message = item.Key;
                }

            }

            if(ValorBuscado.cambios != null && ValorBuscado.coleccionadas != null && ValorBuscado.faltantes != null)
            {
                List<Pagina> Lista = new List<Pagina>();
                Lista.Add(ValorBuscado);
                return View("Index", Lista);
            }
            else
            {
                TempData["msg"] = "<script> alert('Dato No Encontrado');</script>";
                return View("Busqueda");
            }
            
        }

        public ActionResult BusquedaII(FormCollection collection)
        {

            Estampa ValorBuscado = new Estampa();
            if (collection["Pais"] == null && collection["Numero"] == null)
            {
                return View("BusquedaII");
            }
            foreach (var item in DataBase.Instance.Diccionario2)
            {
                string Auxiliar = (collection["Pais"] + "_" + collection["Numero"]);
                Auxiliar = " " + Auxiliar;
                if (collection["Pais"] == "Especial")
                {
                    if (item.Key == collection["Pais"] + collection["Numero"])
                    {
                        ValorBuscado.Nombre = collection["Pais"];
                        if (item.Value.Numero == int.Parse(collection["Numero"]))
                        {
                            ValorBuscado.Numero = int.Parse(collection["Numero"]);
                            ValorBuscado.Obtenida = item.Value.Obtenida;
                        }
                    }
                }
                else if (item.Key == Auxiliar)
                {
                    ValorBuscado.Nombre = collection["Pais"];
                    if (item.Value.Numero == int.Parse(collection["Numero"]))
                    {
                        ValorBuscado.Numero = int.Parse(collection["Numero"]);
                        ValorBuscado.Obtenida = item.Value.Obtenida;
                    }
                }

            }

            if(ValorBuscado.Nombre != null && ValorBuscado.Numero == int.Parse(collection["Numero"]))
            {
                List<Estampa> Lista = new List<Estampa>();
                Lista.Add(ValorBuscado);
                return View("IndexEstampa", Lista);
            }
            else
            {
                TempData["msg1"] = "<script> alert('Dato No Encontrado');</script>";
                return View("BusquedaII");
            }

        }

        public ActionResult UploadFileEstampa()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Aca se hace el Ingreso por medio de Archivo de Texto, ya que el Boton de Result esta Linkeado.
        public ActionResult UploadFileEstampa(HttpPostedFileBase file)
        {

            if (Path.GetExtension(file.FileName) != ".json")
            {
                //Aca se debe de Agregar una Vista de Error, o de Datos No Cargados
                return RedirectToAction("Error", "Shared");
            }

            Stream Direccion = file.InputStream;
            //Se lee el Archivo que se subio, por medio del Lector

            StreamReader Lector = new StreamReader(Direccion);
            string Temp = Lector.ReadToEnd();
            //El Archivo se lee en una lista para luego ingresarlo
            Dictionary<string, Estampa> Diccionario = new Dictionary<string, Estampa>();

            Temp = Temp.TrimStart('{');
            var Temporal = Temp.Split(',');
            List<Estampa> ListadeEstampas = new List<Estampa>();
           
            foreach (var item in Temporal)
            {
                if(item.Contains("Especial"))
                {
                    Estampa Auxiliar1 = new Estampa();
                    Auxiliar1.Nombre = "Especial";
                    if(item.Contains("true"))
                    {
                        Auxiliar1.Obtenida = true;
                    }
                    else
                    {
                        Auxiliar1.Obtenida = false;
                    }
                    var temporal = item.Remove(1, 8);
                    var numero = temporal.Split(':');
                    numero[0] = numero[0].Replace('{', ' ');
                    numero[0] = numero[0].Replace('"', ' ');
                    Auxiliar1.Numero = int.Parse(numero[0]);
                    Diccionario.Add(Auxiliar1.Nombre+Auxiliar1.Numero, Auxiliar1);

                }
                else
                {

                    Estampa Auxiliar2 = new Estampa();
                    if (item.Contains("true"))
                    {
                        Auxiliar2.Obtenida = true;
                    }
                    else
                    {
                        Auxiliar2.Obtenida = false;
                    }
                    var ArregloTemporal = item.Split(':');
                    var AuxiliarNombre = ArregloTemporal[0];
                    var ArregloTemporalNombre = AuxiliarNombre.Split('_');
                    ArregloTemporalNombre[0] = ArregloTemporalNombre[0].Replace('"', ' ');
                    ArregloTemporalNombre[1] = ArregloTemporalNombre[1].Replace('"', ' ');
                    Auxiliar2.Nombre = ArregloTemporalNombre[0];
                    Auxiliar2.Numero = int.Parse(ArregloTemporalNombre[1]);
                    Diccionario.Add(Auxiliar2.Nombre+"_"+Auxiliar2.Numero, Auxiliar2);
                }

            }

            DataBase.Instance.Diccionario2 = Diccionario;
            return RedirectToAction("IndexEstampa");

        }


        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Aca se hace el Ingreso por medio de Archivo de Texto, ya que el Boton de Result esta Linkeado.
        public ActionResult UploadFile(HttpPostedFileBase file)
        {

            if (Path.GetExtension(file.FileName) != ".json")
            {
                //Aca se debe de Agregar una Vista de Error, o de Datos No Cargados
                return RedirectToAction("Error", "Shared");
            }

            Stream Direccion = file.InputStream;
            //Se lee el Archivo que se subio, por medio del Lector

            StreamReader Lector = new StreamReader(Direccion, System.Text.Encoding.Default);
            string Temp = Lector.ReadToEnd();
            //El Archivo se lee en una lista para luego ingresarlo
            Dictionary<string, Pagina> Diccionario = new Dictionary<string, Pagina>();

            Diccionario= JsonConvert.DeserializeObject<Dictionary<string,Pagina>>(Temp);
            DataBase.Instance.Diccionario1 = Diccionario;

            return RedirectToAction("Busqueda");

        }
    }
}

