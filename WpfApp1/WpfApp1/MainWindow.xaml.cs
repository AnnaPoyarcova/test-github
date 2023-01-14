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
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TradeEntities tradeEntities = new TradeEntities();
        DataTable dt = new DataTable();
        DataTable filters = new DataTable();
        DataTable filterandsearch = new DataTable();
        string connectionString;
        public MainWindow()
        {
            InitializeComponent();
            var query =
              from Products in tradeEntities.Products
              select new { Products.ProductArticleNumber, Products.ProductName, Products.ProductDescription, Products.ProductCategory, Products.ProductPhoto, Products.ProductManufacturer, Products.ProductCost, Products.ProductDiscountAmount, Products.ProductQuantityInStock, Products.ProductStatus };
            datagrid.ItemsSource = query.ToList();
            /* dt = Select("SELECT COUNT(*) FROM Product");
             string count1 = Convert.ToString(datagrid.Items.Count);
             string count2 = dt.Rows[0][0].ToString();
             Counts.Content = count1 + " из " + count2;*/
            filters = Select("SELECT ProductManufacturer FROM Products GROUP BY ProductManufacturer", dataTable);
            filter.Items.Add("Все производители");
            filter.SelectedIndex = 0;
            sort.Items.Add("Без сортировки");
            sort.Items.Add("Цена по возрастанию");
            sort.Items.Add("Цена по убыванию");
            sort.SelectedIndex = 0;
            for (int i = 1; i < filters.Rows.Count; i++)
            {
                filter.Items.Add(filters.Rows[i][0].ToString());
            }
           

            
        }
        DataTable dataTable = new DataTable("dataBase");
        public DataTable Select(string selectSQL)
        {
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.TradeConnectionString1"].ConnectionString;
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = selectSQL;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception)
            {
                MessageBox.Show("");
                return null;
            }
        }
    }
}