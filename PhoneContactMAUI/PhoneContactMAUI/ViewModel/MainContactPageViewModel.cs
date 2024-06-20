using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PhoneContactMAUI.DAL.Models;
using PhoneContactMAUI.DAL.Data;
using System.Collections.ObjectModel;

namespace PhoneContactMAUI.ViewModels
{
    public partial class MainContactPageViewModel : ObservableObject
    {
        [ObservableProperty]
        string contactName;

        [ObservableProperty]
        string contactEmail;

        [ObservableProperty]
        int contactPhoneNumber;

        PhoneContact selectedContact;

        [ObservableProperty]
        ObservableCollection<PhoneContact> contactCollection;

        private ContactEntity DataHelper;

        private readonly AppDbContext _context;

        public MainContactPageViewModel(AppDbContext context)
        {
            _context = context;
            contactCollection = new ObservableCollection<PhoneContact>();
            DataHelper = new ContactEntity(_context);
            LoadData();
        }

        public PhoneContact SelectedContact
        {
            get => selectedContact;
            set
            {
                if (selectedContact != value)
                {
                    selectedContact = value;
                    ContactName = selectedContact.Name;
                    ContactEmail = selectedContact.Email;
                    ContactPhoneNumber = selectedContact.PhoneNumber;
                    OnPropertyChanged();
                }
            }
        }

        [RelayCommand]
        async void AddContact()
        {
            var contact = new PhoneContact
            {
                Name = ContactName,
                Email = ContactEmail,
                PhoneNumber = ContactPhoneNumber,
            };
            await DataHelper.AddDataAsync(contact);
            LoadData();

            ContactName = string.Empty;
            ContactEmail = string.Empty;
            ContactPhoneNumber = 0;
        }

        [RelayCommand]
        async void DeleteContact()
        {
            if (selectedContact != null)
            {
                await DataHelper.DeleteDataAsync(selectedContact);
                LoadData();
                ContactName = string.Empty;
                ContactEmail = string.Empty;
                ContactPhoneNumber = 0;
            }
        }

        [RelayCommand]
        async void EditContact()
        {
            if (selectedContact != null)
            {
                var newContact = new PhoneContact
                {
                    Id = SelectedContact.Id,
                    Name = ContactName,
                    Email = ContactEmail,
                    PhoneNumber = ContactPhoneNumber,
                };
                await DataHelper.UpdateDataAsync(newContact);
                LoadData();
            }
        }

        public async void LoadData()
        {
            ContactCollection.Clear();
            foreach (var contact in await DataHelper.GetAllAsync())
            {
                ContactCollection.Add(contact);
            }
        }
    }
}
