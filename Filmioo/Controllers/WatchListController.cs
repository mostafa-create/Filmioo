using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class WatchListController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public WatchListController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> MyWatchList()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var watchlist = await unitOfWork.WatchListRepository
                .FindAsync(w => w.UserId == userId);

            return View(watchlist);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleWatchlist(int movieId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var existingItem = await unitOfWork.WatchListRepository
                .FindAsync(w => w.UserId == userId && w.MovieId == movieId);

            if (existingItem.Any())
            {
                unitOfWork.WatchListRepository.Delete(existingItem.First());
                await unitOfWork.Complete();
                return Json(new { success = true, action = "removed" });
            }

            var movie = await unitOfWork.MovieRepository.FindFirstAsync(M => M.Id == movieId);
            var watchItem = new WatchListItem
            {
                MovieId = movieId,
                UserId = userId,
                MovieTitle = movie.Title,
                PosterImageUrl = movie.Image_Name
            };

            await unitOfWork.WatchListRepository.AddAsync(watchItem);
            await unitOfWork.Complete();

            return Json(new { success = true, action = "added" });
        }
    }
}
