﻿using System.ComponentModel.DataAnnotations;

namespace Rise.Shared.Dtos;

public class CreatePersonDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required]
    public string Company { get; set; }
}