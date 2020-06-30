#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using PublicApi.DTO.v1;
using WebApp.ViewModels;
using WebApp.ViewModels.Mappers;


namespace WebApp.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly ItemVMMapper _mapper = new ItemVMMapper();

        public ItemsController(IAppBLL bll)
        {
            _bll = bll;
        }

        
        // GET: AppUser companies Items
        
        public async Task<IActionResult> AppUserCompaniesItems()
        {
            return View((await _bll.Items.GetAppUserCompaniesItems(User.UserGuidId())).Select(e => _mapper.Map(e)));
        }
        
        // GET: AppUser Items
        
        public async Task<IActionResult> AppUserItems()
        {
            return View((await _bll.Items.GetAppUserItems(User.UserGuidId())).Select(e => new VMMapper<ItemBLL, ItemViewProfileVM>().Map(e)));
        }

        // GET: Items for homepage
        [AllowAnonymous]
        public async Task<IActionResult> Index(ShowItemsVM vm, string? search)
        {
            var items = (await _bll.Items.GetItemsForViewAsync(vm.CategoryId, search)).Select(e =>
                new VMMapper<ItemView, ItemViewVM>().Map(e));
            vm.Items = items;
            vm.CategorySelectList = new SelectList(await _bll.Categories.GetAllAsync(), nameof(CategoryBLL.Id),
                nameof(CategoryBLL.Description));
            
            return View(vm);
        }

        // GET: Items/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            var item = await _bll.Items.GetItemViewAsync(id);

            if (item == null)
            {
                return NotFound(new MessageDTO($"Item with id {id} not found"));
            }
            return View(new VMMapper<SingleItemView, SingleItemViewVM>().Map(item));
        }

        // GET: Items/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ItemCreateEditViewModel
            {
                CompanySelectList = new SelectList(await _bll.Companies.GetAllAppUserCompaniesAsync(User.UserGuidId()), nameof(CompanyBLL.Id), nameof(CompanyBLL.CompanyName)),
                LocationSelectList = new SelectList(await _bll.Locations.GetAllAsync(User.UserGuidId()), nameof(LocationBLL.Id), nameof(LocationBLL.AddressLine)),
                CategorySelectList = new SelectList(await _bll.Categories.GetCategoriesForSelectListAsync(), nameof(CategoryBLL.Id), nameof(CategoryBLL.Description)),
                ProductDescriptionSelectList = new SelectList(await _bll.ProductDescriptions.GetAllAsync(), nameof(ProductDescriptionBLL.Id), nameof(ProductDescriptionBLL.Description))
            };
            
            return View(vm);
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemCreateEditViewModel vm, IEnumerable<IFormFile> images)
        {
            
            vm.AppUserId = User.UserGuidId();
            
            if (ModelState.IsValid)
            {
                var bllEntity = _mapper.Map(vm);
                _bll.Items.Add(bllEntity);
                await _bll.SaveChangesAsync();
                vm.Id = bllEntity.Id;
                
                // Add item ownership
                var itemOwnershipBLL = new ItemOwnershipBLL();
                itemOwnershipBLL.AppUserId = User.UserGuidId();
                itemOwnershipBLL.CompanyId = vm.CompanyId == Guid.Empty ? null : vm.CompanyId;
                
                itemOwnershipBLL.ItemId = bllEntity.Id;
                _bll.ItemOwnerships.Add(itemOwnershipBLL);
                await _bll.SaveChangesAsync();

                if (images != null)
                {
                    foreach (var file in images)
                    {
                        
                        using (var ms = new MemoryStream())
                        {
                            //image.Save(ms, new PngEncoder());
                            var imageBLL = new ImageBLL();
                            imageBLL.AppUserId = User.UserGuidId();
                            await file.CopyToAsync(ms);
                            imageBLL.Picture = ms.ToArray();
                            imageBLL.ItemId = bllEntity.Id;
                            _bll.Images.Add(imageBLL);
                        }
                    }
                    await _bll.SaveChangesAsync();
                }
                
                // Add category to item
                var itemCategoryBLL = new ItemCategoryBLL();
                itemCategoryBLL.AppUserId = User.UserGuidId();
                itemCategoryBLL.CategoryId = vm.CategoryId;
                itemCategoryBLL.ItemId = bllEntity.Id;
                _bll.ItemCategories.Add(itemCategoryBLL);
                
                await _bll.SaveChangesAsync();
                
                // Add prices
                var priceDayBLL = new PriceBLL();
                priceDayBLL.AppUserId = User.UserGuidId();
                priceDayBLL.ItemPrice = vm.ItemDayPrice;
                priceDayBLL.ItemId = bllEntity.Id;
                priceDayBLL.RentalPeriodId = _bll.RentalPeriods.GetRentalPeriodId("Päev");
                _bll.Prices.Add(priceDayBLL);
                
                var priceWeekBLL = new PriceBLL();
                priceWeekBLL.AppUserId = User.UserGuidId();
                priceWeekBLL.ItemPrice = vm.ItemWeekPrice;
                priceWeekBLL.ItemId = bllEntity.Id;
                priceWeekBLL.RentalPeriodId = _bll.RentalPeriods.GetRentalPeriodId("Nädal");
                _bll.Prices.Add(priceWeekBLL);
                
                var priceMonthBLL = new PriceBLL();
                priceMonthBLL.AppUserId = User.UserGuidId();
                priceMonthBLL.ItemPrice = vm.ItemMonthPrice;
                priceMonthBLL.ItemId = bllEntity.Id;
                priceMonthBLL.RentalPeriodId = _bll.RentalPeriods.GetRentalPeriodId("Kuu");
                _bll.Prices.Add(priceMonthBLL);
                
                await _bll.SaveChangesAsync();
                
                // Add item descriptions
                if (vm.ItemDescription != null || vm.ItemDescription!.Length != 0)
                {
                    var itemDescriptionBLL = new ItemDescriptionBLL()
                    {
                        AppUserId = User.UserGuidId(),
                        Description = vm.ItemDescription,
                        ProductDescriptionId = vm.ProductDescriptionId,
                        ItemId = bllEntity.Id
                    };
                    _bll.ItemDescriptions.Add(itemDescriptionBLL);

                }

                if (vm.Descriptions != null && vm.Descriptions!.Count != 0)
                {
                    for (int i = 0; i < vm.Descriptions.Count; i++)
                    {
                        if (vm.Descriptions[i].Length != 0)
                        {
                            var itemDescriptionBLL = new ItemDescriptionBLL()
                            {
                                AppUserId = User.UserGuidId(),
                                Description = vm.Descriptions[i],
                                ProductDescriptionId = vm.ProductDescriptionIds![i],
                                ItemId = bllEntity.Id
                            };
                            
                            _bll.ItemDescriptions.Add(itemDescriptionBLL);
                        }
                        
                    }
                    
                }

                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            vm.CompanySelectList = new SelectList(await _bll.Companies.GetAllAppUserCompaniesAsync(User.UserGuidId()), nameof(CompanyBLL.Id), nameof(CompanyBLL.CompanyName), vm.CompanyId);
            vm.LocationSelectList =
                new SelectList(await _bll.Locations.GetAllAsync(User.UserGuidId()), nameof(LocationBLL.Id), nameof(LocationBLL.AddressLine), vm.LocationId);
            vm.CategorySelectList = new SelectList(await _bll.Categories.GetCategoriesForSelectListAsync(), nameof(CategoryBLL.Id), nameof(CategoryBLL.Description), vm.CategoryId);
            vm.ProductDescriptionSelectList = new SelectList(await _bll.ProductDescriptions.GetAllAsync(), nameof(ProductDescriptionBLL.Id), nameof(ProductDescriptionBLL.Description), vm.ProductDescriptionId);
            
            return View(vm);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var item = await _bll.Items.FirstOrDefaultAsync(id, User.UserGuidId());
            if (item == null)
            {
                return NotFound(new MessageDTO("Item not found"));
            }

            var vm = new ItemEditVMMapper().Map(item);
            
            vm.LocationSelectList =
                new SelectList(await _bll.Locations.GetAllAsync(User.UserGuidId()), nameof(LocationBLL.Id), nameof(LocationBLL.AddressLine), vm.LocationId);
            
            return View(vm);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ItemEditVM vm)
        {
            if (id != vm.Id)
            {
                return BadRequest(new MessageDTO("Id and vm.id do not match"));
            }
            
            if (!await _bll.Items.ExistsAsync(id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"Item with this id {id} not found"));
            }

            vm.AppUserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                await _bll.Items.UpdateAsync(new ItemEditVMMapper().Map(vm));
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            
            vm.LocationSelectList =
                new SelectList(await _bll.Locations.GetAllAsync(User.UserGuidId()), nameof(LocationBLL.Id), nameof(LocationBLL.AddressLine), vm.LocationId);
            
            return View(vm);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _bll.Items.FirstOrDefaultAsync(id, User.UserGuidId());
            if (item == null)
            {
                return NotFound(new MessageDTO("Item not found"));
            }

            return View(_mapper.Map(item));
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Items.RemoveAsync(id, User.UserGuidId());
            await _bll.SaveChangesAsync();

            return RedirectToPage("/Identity/Account/Manage");
            
        }

    }
}
