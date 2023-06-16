using Microsoft.AspNetCore.Mvc;
using Projeto_gamer_Back.Infra;
using Projeto_gamer_Back.Models;

namespace Projeto_gamer_Back.Controllers
{
    [Route("[controller]")]
    public class EquipeController : Controller
    {
        private readonly ILogger<EquipeController> _logger;

        public EquipeController(ILogger<EquipeController> logger)
        {
            _logger = logger;
        }

        //INSTANCIA DO CONTEXT PARA ACESSAR O BANCO DE DADOS 
        Context c = new Context();


        [Route("Listar")] //  ex:   htt://localhost/Equipe/Listar

        public IActionResult Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            //VARIAVEL QUE ARMAZENA AS EQUIPES LISTADAS DO BANCO 
            ViewBag.Equipe = c.Equipe.ToList();

            //RETORNA A VIEW DE EQUIPE (TELA)
            return View();
        }

        [Route("Cadastar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            //INSTANCIA O OBJETO
            Equipe novaEquipe = new Equipe();

            //ATRIBUICAO DE VALORES RECEBIDOS DO FORMULARIO
            novaEquipe.Nome = form["Nome"].ToString();


            // novaEquipe.Imagem = form["Imagem"].ToString(); // VALOR COMO STRING


            // LOGICA DO UPLOAD DE IMAGEM 
            if (form.Files.Count > 0)
            {

                var file = form.Files[0];

                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                //GERAO CAMINHO DO ARQUIVO COMPLETO ATE O CAMINHO DO ARQUIVO (IMAGEM - ESTECAO DO ARQUIVO )
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                novaEquipe.Imagem = file.FileName;

            }

            else
            {
                novaEquipe.Imagem = "padrao.png";
            }


            //ADICIONA UM NOVO OBJETO NA TABELA DO BANCO DE DADOS (BD)
            c.Equipe.Add(novaEquipe);

            //SALVA AS ALTERACOES FEITAS NO BANCO DE DADOS (BD)
            c.SaveChanges();

            //RETORNA PARA O LOCAL CHAMANDO A ROTA DE LISTAR (METODO INDEX)
            return LocalRedirect("~/Equipe/Listar");

        }

        //LOGICA PARA EXCLUIR
        [Route("Excluir/{id}")]

        public IActionResult Excluir(int id)
        {
            Equipe equipeBusca = c.Equipe.First(e => e.IdEquipe == id);

            c.Remove(equipeBusca);

            c.SaveChanges();

            return LocalRedirect("~/Equipe/Listar");

        }

        [Route("Editar/{id}")]
        public IActionResult Editar(int id)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            
            Equipe equipe = c.Equipe.First(x => x.IdEquipe == id);

            ViewBag.Equipe = equipe;

            return View("Edit");
        }

        [Route("Atualizar")]
        public IActionResult Atualizar(IFormCollection form)
        {
            Equipe equipe = new Equipe();

            equipe.IdEquipe = int.Parse(form["IdEquipe"].ToString());

            equipe.Nome = form["Nome"].ToString();

            if (form.Files.Count > 0)
            {
                var file = form.Files[0];

                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var path = Path.Combine(folder, file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                equipe.Imagem = file.FileName;

            }

            else
            {
                equipe.Imagem = "padrao.png";
            }

            Equipe equipeBuscada = c.Equipe.First(e => e.IdEquipe == equipe.IdEquipe);

            equipeBuscada.Nome = equipe.Nome;
            equipeBuscada.Imagem = equipe.Imagem;

            c.Equipe.Update(equipeBuscada);

            c.SaveChanges();


            return LocalRedirect("~/Equipe/Listar");

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }


    }
}