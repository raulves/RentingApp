#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;

using Extensions;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;
using WebApp.ViewModels;
using WebApp.ViewModels.Mappers;

namespace WebApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductDescriptionsController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly ProductDescriptionVMMapper _mapper = new ProductDescriptionVMMapper();

        public ProductDescriptionsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: ProductDescriptions
        public async Task<IActionResult> Index()
        {
            return View((await _bll.ProductDescriptions.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: ProductDescriptions/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var productDescription = await _bll.ProductDescriptions.FirstOrDefaultAsync(id);

            if (productDescription == null)
            {
                return NotFound(new MessageDTO($"Product description with id {id} not found"));
            }
            return View(_mapper.Map(productDescription));
        }

        // GET: ProductDescriptions/Create
        public IActionResult Create()
        {
            var vm = new ProductDescriptionCreateEditViewModel();
            return View(vm);
        }

        // POST: ProductDescriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDescriptionCreateEditViewModel vm)
        {

            if (ModelState.IsValid)
            {
                var bllEntity = _mapper.Map(vm);
                _bll.ProductDescriptions.Add(bllEntity);
                await _bll.SaveChangesAsync();
                vm.Id = bllEntity.Id;
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: ProductDescriptions/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var productDescription = await _bll.ProductDescriptions.FirstOrDefaultAsync(id);
            if (productDescription == null)
            {
                return NotFound(new MessageDTO("ProductDescription not found"));
            }

            var vm = _mapper.Map(productDescription);

            return View(vm);
        }

        // POST: ProductDescriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductDescriptionCreateEditViewModel vm)
        {
            if (id != vm.Id)
            {
                return BadRequest(new MessageDTO("Id and productDescriptionEditDTO.id do not match"));
            }
            
            if (!await _bll.ProductDescriptions.ExistsAsync(vm.Id))
            {
                return NotFound(new MessageDTO($"Product description with this id {id} not found"));
            }

            if (ModelState.IsValid)
            {
                await _bll.ProductDescriptions.UpdateAsync(_mapper.Map(vm));
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: ProductDescriptions/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var productDescription = await _bll.ProductDescriptions.FirstOrDefaultAsync(id);
            if (productDescription == null)
            {
                return NotFound(new MessageDTO("ProductDescription not found"));
            }

            return View(_mapper.Map(productDescription));
        }

        // POST: ProductDescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.ProductDescriptions.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
