using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Director> Directors { get; set; }
        DbSet<Genre> Genres { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
