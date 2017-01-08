using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using InventoryStockUI.DataAccess;
using InventoryStockUI.Models;
using System.Configuration;

namespace InventoryStockUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataContainer _dataContainer;

        public MainWindow()
        {
            InitializeComponent();

            var dataReader = new DataReader();
            _dataContainer = dataReader.ReadData();

            InventoryGrid.ItemsSource = _dataContainer.InventoryItems;
            ExpensesGrid.ItemsSource = _dataContainer.Expenses;

            UpdateForm();
        }

        private void UpdateForm()
        {
            txtbox_StockValue.Text = _dataContainer.InventoryItems.Where(i => i.SoldDate == null && i.RemovedDate == null).Sum(i => i.Valued).ToString();
            txtbox_PastSales.Text = _dataContainer.InventoryItems.Sum(i => i.SoldAt).ToString();
            txtbox_Expenses.Text = _dataContainer.Expenses.Sum(i => i.Cost).ToString();
            txtbox_StockCost.Text = _dataContainer.InventoryItems.Sum(i => i.Purchased).ToString();
            txtbox_Profit.Text = (_dataContainer.InventoryItems.Sum(i => i.SoldAt)
                - _dataContainer.Expenses.Sum(i => i.Cost)
                - _dataContainer.InventoryItems.Sum(i => i.Purchased)
                ).ToString();
        }

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            var dataReader = new DataReader();

            dataReader.SaveData(_dataContainer);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            TabControl tabControl = sender as TabControl; 
            TabItem item = tabControl.SelectedValue as TabItem;
            if (item.Name == "tabHome")
            {
                UpdateForm();
            }
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "d";
            if (e.PropertyType == typeof(System.Decimal))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "C";
        }

        private void Backup_Click(object sender, RoutedEventArgs e)
        {
            string backuplocation = ConfigurationManager.AppSettings["backuplocation"];

            var dataReader = new DataReader();

            try
            {
                dataReader.SaveData(_dataContainer);
                dataReader.BackupData(backuplocation);
                MessageBox.Show("File backed up.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
