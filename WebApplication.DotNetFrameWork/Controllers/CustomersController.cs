using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication.DotNetFrameWork.Core;
using WebApplication.DotNetFrameWork.Infrastructure;

namespace WebApplication.DotNetFrameWork.Controllers
{
    public class CustomersController : ApiController
    {
        private readonly CustomersService _customersService;
        public CustomersController()
        {
            AppDbContext appDbContext = new AppDbContext();
            _customersService = new CustomersService(appDbContext);
        }


        // GET: api/Customers
        [ResponseType(typeof(IEnumerable<OutputCustomerDto>))]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var customers = await _customersService.Get();
                return Ok(customers);
            }
            catch (Exception ex)
            {

                return InternalServerError();
            }
        }

        // GET: api/Customers/5
        [ResponseType(typeof(OutputCustomerDto))]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var customer = await _customersService.Get(id);
                if (customer == null)
                    return NotFound();

                return Ok(customer);
            }            
            catch (Exception)
            {

                return InternalServerError();
            }
        }

        // POST: api/Customers
        [ResponseType(typeof(IEnumerable<InputCustomerDto>))]
        public async Task<IHttpActionResult> Post(InputCustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _customersService.Add(customerDto);
                return CreatedAtRoute("DefaultApi", new { customerDto.Id}, customerDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT: api/Customers/5
        [ResponseType(typeof(IEnumerable<InputCustomerDto>))]        
        public async Task<IHttpActionResult> Put(int id, InputCustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _customersService.Update(id, customerDto);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
