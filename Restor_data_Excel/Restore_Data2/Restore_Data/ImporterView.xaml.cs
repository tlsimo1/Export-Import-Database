using Restore_Data.BLL;
using Restore_Data.Definition;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;
using UCGeneraFi;

namespace Restore_Data
{
    /// <summary>
    /// Logique d'interaction pour ImporterView.xaml
    /// </summary>
    public partial class ImporterView : Window, INotifyPropertyChanged
    {
        BExcel _bExcel = new BExcel();
        string ZipFolderPath_Import, FolderPathzip, FolderPath_Import, VersionDataBase;
        bool IsValid = true;
        BackgroundWorker _workerImport;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private void RaisePropertyChanged(string propertyName)
        {
            var handlers = PropertyChanged;

            handlers(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<TableObject> _listName = new ObservableCollection<TableObject>();
        public ObservableCollection<TableObject> ListName
        {
            get
            { return _listName; }
            set
            { _listName = value; RaisePropertyChanged(""); }
        }
        public ImporterView()
        {
            InitializeComponent();
            DataContext = this;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            lblNameDB.Content = Serveur.NomDatabase;
            var tuple = _bExcel.GetVersionSqlServer();
            lblVersionSql.Content = tuple.Item2;
            VersionDataBase = _bExcel.GetVersionDataBase(Serveur.TableSource);
            lblVersionBD.Content = VersionDataBase;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Tick += (sender, e) =>
            {
                if (IsValid)
                {
                    dg.Height += 10;
                    if (scroll.VerticalOffset == scroll.ScrollableHeight)
                    {
                        scroll.ScrollToEnd();
                    }
                }
            };
            timer.Start();
        }

        private void BtnTerminer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnImporter_Click(object sender, RoutedEventArgs e)
        {
            tab.SelectedIndex = 1;
            _workerImport = new BackgroundWorker();
            _workerImport.DoWork += _workerImport_DoWork;
            _workerImport.ProgressChanged += _workerImport_ProgressChanged;
            _workerImport.RunWorkerCompleted += _workerImport_RunWorkerCompleted;
            _workerImport.WorkerReportsProgress = true;
            _workerImport.WorkerSupportsCancellation = true;
            _workerImport.RunWorkerAsync();
        }
        private void Openfile_Click(object sender, RoutedEventArgs e)
        {
            List<string> path = new List<string>();
            path = _bExcel.OpenFileDialog();
            if (path.Count != 0)
            {
                btnImport.IsEnabled = true;
                Border border = (Border)btnImport.Template.FindName("bdr", btnImport);
                border.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#65A70F"));
                border.Cursor = Cursors.Hand;
                ZipFolderPath_Import = path[0];
                FolderPathzip = path[1];
                FolderPath_Import = path[2];
                txtfile.Text = ZipFolderPath_Import;
            }
        }

        void _workerImport_DoWork(object sender, DoWorkEventArgs e)
        {
            Import_Data(ZipFolderPath_Import, FolderPath_Import, Serveur.NomDatabase, Serveur.NomServeur);
            _bExcel.Delete_Folder(FolderPath_Import);
        }
        void _workerImport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }
        public void Import_Data(string zipFolderPath, string folderpath, string nomDB, string nomServeur)
        {
            Serveur.ErrorMessage = "";
            Connection con = new Connection();
            List<string> listTable = new List<string>();
            List<string> fileName = new List<string>();
            Connection SQLConnection = new Connection();
            string tableName = string.Empty;
            string version = string.Empty;
            string nameValue;

            listTable = _bExcel.GetAllTableFromDataBase();



            SqlTransaction transaction = null;
            VersionDataBase = _bExcel.GetVersionDataBase(Serveur.TableSource);
            if (File.Exists(zipFolderPath))
            {
                string datetime = DateTime.Now.ToString();
                try
                {
                    _bExcel.Delete_Folder(folderpath);
                    _bExcel.CreateFloder(folderpath);
                    

                    CreateZip Cz = new CreateZip();
                    Cz.ExtractFile(zipFolderPath, folderpath);
                    XDocument document = XDocument.Load($"{folderpath}\\{nomDB}.xml");
                    var books = document.Descendants("XmlFile").Select(x => x.Element("Version").Value);

                    foreach (var r in books)
                    {
                        version = r;
                    }
                    _bExcel.DisableConstrainte();
                    _bExcel.DisableTrigger();

                    if (string.IsNullOrEmpty(Serveur.ErrorMessage))
                    {
                        string folderPath = folderpath;
                        string dataBaseName = nomDB;
                        string SQLServerName = nomServeur;
                        var directory = new DirectoryInfo(folderPath);
                        FileInfo[] files = directory.GetFiles("*.xlsx*");

                        foreach (var item in files)
                        {
                            fileName.Add(item.ToString().Replace("dbo_","").Replace(".xlsx",""));
                        }
                        var list3 = listTable.Except(fileName).ToList();
                       

                        string fileFullPath = "";
                        var number = VersionDataBase.Replace(".", ",");
                        var versiondb = Decimal.Parse(number, CultureInfo.InvariantCulture);
                        var number2 = version.Replace(".", ",");
                        var versiondbXML = Decimal.Parse(number2, CultureInfo.InvariantCulture);


                        if (versiondb >= versiondbXML)
                        {
                            SQLConnection.OpenServer();
                            transaction = SQLConnection.con.BeginTransaction();
                            try
                            {

                                if (list3 != null)
                                {
                                    foreach (string item in list3)
                                    {  
                                      _bExcel.DeleteTable(item, SQLConnection.OpenServer(),transaction);
                                    }
                                }
                                foreach (string nomTable in fileName)
                                {
                                    if (!listTable.Contains(nomTable))
                                    {
                                        if (File.Exists(string.Concat(folderPath, $"\\defintionTable\\{nomTable}.txt")))
                                        {
                                            string text = File.ReadAllText(string.Concat(folderPath, $"\\defintionTable\\{nomTable}.txt"));
                                            
                                             _bExcel.CreateTable(text, SQLConnection.con,transaction);
                                        }
                                    }
                                }

                                foreach (FileInfo file in files)
                                {
                                    string filename = "";
                                    fileFullPath = string.Concat(folderPath, "\\", file.Name);
                                    filename = file.Name.Replace(".xlsx", "");
                                    string ConStr;
                                    string HDR;
                                    HDR = "YES";
                                    string filenameXml = string.Concat(nomDB, ".xml");
                                    if (file.Name != filenameXml && !file.Name.Equals("ScriptSql.txt"))
                                    {
                                        ConStr = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={fileFullPath};Extended Properties=\"Excel 12.0;HDR={HDR};IMEX=1\"";
                                        using (OleDbConnection cnn = new OleDbConnection(ConStr))
                                        {
                                            if (cnn.State == ConnectionState.Open)
                                                cnn.Close();

                                            cnn.Open();

                                            DataTable dtSheet = cnn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                            string sheetname;
                                            sheetname = string.Empty;
                                            string percent = string.Empty;
                                            //  int count = 0;
                                            if (string.IsNullOrEmpty(Serveur.ErrorMessage))
                                            {
                                                foreach (DataRow drSheet in dtSheet.Rows)
                                                {
                                                    if (drSheet["TABLE_NAME"].ToString().Contains("$"))
                                                    {
                                                        sheetname = drSheet["TABLE_NAME"].ToString();
                                                        using (OleDbCommand oconn = new OleDbCommand($"select * from [{sheetname}]", cnn))
                                                        {
                                                            tableName = sheetname.Replace("dbo_", "");
                                                            nameValue = tableName.Replace("$", "");
                                                            if (ListName.Count != 0)
                                                            {
                                                                Application.Current.Dispatcher.BeginInvoke(
                                                                DispatcherPriority.Background,
                                                                new Action(() =>
                                                                {
                                                                    ListName.Clear();
                                                                }));
                                                            }
                                                            
                                                            TableObject tableTest = new TableObject() { Values = nameValue, IsCheked = false };
                                                            App.Current.Dispatcher.Invoke(new Action(() => ListName.Add(tableTest)));
                                                            Thread.Sleep(1000);
                                                            OleDbDataAdapter adp = new OleDbDataAdapter(oconn);
                                                            DataTable dt = new DataTable();
                                                            adp.Fill(dt);
                                                            sheetname = sheetname.Replace("$", "");
                                                            try
                                                            {


                                                                tableName = filename.Replace("dbo_", "");
                                                                string query = $"delete from {tableName}";
                                                                SqlCommand cmd = new SqlCommand(query, SQLConnection.con, transaction);
                                                                
                                                                cmd.ExecuteNonQuery();
                                                                using (SqlBulkCopy blk = new SqlBulkCopy(SQLConnection.con, SqlBulkCopyOptions.Default, transaction))
                                                                {
                                                                    
                                                                    //blk.DestinationTableName = "[" + filename + "_" + tableName + "]";
                                                                    blk.DestinationTableName = tableName.Replace("dbo_", "dbo.");
                                                                    if (filename != "dbo_sysdiagrams")
                                                                    {
                                                                        blk.WriteToServer(dt);
                                                                    }
                                                                    int countRow = dt.Rows.Count;
                                                                    
                                                                    percent = $"{countRow} / {countRow}";
                                                                    App.Current.Dispatcher.Invoke(new Action(() => tableTest.CountProgress = percent));
                                                                    App.Current.Dispatcher.Invoke(new Action(() => tableTest.IsCheked = true));
                                                                    if (string.IsNullOrEmpty(Serveur.ErrorMessage))
                                                                        Serveur.IsValide = true;
                                                                    else
                                                                        Serveur.IsValide = false;
                                                                }
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                Serveur.ErrorMessage = ex.Message;
                                                                Serveur.IsValide = false;
                                                                
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(Serveur.ErrorMessage))
                                    {
                                        break;
                                    }
                                }
                               
                            }
                            catch (Exception ex)
                            {
                                Serveur.ErrorMessage = ex.Message;
                            }
                            //finally
                            //{
                            //    SQLConnection.closeserver();
                            //}
                            

                           
                            if (string.IsNullOrEmpty(Serveur.ErrorMessage))
                            {
                                if (string.IsNullOrEmpty(Serveur.ErrorMessage))
                                    _bExcel.DeleteAllScript(SQLConnection.OpenServer(), transaction);
                                if (string.IsNullOrEmpty(Serveur.ErrorMessage))
                                    _bExcel.ExecuteQuery(string.Concat(folderpath, @"\ScriptSql.txt"), SQLConnection.OpenServer(), transaction);
                                if (string.IsNullOrEmpty(Serveur.ErrorMessage))
                                    _bExcel.CompressFolder(folderpath);
                                if (string.IsNullOrEmpty(Serveur.ErrorMessage))
                                    transaction.Commit();
                                else
                                {
                                    Serveur.IsValide = false;
                                }
                                SQLConnection.closeserver();
                                _bExcel.EnableAllConstrainte();
                                _bExcel.EnableTrigger();
                            }
                        }
                        else
                        {
                            Serveur.IsValide = false;
                            Serveur.ErrorMessage = "la Version de la base de donnée est invalide";
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Serveur.ErrorMessage = ex.Message;
                    Serveur.IsValide = false;
                    _bExcel.Delete_Folder(zipFolderPath.Replace(".zip", ""));
                    
                }
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _workerImport.CancelAsync();
        }
        void _workerImport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
            }
            else if (e.Error != null)
            {
                GeneraFiMessageBox.Show(Serveur.ErrorMessage);
                this.Close();
            }
            else
            {
                if (Serveur.IsValide == false)
                {
                    GeneraFiMessageBox.Show(Serveur.ErrorMessage);
                    this.Close();
                }
                else
                {
                    tab.SelectedIndex = 2;
                    IsValid = false;
                }
            }
        }
    }
}
