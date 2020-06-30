#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Helpers;
using WebApp.ViewModels;
using WebApp.ViewModels.Identity;
using WebApp.ViewModels.Mappers;

namespace WebApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class  AdminsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IAppBLL _bll;

        public AdminsController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IAppBLL bll)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _bll = bll;
        }

        // Admin page
        public async Task<IActionResult> Index()
        {
            
            
            var items = (_bll.Items.GetAllAsync().Result
                    .OrderByDescending(b => b.Bookings!.Count)
                    .Take(5))
                .Select(i => new ItemAdminVM()
                {
                    Description = i.Description,
                    Brand = i.Brand,
                    Model = i.Model,
                    BookingsCount =  i.Bookings!.Count,
                    Revenue = i.Bookings.Sum(s => s.BookingTotal)
                });

            var topItemOwners = await _userManager.Users
                .Include(a => a.Items)
                .OrderByDescending(i => i.Items!.Count)
                .Take(5)
                .Select(i => new AppUserAdminIndexVM()
                {
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    ItemsCount = i.Items!.Count
                })
                .ToListAsync();
            
            var vm = new AdminIndexVM()
            {
                TotalRevenue = _bll.Bookings.GetAllAsyncForAdminVM(null).Result.Sum(a => a.BookingTotal),
                TotalSales = _bll.Bookings.GetAllAsyncForAdminVM(null).Result.Count(),
                UsersCount = _userManager.Users.Count(),
                ProductsCount = _bll.Items.GetAllAsync().Result.Count(),
                Top5Items = items,
                Top5ItemOwners = topItemOwners

            };

            return View(vm);
        }
        
        // APPUSER methods

        // GET: Admins/AppUsers
        public async Task<IActionResult> AppUsers(string currentFilter, string? search, int? pageNumber)
        {
            if (search != null)
            {
                pageNumber = 1;
            }
            else
            {
                search = currentFilter;
            }
            ViewData["CurrentFilter"] = search;
            
            
            var appUsers = _userManager.Users.AsNoTracking()
                .Include(b => b.Bookings)
                .Include(i => i.Items)
                .Include(l => l.Location)
                .OrderBy(a => a.FirstName)
                .AsQueryable();
                
            if (!String.IsNullOrEmpty(search))
            {
                appUsers = appUsers
                    .Where(d => 
                        d.FirstName.ToLower().Contains(search.ToLower()) 
                        || d.LastName.ToLower().Contains(search.ToLower())
                        || d.Email.ToLower().Contains(search.ToLower())
                        || d.Phone!.ToLower().Contains(search.ToLower())
                        );
            }
            
            var vm = appUsers
                .Select(a => new AppUserAdminVM()
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Email = a.Email,
                    Phone = a.Phone,
                    Revenue = a.BookingsAsItemOwner.Sum(b => b.BookingTotal),
                    Location = new VMMapper<Location, LocationCreateEditViewModel>().Map(a.Location!),
                    Items = (ICollection<ItemCreateEditViewModel>?) a.Items.Select(i => new VMMapper<Item, ItemCreateEditViewModel>().Map(i)),
                    BookingsAsRenter = (ICollection<BookingsCreateEditViewModel>?) a.BookingsAsRenter.Select(b => new VMMapper<Booking, BookingsCreateEditViewModel>().Map(b)),
                    BookingsAsItemOwner = (ICollection<BookingsCreateEditViewModel>?) a.BookingsAsItemOwner.Select(b => new VMMapper<Booking, BookingsCreateEditViewModel>().Map(b))
                    
                })
                ;
            
            int pageSize = 5;
            
            return View(await PaginatedList<AppUserAdminVM>.CreateAsync(vm, pageNumber ?? 1, pageSize));
        }
        
        // GET: Admins/AppUser/5
        public async Task<IActionResult> AppUser(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(AppUsers));
            }
            
            var appUser = await _userManager.FindByIdAsync(id.ToString());
            if (appUser == null)
            {
                return RedirectToAction(nameof(AppUsers));
            }

            return View(appUser);
        }
        
        // GET: Admins/CreateUser
        public IActionResult CreateUser()
        {
            return View();
        }

        // POST: Admins/CreateUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(AppUserCreateEditVM appUserModel)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(appUserModel.Email);
                if (user == null)
                {
                    user = new AppUser
                    {
                        UserName = appUserModel.Email,
                        FirstName = appUserModel.FirstName,
                        LastName = appUserModel.LastName,
                        Email = appUserModel.Email
                    };
                    var result = await _userManager.CreateAsync(user, appUserModel.Password);
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("User creation failed!");

                    }
                    var role = _roleManager.FindByNameAsync("user").Result;
                    if (role != null)
                    {
                        var roleResult = _userManager.AddToRoleAsync(user, "user").Result;
                    }
                    
                }

                return RedirectToAction(nameof(AppUsers));

            }

            return View(appUserModel);
        }

        // GET: Admins/EditAppUser/5
        public async Task<IActionResult> EditAppUser(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(AppUsers));
            }

            var appUser = await _userManager.FindByIdAsync(id.ToString());
            if (appUser == null)
            {
                return RedirectToAction(nameof(AppUsers));
            }
            
            return View(new VMMapper<AppUser, AppUserCreateEditVM>().Map(appUser));
        }
        
        // POST: Admins/EditAppUser/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAppUser(Guid id, AppUserCreateEditVM appUser)
        {
            if (id != appUser.Id)
            {
                return RedirectToAction(nameof(AppUsers));
            }
            
            
            if (ModelState.IsValid)
            {
                await _userManager.UpdateAsync(new VMMapper<AppUserCreateEditVM, AppUser>().Map(appUser));
                return RedirectToAction(nameof(Index));
            }

            return View(appUser);
        }
        
        // GET: Admins/DeleteAppUser/5
        public async Task<IActionResult> DeleteAppUser(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(AppUsers));
            }

            var appUser = await _userManager.FindByIdAsync(id.ToString());
            if (appUser == null)
            {
                return RedirectToAction(nameof(AppUsers));
            }

            return View(new VMMapper<AppUser, AppUserDeleteVM>().Map(appUser));
        }
        
        
        // POST: Admins/DeleteAppUser/5
        [HttpPost, ActionName("DeleteAppUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var appUser = await _userManager.FindByIdAsync(id.ToString());
            await _userManager.DeleteAsync(appUser);
            return RedirectToAction(nameof(AppUsers));
        }
        
        
        // ROLE methods
        
        // UserRoles
        
        // GET: Admins/Roles
        public async Task<IActionResult> Roles(string currentFilter, string? search, int? pageNumber)
        {
            if (search != null)
            {
                pageNumber = 1;
            }
            else
            {
                search = currentFilter;
            }
            ViewData["CurrentFilter"] = search;
            
            var roles = _roleManager.Roles.AsNoTracking()
                .OrderBy(d => d.DisplayName)
                .AsQueryable();
            
            if (!String.IsNullOrEmpty(search))
            {
                roles = roles
                    .Where(a => a.DisplayName.ToLower().Contains(search.ToLower())
                                || a.Name.ToLower().Contains(search.ToLower()));
            }

            var vm = roles
                .Select(role => new VMMapper<AppRole, AppRoleViewModel>().Map(role));
                
            int pageSize = 5;
            
            return View(await PaginatedList<AppRoleViewModel>.CreateAsync(vm, pageNumber ?? 1, pageSize));
        }
        
        // GET: Admins/CreateRole
        public IActionResult CreateRole()
        {
            return View();
        }
        
        // POST: Admins/CreateRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(AppRoleViewModel appRole)
        {
            var role = await _roleManager.FindByNameAsync(appRole.Name);
            if (role == null)
            {
                if (ModelState.IsValid)
                {
                    role = new AppRole();
                    role.Name = appRole.Name.ToLower();
                    role.DisplayName = appRole.DisplayName.First().ToString().ToUpper() +
                                       appRole.DisplayName.Substring(1);
                    var result = await _roleManager.CreateAsync(role);
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("Role creation failed!");
                    }
                    return RedirectToAction(nameof(Roles));
                }

                return View(appRole);
            }
            // Role already exists
            ModelState.AddModelError("exists", "Role already exists.");
            return View(appRole);
        }
        
        // GET: Admins/EditRole/5
        public async Task<IActionResult> EditRole(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Roles));
            }

            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return RedirectToAction(nameof(Roles));
            }
            
            return View(new VMMapper<AppRole, AppRoleViewModel>().Map(role));
        }
        
        // POST: Admins/EditRole/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(Guid id, AppRoleViewModel appRole)
        {
            if (id != appRole.Id)
            {
                return RedirectToAction(nameof(Roles));
            }
            
            
            if (ModelState.IsValid)
            {
                await _roleManager.UpdateAsync(new VMMapper<AppRole, AppRoleViewModel>().Map(appRole));
                return RedirectToAction(nameof(Roles));
            }

            return View(appRole);
        }
        
        // GET: Admins/DeleteRole/5
        public async Task<IActionResult> DeleteRole(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Roles));
            }

            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return RedirectToAction(nameof(Roles));
            }

            return View(new VMMapper<AppRole, AppRoleViewModel>().Map(role));
        }
        
        // POST: Admins/DeleteRole/5
        [HttpPost, ActionName("DeleteRole")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRoleConfirmed(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            await _roleManager.DeleteAsync(role);
            return RedirectToAction(nameof(Roles));
        }

        // Get AppUsers in Role
        public async Task<IActionResult> AppUserRoles(string currentFilter, string? search, int? pageNumber)
        {
            if (search != null)
            {
                pageNumber = 1;
            }
            else
            {
                search = currentFilter;
            }
            ViewData["CurrentFilter"] = search;

            var users =await _userManager.Users.AsNoTracking().ToListAsync();
            var appUserRoles = new Dictionary<string, IList<string>>();
            foreach (var user in users)
            {
                appUserRoles.Add(user.Id.ToString(), await _userManager.GetRolesAsync(user));
            }

            var appUsers = _userManager.Users
                .AsNoTracking()
                .OrderBy(a => a.FirstName)
                .AsQueryable();
            
            if (!String.IsNullOrEmpty(search))
            {
                appUsers = appUsers
                    .Where(a => a.FirstName.ToLower().Contains(search.ToLower())
                                || a.LastName.ToLower().Contains(search.ToLower())
                                || a.Email.ToLower().Contains(search.ToLower())
                                /*|| appUserRoles.GetValueOrDefault(a.Id.ToString())
                                    .Any(s => s.ToLower().Contains(search.ToLower()))*/);

            }

            var vm = appUsers
                .Select(a => new AppUserRoleVM()
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Email = a.Email,
                    Roles = appUserRoles.GetValueOrDefault(a.Id.ToString())
                });

            int pageSize = 5;
            
            return View(await PaginatedList<AppUserRoleVM>.CreateAsync(vm, pageNumber ?? 1, pageSize));
        }

        // GET
        public IActionResult AddAppUserToRole()
        {
            var vm = new AppUserToRoleCreateViewModel();
            vm.AppUserSelectList = new SelectList(_userManager.Users, nameof(Domain.App.Identity.AppUser.Id), nameof(Domain.App.Identity.AppUser.FullName));
            vm.RoleSelectList = new SelectList(_roleManager.Roles, nameof(AppRole.Id), nameof(AppRole.Name));
            return View(vm);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAppUserToRole(AppUserToRoleCreateViewModel vm)
        {
            var user = await _userManager.FindByIdAsync(vm.AppUserId.ToString());
            var role = _roleManager.Roles.FirstOrDefaultAsync(a => a.Id == vm.RoleId).Result.Name;

            if (!await _userManager.IsInRoleAsync(user, role))
            {
                var userResult = await _userManager.AddToRoleAsync(user, role);
                return RedirectToAction(nameof(AppUserRoles));
            }
            
            ModelState.AddModelError("notice", "User is already in that role. ");
            vm.RoleSelectList = new SelectList(_roleManager.Roles, nameof(AppRole.Id), nameof(AppRole.Name), vm.RoleId);
            vm.AppUserSelectList = new SelectList(_userManager.Users, nameof(Domain.App.Identity.AppUser.Id), nameof(Domain.App.Identity.AppUser.FullName), vm.AppUserId);

            return View(vm);
        }

        // GET
        public async Task<IActionResult> RemoveAppUserFromRole(Guid id)
        {
            var vm = new AppUserToRoleDeleteViewModel();
            var appUser = await _userManager.FindByIdAsync(id.ToString());
            vm.AppUser = appUser;
            var userRoleNames = await _userManager.GetRolesAsync(appUser);
            var userRoles = new List<AppRole>();
            foreach (var role in userRoleNames)
            {
                userRoles.Add(await _roleManager.FindByNameAsync(role));
            }
            
            vm.Roles = new SelectList(userRoles, nameof(AppRole.Id), nameof(AppRole.Name));
            
            if (vm.AppUser == null)
            {
                return RedirectToAction(nameof(AppUserRoles));
            }

            return View(vm);
        }
        
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveAppUserFromRole(AppUserToRoleDeleteViewModel vm)
        {
            var role = _roleManager.Roles.FirstOrDefaultAsync(a => a.Id == vm.RoleId).Result.Name;
            var appUser = await _userManager.FindByIdAsync(vm.Id.ToString());
            
            if (await _userManager.IsInRoleAsync(appUser, role))
            {
                var userResult = await _userManager.RemoveFromRoleAsync(appUser, role);
                return RedirectToAction(nameof(AppUserRoles));
            }
            
            ModelState.AddModelError("notice", "User is not in this role.");
            vm.AppUser = appUser;
            var userRoleNames = await _userManager.GetRolesAsync(appUser);
            var userRoles = new List<AppRole>();
            foreach (var roleName in userRoleNames)
            {
                userRoles.Add(await _roleManager.FindByNameAsync(roleName));
            }
            vm.Roles = new SelectList(userRoles, nameof(AppRole.Id), nameof(AppRole.Name));
            return View(vm);
        }
        public class AppUserModel
        {

            [Required]
            [MaxLength(64)]
            [MinLength(1)]
            public string FirstName { get; set; } = default!;
            
            [Required]
            [MaxLength(64)]
            [MinLength(1)]
            public string LastName { get; set; } = default!;
            
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; } = default!;

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; } = default!;

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; } = default!;
        }
        
    }
}