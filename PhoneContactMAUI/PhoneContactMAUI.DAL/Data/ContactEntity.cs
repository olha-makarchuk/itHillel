using Microsoft.EntityFrameworkCore;
using PhoneContactMAUI.DAL.Models;

namespace PhoneContactMAUI.DAL.Data
{
	public class ContactEntity : IDataHelper<PhoneContact>
	{
		private readonly AppDbContext _context;

		public ContactEntity(AppDbContext context)
		{
			_context = context;
		}

		public async Task AddDataAsync(PhoneContact table)
		{
			await _context.ContactsList.AddAsync(table);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteDataAsync(PhoneContact table)
		{
			_context.ContactsList.Remove(table);
			await _context.SaveChangesAsync();
		}

		public async Task<PhoneContact> FindAsync(int Id)
		{
			return await _context.ContactsList.FindAsync(Id);
		}

		public async Task<List<PhoneContact>> GetAllAsync()
		{
			return await _context.ContactsList.ToListAsync();
		}

		public async Task UpdateDataAsync(PhoneContact table)
		{
			_context.ContactsList.Update(table);
			await _context.SaveChangesAsync();
		}
	}
}
