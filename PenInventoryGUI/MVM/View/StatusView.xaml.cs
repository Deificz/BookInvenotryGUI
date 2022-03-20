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
using MySql.Data.MySqlClient;
using System.Data;
using BookInventoryGUI;



namespace PenInventoryGUI.MVM.View
{
    /// <summary>
    /// Interaction logic for StatusView.xaml
    /// </summary>
    public partial class StatusView : UserControl
    {
        DataTable dtbl = new DataTable();
       
        public StatusView()
        {
            InitializeComponent();
            string server = "localhost";
            string database = "inventory_sys";
            string uid = "root";
            string pass = "123";
            string connectionString = "Server=" + server + ";" + "Database=" + database + ";" + "UID=" + uid + ";" + "Password=" + pass + ";";
            MySqlConnection conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();
                MySqlDataAdapter sqlDa = new MySqlDataAdapter("SELECT pen_datetime as 'Entry DateTime', pen_id as 'Pen_ID',pen_brand as 'Pen_Brand',pen_model as 'Pen_Model',pen_color as 'Pen_Color',pen_quantity as 'Pen_Quantity',pen_size as 'Pen_Size',pen_container as 'Pen_Container' FROM pen_description", conn);
                sqlDa.Fill(dtbl);
                ListDataGrid.ItemsSource = dtbl.DefaultView;
                sqlDa.Update(dtbl);
                conn.Close();
            }
        }

        private void box_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                e.Handled = true;
                DataView dv = dtbl.DefaultView;
                dv.RowFilter = "Pen_ID LIKE '" + box.Text + "%'";
                ListDataGrid.ItemsSource = dv;
            }
        }
    }
}
