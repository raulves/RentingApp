#pragma warning disable 1591
namespace WebApp.ViewModels
{
    public class AppUserAdminIndexVM
    {
        public string FirstName { get; set; } = default!;
        
        public string LastName { get; set; } = default!;

        public int ItemsCount { get; set; }
    }
}