// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using DiscussionLibrarySantiago;
using DiscussionMvcSantiago.Migrations;
using DiscussionMvcSantiago.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace DiscussionMvcSantiago.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserStore<AppUser> _userStore;
        private readonly IUserEmailStore<AppUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IAppUserRepo _appUserRepo;

        public RegisterModel(
            UserManager<AppUser> userManager,
            IUserStore<AppUser> userStore,
            SignInManager<AppUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IAppUserRepo appUserRepo)

        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _appUserRepo = appUserRepo;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        
        public class InputModel //ViewModel (Class)
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [Display(Name = "Fullname")]
            public string Fullname { get; set; }

            [Required]
            [Display(Name = "Phone")]
            public string Phone { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "UserRole")]
            public string UserRole { get; set; }

            [Display(Name = "Officer's Supervisor")]
            public string SupervisorId { get; set; }

        }


        public async Task OnGetAsync(string returnUrl = null) //HTTPGet
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ViewData["AllSupervisors"] = new SelectList(_appUserRepo.GetAllSupervisors(), "Id", "Fullname");
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null) //HTTPPost
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if(Input.UserRole == "SelectUserRole")
            {
                ModelState.AddModelError("NeedsUserRole", "Need to select a user role");
            }

            if (ModelState.IsValid)
            {
                //var user = CreateUser();

                //await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                //await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                //var result = await _userManager.CreateAsync(user, Input.Password);

                AppUser appUser = new AppUser();

                if(Input.UserRole == "Officer")
                {
                    appUser = new Officer(Input.Fullname, Input.Email, Input.Phone, Input.Password);    
                }
                else if (Input.UserRole == "Supervisor")
                {
                    appUser = new Supervisor(Input.Fullname, Input.Email, Input.Phone, Input.Password);
                }
                else if (Input.UserRole == "Mechanic")
                {
                    appUser = new Mechanic(Input.Fullname, Input.Email, Input.Phone, Input.Password);
                }

                var result = await _userManager.CreateAsync(appUser);


                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    //User has been created. Assign user to their role

                    _userManager.AddToRoleAsync(appUser, Input.UserRole).Wait();

                    string userId = await _userManager.GetUserIdAsync(appUser);

                    if(Input.UserRole == "Officer")
                    {
                        SupervisorOfficer supervisorOfficer = 
                            new SupervisorOfficer(Input.SupervisorId, userId, DateTime.Now.Date);
                        _appUserRepo.AddSupervisorOfficer(supervisorOfficer);
                    }

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email", 
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //_emailSender.SendEmailAsync(email, subject, message + $"by clicking <a href='{callbackUrl}'>here</a>").Wait();

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(appUser, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private AppUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<AppUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(AppUser)}'. " +
                    $"Ensure that '{nameof(AppUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<AppUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<AppUser>)_userStore;
        }
    }
}
