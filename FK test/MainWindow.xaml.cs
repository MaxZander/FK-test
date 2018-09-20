using MySql.Data.MySqlClient;
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

namespace FK_test
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetConnexion();
            cmdsql();
            CloseConnexion();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private static MySqlConnection cnx = null;

        private static string _ConnectionString;

        public static MySqlConnection GetConnexion()
        {
            _ConnectionString = "Server=localhost; database=dbtest; password=; UID=root; sslmode=none;";

            if (cnx == null)
            {
                cnx = new MySqlConnection();
                cnx.ConnectionString = _ConnectionString;

                cnx.Open();
            }
            return cnx;
        }

        public static void CloseConnexion()
        {
            cnx.Close();
            cnx = null;
        }
        private void cmdsql()
        {
            MySqlCommand cmd = GetConnexion().CreateCommand();
            cmd.CommandText = "SELECT * FROM `marque` WHERE 1";
            //SELECT Mo.modele as 'MODELE FORD DISPONIBLE' FROM modele Mo, marque Ma WHERE Mo.IDMarque=Ma.IDMarque and Ma.Marque='Ford'
            MySqlDataReader reader = cmd.ExecuteReader();
            marquecb.Items.Clear();
            modelecb.Items.Clear();
            while (reader.Read())
            {
                try
                {

                    marquecb.Items.Add(reader.GetString(1));
                    //h.Add(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                }
                catch (Exception e)
                {
                    MessageBox.Show("Erreur DB \n" + e.Message);
                }

            }
            reader.Close();
        }
        private void UpdateModele()
        {
            MySqlCommand cmd = GetConnexion().CreateCommand();
            cmd.CommandText = "SELECT Mo.modele FROM modele Mo, marque Ma WHERE Mo.IDMarque=Ma.IDMarque and Ma.Marque='"+ marquecb.SelectedItem +"'";
            //SELECT Mo.modele as 'MODELE FORD DISPONIBLE' FROM modele Mo, marque Ma WHERE Mo.IDMarque=Ma.IDMarque and Ma.Marque='Ford'
            MySqlDataReader reader = cmd.ExecuteReader();
            modelecb.Items.Clear();
            while (reader.Read())
            {
                try
                {
                    modelecb.Items.Add(reader.GetString(0));
                    //h.Add(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                }
                catch (Exception e)
                {
                    MessageBox.Show("Erreur DB \n" + e.Message);
                }

            }
            reader.Close();
        }

        private void marquecb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateModele();
        }
    }
}
