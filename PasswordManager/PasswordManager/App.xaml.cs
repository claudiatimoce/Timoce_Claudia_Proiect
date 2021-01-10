using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PasswordManager.Data;
using System.IO;

namespace PasswordManager
{
    public partial class App : Application
    {
        static PasswordManagerDataBase database;
        public static PasswordManagerDataBase Database
        {
            get
            {
                if (database == null)
                {
                    database = new
                   PasswordManagerDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.
                   LocalApplicationData), "PasswordManager.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ListEntryPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
