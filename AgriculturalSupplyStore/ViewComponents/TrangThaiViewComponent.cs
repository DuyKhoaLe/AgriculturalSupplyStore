using AgriculturalSupplyStore.Data;
using AgriculturalSupplyStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriculturalSupplyStore.ViewComponents
{
    [ViewComponent]
    public class TrangThaiViewComponent : ViewComponent
    {
        private readonly AgriculturalsupplystoreDbContext _dbContext;

        public TrangThaiViewComponent(AgriculturalsupplystoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(int matrangthai)
        {
            var trangthais = await _dbContext.TrangThais
                .Where(p => p.MaTrangThai == matrangthai)
                .Select(p => new TrangThaiVM
                {
                    MaTrangThai = p.MaTrangThai,
                    TenTrangThai =  p.TenTrangThai
                })
                .ToListAsync();
            return View(trangthais);
        }
    }
}
