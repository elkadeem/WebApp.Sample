using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.DotNetFrameWork.Infrastructure;

namespace WebApplication.DotNetFrameWork.Core
{
    public class CustomersService
    {
        private readonly AppDbContext _dbContext;

        public CustomersService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OutputCustomerDto>> Get()
        {
            var items = await _dbContext.Customers.ToListAsync();
            return items.Select(c => c.ToOutputCustomer()).ToList();
        }

        public async Task<OutputCustomerDto> Get(int id)
        {
            var item = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (item == null)
                return null;

            return item.ToOutputCustomer();
        }
        public async Task Add(InputCustomerDto customerDto)
        {
            if (customerDto is null)
            {
                throw new ArgumentNullException(nameof(customerDto));
            }

            var newCustomer = customerDto.ToCustomer();

            //Add businsess validation her for entities
            _dbContext.Customers.Add(newCustomer);
            await _dbContext.SaveChangesAsync();
            customerDto.Id = newCustomer.Id;
        }

        public async Task Update(int id, InputCustomerDto customerDto)
        {
            if (customerDto is null)
            {
                throw new ArgumentNullException(nameof(customerDto));
            }

            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (customer == null)
                throw new ArgumentException("Customer is not exist.", nameof(id));

            customer.Name = customerDto.Name;
            customer.Email = customerDto.Email;
            customer.Phone = customer.Phone;

            //To throw exception
            customer.Name = String.Empty;
            //Add businsess validation her for entities
            
            await _dbContext.SaveChangesAsync();            
        }
    }
}