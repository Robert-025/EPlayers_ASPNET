using System;
using System.Collections.Generic;
using System.IO;
using EPlayers_ASPNET.Interfaces;

namespace EPlayers_ASPNET.Models
{
    public class Jogador : EPlayersBase , IJogador
    {
        public int IdJogador { get; set; }
        public string Nome { get; set; }
        public int IdEquipe { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        private const string PATH = "Database/Jogador.csv";

        public Jogador()
        {
            CreateFolderAndFile(PATH);
        }

        public string Prepare(Jogador j)
        {
            return $"{j.IdJogador};{j.Nome};{j.Email};{j.Senha}";
        }

        public void Create(Jogador j)
        {
            string[] linhas = { Prepare(j) };
            File.AppendAllLines(PATH, linhas);
        }

        public List<Jogador> ReadAll()
        {
            List<Jogador> jogadores = new List<Jogador>();
            string[] linhas = File.ReadAllLines(PATH);

            foreach (var item in linhas)
            {
                string[] linha = item.Split(";");

                Jogador j = new Jogador();

                j.IdJogador = Int32.Parse( linha[0] );
                j.Nome = linha[1];
                j.Email = linha[2];
                j.Senha = linha[3];

                jogadores.Add(j);
            }

            return jogadores;
        }

        public void Update(Jogador j)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);

            linhas.RemoveAll(x => x.Split(";")[0] == j.IdJogador.ToString());

            linhas.Add( Prepare(j) );

            RewriteCSV(PATH, linhas);
        }

        public void Delete(int id)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);

            linhas.RemoveAll(x => x.Split(";")[0] == id.ToString());

            RewriteCSV(PATH, linhas);
        }
    }
}