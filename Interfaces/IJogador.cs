using System.Collections.Generic;
using EPlayers_ASPNET.Models;

namespace EPlayers_ASPNET.Interfaces
{
    public interface IJogador
    {
        //Incluindo os métodos de CRUD

        void Create(Jogador j);
        List<Jogador> ReadAll();
        void Update(Jogador j);
        void Delete(int id);
        
    }
}