using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerService
{
    public class AddingService
    {
        private CustomerContext _context;

        public AddingService(CustomerContext context)
        {
            _context = context;
        }
        public Customer CreateCustomer(Customer customer)
        {
            var newCustomer=_context.Customers.Add(customer);
            _context.SaveChanges();
            return newCustomer.Entity;
        }
        public void UpdateCustomer(Customer customer)
        {
            var cust = _context.Customers.FirstOrDefault(c => c.CustomerID == customer.CustomerID);
            Console.WriteLine(cust.Firstname);
            if (cust != null)
            {
                cust.Firstname = customer.Firstname;
                cust.Lastname = customer.Lastname;
                cust.Title = customer.Title;
                _context.SaveChanges();
            }
        }
        public IEnumerable<Customer> FindByLastname(string lastName)
        {
            return _context.Customers
                .Where(c => c.Lastname.Contains(lastName))
                .ToList();
        }

        public Customer FindById(int customerID)
        {
            return _context.Customers.FirstOrDefault(c=>c.CustomerID==customerID);
        }
    }
}