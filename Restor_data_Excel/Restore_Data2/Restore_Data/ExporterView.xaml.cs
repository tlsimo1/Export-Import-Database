using Restore_Data.BLL;
using Restore_Data.Definition;
using Restore_Data.UC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using UCGeneraFi;

namespace Restore_Data
{
    /// <summary>
    /// Logique d'interaction pour ExporterView.xaml
    /// </summary>
    public partial class ExporterView : Window, INotifyPropertyChanged
    {
       




        BExcel _bExcel = new BExcel();
        BackgroundWorker _workerExport = new BackgroundWorker();
        string VersionDataBase = string.Empty;
        OleDbConnection Excel_OLE_Con = new OleDbConnection();
        bool IsValid = true;
        string FolderPath = string.Empty;
        string Error = string.Empty;

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
            set { _listName = value; RaisePropertyChanged(""); }
        }


        public static readonly DependencyProperty PersonProperty =
                          DependencyProperty.Register("TableObject", typeof(TableObject),
                                                      typeof(UCRestoreDB));
        public TableObject tableTest = new TableObject();
        public TableObject TableTest
        {
            get { return tableTest; }
            set { tableTest = value; RaisePropertyChanged(""); }
        }
        public ExporterView()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            lblNameDB.Content = Serveur.NomDatabase;
            var tuple = _bExcel.GetVersionSqlServer();
            lblVersionSql.Content = tuple.Item2;
            VersionDataBase = _bExcel.GetVersionDataBase(Serveur.TableSource); ;
            lblVersionBD.Content = VersionDataBase;
            DataContext = this;
            //DispatcherTimer timer = new DispatcherTimer();
            //timer.Interval = new TimeSpan(0, 0, 2);
            //timer.Tick += ((sender, e) =>
            //{
            //    if (IsValid)
            //    {
            //        dg.Height += 10;
            //        if (scroll.VerticalOffset == scroll.ScrollableHeight)
            //        {
            //            scroll.ScrollToEnd();
            //        }
            //    }
            //});
            //timer.Start();
            this.Closing += ExporterView_Closing;
        }
        private void Openfile_Click(object sender, RoutedEventArgs e)
        {

            string folderPath_EXP = string.Empty;
            try
            {
                BExcel _BExcel = new BExcel();
                Microsoft.Win32.SaveFileDialog _saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                _saveFileDialog.DefaultExt = "gsv";
                _saveFileDialog.FileName = "GenerafiPaie";
                _saveFileDialog.Filter = "gsv File (.gsv)|*.gsv";
                var result = _saveFileDialog.ShowDialog();
                if (result ?? false)
                {
                    btnImport.IsEnabled = true;
                    Border border = (Border)btnImport.Template.FindName("bdr", btnImport);
                    border.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#65A70F"));
                    border.Cursor = Cursors.Hand;
                    string name = _saveFileDialog.FileName;

                    txtfile.Text = name;
                    _BExcel.CreateFloder(name.Replace(".gsv", ""));
                    folderPath_EXP = name.Replace(".gsv", "");
                    Serveur.FolderPath = folderPath_EXP;

                }
            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;
            }
        }
        private void BtnExporter_Click(object sender, RoutedEventArgs e)
        {
            tab.SelectedIndex = 1;

            _workerExport.DoWork += _workerExport_DoWork;
            _workerExport.ProgressChanged += _workerExport_ProgressChanged;
            _workerExport.RunWorkerCompleted += _workerExport_RunWorkerCompleted;
            _workerExport.WorkerReportsProgress = true;
            _workerExport.WorkerSupportsCancellation = true;
            _workerExport.RunWorkerAsync();

        }
        void _workerExport_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!string.IsNullOrEmpty(VersionDataBase))
            {
                Exporter(Serveur.FolderPath, Serveur.NomDatabase);
            }
        }
        void _workerExport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //double percent = (e.ProgressPercentage * 100) / CountTableName;
        }
        public void Exporter(string folderPath_Export, string dataBaseName)
        {
            List<string> listTableNameFromDataBase = new List<string>();
            _bExcel.DisableConstrainte();
            _bExcel.DisableTrigger();
            listTableNameFromDataBase = _bExcel.GetAllTableFromDataBase();
            FolderPath = folderPath_Export;
            int countTableName = 0;
            string paramTableName = string.Empty;
            string nameValue;
            

            if (string.IsNullOrEmpty(Serveur.ErrorMessage))
            {
                string datetime = DateTime.Now.ToString();
                try
                {
                    string folderPath = folderPath_Export + "\\";
                    string query = @"SELECT Schema_name(schema_id) AS SchemaName,name AS TableName FROM  sys.tables WHERE  is_ms_shipped = 0";
                    SqlCommand cmd = new SqlCommand(query, Connection.connect());
                    
                    DataTable dt = new DataTable();

                    dt.Load(cmd.ExecuteReader());
                    Connection.disconnect();
                    countTableName = dt.Rows.Count;
                    int countTable = 0;
                    foreach (DataRow dt_row in dt.Rows)
                    {
                        countTable++;
                        string SchemaName = string.Empty;
                        string TableName = string.Empty;
                        object[] array = dt_row.ItemArray;
                        SchemaName = array[0].ToString();
                        TableName = array[1].ToString();
                        paramTableName = TableName;
                        nameValue = TableName;
                        TableObject tableTest = new TableObject() { Values = nameValue, IsCheked = false ,CountTable=$"{countTable}/{countTableName}"};

                        //if (ListName.Count != 0)
                        //{
                        //    Application.Current.Dispatcher.BeginInvoke(
                        //    DispatcherPriority.Background,
                        //    new Action(() =>
                        //    {
                        //        ListName.Clear();
                        //    }));
                        //}

                        App.Current.Dispatcher.Invoke(new Action(() => TableTest = new TableObject() { Values = nameValue, IsCheked = false, CountTable = $"{countTable}/{countTableName}" }));
                        Thread.Sleep(500);
                        string ExcelFileName = "";
                        ExcelFileName = SchemaName + "_" + TableName + ".xlsx";
                        OleDbTransaction transaction = null;
                        string connstring = string.Empty;
                        if (string.IsNullOrEmpty(Error))
                        {
                            using (Excel_OLE_Con = new OleDbConnection(connstring))
                            {
                                OleDbCommand command = new OleDbCommand();
                                command.Connection = Excel_OLE_Con;
                                try
                                {
                                    connstring = $"Provider=Microsoft.ACE.OLEDB.12.0; Data Source={folderPath}{ExcelFileName};Extended Properties=\"Excel 12.0 Xml;HDR=YES;\"";
                                    string queryString = $"SELECT * from { SchemaName }.{ TableName }";
                                    SqlDataAdapter _adapter = new SqlDataAdapter(queryString, Connection.connect());
                                    DataSet ds = new DataSet();
                                    _adapter.Fill(ds);
                                    Connection.disconnect();
                                    if (string.IsNullOrEmpty(Error))
                                    {
                                        _bExcel.GenerateXML(folderPath_Export + "//" + dataBaseName + ".xml", dataBaseName, VersionDataBase, Serveur.TableSource);
                                    }

                                    string tableColumns = string.Empty;
                                    foreach (DataTable table in ds.Tables)
                                    {
                                        foreach (DataColumn column in table.Columns)
                                        {
                                            tableColumns += column + "],[";
                                        }
                                    }
                                    tableColumns = ("[" + tableColumns.Replace(",", " Text,").TrimEnd(','));
                                    tableColumns = tableColumns.Remove(tableColumns.Length - 2);
                                    Excel_OLE_Con.ConnectionString = connstring;
                                    Excel_OLE_Con.Open();
                                    transaction = Excel_OLE_Con.BeginTransaction(IsolationLevel.ReadCommitted);
                                    command.Connection = Excel_OLE_Con;
                                    command.Transaction = transaction;
                                    command.Connection = Excel_OLE_Con;
                                    command.CommandText = $"Create table [{SchemaName}_{TableName}] ({tableColumns})";
                                    command.ExecuteNonQuery();
                                    foreach (DataTable table in ds.Tables)
                                    {
                                        string sqlCommandInsert = "";
                                        string sqlCommandValue = "";
                                        foreach (DataColumn dataColumn in table.Columns)
                                        {
                                            sqlCommandValue += dataColumn + "],[";
                                        }
                                        sqlCommandValue = "[" + sqlCommandValue.TrimEnd(',');
                                        sqlCommandValue = sqlCommandValue.Remove(sqlCommandValue.Length - 2);
                                        sqlCommandInsert = $"INSERT into [{ SchemaName }_{ TableName }] ({ sqlCommandValue }) VALUES(";

                                        string percent = string.Empty;
                                        int columnCount = table.Columns.Count;
                                        int countRow = table.Rows.Count;
                                        int count = 0;
                                        if (countRow == 0)
                                        {
                                            percent = $"{count} / {countRow}";
                                            App.Current.Dispatcher.Invoke(new Action(() => TableTest.CountProgress = percent));
                                        }
                                        foreach (DataRow row in table.Rows)
                                        {
                                            try
                                            {
                                                count++;
                                                string parametresNom = string.Empty;
                                                string result = string.Empty;
                                                command.Parameters.Clear();
                                                for (int i = 0; i < columnCount; i++)
                                                {
                                                    int index = table.Rows.IndexOf(row);
                                                    result = table.Rows[index].ItemArray[i].ToString();
                                                    parametresNom += $"@Param{i + 1},";
                                                    command.Parameters.AddWithValue($"@Param{i + 1}", result);

                                                }
                                                parametresNom = parametresNom.TrimEnd(',');
                                                var Tcommand = sqlCommandInsert + parametresNom + ")";
                                                command.CommandText = Tcommand;
                                                if (string.IsNullOrEmpty(Error))
                                                {
                                                    command.ExecuteNonQuery();
                                                }

                                                percent = $"{count} / {countRow}";
                                                App.Current.Dispatcher.Invoke(new Action(() => TableTest.CountProgress = percent));

                                            }
                                            catch (Exception ex)
                                            {
                                                Serveur.ErrorMessage = ex.Message;
                                                Serveur.IsValide = false;
                                                break;
                                            }
                                            Serveur.IsValide = true;
                                        }
                                    }
                                    if (Serveur.IsValide != false)
                                    {
                                        if (string.IsNullOrEmpty(Error))
                                        {
                                            transaction.Commit();
                                            App.Current.Dispatcher.Invoke(new Action(() => TableTest.IsCheked = true));
                                        }

                                    }
                                    else
                                    {
                                        Excel_OLE_Con.Close();
                                        Excel_OLE_Con.ResetState();
                                        _bExcel.Delete_Folder(folderPath_Export);
                                        break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Serveur.ErrorMessage = ex.Message;
                                    transaction.Rollback();
                                    Serveur.IsValide = false;
                                    break;
                                }
                                Excel_OLE_Con.Close();
                                Excel_OLE_Con.ResetState();
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(Error))
                    {
                        _bExcel.EnableAllConstrainte();
                        _bExcel.EnableTrigger();
                        _bExcel.CreateFile(folderPath_Export);
                        _bExcel.CreateFloder(string.Concat(folderPath_Export,@"\defintionTable"));
                        foreach( string item in listTableNameFromDataBase)
                        {
                            _bExcel.CreateFileFromTable(item, folderPath_Export + @"\defintionTable");
                        }
                       

                        if (string.IsNullOrEmpty(Serveur.ErrorMessage))
                            _bExcel.CompressFolder(folderPath_Export);
                        _bExcel.CreateFloder(Serveur.CreatePathFolder);
                        _bExcel.CopyFolder(folderPath_Export);
                        _bExcel.Delete_Folder(folderPath_Export);
                    }
                    Excel_OLE_Con.Dispose();
                }
                catch (Exception ex)
                {
                    Serveur.ErrorMessage = ex.Message;
                    Serveur.IsValide = false;
                    _bExcel.Delete_Folder(folderPath_Export);
                }
            }
            else
            {
                _bExcel.Delete_Folder(folderPath_Export);
            }
        }
        private void ExporterView_Closing(object sender, CancelEventArgs e)
        {

            try
            {

                Excel_OLE_Con.Dispose();
                _bExcel.Delete_Folder(FolderPath);
                Error = "test";
            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;
            }


        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _workerExport.CancelAsync();
        }
        void _workerExport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
        private void BtnTerminer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        static string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    var test = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    return test;
                }
            }
        }

     
    }
}

