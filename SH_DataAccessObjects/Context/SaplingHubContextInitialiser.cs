using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SH_BusinessObjects.Common.Interface;
using SH_BusinessObjects.Entities;
using SH_BusinessObjects.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_DataAccessObjects.Context
{
    public class SaplingHubContextInitialiser(ILogger<SaplingHubContextInitialiser> logger, SaplingHubContext context
        , UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager
        )
    {
        private readonly ILogger<SaplingHubContextInitialiser> _logger = logger;
        private readonly SaplingHubContext _context = context;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            await SeedRolesAsync();

            await SeedUsersAsync();

            SeedOtherData();

            await _context.SaveChangesAsync();
        }

        private void SeedOtherData()
        {
            if (!_context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new() { Id = Guid.Parse("a24f8fb5-c8c4-4e75-bf9f-d4217ab0ea4b"), Name = "Giống cây có múi", Description = "Giống cây có múi là nhóm cây ưa sáng, thường trồng ở vùng nhiệt đới và cận nhiệt đới. Những cây này nổi tiếng với trái cây có vỏ mềm, thịt mọng và hương vị đặc trưng. Trong số đó, có cam, quýt và bưởi là những giống cây phổ biến, được trồng rộng rãi vì giá trị dinh dưỡng cao và hương vị tuyệt vời. Được chọn lựa cẩn thận, giống cây có múi không chỉ mang lại nguồn thực phẩm phong phú mà còn góp phần làm đẹp và bổ sung cho hệ thống đồng canh tác." },
                    new() { Id = Guid.Parse("39d6a159-33a2-424e-8ec6-15d8d9e5012f"), Name = "Cây giống Chanh", Description = "Chanh ta thuộc loài cây bụi, thân cây có nhiều gai nhọn và hiếm khi mọc thẳng mà tỏa nhiều nhánh. Lá có hình bầu dục hơi nhọn ở hai đầu. Hoa chanh thường có màu trắng ngả sang màu vàng, có gân màu tím nhạt. Thời gian ra quả nhiều nhất của chanh ta là từ tháng 5 tới tháng 9, trái chín sau từ 5 tới 6 tháng từ lúc hoa nở." },
                    new() { Id = Guid.Parse("9739bce1-522c-4c3d-8278-146a7f6d3939"), Name = "Cây giống Dừa", Description = "Cây dừa, phổ biến ở vùng nhiệt đới, đóng vai trò quan trọng trong nền kinh tế và cuộc sống hàng ngày. Từ nước dừa ngọt, thịt dừa mềm ngon, đến dầu dừa giàu chất béo và lá dừa đa dạng ứng dụng, cây dừa mang lại không chỉ nguồn thực phẩm mà còn là biểu tượng của lối sống nhiệt đới, tươi mới và phồn thịnh." },
                    new() { Id = Guid.Parse("7f55211d-5bbc-43a8-9e00-8b911198f25f"), Name = "Cây giống Mít", Description = "Cây mít, một biểu tượng của vùng nhiệt đới, không chỉ là nguồn thực phẩm đa dạng mà còn đóng vai trò quan trọng trong sinh kế và văn hóa. Trái mít với hương thơm đặc trưng mang lại trải nghiệm ẩm thực độc đáo. Cây mít cũng là nguồn lá làm đồ chất lượng và đóng góp vào bảo vệ môi trường thông qua sự sáng tạo và bền vững trong chế biến sản phẩm mít. Hình ảnh cây mít cổ thụ tượng trưng cho sức sống và sự đa dạng của vùng nhiệt đới." },
                    new() { Id = Guid.Parse("54a3bd15-5f50-46da-a35b-cc063bb09ec0"), Name = "Cây giống Bơ", Description = "Bơ (Persea americana) là một cây có nguồn gốc từ vùng nhiệt đới, thuộc họ Lauraceae, và nổi tiếng với quả bơ độc đáo. Cây bơ thích ứng với khí hậu ấm, nhiệt đới và đất thoát nước tốt. Có nhiều loại bơ khác nhau như Hass, Fuerte, Reed, Gwen và Pinkerton, mỗi loại mang đặc điểm riêng. Quả bơ chứa nhiều dưỡng chất, vitamin E, K và khoáng chất, giúp cung cấp năng lượng và hỗ trợ sức khỏe. Ngoài thức ăn, dầu bơ được chiết xuất từ hạt còn được sử dụng trong nấu ăn và sản phẩm chăm sóc da. Với những ứng dụng đa dạng này, bơ không chỉ là một nguồn thực phẩm dinh dưỡng mà còn có tác động tích cực đối với sức khỏe và làm đẹp." },
                    new() { Id = Guid.Parse("06f5d19c-8a63-4539-9eff-b43388b0287b"), Name = "Cây giống Sầu riêng", Description = "Giống cây sầu riêng là một trong những loại cây trồng đặc trưng của vùng nhiệt đới, nổi tiếng với trái ngọt béo, hương thơm đặc trưng và chất lượng dinh dưỡng cao. Sầu riêng được biết đến với sự đa dạng giữa các giống như Ri 6, Monthong, Musanking, Black Thorn,... Việc trồng cây sầu riêng không chỉ mang lại nguồn thực phẩm tuyệt vời mà còn tạo nên một phần không gian xanh tươi, đặc sắc và phản ánh nền văn hóa ẩm thực độc đáo." },
                    new() { Id = Guid.Parse("b281b3c0-bbff-481a-b2cf-4e77043ff829"), Name = "Cây giống Mận", Description = "Cây mận, vẻ đẹp nhiệt đới được tạo nên từ những trái ngọt mềm, hương thơm dịu dàng và chất dinh dưỡng tuyệt vời. Với sự linh hoạt và tính đa dạng, giống cây mận không chỉ là nguồn thực phẩm quan trọng, mà còn là biểu tượng của vùng nhiệt đới, đem đến cho vườn trái cây không gian sống tươi mới và trải nghiệm ẩm thực độc đáo." },
                    new() { Id = Guid.Parse("bf85fde2-f54d-42a6-a1d0-85831351ce65"), Name = "Cây giống Xoài", Description = "Cây xoài, một loại cây ưa nhiệt đới, là nguồn thực phẩm quen thuộc với vị ngọt, mềm mại và hương thơm đặc trưng. Với độ phong phú và sự linh hoạt trong ứng dụng, giống cây xoài là lựa chọn lý tưởng cho nông dân và người tiêu dùng, đồng thời tạo nên một phần không gian xanh tươi và trải nghiệm ẩm thực độc đáo trong vườn trái cây." },
                    new() { Id = Guid.Parse("4fe8ae8d-82f7-4bb4-96a0-8d4758180d62"), Name = "Cây giống Ổi", Description = "Cây ổi, một biểu tượng của vùng nhiệt đới, là nguồn thực phẩm phổ biến với hương vị ngọt ngào và giá trị dinh dưỡng cao. Với khả năng thích ứng đa dạng và kháng bệnh tốt, giống cây ổi đóng vai trò quan trọng trong việc cung cấp nguồn thực phẩm lành mạnh và là điểm nhấn xanh mát trong vườn trái cây." },
                    new() { Id = Guid.Parse("53b9412f-a31e-4feb-b5a0-e4c539abe91b"), Name = "Giống cây đô thị", Description = "Trong môi trường đô thị và khu dân cư, việc trồng cây ăn trái như cây chanh, cây cóc, và cây ổi ngày càng phổ biến. Những loại cây này không chỉ mang lại quả ngon, giàu chất dinh dưỡng mà còn tạo điểm nhấn xanh mát cho không gian sống. Việc trồng cây ăn trái trong thành phố không chỉ thúc đẩy nguyên lý bền vững mà còn hỗ trợ việc cải thiện môi trường sống và chất lượng cuộc sống cho cộng đồng." },
                    new() { Id = Guid.Parse("dd89e54d-3e7e-4117-acfb-59d65bc7a9b9"), Name = "Cây giống khác", Description = "Các loại cây giống khác" },
                };
                _context.Categories.AddRange(categories);
            }
            if (!_context.Saplings.Any())
            {
                //var saplings = new List<Sapling>
                //{
                //    new() {
                //        Name = "Sapling 1",
                //        Description = "Description 1",
                //        Price = 100,
                //        ImageUrl = "https://via.placeholder.com/150"
                //    },
                //};
                //_context.Saplings.AddRange(saplings);
            }
        }

        private async Task SeedUsersAsync()
        {
            if (await _userManager.FindByEmailAsync("admin@gmail.com") == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                    _logger.LogInformation("The admin user has been seeded.");
                }
                else
                {
                    throw new Exception($"An error occurred while seeding the admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

            if (await _userManager.FindByEmailAsync("user@gmail.com") == null)
            {
                var normalUser = new ApplicationUser
                {
                    UserName = "user@gmail.com",
                    Email = "user@gmail.com",
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(normalUser, "User@123");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(normalUser, "User");
                    _logger.LogInformation("The normal user has been seeded.");
                }
                else
                {
                    throw new Exception($"An error occurred while seeding the normal user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }

        private async Task SeedRolesAsync()
        {
            string[] roleNames = ["Admin", "User"];

            foreach (var roleName in roleNames)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    var role = new IdentityRole { Name = roleName };
                    var result = await _roleManager.CreateAsync(role);
                    if (!result.Succeeded)
                    {
                        throw new Exception($"An error occurred while seeding the {roleName} role.");
                    }
                    else
                    {
                        _logger.LogInformation($"The {roleName} role has been seeded.");
                    }
                }
            }
        }
    }
}
