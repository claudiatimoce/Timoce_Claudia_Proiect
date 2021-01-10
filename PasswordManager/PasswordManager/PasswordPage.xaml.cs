using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PasswordManager.Models;

namespace PasswordManager
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PasswordPage : ContentPage
    {
        AccountList al;
        public PasswordPage(AccountList alist)
        {
            InitializeComponent();
            al = alist;
        }
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var password = (Password)BindingContext;
            await App.Database.SavePasswordAsync(password);
            listView.ItemsSource = await App.Database.GetPasswordsAsync();
        }
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var password = (Password)BindingContext;
            await App.Database.DeletePasswordAsync(password);
            listView.ItemsSource = await App.Database.GetPasswordsAsync();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetPasswordsAsync();
        }
        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            Password p;
            if (e.SelectedItem != null)
            {
                p = e.SelectedItem as Password;
                var lp = new ListPassword()
                {
                    AccountListID = al.ID,
                    PasswordID = p.ID
                };
                await App.Database.SaveListPasswordAsync(lp);
                p.ListPasswords = new List<ListPassword> { lp };

                await Navigation.PopAsync();
            }
        }
    }
}