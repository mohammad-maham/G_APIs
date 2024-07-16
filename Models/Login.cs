﻿using System.ComponentModel.DataAnnotations;

namespace G_APIs.Models;

public class Login
{
    [Required]
    public  string Mobile { get; set; }

    [Required]
    public string Password { get; set; } = "123";

    [Required]
    public string ConfirmPassword { get; set; }

    [Required]
    public string NationalCode { get; set; } = "admin";


    [Required]
    public string ConfirmCode { get; set; }

    [Required]
    public string Captcha { get; set; }

}