﻿using FooBlog.STS.Identity.Configuration;
using System.ComponentModel.DataAnnotations;
using FooBlog.Shared.Configuration.Identity;

namespace FooBlog.STS.Identity.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        public LoginResolutionPolicy? Policy { get; set; }
        //[Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Username { get; set; }
    }
}






