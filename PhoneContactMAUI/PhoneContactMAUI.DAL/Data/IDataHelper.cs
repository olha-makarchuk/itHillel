namespace PhoneContactMAUI.DAL.Data
{
	public interface IDataHelper<TableEntity>
	{
		Task<List<TableEntity>> GetAllAsync();
		Task<TableEntity> FindAsync(int Id);
		Task AddDataAsync(TableEntity table);
		Task DeleteDataAsync(TableEntity table);
		Task UpdateDataAsync(TableEntity table);
	}
}
