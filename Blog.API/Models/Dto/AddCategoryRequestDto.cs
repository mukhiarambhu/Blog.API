﻿using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models.Dto
{
    public class AddCategoryRequestDto
    {
        [Required(ErrorMessage ="Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "UrlHandle is Required")]
        public string UrlHandle { get; set; }
    }
}
