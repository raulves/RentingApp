using System.Collections.Generic;

#pragma warning disable 1591
namespace WebApp.ViewModels
{
    public class AdminIndexVM
    {
        public decimal TotalRevenue { get; set; }
        public int TotalSales { get; set; }
        public int UsersCount { get; set; }
        public int ProductsCount { get; set; }

        public IEnumerable<ItemAdminVM>? Top5Items { get; set; }
        
        public IEnumerable<AppUserAdminIndexVM>? Top5ItemOwners { get; set; }
        
        
    }
}