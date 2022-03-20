using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace PenInventoryGUI.MVM.View
{
    /// <summary>
    /// Interaction logic for AddView.xaml
    /// </summary>
    public partial class AddView : UserControl
    {
        DataTable LedgerDT = new DataTable();
       
        public AddView()
        {
            InitializeComponent();
        }

        private void LedgerDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            string server = "localhost";
            string database = "inventory_sys";
            string uid = "root";
            string pass = "123";
            string connectionString = "Server=" + server + ";" + "Database=" + database + ";" + "UID=" + uid + ";" + "Password=" + pass + ";";
            MySqlConnection conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();
                MySqlDataAdapter sqlDa = new MySqlDataAdapter("SELECT pen_id as 'Pen ID', pen_brand as 'Pen Brand', pen_model as 'Pen Model',pen_color as 'Pen Color',pen_quantity as 'Pen Quantity',pen_size as 'Pen Size',pen_container as 'Pen Container', pen_datetime as 'Entry DateTime', pen_status as 'Status' FROM pen_description", conn);
                sqlDa.Fill(LedgerDT);
                LedgerDataGrid.ItemsSource = LedgerDT.DefaultView;
                sqlDa.Update(LedgerDT);
                conn.Close();
            }
        }
        private void LedgerDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if(row_selected != null)
            {
                txtAdd_ID.Text = row_selected["Pen ID"].ToString();
                txtAdd_Brand.Text = row_selected["Pen Brand"].ToString();
                txtAdd_Model.Text = row_selected["Pen Model"].ToString();
                txtAdd_Color.Text = row_selected["Pen Color"].ToString();
                txtAdd_Quantity.Text = row_selected["Pen Quantity"].ToString();
                txtAdd_Size.Text = row_selected["Pen Size"].ToString();
                txtAdd_Container.Text = row_selected["Pen Container"].ToString();
                txtAdd_DateTime.Text = row_selected["Entry DateTime"].ToString();
                txtAdd_Status.Text = row_selected["Status"].ToString();
            }
        }
       
        private void btn_LedgerAddConfirm_Click(object sender, RoutedEventArgs e)
        {
            string server = "localhost";
            string database = "inventory_sys";
            string uid = "root";
            string pass = "123";
            string connectionString = "Server=" + server + ";" + "Database=" + database + ";" + "UID=" + uid + ";" + "Password=" + pass + ";";
            MySqlConnection conn = new MySqlConnection(connectionString);
            txtAdd_DateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                        string insertQuery = "INSERT INTO pen_description(pen_id,pen_brand,pen_model,pen_color,pen_quantity,pen_size,pen_container,pen_datetime,pen_status) VALUES ('" + txtAdd_ID.Text + "','" + txtAdd_Brand.Text + "','" + txtAdd_Model.Text + "','" + txtAdd_Color.Text + "','" + txtAdd_Quantity.Text + "','" + txtAdd_Size.Text + "','" + txtAdd_Container.Text + "','" + txtAdd_DateTime.Text + "','" + txtAdd_Status.Text + "')";
                        MySqlCommand cmd = new MySqlCommand(insertQuery, conn);
                    try
                    {
                        conn.Open();
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            MySqlDataAdapter sqlDa = new MySqlDataAdapter("SELECT pen_id as 'Pen ID', pen_brand as 'Pen Brand', pen_model as 'Pen Model',pen_color as 'Pen Color',pen_quantity as 'Pen Quantity',pen_size as 'Pen Size',pen_container as 'Pen Container', pen_datetime as 'Entry DateTime', pen_status as 'Status' FROM pen_description", conn);

                            MessageBox.Show("Record Added");
                            LedgerDT.Clear();
                            sqlDa.Fill(LedgerDT);
                            LedgerDataGrid.ItemsSource = LedgerDT.DefaultView;
                            sqlDa.Update(LedgerDT);
                            LedgerDT.DefaultView.Sort = "Entry DateTime";
                            txtAdd_ID.Text = string.Empty;
                            txtAdd_Brand.Text = string.Empty;
                            txtAdd_Model.Text = string.Empty;
                            txtAdd_Color.Text = string.Empty;
                            txtAdd_Quantity.Text = string.Empty;
                            txtAdd_Size.Text = string.Empty;
                            txtAdd_Container.Text = string.Empty;
                            txtAdd_DateTime.Text = string.Empty;
                            txtAdd_Status.Text = string.Empty;
                        }
                        else
                            MessageBox.Show("Please Check again");

                        conn.Close();
                    }
                    catch (MySqlException)
                    {
                        MessageBox.Show("Please Fill the Required Fields");
                    }
   
                }
            }
            catch (MySqlException err)
            {
                MessageBox.Show(err.Message.ToString());
            }
        }
        private void btn_LedgerRemoveConfirm_Click(object sender, RoutedEventArgs e)
        {
            string server = "localhost";
            string database = "inventory_sys";
            string uid = "root";
            string pass = "123";
            string connectionString = "Server=" + server + ";" + "Database=" + database + ";" + "UID=" + uid + ";" + "Password=" + pass + ";";
            MySqlConnection conn = new MySqlConnection(connectionString);

            MessageBoxResult result = MessageBox.Show("Do you want to Continue?", "Remove Record", MessageBoxButton.OKCancel);
            if(result== MessageBoxResult.OK)
            {
                try
                {
                    conn.Open();
                    string insertQuery = "DELETE FROM pen_description WHERE pen_ID ='" + txtAdd_ID.Text + "';";
                    MySqlCommand cmd = new MySqlCommand(insertQuery, conn);
                    cmd.ExecuteNonQuery();
                    LedgerDT.Rows.RemoveAt(LedgerDataGrid.SelectedIndex);
                    conn.Close();
                    txtAdd_ID.Text = string.Empty;
                    txtAdd_Brand.Text = string.Empty;
                    txtAdd_Model.Text = string.Empty;
                    txtAdd_Color.Text = string.Empty;
                    txtAdd_Quantity.Text = string.Empty;
                    txtAdd_Size.Text = string.Empty;
                    txtAdd_Container.Text = string.Empty;
                    txtAdd_DateTime.Text = string.Empty;
                    txtAdd_Status.Text = string.Empty;
                }
                catch
                {
                    MessageBox.Show("Please Select the Desired Record");
                }
            }
            
        }
        private void btn_LedgerClear_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to Continue?", "Clear Ledger", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                LedgerDT.Clear();
                txtAdd_ID.Text = string.Empty;
                txtAdd_Brand.Text = string.Empty;
                txtAdd_Model.Text = string.Empty;
                txtAdd_Color.Text = string.Empty;
                txtAdd_Quantity.Text = string.Empty;
                txtAdd_Size.Text = string.Empty;
                txtAdd_Container.Text = string.Empty;
                txtAdd_DateTime.Text = string.Empty;
                txtAdd_Status.Text = string.Empty;
            }
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            string server = "localhost";
            string database = "inventory_sys";
            string uid = "root";
            string pass = "123";
            string connectionString = "Server=" + server + ";" + "Database=" + database + ";" + "UID=" + uid + ";" + "Password=" + pass + ";";
            MySqlConnection conn = new MySqlConnection(connectionString);

            MessageBoxResult result = MessageBox.Show("Do you want to Continue?", "Update Record", MessageBoxButton.OKCancel);
            
             if (result == MessageBoxResult.OK)
                {
                 try
                  {
                    conn.Open();
                    string insertQuery = "UPDATE pen_description SET pen_id='" + txtAdd_ID.Text + "',pen_brand='" + txtAdd_Brand.Text + "',pen_model='" + txtAdd_Model.Text + "',pen_color='" + txtAdd_Color.Text + "',pen_Quantity='" + txtAdd_Quantity.Text + "',pen_size='" + txtAdd_Size.Text + "',pen_container='" + txtAdd_Container.Text + "' ,pen_status='" + txtAdd_Status.Text + "' WHERE pen_id='" + txtAdd_ID.Text + "';";
                    MySqlCommand cmd = new MySqlCommand(insertQuery, conn);
                    cmd.ExecuteNonQuery();
                    MySqlDataAdapter sqlDa = new MySqlDataAdapter("SELECT pen_id as 'Pen ID', pen_brand as 'Pen Brand', pen_model as 'Pen Model',pen_color as 'Pen Color',pen_quantity as 'Pen Quantity',pen_size as 'Pen Size',pen_container as 'Pen Container', pen_datetime as 'Entry DateTime' , pen_status as 'Status' FROM pen_description", conn);
                    LedgerDT.Clear();
                    sqlDa.Fill(LedgerDT);
                    LedgerDataGrid.ItemsSource = LedgerDT.DefaultView;
                    sqlDa.Update(LedgerDT);

                    txtAdd_ID.Text = string.Empty;
                    txtAdd_Brand.Text = string.Empty;
                    txtAdd_Model.Text = string.Empty;
                    txtAdd_Color.Text = string.Empty;
                    txtAdd_Quantity.Text = string.Empty;
                    txtAdd_Size.Text = string.Empty;
                    txtAdd_Container.Text = string.Empty;
                    txtAdd_DateTime.Text = string.Empty;
                    txtAdd_Status.Text = string.Empty;
                    conn.Close();
                   }
                 catch
                   {
                   MessageBox.Show("Please Select the Desired Record");
                   }
            }
        }
            
    }
}
