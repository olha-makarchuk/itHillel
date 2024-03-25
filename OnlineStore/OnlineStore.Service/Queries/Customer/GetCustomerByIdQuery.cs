using Microsoft.EntityFrameworkCore;
using OnlineStore.Contract.Response;
using OnlineStore.Data.Context;
using OnlineStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Queries.Customer
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<int, CustomerResponse>
    {
        private readonly OnlineStoreContext _context;

        public GetCustomerByIdQueryHandler(OnlineStoreContext context)
        {
            _context = context;
        }

        public async Task<CustomerResponse> Handle(int customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Customers
                .AsNoTracking()
                .Where(x => x.IdCustomer == customerId)
                .Select(x => new CustomerResponse
                {
                    IdCustomer = x.IdCustomer,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    Age = x.Age
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
