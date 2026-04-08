using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Demo.PL.Controllers
{ 
    public class AccountController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = new ApplicationUser()
                {
                    UserName = model.EmailAddress.Split("@")[0],
                    Email = model.EmailAddress,
                    IsAgree = model.IsAgree,
                    FName = model.FName,
                    LName = model.LName
                };
                var result = await userManager.CreateAsync(User, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(User, "User");
                    await signInManager.SignInAsync(User, isPersistent: false);
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        #endregion
        #region Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = await userManager.FindByEmailAsync(model.EmailAddress);
                if (User is not null)
                {
                    var result = await userManager.CheckPasswordAsync(User, model.Password);
                    if (result)
                    {
                        var loginresult = await signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false);
                        if (loginresult.Succeeded)
                            return RedirectToAction("Index", "Home");
                    }
                    else
                        ModelState.AddModelError("Password", "Password Is Incorrect");
                }
                else
                    ModelState.AddModelError("EmailAddress", "Email Does Not Exists");
            }
            return View(model);
        }
        #endregion
        #region Sign Out
        public new async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion
        #region ForgetPassword
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = await userManager.FindByEmailAsync(model.EmailAddress);
                if (User is not null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(User);
                    var ResetPassordLink = Url.Action("ResetPassword", "Account", new { email = User.Email, Token = token }, Request.Scheme);

                    var email = new DAL.Models.Email()
                    {
                        Subject = "Reset Password",
                        To = model.EmailAddress,
                        Body = ResetPassordLink
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
                else
                {
                    ModelState.AddModelError("EmailAddress", "Email Does Not Exists");
                }
            }
            return View("ForgetPassword", model);
        }
        public IActionResult CheckYourInbox()
        {
            return View();
        }
        #endregion

        #region ResetPassword
        public IActionResult ResetPassword(string Email, string Token)
        {
            TempData["Email"] = Email;
            TempData["Token"] = Token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                string Email = TempData["Email"] as string;
                string Token = TempData["Token"] as string;
                var User = await userManager.FindByEmailAsync(Email);
                var result = await userManager.ResetPasswordAsync(User, Token, model.NewPassword);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login");
            var reviews = await unitOfWork.ReviewRepository.GetLastReviewsByUserIdAsync(user.Id, 10);
            var watchListItems = await unitOfWork.WatchListRepository.GetWatchListByUserIdAsync(user.Id);
            var viewModel = new UserViewModel
            {
                FName = user.FName,
                LName = user.LName,
                Email = user.Email!,
                ProfileImageUrl = user.ProfileImageUrl,
                JoiningDate = user.JoiningDate,
                LastReviews = mapper.Map<IEnumerable<ReviewViewModel>>(reviews),
                WatchList = watchListItems.Select(w => mapper.Map<MovieViewModel>(w.Movie)).ToList()
            };
            return View(viewModel);
        }
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var model = new EditProfileViewModel
            {
                Id = user.Id,
                FName = user.FName,
                LName = user.LName,
                Email = user.Email,
                ExistingImageUrl = user.ProfileImageUrl
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(EditProfileViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await userManager.FindByIdAsync(model.Id);
            if (user == null) return NotFound();

            if (model.ImageFile != null)
            {
                user.ProfileImageUrl = DocumentSettings.UploadFile(model.ImageFile, "images");
            }

            user.FName = model.FName;
            user.LName = model.LName;

            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Profile");
            }

            foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
            return View(model);
        }
        #endregion
    }
}
