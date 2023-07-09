using Domain.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using Persistance.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repo.Repositories
{
    public class MatchRequestRepository : IMatchRequestRepository
    {
        private readonly ApplicationDbContext _context;
        public MatchRequestRepository(ApplicationDbContext context)
        {
            _context= context;
        }
        public async Task<List<MatchRequest>> GetPlannedMatchAsync(int clubBranchId)
        {
            var allMatchRequest = await _context.MatchRequests.ToListAsync();//pobranie wszystkich zapytan o mecz
            List<MatchRequest> allMatchRequestByBranchClub = new List<MatchRequest>();//utworzenie listy, którą będziemy zwracać
            foreach (var item in allMatchRequest)
            {
                if (item.ReceiverId == clubBranchId && item.SenderId == clubBranchId && item.IsAccepted == true) 
                {
                    allMatchRequestByBranchClub.Add(item);//Dodawanie do listy tylko zapytan o mecz dla danego odzdziału w klubie i tych ktore sa zaakeptowane
                }
            }
            return allMatchRequestByBranchClub;
        }
    }
}
