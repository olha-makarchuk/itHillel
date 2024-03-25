using Microsoft.EntityFrameworkCore;
using OnlineStore.Contract.Response;
using OnlineStore.Data.Context;
using OnlineStore.Data.Entities;
using OnlineStore.Service.Commands.CategoryCommands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Commands.CustomerCommand
{
    public class UpsertCustomerCommand
    {
        public int IdCustomer { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public Customer UpsertCustomer()
        {
            var customer = new Customer
            {
                IdCustomer = IdCustomer,
                FirstName = FirstName,
                MiddleName = MiddleName,
                LastName = LastName,
                Age = Age
            };

            return customer;
        }
    }

    public class UpserCustomerCommandHandler :  IRequestHandler<UpsertCustomerCommand, CustomerResponse>
    {
        private readonly OnlineStoreContext _context;
        public UpserCustomerCommandHandler(OnlineStoreContext context)
        {
            _context = context;
        }

        public async Task<CustomerResponse> Handle(UpsertCustomerCommand request, CancellationToken cancellationToken = default)
        {
            var customer = await GetCustomerAsync(request.IdCustomer, cancellationToken);

            if (customer == null)
            {
                customer = request.UpsertCustomer();
                await _context.AddAsync(customer, cancellationToken);
            }

            customer.IdCustomer = request.IdCustomer;
            customer.FirstName = request.FirstName;
            customer.MiddleName = request.MiddleName;
            customer.LastName = request.LastName;
            customer.Age = request.Age;

            await _context.SaveChangesAsync(cancellationToken);

            return new CustomerResponse
            {
                IdCustomer = customer.IdCustomer,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                Age = request.Age
            };
        }

        private async Task<Customer> GetCustomerAsync(int customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Customers.SingleOrDefaultAsync(x => x.IdCustomer == customerId, cancellationToken);
        }
    }
}
