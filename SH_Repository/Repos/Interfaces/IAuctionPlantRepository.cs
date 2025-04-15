using SH_BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_Repositories.Repos.Interfaces
{
    public interface IAuctionPlantRepository
    {
        Task<List<AuctionPlant>> GetAllAsync();
        Task<AuctionPlant?> GetByIdAsync(Guid id);
        Task AddAsync(AuctionPlant auctionPlant);
        Task UpdateAsync(AuctionPlant auctionPlant);
        Task UpdateCurrentHighestBidAsync(Guid auctionPlantId, decimal currentHighestBid);
        Task DeleteAsync(Guid id);
    }
}
