using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
            BindingContext = this;
            

        }

        protected override void OnStart()
        {
            var files = Directory.EnumerateFiles(Environment.GetFolderPath(
        Environment.SpecialFolder.LocalApplicationData), "*.expenses.txt");
            foreach (var file in files)
            {
                File.Delete(file);
            }
            var budgetFile = Directory.EnumerateFiles(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), "Budget.budget.txt").FirstOrDefault();
          if(budgetFile != null) File.Delete(budgetFile);
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
