using System.Collections.Generic;
using System.IO;
using EPlayers_AspNetCore.Interfaces;

namespace EPlayers_ASPNET.Models
{
    public class Equipe : EPlayersBase , IEquipe
    {
        public int IdEquipe { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }

        private const string PATH = "Database/Equipe.csv";

        public Equipe()
        {
            CreateFolderAndFile(PATH);
        }

        public string Prepare(Equipe e)
        {
            return $"{e.IdEquipe};{e.Nome};{e.Imagem}";
        }

        public void Create(Equipe e)
        {
            string[] linhas = { Prepare(e) };
            File.AppendAllLines(PATH, linhas);
        }

        public void Delete(int id)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);

            //Removemos a linha que tenha o código a ser alterado

            linhas.RemoveAll(x => x.Split(";")[0] == id.ToString());

            //Reescreve o CSV com as linhas alteradas
            RewriteCSV(PATH, linhas);
        }

        public List<Equipe> ReadAll()
        {
            List<Equipe> equipes = new List<Equipe>();
            //Ler todas as linhas do CSV
            string[] linhas = File.ReadAllLines(PATH);

            //Percorrer as linhas e adicionar na lista de equipers cada objeto

            foreach (var item in linhas)
            {
                //Dividimos os atributos pelo ; e transformamos eles em array
                string[] linha = item.Split(";");

                //Cria o objeto equipe para ser alimentado
                Equipe e = new Equipe();

                //Alimentamos a equipe pelos atributos do array
                e.IdEquipe  = int.Parse( linha[0] );
                e.Nome      = linha[1];
                e.Imagem    = linha[2];

                //Adicionamos a equipe criada na lista de equipes criada acima
                equipes.Add(e); 
            }

            return equipes;
        }

        public void Update(Equipe e)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);

            //Removemos a linha que tenha o código a ser alterado

            linhas.RemoveAll(x => x.Split(";")[0] == e.IdEquipe.ToString());

            //Adiciona a linha alterada no final do arquivo com o mesmo código

            linhas.Add( Prepare(e) );

            //Reescreve o CSV com as linhas alteradas
            RewriteCSV(PATH, linhas);
        }
    }
}
