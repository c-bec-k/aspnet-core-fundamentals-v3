﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimpleCrm.Web.Models.Account
{
  public class LoginUserViewModel
  {
    [Required(), DisplayName("Email"), MaxLength(256), DataType(DataType.EmailAddress)]
    public string UserName { get; set; }

    [Required(), DataType(DataType.Password)]
    public string Password { get; set; }

    public string ReturnUrl { get; set; }

    [DisplayName("Remember Me?")]
    public bool RememberMe { get; set; }
  }
}
