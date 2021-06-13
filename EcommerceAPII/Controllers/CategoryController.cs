using EcommerceAPI.DTO;
using EcommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private MyDBContext myDBContext;
        public CategoryController(MyDBContext context)
        {
            myDBContext = context;
        }

        [HttpGet("findcategoryid/{id}")]
        public ActionResult<IEnumerable<ProductDto>> GetProductbyCat(int id)
        {
            var id_product_cat = myDBContext.Products.Select(p => new ProductDto()
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Price = p.Price,
                DescriptionP = p.DescriptionP,
                Image = p.Image,
                Stock = p.Stock,
                CategoryName = p.Catergory.CategoryName,
                CatergoryId = p.CatergoryId,
                Sku = p.Sku
            }).Where(s => s.CatergoryId == id).ToList();

            return (id_product_cat);
        }
        [HttpGet("getallcategory")]

        public ActionResult<IEnumerable<CategoryDto>> Get()
        {
            var posts = myDBContext.Categories.Select(p => new CategoryDto() { CatergoryId = p.CatergoryId, CategoryName = p.CategoryName, Description = p.Description }).ToList();
            return Ok(posts);
        }
        [HttpPost("addcategory")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post([FromBody] Category category)
        {
            await myDBContext.Categories.AddAsync(category);
            await myDBContext.SaveChangesAsync();
            return Ok(category);
        }
        [HttpPut("editcategory/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(int id, [FromBody] Category category)
        {
            category.CatergoryId = id;
            myDBContext.Entry(category).State = EntityState.Modified;
            await myDBContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("deletecategory/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await myDBContext.Categories.AnyAsync(x => x.CatergoryId == id);
            if (!exists)
            {
                return NotFound();
            }

            myDBContext.Remove(new Category() { CatergoryId = id });
            await myDBContext.SaveChangesAsync();

            return NoContent();
        }
    }
}