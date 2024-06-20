using PhoneContactMAUI.DAL.Data;

namespace PhoneContactMAUI
{
    public partial class MainPage : ContentPage
    {
        private readonly AppDbContext _context;


        public MainPage()
        {
            InitializeComponent();
            _context = MauiProgram.ServiceProvider.GetService<AppDbContext>();
            Container.Content = new Views.MainContactPage(_context);
        }
    }
}
