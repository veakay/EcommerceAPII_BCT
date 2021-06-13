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
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private MyDBContext myDBContext;
        public CustomerController(MyDBContext context)
        {
            myDBContext = context;
        }

        [HttpGet("getallcustomerdetails")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<IEnumerable<customerDto>> Get()
        {
            var posts = myDBContext.Customers.Select(x => new customerDto()
            {
                CustomerId = x.CustomerId,
                Email = x.Email,
                Name = x.Name,
                PhoneNumber = x.PhoneNumber,
                BillingAddress = x.BillingAddress,
                DefaultAddress = x.DefaultAddress,
                Country = x.Country
            }).ToList();
            return Ok(posts);
        }

        [HttpGet("getcustomerbyid/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<IEnumerable<customerDto>> GetbyId(int id)
        {
            var getbyid = myDBContext.Customers.Select(x => new customerDto()
            {
                CustomerId = x.CustomerId,
                Email = x.Email,
                Name = x.Name,
                PhoneNumber = x.PhoneNumber,
                BillingAddress = x.BillingAddress,
                DefaultAddress = x.DefaultAddress,
                Country = x.Country
            }).Where(x => x.CustomerId == id);
            return Ok(getbyid);
        }
        [HttpPost("adddetails")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post([FromBody] Customer customer)
        {
            await myDBContext.Customers.AddAsync(customer);
            await myDBContext.SaveChangesAsync();
            return Ok(customer);
        }
        [HttpPut("editcustomer/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(int id, [FromBody] Customer customer)
        {
            customer.CustomerId = id;
            myDBContext.Entry(customer).State = EntityState.Modified;
            await myDBContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("deleteproduct/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await myDBContext.Customers.AnyAsync(x => x.CustomerId == id);
            if (!exists)
            {
                return NotFound();
            }

            myDBContext.Remove(new Customer() { CustomerId = id });
            await myDBContext.SaveChangesAsync();

            return NoContent();
        }
    }
}