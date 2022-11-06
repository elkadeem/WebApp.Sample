using log4net;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using WebApplication.DotNetFrameWork.Core;
using WebApplication.DotNetFrameWork.Infrastructure;

namespace WebApplication.DotNetFrameWork.Controllers
{
    public class CustomersController : ApiController
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(CustomersController));
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
            log.Debug("Getting customers data");
            var customers = await _customersService.Get();
            log.Debug($"Get '{customers.Count}' customers.");
            return Ok(customers);
        }

        // GET: api/Customers/5
        [ResponseType(typeof(OutputCustomerDto))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public async Task<IHttpActionResult> Get(int id)
        {
            log.Debug($"Getting customer with id '{id}'");
            var customer = await _customersService.Get(id);
            if (customer == null)
            {
                log.Error($"No customer with id '{id}'");
                return NotFound();
            }

            log.Debug($"Return customer with id '{id}'");
            return Ok(customer);
        }

        // POST: api/Customers
        [ResponseType(typeof(IEnumerable<InputCustomerDto>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ModelStateDictionary))]
        public async Task<IHttpActionResult> Post(InputCustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                log.Error("Model is not valid");
                return BadRequest(ModelState);
            }

            log.Debug("Adding customer");
            await _customersService.Add(customerDto);
            return CreatedAtRoute("DefaultApi", new { customerDto.Id }, customerDto);
        }

        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(HttpError))]
        public async Task<IHttpActionResult> Put(int id, InputCustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                log.Error("Model is not valid");
                return BadRequest(ModelState);
            }

            if (id != customerDto.Id)
            {
                return BadRequest();
            }

            try
            {
                log.Debug($"Updating customer with id '{id}'");
                await _customersService.Update(id, customerDto);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (ArgumentException ex) when (ex.ParamName == nameof(id))
            {
                log.Error($"No customer with id '{id}' to update.");
                return NotFound();
            }
        }

    }
}
