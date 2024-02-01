using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace MVC.Model{
public class AppUser:IdentityUser
{
[Required]
public string name{get;set;}
public string? City {get;set;}
public string? Postcode { get; set; }
public string? County { get; set; }
public string? Address { get; set; }


}
}
