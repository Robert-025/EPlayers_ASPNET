using System.Collections.Generic;
using System.IO;

namespace EPlayers_ASPNET.Models
{
    public abstract class EPlayersBase
    {
       public void CreateFolderAndFile(string path)
       {
           //Database/Equipe.csv
           string folder = path.Split("/")[0];

           if (!Directory.Exists(folder))
           {
               Directory.CreateDirectory(folder);
           }

           if (!File.Exists(path))
           {
               File.Create(path);
           }
       }

        public List<string> ReadAllLinesCSV(string path)
        {
            List<string> linhas = new List<string>();

            //Using -> Para abrir e fechar certo tipo de arquivo ou conexão
            //StreamReader -> Ler as informações do CVS

            using ( StreamReader file = new StreamReader(path) )
            {
                string linha;

                while ((linha = file.ReadLine())!= null)
                {
                    linhas.Add(linha);
                }
            }

            return linhas;
        }

        public void RewriteCSV(string path, List<string> linhas)
        {
            //StramWriter -> Escrita de arquivos

            using( StreamWriter ouput = new StreamWriter(path) )
            {
                foreach (var item in linhas)
                {
                    ouput.Write(item + "\n");
                }
            }
        }
    }
}