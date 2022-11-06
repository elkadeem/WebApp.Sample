using System;

namespace WebApplication.DotNetFrameWork.Models
{
    public class Customer
    {
        private Customer()
        {

        }
        public Customer(int id, string name, string email)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}