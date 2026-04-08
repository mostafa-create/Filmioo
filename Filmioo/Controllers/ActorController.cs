using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Buffers;

namespace Demo.PL.Controllers
{
    public class ActorController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public ActorController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Actor> Actors;
            if (string.IsNullOrEmpty(SearchValue))
            {
                Actors = await unitOfWork.ActorRepository.GetAllAsync();
            }
            else
            {
                Actors = await unitOfWork.ActorRepository.SearchByName(SearchValue);
            }
            var MappedActors = mapper.Map<IEnumerable<Actor>, IEnumerable<ActorViewModel>>(Actors);
            return View(MappedActors);
        }
        public async Task<IActionResult> GetSearchSuggestions(string term)
        {
            if (string.IsNullOrEmpty(term)) return BadRequest();

            var Actors = await unitOfWork.ActorRepository.SearchByName(term);
            var results = Actors.Select(m => new {
                id = m.Id,
                name = $"{m.FirstName} {m.LastName}",
            }).Take(5);

            return Json(results);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddActor()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddActor(ActorViewModel ActorVM)
        {
            if (ModelState.IsValid)
            {
                ActorVM.ProfileImageUrl = DocumentSettings.UploadFile(ActorVM.ProfileImageFile, "images");
                var MappedActor = mapper.Map<ActorViewModel, Actor>(ActorVM);
                await unitOfWork.ActorRepository.AddAsync(MappedActor);
                await unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(ActorVM);
        }
		public async Task<IActionResult> Details(int id)
		{
            var actor = await unitOfWork.ActorRepository.GetActorDetailsAsync(id);
			if (actor == null)
			{
				return NotFound();
			}
			var mappedActor = mapper.Map<Actor, ActorViewModel>(actor);
			return View(mappedActor);
		}
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var actor = await unitOfWork.ActorRepository.GetByIdAsync(id);
            if (actor == null) return NotFound();
            var model = mapper.Map<UpdateActorViewModel>(actor);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateActorViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var actor = await unitOfWork.ActorRepository.GetByIdAsync(model.Id);
            if (actor == null) return NotFound();

            if (model.ImageFile != null)
            {
                DocumentSettings.DeleteFile(actor.ProfileImageUrl, "images");
                actor.ProfileImageUrl = DocumentSettings.UploadFile(model.ImageFile, "images");
            }
            mapper.Map(model, actor);
            unitOfWork.ActorRepository.Update(actor);
            await unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var actor = await unitOfWork.ActorRepository.FindFirstAsync(M => M.Id == id);
            if (actor == null)
            {
                return NotFound();
            }
            try
            {
                unitOfWork.ActorRepository.Delete(actor);
                await unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to delete Actor. " +ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

    }
}
