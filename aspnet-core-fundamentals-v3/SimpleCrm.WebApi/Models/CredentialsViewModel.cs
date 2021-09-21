using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleCrm.WebApi.Models
{
  public class CredentialsViewModel
  {
    [MaxLength(30), Required]
    public string EmailAddress { get; set; }


    [MaxLength(72), Required]
    public string Password { get; set; }
  }
}
