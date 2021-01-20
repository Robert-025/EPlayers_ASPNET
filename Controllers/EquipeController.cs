using System;
using System.IO;
using EPlayers_ASPNET.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EPlayers_AspNetCore.Controllers
{
    [Route("Equipe")]
    public class EquipeController : Controller
    {
        //Instaciamos um objeto de Equipe
        Equipe equipeModel = new Equipe();
        
        [Route("Listar")]
        public IActionResult Index()
        {
            //Listando todas as equipes e enviando para a View, através da ViewBag
            ViewBag.Equipes = equipeModel.ReadAll();

            return View();
        }

        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        { //IFormCollection: usado para armazenar as informações vem da web

            //Criamos uma nova equipe
            Equipe novaEquipe   = new Equipe();

            //Armazenamos os dados enviados pelo usuário pelo form e salvamos no objeto novaEquipe
            novaEquipe.IdEquipe = Int32.Parse( form["IdEquipe"] );
            novaEquipe.Nome     = form["Nome"];

            //Início do Upload ------------------------------------------------------------------------

            //Veirificamos se o usuário anexou algum arquivo
            if ( form.Files.Count > 0 )
            {
                //Armazenamos o arquivo na variável file
                var file = form.Files[0];

                //Feito para definir/amazenar o caminho das imagens
                var folder = Path.Combine( Directory.GetCurrentDirectory(), "wwwroot/img/Equipes" );

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                                                // localhost:5001   +                   + Equipes + nome do arquivo(equipes.jpg, por exemplo) 
                var path = Path.Combine( Directory.GetCurrentDirectory(), "wwwroot/img;", folder, file.FileName );

                //FileStream: recebeu o caminho de manipulação do arquivo (neste exemplo é a criação) e digo o que fazer no arquivo (FileMode.Create)
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    //Salvamos o arquivo no caminho especifícado
                    file.CopyTo(stream);
                }

                //Atribuímos a imagem na imagem
                novaEquipe.Imagem = file.FileName;
            }
            else
            {
                novaEquipe.Imagem = "padrao.png";
            }

            //Fim do Upload ---------------------------------------------------------------------------

            //Salvamos a novaEquipe no CSV pelo método Create
            equipeModel.Create(novaEquipe);

            ViewBag.Equipes = equipeModel.ReadAll();

            //Redirecionando as informações para a mesma página
            return LocalRedirect("~/Equipe/Listar");
        }

        [Route("{id}")]
        public IActionResult Excluir(int id)
        {
            equipeModel.Delete(id);

            ViewBag.Equipes = equipeModel.ReadAll();

            return LocalRedirect("~/Equipe/Listar");
        }
    }
}