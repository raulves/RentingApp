using System.Collections.Generic;

#pragma warning disable 1591
namespace WebApp.ViewModels
{
    public class ItemAdminVM
    {
        public string Description { get; set; } = default!;
        
        public string Brand { get; set; } = default!;
        
        public string Model { get; set; } = default!;

        public int BookingsCount { get; set; }

        public decimal Revenue { get; set; }
    }
}