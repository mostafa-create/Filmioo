using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Buffers;
using System.Security.Claims;


namespace Demo.PL.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public MovieController(IUnitOfWork unitOfWork
            , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Movies(string SearchValue)
        {
            IEnumerable<Movie> Movies;
            if (string.IsNullOrEmpty(SearchValue))
            {
                Movies = await unitOfWork.MovieRepository.GetAllAsync();
            }
            else
            {
                Movies = await unitOfWork.MovieRepository.SearchByName(SearchValue);
            }
            var MappedMovies = mapper.Map<IEnumerable<Movie>, IEnumerable<MovieViewModel>>(Movies);
            return View(MappedMovies);
        }
		[HttpGet]
		public async Task<IActionResult> GetSearchSuggestions(string term)
		{
			if (string.IsNullOrEmpty(term)) return BadRequest();

			var movies = await unitOfWork.MovieRepository.SearchByName(term);
			var results = movies.Select(m => new {
				id = m.Id,
				title = m.Title,
				year = m.Release_Date?.Year
			}).Take(5); 

			return Json(results);
		}
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddMovie()
        {
            return View();
        }
        [HttpPost]
        [RequestSizeLimit(52428800)]
        public async Task<IActionResult> AddMovie(MovieViewModel MediaVM)
        {
            if (ModelState.IsValid)
            {
                MediaVM.Image_Name = DocumentSettings.UploadFile(MediaVM.Image, "images");
                var MappedMedia = mapper.Map<MovieViewModel, Movie>(MediaVM);
                await unitOfWork.MovieRepository.AddAsync(MappedMedia);
                await unitOfWork.Complete();
                return RedirectToAction("Movies");
            }
            return View(MediaVM);
        }
        public async Task<IActionResult> Details(int id)
        {
            var movie = await unitOfWork.MovieRepository.GetMovieDetailsAsync(id);

            if (movie == null)
                return NotFound();

            var mappedMovie = mapper.Map<Movie, MovieViewModel>(movie);

            return View(mappedMovie);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignActors()
        {
            var movies = await unitOfWork.MovieRepository.GetAllAsync();
            var actors = await unitOfWork.ActorRepository.GetAllAsync();

            var viewModel = new AssignActorsViewModel
            {
                MovieList = movies.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Title }),
                ActorList = actors.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = $"{a.FirstName} {a.LastName}" })
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AssignActors(AssignActorsViewModel model)
        {
            if (model.MovieId > 0)
            {
                await unitOfWork.MovieRepository.UpdateMovieActorsAsync(model.MovieId, model.SelectedActorIds);
                await unitOfWork.Complete(); 

                return RedirectToAction("Details", new { id = model.MovieId });
            }

            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignGenres()
        {
            var movies = await unitOfWork.MovieRepository.GetAllAsync();
            var genres = await unitOfWork.GenreRepository.GetAllAsync();

            var viewModel = new AssignGenreViewModel
            {
                MovieList = movies.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Title }),
                GenreList = genres.Select(g => new SelectListItem { Value = g.GenreId.ToString(), Text = g.Name })
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AssignGenres(AssignGenreViewModel model)
        {
            if (model.MovieId > 0)
            {
                await unitOfWork.MovieRepository.UpdateMovieGenresAsync(model.MovieId, model.SelectedGenreIds);
                await unitOfWork.Complete(); 

                return RedirectToAction("Details", new { id = model.MovieId });
            }

            return View(model);
        }
        public async Task<IActionResult> AllActors(int id)
        {
            var movie = await unitOfWork.MovieRepository.GetMovieWithActorsAsync(id);
            if (movie == null) return NotFound();
            var actorViewModels = movie.Actors.Select(ma => new ActorViewModel
            {
                Id = ma.Actor.Id,
                FirstName = ma.Actor.FirstName,
                LastName = ma.Actor.LastName,
                ProfileImageUrl = ma.Actor.ProfileImageUrl
            });
            ViewData["Movie"] = movie.Title;
            return View(actorViewModels);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddReview(ReviewViewModel reviewVM)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized(); 

            var existingReview = (await unitOfWork.ReviewRepository
                .FindAsync(r => r.ApplicationUserId == userId && r.MovieId == reviewVM.MovieId))
                .FirstOrDefault();

            ModelState.Remove("Movie");
            ModelState.Remove("ApplicationUser");
            ModelState.Remove("ApplicationUserId");

            if (ModelState.IsValid)
            {
                if (existingReview != null)
                {
                    existingReview.Score = reviewVM.Score;
                    existingReview.ReviewText = reviewVM.ReviewText;
                    existingReview.DatePosted = DateTime.Now;
                    unitOfWork.ReviewRepository.Update(existingReview);
                }
                else
                {
                    reviewVM.ApplicationUserId = userId;
                    reviewVM.DatePosted = DateTime.Now;
                    var mappedReview = mapper.Map<ReviewViewModel, Review>(reviewVM);
                    await unitOfWork.ReviewRepository.AddAsync(mappedReview);
                }

                await unitOfWork.Complete();
                return Json(new
                {
                    success = true,
                    score = reviewVM.Score?.ToString("0"),
                    text = reviewVM.ReviewText,
                    user = User.Identity.Name 
                });
            }

            return BadRequest(new { success = false, message = "Invalid data" });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> EditReview(int id, int score, string reviewText)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var review = await unitOfWork.ReviewRepository.GetByIdAsync(id);

            if (review == null || review.ApplicationUserId != userId)
            {
                return Json(new { success = false, message = "Unauthorized or review not found." });
            }

            review.Score = score;
            review.ReviewText = reviewText;
            review.DatePosted = DateTime.Now; 
            unitOfWork.ReviewRepository.Update(review);
            await unitOfWork.Complete();

            return Json(new { success = true, message = "Review updated successfully!" });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var review = await unitOfWork.ReviewRepository.GetByIdAsync(id);

            if (review == null || review.ApplicationUserId != userId)
            {
                return Json(new { success = false });
            }

            unitOfWork.ReviewRepository.Delete(review);
            await unitOfWork.Complete();

            return RedirectToAction("Details", new { id = review.MovieId });
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await unitOfWork.MovieRepository.GetByIdAsync(id);
            if (movie == null) return NotFound();
            var model = mapper.Map<UpdateMovieViewModel>(movie);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateMovieViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var movie = await unitOfWork.MovieRepository.GetByIdAsync(model.Id);
            if (movie == null) return NotFound();

            if (model.ImageFile != null)
            {
                DocumentSettings.DeleteFile(movie.Image_Name, "images");
                movie.Image_Name = DocumentSettings.UploadFile(model.ImageFile, "images");
            }
            mapper.Map(model, movie);
            unitOfWork.MovieRepository.Update(movie);
            await unitOfWork.Complete();
            return RedirectToAction(nameof(Movies));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await unitOfWork.MovieRepository.FindFirstAsync(M => M.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            try
            {
                unitOfWork.MovieRepository.Delete(movie);
                await unitOfWork.Complete();
                return RedirectToAction("Movies");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to delete movie. " + ex.Message);
                return RedirectToAction(nameof(Details), new { id = id });
            }
        }
    }
}
