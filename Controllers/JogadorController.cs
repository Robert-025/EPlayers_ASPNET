using System;
using EPlayers_ASPNET.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EPlayers_ASPNET.Controllers
{
    [Route("Jogador")]
    public class JogadorController : Controller
    {
        Jogador jogadorModel    = new Jogador();
        Equipe equipeModel      = new Equipe();


        [Route("Listar")]
        public IActionResult Index()
        {
            ViewBag.Equipes   = equipeModel.ReadAll();
            ViewBag.Jogadores = jogadorModel.ReadAll();

            return View();
        }
        

        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            Jogador novoJogador     = new Jogador();

            novoJogador.IdJogador   = Int32.Parse( form["IdJogador"] );
            novoJogador.Nome        = form["Nome"];
            novoJogador.Email       = form["Email"];
            novoJogador.Senha       = form["Senha"];
            
            jogadorModel.Create(novoJogador);            
            ViewBag.Jogadores = jogadorModel.ReadAll();

            return LocalRedirect("~/Jogador");
        }

        [Route("{id}")]
        public IActionResult Excluir(int id)
        {
            equipeModel.Delete(id);

            ViewBag.Jogadores = jogadorModel.ReadAll();

            return LocalRedirect("~/Jogador/Listar");
        }
    }
}