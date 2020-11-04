using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.BAL.Models
{
  public class UserRegisterationRequest
  {  
    public string UserName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
    [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
    public string ConfirmPassword { get; set; }
    public string PhoneNumber { get; set; }
    public string Roles { get; set; }
  }
}
