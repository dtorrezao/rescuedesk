using Newtonsoft.Json;
using RescueDesk.Models;
using RescueDesk.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RescueDesk.Controllers
{
    [Authorize(Roles = "Administrador, Funcionário")] // Todas as acções deste controlador estão disponiveis para os tipos de utlizador
    public class LocalidadesController : Controller
    {
        static IEnumerable<Localidade> localidades;

        // GET: Localidades
        public ActionResult Index()
        {
            AddressService addressService = new AddressService();

            return View(addressService.ObterLocalidades());
        }

        // GET: Localidades/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Localidades/Create
        public ActionResult Create()
        {
            Localidade localidade = new Localidade();

            return View(localidade);
        }

        // POST: Localidades/Create
        [HttpPost]
        public ActionResult Create(Localidade localidade)
        {
            AddressService addressService = new AddressService();
            if (addressService.CreateLocalidade(localidade))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(localidade);
            }
        }

        // GET: Localidades/Edit/5
        public ActionResult Edit(string id)
        {
            AddressService addressService = new AddressService();

            return View(addressService.ObterLocalidade(id));
        }

        // POST: Localidades/Edit/5
        [HttpPost]
        public ActionResult Edit(Localidade localidade, string oldID)
        {
            AddressService addressService = new AddressService();
            if (addressService.UpdateLocalidade(localidade, oldID))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(localidade);
            }
        }

        // GET: Localidades/Delete/5
        public ActionResult Delete(string id)
        {
            AddressService addressService = new AddressService();

            return View(addressService.ObterLocalidade(id));
        }

        // POST: Localidades/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string codpostal)
        {
            AddressService addressService = new AddressService();
            if (addressService.DeleteLocalidade(codpostal))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Delete", new { id = codpostal });
            }
        }

        // GET: Localidades/ImportFicheiro
        public ActionResult ImportFicheiro()
        {
            return View();
        }
        // POST: Localidades/ImportFicheiro
        [HttpPost]
        public ActionResult ImportFicheiro(HttpPostedFileBase file)
        {
            if (file != null)
            {
                List<Localidade> localidades = new List<Localidade>();
                if (file.ContentLength > 0)
                {
                    //// Gravar o ficheiro Localmente
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/"), fileName);
                    file.SaveAs(path);

                    //// Percorrer ficheiro para parse
                    //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-read-a-text-file-one-line-at-a-time
                    int counter = 0;
                    string line;

                    // Read the file and display it line by line.  
                    System.IO.StreamReader importedFile = new System.IO.StreamReader(path, System.Text.Encoding.UTF7);
                    while ((line = importedFile.ReadLine()) != null)
                    {
                        localidades.Add(Localidade.Parse(line));
                        counter++;
                    }

                    importedFile.Close();

                    // Gravar Localidades
                    AddressService addressService = new AddressService();

                    List<Localidade> localidadesExistentes = addressService.ObterLocalidades();
                    List<Localidade> localidadesAActualizar = new List<Localidade>();
                    List<Localidade> localidadesAInserir = new List<Localidade>();

                    foreach (Localidade localidade in localidades)
                    {
                        if (localidadesExistentes.Exists(x => x.codpostal == localidade.codpostal))
                        {
                            localidadesAActualizar.Add(localidade);
                        }
                        else
                        {
                            localidadesAInserir.Add(localidade);

                        }
                    }

                    addressService.UpdateLocalidades(localidadesAActualizar);
                    addressService.CreateLocalidades(localidadesAInserir);
                }
            }

            return View();
        }

        public string ObterLocalidades()
        {
            AddressService addressService = new AddressService();
            localidades = addressService.ObterLocalidades();

            //// Faz Pesquisa
            string searchParam = Request.Params["search[value]"].ToLower();
            if (!string.IsNullOrEmpty(searchParam))
            {
                localidades = localidades.Where(x => x.nomeLocalidade.ToLower().Contains(searchParam) || x.codpostal.ToLower().Contains(searchParam));
            }

            //// Faz Paginação
            List<Localidade> reducedlocalidades = localidades.Skip(int.Parse(Request.Params["start"])).Take(int.Parse(Request.Params["length"])).ToList();
            var chk = new
            {
                draw = Request.Params["draw"],
                //Data representa um array com as colunas (Ncolunas = quantidade de items devolvidos
                data = reducedlocalidades.Select(x =>
                    new string[] {
                        x.codpostal.ToString(),
                        x.nomeLocalidade.ToString(),
                    }),
                recordsTotal = localidades.Count(),
                recordsFiltered = localidades.Count()
            };
            return JsonConvert.SerializeObject(chk);
        }

    }
}
