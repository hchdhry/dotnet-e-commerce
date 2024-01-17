﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Model;

public class Product
{
    [Key]
    public int Id{get;set;}
    [Required]
    public string? Title {get;set;}

    [Required]
    public string ISBN{get;set;}
    [Required]
    public string Author{get;set;}

    [Required]
    public string Description{get;set;}
   
    [Required]
    [Display(Name = "List price")]
    [Range(1, 1000)]
    public double ListPrice { get; set; }

    [Required]
    [Display(Name = "price 1-50")]
    [Range(1, 1000)]
    public double Price { get; set; }

    [Required]
    [Display(Name = "price 50+")]
    [Range(1, 1000)]
    public double Price50 { get; set; }

    [Required]
    [Display(Name = "price 100+")]
    [Range(1, 1000)]
    public double Price100 { get; set; }

    public int CategoryId{get;set;}
    [ForeignKey("CategoryId")]
    public Category Category {get;set;}
    public string ImageURL{get;set;}
}