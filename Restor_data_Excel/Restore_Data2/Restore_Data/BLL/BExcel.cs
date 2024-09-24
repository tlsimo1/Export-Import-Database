using Restore_Data.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restore_Data.BLL
{
    public class BExcel
    {
        GExcel _GExcel = new GExcel();
        public void GenerateXML(string name, string dataBaseName, string version, string tableSource)
        {
            _GExcel.GenerateXML(name, dataBaseName, version, tableSource);
        }
        public void CopyFolder(string folderName)
        {
            _GExcel.CopyFolder(folderName);
        }
        public Tuple<string, string> GetVersionSqlServer()
        {
            return _GExcel.GetVersionSqlServer();
        }
        public string GetVersionDataBase(string Nomtable)
        {
            return _GExcel.GetVersionDataBase(Nomtable);
        }
        public void DisableTrigger()
        {
            _GExcel.DisableTrigger();
        }
        public void EnableTrigger()
        {
            _GExcel.EnableTrigger();
        }
        public void DisableConstrainte()
        {
            _GExcel.DisableConstrainte();
        }
        public void EnableAllConstrainte()
        {
            _GExcel.EnableAllConstrainte();
        }
        public void Delete_Folder(string folderpath)
        {
            _GExcel.Delete_Folder(folderpath);
        }
        public void Delete_ZipFolder(string folderpathzip)
        {
            _GExcel.Delete_ZipFolder(folderpathzip);
        }
        public void CreateFloder(string folderpath)
        {
            _GExcel.CreateFloder(folderpath);
        }
        public string SaveFileDialog()
        {
            return _GExcel.SaveFileDialog();
        }
        public List<string> OpenFileDialog()
        {
            return _GExcel.OpenFileDialog();
        }
        public void CompressFolder(string folderPath)
        {
            _GExcel.CompressFolder(folderPath);
        }
        public DataTable GetAll_Fonction()
        {
            return _GExcel.GetAll_Fonction();
        }
        public DataTable GetAll_Procedure()
        {
            return _GExcel.GetAll_Procedure();
        }
        public DataTable GetAll_Views()
        {
            return _GExcel.GetAll_Views();
        }
        public DataTable GetAll_Trigger()
        {
            return _GExcel.GetAll_Trigger();
        }
        public void DeleteAllScript(SqlConnection con, SqlTransaction transaction)
        {
            _GExcel.DeleteAllScript(con, transaction);
        }
        public void ExecuteQuery(string fileName, SqlConnection con, SqlTransaction transaction)
        {
            _GExcel.ExecuteQuery(fileName, con, transaction);
        }
        public List<string> GetAllTableFromDataBase()
        {
            return _GExcel.GetAllTableFromDataBase();
        }
        public void Generation_CreateTable(string TableName)
        {
            _GExcel.Generation_CreateTable(TableName);
        }
        public void CreateTable(string query, SqlConnection con, SqlTransaction transaction)
        {
            _GExcel.CreateTable(query, con, transaction);
        }
        public void DeleteTable(string tableName, SqlConnection con, SqlTransaction transaction)
        {
            _GExcel.DeleteTable(tableName, con, transaction);
        }
        public void CreateFile(string FileName)
        {
            _GExcel.CreateFile(FileName);
        }
        public void CreateFileFromTable(string FileName, string FilePath)
        {
            _GExcel.CreateFileFromTable(FileName, FilePath);
        }
    }
}
