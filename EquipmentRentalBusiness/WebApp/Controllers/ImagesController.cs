#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using PublicApi.DTO.v1;
using WebApp.ViewModels;
using WebApp.ViewModels.Mappers;

namespace WebApp.Controllers
{
    [Authorize]
    public class ImagesController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly ImageVMMapper _mapper = new ImageVMMapper();

        public ImagesController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: Images
        public async Task<IActionResult> Index()
        {
            return View((await _bll.Images.GetAllAsync(User.UserGuidId())).Select(e => _mapper.Map(e)));
        }

        // GET: Images/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var image = await _bll.Images.FirstOrDefaultAsync(id, User.UserGuidId());

            if (image == null)
            {
                return NotFound(new MessageDTO($"AppUser image with id {id} not found"));
            }

            return View(_mapper.Map(image));
        }

        // GET: Images/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ImageCreateEditViewModel
            {
                ItemSelectList = new SelectList(await _bll.Items.GetAllAsync(User.UserGuidId()), nameof(ItemBLL.Id), nameof(ItemBLL.Description))
            };
            
            return View(vm);
        }

        // POST: Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ImageCreateEditViewModel vm, IEnumerable<IFormFile> images)
        {
            vm.AppUserId = User.UserGuidId();
            
            if (ModelState.IsValid)
            {
                if (images != null)
                {
                    //Convert Image to byte and save to database
                    foreach (var file in images)
                    {
                        
                        using (var ms = new MemoryStream())
                        {
                            //image.Save(ms, new PngEncoder());
                            var imageBLL = new ImageBLL();
                            imageBLL.AppUserId = User.UserGuidId();
                            await file.CopyToAsync(ms);
                            imageBLL.Picture = ms.ToArray();
                            imageBLL.ItemId = vm.ItemId;
                            _bll.Images.Add(imageBLL);
                        }
                    }
                    await _bll.SaveChangesAsync();
                }
                
                return RedirectToAction(nameof(Index));
            }
            vm.ItemSelectList = new SelectList(await _bll.Items.GetAllAsync(User.UserGuidId()), nameof(ItemBLL.Id), nameof(ItemBLL.Description), vm.ItemId);
            
            return View(vm);
        }

        // GET: Images/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            
            var image = await _bll.Images.FirstOrDefaultAsync(id, User.UserGuidId());

            if (image == null)
            {
                return NotFound(new MessageDTO($"AppUser image with id {id} not found"));
            }
            
            var vm = _mapper.Map(image);
            
            vm.ItemSelectList = new SelectList(await _bll.Items.GetAllAsync(User.UserGuidId()), nameof(ItemBLL.Id), nameof(ItemBLL.Description), vm.ItemId);
            
            return View(vm);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ImageCreateEditViewModel vm)
        {
            if (id != vm.Id)
            {
                return BadRequest(new MessageDTO("Id and vm.id do not match"));
            }

            if (!await _bll.Images.ExistsAsync(vm.Id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"Current user does not have image with this id {id}"));
            }
            
            vm.AppUserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                await _bll.Images.UpdateAsync(_mapper.Map(vm));
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            vm.ItemSelectList = new SelectList(await _bll.Items.GetAllAsync(User.UserGuidId()), nameof(ItemBLL.Id), nameof(ItemBLL.Description), vm.ItemId);
            
            return View(vm);
        }

        // GET: Images/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            
            var image = await _bll.Images.FirstOrDefaultAsync(id, User.UserGuidId());

            if (image == null)
            {
                return NotFound(new MessageDTO($"AppUser image with id {id} not found"));
            }

            return View(_mapper.Map(image));
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Images.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
