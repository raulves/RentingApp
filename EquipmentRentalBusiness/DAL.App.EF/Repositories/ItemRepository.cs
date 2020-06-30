using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.DTO.Identity;
using DAL.App.EF.Mappers;



using Domain.App;
using Domain.App.Identity;
using ee.itcollege.Raul.Vesinurm.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ItemRepository : 
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Item, ItemDAL>, IItemRepository
    {
        public ItemRepository(AppDbContext dbContext) : base(dbContext, 
            new DALMapper<Item, ItemDAL>())
        {
        }
        
        public virtual async Task<IEnumerable<ItemView>> GetItemsForViewAsync(Guid? categoryId, string? search)
        {
            var items = RepoDbSet.AsNoTracking()
                .Include(i => i.Images)
                .Include(l => l.Location)
                .Include(p => p.Prices).AsQueryable();

            if (!String.IsNullOrEmpty(search))
            {
                items = items
                    .Where(d => 
                        d.Description.ToLower().Contains(search.ToLower()) 
                        || d.Brand.ToLower().Contains(search.ToLower())
                        || d.Model.ToLower().Contains(search.ToLower())
                        || d.Location!.City.ToLower().Contains(search.ToLower()));
            }

            if (categoryId != Guid.Empty)
            {
                items = items
                    .Where(c => c.ItemCategories.Any(a => a.CategoryId == categoryId));
            }

            if (!String.IsNullOrEmpty(search) || categoryId != Guid.Empty)
            {
                return await items
                    .Select(a => new ItemView()
                {
                    Id = a.Id,
                    Description = a.Description,
                    Image = a.Images.FirstOrDefault().Picture,
                    AddressLine = a.Location!.AddressLine,
                    Price = a.Prices.FirstOrDefault(p => p.RentalPeriod!.Description == "Päev").ItemPrice
                    
                }).ToListAsync();
                
            }
            // Return last 50 new items
            return await items
                .OrderBy(d => d.CreatedAt)
                .Take(50)
                .Select(a => new ItemView()
                {
                    Id = a.Id,
                    Description = a.Description,
                    Image = a.Images.FirstOrDefault().Picture,
                    AddressLine = a.Location!.AddressLine,
                    Price = a.Prices.FirstOrDefault(p => p.RentalPeriod!.Description == "Päev").ItemPrice
                    
                }).ToListAsync();
            
        }

        public async Task<SingleItemView> GetItemViewAsync(Guid id)
        {
            return await RepoDbSet.AsNoTracking()
                .Include(a => a.AppUser)
                .Include(i => i.ItemCategories)
                .Include(i => i.Images)
                .Include(l => l.Location)
                .Include(b => b.Bookings)
                .Include(i => i.ItemsBooked)
                .Include(p => p.Prices)
                .Include(i => i.ItemDescriptions)
                .ThenInclude(p => p.ProductDescription)
                .ThenInclude(a => a!.Description)
                .ThenInclude(s => s!.Translations)
                .Include(i => i.ItemOwnerships)
                .ThenInclude(c => c.Company)
                .Select(a => new SingleItemView()
                {
                    Id = a.Id,
                    // Item owner Id
                    AppUserId = a.AppUserId,
                    AppUser = new DALMapper<AppUser, AppUserDAL>().Map(a.AppUser!),
                    Description = a.Description,
                    Brand = a.Brand,
                    Model = a.Model,
                    BookingStartDay = DateTime.Now,
                    BookingEndDay = DateTime.Now,
                    PricePerDay = a.Prices.FirstOrDefault(p => p.RentalPeriod!.Description == "Päev").ItemPrice,
                    PricePerWeek = a.Prices.FirstOrDefault(p => p.RentalPeriod!.Description == "Nädal").ItemPrice,
                    PricePerMonth = a.Prices.FirstOrDefault(p => p.RentalPeriod!.Description == "Kuu").ItemPrice,
                    ItemCategories = (ICollection<ItemCategoryDAL>?) (a.ItemCategories).Select(e => new DALMapper<ItemCategory, ItemCategoryDAL>().Map(e)),
                    Images = (ICollection<ImageDAL>?) (a.Images).Select(i => new DALMapper<Image, ImageDAL>().Map(i)),
                    LocationId = a.LocationId,
                    Location = new DALMapper<Location, LocationDAL>().Map(a.Location!),
                    Bookings = (ICollection<BookingDAL>?) (a.Bookings).Select(b => new DALMapper<Booking, BookingDAL>().Map(b)),
                    ItemsBooked = (ICollection<ItemBookedDAL>?) (a.ItemsBooked).Select(i => new DALMapper<ItemBooked, ItemBookedDAL>().Map(i)),
                    ItemDescriptions = (ICollection<ItemDescriptionDAL>?) (a.ItemDescriptions).Select(i => new DALMapper<ItemDescription, ItemDescriptionDAL>().Map(i)),
                    ItemOwnerCompanyId = a.ItemOwnerships.FirstOrDefault(c => c.CompanyId != null).CompanyId,
                    ItemOwnerCompany = new DALMapper<Company, CompanyDAL>().Map(a.ItemOwnerships.FirstOrDefault(c => c.CompanyId != null).Company!),
                    HasVatNumber = a.ItemOwnerships.FirstOrDefault(c => c.CompanyId != null).Company!.VatNumber != null ? true : false
                    
                })
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

        }

        public async Task<IEnumerable<ItemDAL>> GetAppUserCompaniesItems(Guid userId, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(i => i.ItemCategories)
                .Include(i => i.Images)
                .Include(l => l.Location)
                .Include(b => b.Bookings)
                .Include(i => i.ItemsBooked)
                .Include(p => p.Prices)
                .Include(i => i.ItemDescriptions)
                .Include(i => i.ItemOwnerships)
                .Where(e => e.ItemOwnerships.Any(i => i.AppUserId == userId && i.CompanyId != null));
            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            return result;
        }

        public async Task<IEnumerable<ItemDAL>> GetAppUserItems(Guid userId, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(i => i.ItemCategories)
                .Include(i => i.Images)
                .Include(l => l.Location)
                .Include(b => b.Bookings)
                .Include(i => i.ItemsBooked)
                .Include(p => p.Prices)
                .Include(i => i.ItemDescriptions)
                .Include(i => i.ItemOwnerships)
                .Where(e => e.ItemOwnerships.Any(i => i.AppUserId == userId && i.CompanyId == null));
            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            return result;
        }

        


        public override async Task<IEnumerable<ItemDAL>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(a => a.AppUser)
                .Include(i => i.ItemCategories)
                .Include(i => i.Images)
                .Include(l => l.Location)
                .Include(b => b.Bookings)
                .Include(i => i.ItemsBooked)
                .Include(p => p.Prices)
                .Include(i => i.ItemDescriptions);
            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            return result;
        }
        
        

        public override async Task<ItemDAL> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(a => a.AppUser)
                .Include(i => i.ItemCategories)
                .Include(i => i.Images)
                .Include(l => l.Location)
                .Include(b => b.Bookings)
                .Include(i => i.ItemsBooked)
                .Include(p => p.Prices)
                .Include(i => i.ItemDescriptions);
            var domainItem = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainItem);
            return result;
        }
    }
}