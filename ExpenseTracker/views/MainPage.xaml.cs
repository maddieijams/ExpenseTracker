using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.models;
using ExpenseTracker.views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker
{

    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public int RemainingBudget { get; set; }
        public MainPage()
        {
            InitializeComponent();
            BudgetLabel.Text = RemainingBudget.ToString();
        }

        protected override void OnAppearing()
        {
            var budgetFile = Directory.EnumerateFiles(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), "Budget.budget.txt").FirstOrDefault();
            //File.Delete(budgetFile);
            if (File.Exists(budgetFile))
            {
                //AddBudgetButton.IsVisible = false;
                //BudgetLabel.IsVisible = true;

                Debug.WriteLine(budgetFile, File.ReadAllText(budgetFile));
                if (Int32.TryParse(File.ReadAllText(budgetFile), out int value))
                {
                    Debug.WriteLine("yes");
                    RemainingBudget = value;
                } else
                {
                    RemainingBudget = 0;
                }
            } else
            {
                //BudgetLabel.IsVisible = false;
                //AddBudgetButton.IsVisible = true;

                RemainingBudget = 0;
            }
            Debug.WriteLine("remaining", RemainingBudget);


            var expenses = new List<Expense>();
            var files = Directory.EnumerateFiles(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), "*.expenses.txt");
   
                foreach (var filename in files)
            {
                //File.Delete(filename);
                var split = File.ReadAllText(filename).Split('_');
                var amount = Int32.TryParse(split[0], out int amt) ? amt : 0;

                var note = new Expense
                {

                    Name = split[1],
                    Amount = amount,
                    Date = File.GetCreationTime(filename),
                    FileName = filename
                };

                expenses.Add(note);
            }
            ExpenseListView.ItemsSource = expenses.OrderByDescending(n => n.Date);

            var expensesSum = expenses.Select(exp => exp.Amount).Sum();
            
            if (RemainingBudget > 0 && expensesSum > 0) RemainingBudget -= expensesSum;
            BudgetLabel.Text = $"Remaining Budget: {RemainingBudget}";

        }

        async void AddBudgetClicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new AddBudget
            {
                BindingContext = new Budget()
            });
        }

        async void AddExpenseButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new AddExpense
            {
                BindingContext = new Expense()
            });
        }
    }
}
