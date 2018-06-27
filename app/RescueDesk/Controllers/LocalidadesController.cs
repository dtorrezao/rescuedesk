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
        public static readonly string[] ChavesMestre = new string[] { "keyLocalidades" };

        // GET: Localidades
        public ActionResult Index()
        {
            AddressService addressService = new AddressService();

            return View();
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

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public string ObterLocalidades()
        {
            AddressService addressService = new AddressService();
            List<Localidade> localidades = new List<Localidade>();

            ////https://www.devmedia.com.br/cache-no-asp-net/6704
            System.Web.Caching.Cache dadosCache = HttpRuntime.Cache;
            if (dadosCache.Get("Localidades") == null)
            {
                dadosCache.Insert(ChavesMestre[0], DateTime.Now);
                localidades.Clear();
                localidades = addressService.ObterLocalidades();
                System.Web.Caching.CacheDependency cd = new System.Web.Caching.CacheDependency(null, ChavesMestre);
                dadosCache.Insert("Localidades", localidades, cd);
            }
            else
            {
                localidades.Clear();
                localidades = dadosCache.Get("Localidades") as List<Localidade>;
            }

            //// Faz Pesquisa
            string searchParam = Request.Params["search[value]"].ToLower();
            if (!string.IsNullOrEmpty(searchParam))
            {
                localidades = localidades.Where(x => x.nomeLocalidade.ToLower().Contains(searchParam) || x.codpostal.ToLower().Contains(searchParam)).ToList();
            }

            //// Faz Paginação

            if (Request.Params["order[0][dir]"] == "asc")
            {
                switch (Request.Params["order[0][column]"])
                {
                    case "0":
                        localidades = localidades.OrderBy(x => x.codpostal).ToList();
                        break;

                    case "1":
                        localidades = localidades.OrderBy(x => x.nomeLocalidade).ToList();
                        break;

                    default:
                        break;
                }
            }
            else
            {
                switch (Request.Params["order[0][column]"])
                {
                    case "0":
                        localidades = localidades.OrderByDescending(x => x.codpostal).ToList();
                        break;
                    case "1":
                        localidades = localidades.OrderByDescending(x => x.nomeLocalidade).ToList();
                        break;

                    default:
                        break;
                }
            }
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

        [AllowAnonymous]
        public string ObterLocalidadesFiltradas()
        {
            AddressService addressService = new AddressService();

            string query = Request.Form["data[q]"].ToLower();

            List<Localidade> localidades = new List<Localidade>();

            ////https://www.devmedia.com.br/cache-no-asp-net/6704
            System.Web.Caching.Cache dadosCache = HttpRuntime.Cache;
            if (dadosCache.Get("Localidades") == null)
            {
                dadosCache.Insert(ChavesMestre[0], DateTime.Now);
                localidades.Clear();
                localidades = addressService.ObterLocalidades();
                System.Web.Caching.CacheDependency cd = new System.Web.Caching.CacheDependency(null, ChavesMestre);
                dadosCache.Insert("Localidades", localidades, cd);
            }
            else
            {
                localidades.Clear();
                localidades = dadosCache.Get("Localidades") as List<Localidade>;
            }

            localidades = localidades.Where(x => x.codpostal.ToLower().Contains(query) || x.nomeLocalidade.ToLower().Contains(query)).Take(50).ToList();
            var list = localidades.Select(x => new { id = x.codpostal, text = string.Format("{0} - {1}", x.codpostal, x.nomeLocalidade) }).ToList();
            var chk = new
            {
                q = query,
                results = list
            };
            return JsonConvert.SerializeObject(chk);
        }
    }
}
