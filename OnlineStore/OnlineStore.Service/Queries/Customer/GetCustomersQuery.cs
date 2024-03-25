using Microsoft.EntityFrameworkCore;
using OnlineStore.Contract.Response;
using OnlineStore.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Queries.Customer
{
    public class GetCustomersQueryHandler :
        IRequestHandler<IList<CustomerResponse>>
    {
        private readonly OnlineStoreContext _context;
        public GetCustomersQueryHandler(OnlineStoreContext context)
        {
            _context = context;
        }

        public async Task<IList<CustomerResponse>> Handle(CancellationToken cancellationToken = default)
        {
            return await _context.Customers
                .AsNoTracking()
                .Select(x => new CustomerResponse
                {
                    IdCustomer = x.IdCustomer,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    Age = x.Age
                })
                .OrderByDescending(x => x.IdCustomer)
                .ToListAsync(cancellationToken);
        }
    }
}
