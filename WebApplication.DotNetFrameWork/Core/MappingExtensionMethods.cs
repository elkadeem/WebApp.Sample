using WebApplication.DotNetFrameWork.Models;

namespace WebApplication.DotNetFrameWork.Core
{
    public static class MappingExtensionMethods
    {
        public static OutputCustomerDto ToOutputCustomer(this Customer customer)
        {
            if (customer == null)
                return null;

            return new OutputCustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
            };
        }

        public static Customer ToCustomer(this InputCustomerDto customerDto)
        {
            if (customerDto == null)
                return null;

            return new Customer(0, customerDto.Name, customerDto.Email)
            {
                Phone = customerDto.Phone,
            };
        }

    }
}