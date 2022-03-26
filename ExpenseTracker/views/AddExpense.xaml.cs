using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ExpenseTracker.models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker
{
 
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddExpense : ContentPage
    {
        public AddExpense()
        {
            InitializeComponent();
        }

   

        private async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var expense = (Expense)BindingContext;
            if (string.IsNullOrEmpty(expense.FileName))
            {
                //Create new file for expense
                expense.FileName = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                    $"{Path.GetRandomFileName()}.expenses.txt");
            }
            File.WriteAllText(expense.FileName, $"{ExpenseAmount.Text}_{ExpenseName.Text}");
            await Navigation.PopModalAsync();
        }
    }
}
