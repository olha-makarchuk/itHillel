using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Session> Sessions { get; set; }
        DbSet<Movie> Movies { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
