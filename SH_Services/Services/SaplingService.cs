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
    public class SaplingService : ISaplingService
    {
        private readonly ISaplingRepository _saplingRepository;

        public SaplingService(ISaplingRepository saplingRepository)
        {
            _saplingRepository = saplingRepository;
        }

        public async Task<List<Sapling>> GetAllAsync()
        {
            return await _saplingRepository.GetAllAsync();
        }

        public async Task<Sapling?> GetByIdAsync(Guid id)
        {
            return await _saplingRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(AddSaplingModel sapling)
        {
            if (string.IsNullOrEmpty(sapling.Name))
                throw new ArgumentException("Tên cây không được để trống.");
            if (sapling.Price < 0)
                throw new ArgumentException("Giá không được âm.");

            await _saplingRepository.AddAsync(sapling);
        }

        public async Task UpdateAsync(Guid id, Sapling sapling)
        {
            var existingSapling = await _saplingRepository.GetByIdAsync(id);
            if (existingSapling == null)
                throw new KeyNotFoundException("Không tìm thấy cây để cập nhật.");

            existingSapling.Name = sapling.Name;
            existingSapling.Description = sapling.Description;
            existingSapling.Price = sapling.Price;
            existingSapling.StockQuantity = sapling.StockQuantity;
            existingSapling.CategoryId = sapling.CategoryId;
            existingSapling.ImageUrl = sapling.ImageUrl;

            await _saplingRepository.UpdateAsync(existingSapling);
        }

        public async Task DeleteAsync(Guid id)
        {
            var sapling = await _saplingRepository.GetByIdAsync(id);
            if (sapling == null)
                throw new KeyNotFoundException("Không tìm thấy cây để xóa.");

            await _saplingRepository.DeleteAsync(id);
        }
    }
}
