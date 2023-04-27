using Domain.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Helper
{
    public static class GameClassess
    {
        public static List<GameClass> GetAll()
        {

            return new List<GameClass>
            {
                new GameClass() { Id = 1, Name = "B klasa"},
                new GameClass() { Id = 2, Name = "A klasa"}
            };
        }
    }
}
