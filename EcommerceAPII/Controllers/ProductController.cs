using EcommerceAPI.DTO;
using EcommerceAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private MyDBContext myDBContext;
        public ProductController(MyDBContext context)
        {
            myDBContext = context;
        }
        [HttpGet("getallproduct")]

        public ActionResult<IEnumerable<ProductDto>> Get()
        {
            var posts = myDBContext.Products.Select(p => new ProductDto()
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
            });
            return Ok(posts);
        }

        [HttpGet("getproductbyid/{id:int}")]
        public ActionResult<IEnumerable<ProductDto>> GetbyId(int id)
        {
            var getbyid = myDBContext.Products.Select(p => new ProductDto()
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
            }).Where(x => x.ProductId == id);
            return Ok(getbyid);
        }

        [HttpPost("addproduct")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post([FromBody] Product product)
        {
            await myDBContext.Products.AddAsync(product);
            await myDBContext.SaveChangesAsync();
            return Ok(product);
        }
        [HttpPut("editproduct/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(int id, [FromBody] Product product)
        {
            product.ProductId = id;
            myDBContext.Entry(product).State = EntityState.Modified;
            await myDBContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("deleteproduct/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await myDBContext.Products.AnyAsync(x => x.ProductId == id);
            if (!exists)
            {
                return NotFound();
            }

            myDBContext.Remove(new Product() { ProductId = id });
            await myDBContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("minmixfilter/{minn}/{maxm}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> getminmax(int minn, int maxm)
        {
            var prod = await myDBContext.Products.Select(p => new ProductDto()
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
            }).Where(x => x.Price <= maxm & x.Price >= minn).OrderBy(x => x.Price).ToListAsync();
            if (!prod.Any())
            {
                return NotFound();
            }
            return prod;

        }


    }
}