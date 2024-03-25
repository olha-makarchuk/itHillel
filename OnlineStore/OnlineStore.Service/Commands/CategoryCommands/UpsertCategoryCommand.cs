using Microsoft.EntityFrameworkCore;
using OnlineStore.Contract.Response;
using OnlineStore.Data.Context;
using OnlineStore.Data.Entities;

namespace OnlineStore.Service.Commands.CategoryCommands
{
    public class UpsertCategoryCommand
    {
        public int IdCategory { get; set; }

        public string Name { get; set; }

        public Category UpsertCategory()
        {
            var category = new Category
            {
                IdCategory = IdCategory,
                Name = Name
            };

            return category;
        }
    }

    public class UpserCategotyCommandHandler : IRequestHandler<UpsertCategoryCommand, CategoryResponse>
    {
        private readonly OnlineStoreContext _context;
        public UpserCategotyCommandHandler(OnlineStoreContext context)
        {
            _context = context;
        }

        public async Task<CategoryResponse> Handle(UpsertCategoryCommand request, CancellationToken cancellationToken = default)
        {
            var category = await GetCategoryAsync(request.IdCategory, cancellationToken);

            if (category == null)
            {
                category = request.UpsertCategory();
                await _context.AddAsync(category, cancellationToken);
            }

            category.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return new CategoryResponse
            {
                IdCategory = category.IdCategory,
                Name = request.Name,
            };
        }

        private async Task<Category> GetCategoryAsync(int categotyId, CancellationToken cancellationToken = default)
        {
            return await _context.Categories.SingleOrDefaultAsync(x => x.IdCategory == categotyId, cancellationToken);
        }
    }
}   