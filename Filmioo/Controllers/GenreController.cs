using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class GenreController: Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public GenreController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Genres()
        {
            var Genres = await unitOfWork.GenreRepository.GetAllAsync();
            var MappedGenres = mapper.Map<IEnumerable<Genre>, IEnumerable<GenreViewModel>>(Genres);
            return View(MappedGenres);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddGenre()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddGenre(GenreViewModel GenreVM)
        {
            if (ModelState.IsValid)
            {
                var MappedGenre = mapper.Map<GenreViewModel, Genre>(GenreVM);
                await unitOfWork.GenreRepository.AddAsync(MappedGenre);
                await unitOfWork.Complete();
                return RedirectToAction("Genres");
            }
            return View(GenreVM);
        }
        public async Task<IActionResult> Details(int id)
        {
            var genre = await unitOfWork.GenreRepository.GetGenreWithMoviesAsync(id);
            if (genre == null) return NotFound();
            var viewModel = mapper.Map<GenreViewModel>(genre);
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] 
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var Genre = await unitOfWork.GenreRepository.FindFirstAsync(M => M.GenreId == id);
            if (Genre == null)
            {
                return NotFound();
            }
            try
            {
                unitOfWork.GenreRepository.Delete(Genre);
                await unitOfWork.Complete();
                return RedirectToAction(nameof(Genres));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to delete Genre. " + ex.Message);
                return RedirectToAction(nameof(Genres));
            }
        }
    }
}
