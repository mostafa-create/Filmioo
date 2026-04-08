using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.PL.ViewModels;
using Filmioo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Filmioo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _UnitOfWork;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _UnitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(string searchString)
        {
			if (!string.IsNullOrEmpty(searchString))
			{
				var searchResults = await _UnitOfWork.MovieRepository.SearchByName(searchString);
				return View("SearchResults", searchResults);
			}
			var HomeMovies = new HomeViewModel
            {
                Trending = await _UnitOfWork.MovieRepository.GetTrending(10),
                RecentlyAdded = await _UnitOfWork.MovieRepository.GetRecentlyAdded(10),
                TopRated = await _UnitOfWork.MovieRepository.GetTopRated(10)
            };
            return View(HomeMovies);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
