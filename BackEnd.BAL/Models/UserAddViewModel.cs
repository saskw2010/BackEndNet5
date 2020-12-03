using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.BAL.Models
{
  public class UserAddViewModel
  {
    public string UserName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
    [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
    public string ConfirmPassword { get; set; }
    public string PhoneNumber { get; set; }
    public List<AspNetUsersTypesViewModel> aspNetUsersTypesViewModel { get; set; }
  }
}
