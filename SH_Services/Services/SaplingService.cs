using SH_BusinessObjects.Common.Model.Sapling;
using SH_BusinessObjects.Entities;
using SH_Repositories.Repos.Interfaces;
using SH_Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_Services.Services
{
    public class SaplingService(ISaplingRepository saplingRepository) : ISaplingService
    {
        private readonly ISaplingRepository _saplingRepository = saplingRepository;

        public async Task<List<Sapling>> GetAllAsync()
        {
            return await _saplingRepository.GetAllAsync();
        }

        public async Task<Sapling?> GetByIdAsync(Guid id)
        {
            return await _saplingRepository.GetByIdAsync(id);
        }

        public async Task<Sapling> CreateAsync(SaplingModel sapling)
        {
            Sapling newSapling = new()
            {
                Id = new Guid(),
                Name = sapling.Name,
                Price = sapling.Price,
                StockQuantity = sapling.StockQuantity,
                CategoryId = sapling.CategoryId,
                ImageUrl = sapling.ImageUrl,
            };
            await _saplingRepository.AddAsync(newSapling);
            return newSapling;
        }

        public async Task UpdateAsync(Guid id, SaplingModel sapling)
        {
            var existingSapling = await _saplingRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Not found sapling to update.");
            existingSapling.Name = sapling.Name;
            existingSapling.Description = sapling.Description;
            existingSapling.Price = sapling.Price;
            existingSapling.StockQuantity = sapling.StockQuantity;
            existingSapling.CategoryId = sapling.CategoryId;
            existingSapling.ImageUrl = sapling.ImageUrl;

            if(!await _saplingRepository.CategoryExists(sapling.CategoryId))
                throw new KeyNotFoundException("Not found category.");

            await _saplingRepository.UpdateAsync(existingSapling);
        }

        public async Task DeleteAsync(Guid id)
        {
            _ = await _saplingRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Not found sapling to delete.");
            await _saplingRepository.DeleteAsync(id);
        }

        public async Task<List<Sapling>> GetByCategoryIdAsync(Guid categoryId)
        {
            if(!await _saplingRepository.CategoryExists(categoryId))
                throw new KeyNotFoundException("Not found category.");
            return await _saplingRepository.GetByCategoryIdAsync(categoryId);
        }
    }
}
