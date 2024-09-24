using Microsoft.Win32;
using Restore_Data.BLL;
using Restore_Data.Data;
using Restore_Data.UC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using UCGeneraFi;
using WPFFolderBrowser;


namespace Restore_Data
{
    /// <summary>     
    /// Interaction logic for MainWindow.xaml
    /// </summary>    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        private void btn_Import_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GeneraFiBoxResult res = GeneraFiMessageBox.Show("Voulez-vous vraiment restaurer la base de donnée O/N ?", "Restaurer", GeneraFiMsgBoxButton.OuiNonAnnuler);
                if (res == GeneraFiBoxResult.Oui)
                {
                    Serveur.NomDatabase = txt_namedb.Text;
                    Serveur.NomServeur = txt_servername.Text;
                    Serveur.ColumnName = txt_nameColonne.Text;
                    Serveur.TableSource = txt_nameTable.Text;
                    ImporterView Imp = new ImporterView();
                    Imp.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;
            }
        }
        private void btn_Export_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                Serveur.NomDatabase = txt_namedb.Text;
                Serveur.NomServeur = txt_servername.Text;
                Serveur.ColumnName = txt_nameColonne.Text;
                Serveur.TableSource = txt_nameTable.Text;
                ExporterView EXP = new ExporterView();
                EXP.ShowDialog();
            }
            catch (Exception ex )
            {
                Serveur.ErrorMessage = ex.Message;
                
            }
        }
    }
}
