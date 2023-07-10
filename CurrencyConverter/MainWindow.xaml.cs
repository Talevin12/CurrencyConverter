using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace CurrencyConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            BindCurrency();
        }

        private void BindCurrency()
        {
            DataTable dtCurrency = new DataTable();
            dtCurrency.Columns.Add("Text");
            dtCurrency.Columns.Add("Value");

            dtCurrency.Rows.Add("--SELECT--", 0);
            dtCurrency.Rows.Add("USD", 0.012);
            dtCurrency.Rows.Add("EUR", 0.011);
            dtCurrency.Rows.Add("JPY", 1.74);
            dtCurrency.Rows.Add("GBP", 0.0095);
            dtCurrency.Rows.Add("CNY", 0.088);
            dtCurrency.Rows.Add("AUD", 0.018);
            dtCurrency.Rows.Add("CAD", 0.016);
            dtCurrency.Rows.Add("CHF", 0.011);
            dtCurrency.Rows.Add("HKD", 0.094);
            dtCurrency.Rows.Add("SGD", 0.016);
            dtCurrency.Rows.Add("SEK", 0.13);
            dtCurrency.Rows.Add("KRW", 15.84);
            dtCurrency.Rows.Add("NOK", 0.13);
            dtCurrency.Rows.Add("NZD", 0.02);
            dtCurrency.Rows.Add("INR", 1);
            dtCurrency.Rows.Add("MXN", 0.21);
            dtCurrency.Rows.Add("TWD", 0.38);
            dtCurrency.Rows.Add("ZAR", 0.23);
            dtCurrency.Rows.Add("BRL", 0.06);
            dtCurrency.Rows.Add("DKK", 0.08);
            dtCurrency.Rows.Add("PLN", 0.05);
            dtCurrency.Rows.Add("THB", 0.43);
            dtCurrency.Rows.Add("ILS", 0.04);
            dtCurrency.Rows.Add("IDR", 183.39);
            dtCurrency.Rows.Add("CZK", 0.27);
            dtCurrency.Rows.Add("ILS", 0.04);


            cmbFromCurrency.ItemsSource = dtCurrency.DefaultView;
            cmbFromCurrency.DisplayMemberPath = "Text";
            cmbFromCurrency.SelectedValuePath = "Value";
            cmbFromCurrency.SelectedIndex = 0;

            cmbToCurrency.ItemsSource = dtCurrency.DefaultView;
            cmbToCurrency.DisplayMemberPath = "Text";
            cmbToCurrency.SelectedValuePath = "Value";
            cmbToCurrency.SelectedIndex = 0;
        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            //Create a variable as ConvertedValue with double data type to store currency converted value
            double ConvertedValue;

            //Check amount textbox is Null or Blank
            if (txtCurrency.Text == null || txtCurrency.Text.Trim() == "")
            {
                //If amount textbox is Null or Blank it will show the below message box   
                MessageBox.Show("Please Enter Currency", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                //After clicking on message box OK sets the Focus on amount textbox
                txtCurrency.Focus();
                return;
            }
            //Else if the currency from is not selected or it is default text --SELECT--
            else if (cmbFromCurrency.SelectedValue == null || cmbFromCurrency.SelectedIndex == 0)
            {
                //It will show the message
                MessageBox.Show("Please Select Currency From", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                //Set focus on From Combobox
                cmbFromCurrency.Focus();
                return;
            }
            //Else if Currency To is not Selected or Select Default Text --SELECT--
            else if (cmbToCurrency.SelectedValue == null || cmbToCurrency.SelectedIndex == 0)
            {
                //It will show the message
                MessageBox.Show("Please Select Currency To", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                //Set focus on To Combobox
                cmbToCurrency.Focus();
                return;
            }

            //If From and To Combobox selected values are same
            if (cmbFromCurrency.Text == cmbToCurrency.Text)
            {
                //The amount textbox value set in ConvertedValue.
                //double.parse is used to convert datatype String To Double.
                //Textbox text have string and ConvertedValue is double datatype
                ConvertedValue = double.Parse(txtCurrency.Text);

                //Show in label converted currency and converted currency name.
                // and ToString("N3") is used to place 000 after after the(.)
                lblCurrency.Content = cmbToCurrency.Text + " " + ConvertedValue.ToString("N3");
            }
            else
            {
                string valueToConvert = txtCurrency.Text;
                if (txtCurrency.Text[txtCurrency.Text.Length-1] == '.')
                {
                    valueToConvert = txtCurrency.Text +'0';
                }

                //Calculation for currency converter is From Currency value multiply(*) 
                // with amount textbox value and then the total is divided(/) with To Currency value
                ConvertedValue = (double.Parse(cmbToCurrency.SelectedValue.ToString()) * 
                    double.Parse(valueToConvert)) / 
                    double.Parse(cmbFromCurrency.SelectedValue.ToString());

                //Show in label converted currency and converted currency name.
                lblCurrency.Content = cmbToCurrency.Text + " " + ConvertedValue.ToString("N3");
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            //ClearControls method  is used to clear all control value
            ClearControls();
        }

        //Allow only the integer value in TextBox
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            string input = ((TextBox)sender).Text + e.Text;

            //Regular Expression to add regex add library using System.Text.RegularExpressions;
            Regex regex = new Regex("^-?\\d+\\.?\\d*$");
            MessageBox.Show(input + " | " + regex.IsMatch(input), "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            e.Handled = !regex.IsMatch(input);        
        }

        //ClearControls used for clear all controls value
        private void ClearControls()
        {
            txtCurrency.Text = string.Empty;
            if (cmbFromCurrency.Items.Count > 0)
                cmbFromCurrency.SelectedIndex = 0;
            if (cmbToCurrency.Items.Count > 0)
                cmbToCurrency.SelectedIndex = 0;
            lblCurrency.Content = string.Empty;
            txtCurrency.Focus();
        }
    }
}
