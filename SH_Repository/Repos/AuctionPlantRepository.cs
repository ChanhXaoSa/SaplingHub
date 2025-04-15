using SH_BusinessObjects.Entities;
using SH_DataAccessObjects.DAO.Interfaces;
using SH_Repositories.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_Repositories.Repos
{
    public class AuctionPlantRepository(IAuctionPlantDAO auctionPlantDAO) : IAuctionPlantRepository
    {
        private readonly IAuctionPlantDAO _auctionPlantDAO = auctionPlantDAO;
        public async Task<List<AuctionPlant>> GetAllAsync()
        {
            return await _auctionPlantDAO.GetAllAsync();
        }
        public async Task<AuctionPlant?> GetByIdAsync(Guid id)
        {
            return await _auctionPlantDAO.GetByIdAsync(id);
        }
        public async Task AddAsync(AuctionPlant auctionPlant)
        {
            await _auctionPlantDAO.AddAsync(auctionPlant);
        }
        public async Task UpdateAsync(AuctionPlant auctionPlant)
        {
            await _auctionPlantDAO.UpdateAsync(auctionPlant);
        }
        public async Task UpdateCurrentHighestBidAsync(Guid auctionPlantId, decimal currentHighestBid)
        {
            await _auctionPlantDAO.UpdateCurrentHighestBidAsync(auctionPlantId, currentHighestBid);
        }
        public async Task DeleteAsync(Guid id)
        {
            await _auctionPlantDAO.DeleteAsync(id);
        }
    }
}
