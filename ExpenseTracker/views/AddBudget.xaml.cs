using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ExpenseTracker.models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBudget : ContentPage
    {
        public AddBudget()
        {
            InitializeComponent();
        }

        async void SubmitBudget_Clicked(System.Object sender, System.EventArgs e)
        {

            var budget = (Budget)BindingContext;

            if (File.Exists(budget.FileName))
            {
                File.Delete(budget.FileName);
            }

            if (string.IsNullOrEmpty(budget.FileName))
            {
                //Create new file for budget
                budget.FileName = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                    $"Budget.budget.txt");
            }
   
            File.WriteAllText(budget.FileName, $"{BudgetInput.Text}");
            await Navigation.PopModalAsync();
        }
    }
}
