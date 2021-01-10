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
    public partial class ListPage : ContentPage
    {
        public ListPage()
        {
            InitializeComponent();
        }
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var alist = (AccountList)BindingContext;
            alist.Date = DateTime.UtcNow;
            await App.Database.SaveAccountListAsync(alist);
            await Navigation.PopAsync();
        }
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var alist = (AccountList)BindingContext;
            await App.Database.DeleteAccountListAsync(alist);
            await Navigation.PopAsync();
        }
        async void OnChooseButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PasswordPage((AccountList)
           this.BindingContext)
            {
                BindingContext = new Password()
            });

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var shopl = (AccountList)BindingContext;

            listView.ItemsSource = await App.Database.GetListPasswordsAsync(shopl.ID);
        }
    }
}