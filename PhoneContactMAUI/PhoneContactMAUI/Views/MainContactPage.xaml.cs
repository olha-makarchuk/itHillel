using PhoneContactMAUI.DAL.Data;
using PhoneContactMAUI.ViewModels;

namespace PhoneContactMAUI.Views;

public partial class MainContactPage : ContentView
{
    private readonly AppDbContext _context;

    public MainContactPage(AppDbContext context)
    {
		InitializeComponent();
        _context = context;
        BindingContext = new MainContactPageViewModel(context);
    }
}