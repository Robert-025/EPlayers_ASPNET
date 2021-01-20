
using System.Collections.Generic;
using EPlayers_ASPNET.Models;

namespace EPlayers_AspNetCore.Interfaces
{
    public interface IEquipe
    {
         //CRUD

         void Create(Equipe e);
         List<Equipe> ReadAll();
         void Update(Equipe e);
         void Delete(int id);
    }
}