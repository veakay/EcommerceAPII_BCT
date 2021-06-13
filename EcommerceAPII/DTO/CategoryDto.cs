using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using EcommerceAPI.Models;

namespace EcommerceAPI.DTO
{
    public class CategoryDto
    {
        public int CatergoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
