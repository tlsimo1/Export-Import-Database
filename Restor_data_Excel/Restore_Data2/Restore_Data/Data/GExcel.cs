using Dapper;
using Ionic.Zip;
using Restore_Data.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using System.Xml.Linq;
using UCGeneraFi;

namespace Restore_Data.Data
{
    public class GExcel : Connection
    {
        public void GenerateXML(string name, string dataBaseName, string version, string tableSource)
        {
            try
            {
                var tuple = GetVersionSqlServer();
                XmlFile x = new XmlFile();
                x.DataBaseName = dataBaseName;
                x.SQLVersion = tuple.Item1;
                x.SQLVersionNumber = tuple.Item2;
                x.Date = DateTime.Now;
                x.Version = version;
                x.TableSource = tableSource;
                XmlSerializer SerializerObj = new XmlSerializer(typeof(XmlFile));
                TextWriter WriteFileStream = new StreamWriter(name);
                SerializerObj.Serialize(WriteFileStream, x);
                WriteFileStream.Close();
            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;

            }
        }
        public void CopyFolder(string folderName)
        {
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    if (Directory.Exists(Serveur.Path_appdata))
                        Directory.Delete(Serveur.Path_appdata, true);
                    zip.Password = Serveur.MotDePass;
                    zip.AddDirectory(folderName);
                    zip.Save(Serveur.Path_appdata);
                }
            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;
            }
        }
        public void DisableTrigger()
        {
            try
            {
                string query = @"sp_msforeachtable @command1 ='ALTER TABLE ? DISABLE TRIGGER all'";
                using (SqlCommand cmd = new SqlCommand(query, Connection.connect()))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;
            }
        }
        public void EnableTrigger()
        {
            try
            {

                string query = @"sp_msforeachtable @command1 ='ALTER TABLE ? ENABLE TRIGGER all'";
                using (SqlCommand cmd = new SqlCommand(query, Connection.connect()))
                {
                    cmd.CommandTimeout = 300;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;
            }
        }
        public void DisableConstrainte()
        {
            try
            {

                string query = @"EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'";
                using (SqlCommand cmd = new SqlCommand(query, Connection.connect()))
                {
                    cmd.CommandTimeout = 300;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;
            }

        }
        public void EnableAllConstrainte()
        {
            try
            {

                string query = @"sp_MSforeachtable @command1 = 'ALTER TABLE ? CHECK CONSTRAINT ALL'";
                using (SqlCommand cmd = new SqlCommand(query, Connection.connect()))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;
            }

        }
        public void Delete_Folder(string folderpath)
        {
            try
            {
                if (Directory.Exists(folderpath))
                    Directory.Delete(folderpath, true);
            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;
            }
        }
        public void Delete_ZipFolder(string folderpathzip)
        {
            try
            {
                string ext = System.IO.Path.GetExtension(folderpathzip);

                if (ext == ".zip" && File.Exists(folderpathzip))
                    File.Delete(folderpathzip);
            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;
            }
        }
        public void CreateFloder(string folderpath)
        {
            try
            {
                Directory.CreateDirectory(folderpath);
            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;
            }
        }
        public void CompressFolder(string folderPath)
        {
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.Password = Serveur.MotDePass; ;
                    zip.AddDirectory(folderPath);
                    if (File.Exists(folderPath))
                        File.Delete(folderPath);
                    zip.Save(string.Concat(folderPath, ".gsv"));
                }
                #region

                //zip.Password = "123";
                ////zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                //zip.AddDirectory(@"D:\testzip");
                //zip.Save(@"D:\testzipEncrypte.zip");
                // zip.AddDirectory(@"D:\Test_Export");

                //string parent = System.IO.Path.GetDirectoryName(folderPath);
                //string name = System.IO.Path.GetFileName(folderPath);
                //string fileName = System.IO.Path.Combine(parent, name + ".gsv");
                //if (File.Exists(fileName))
                //    File.Delete(fileName);
                //System.IO.Compression.ZipFile.CreateFromDirectory(folderPath, fileName);
                //zip.Password = "123";
                ////...but this will be password protected
                //zip.AddDirectory(@"D:\Test_Export");
                //zip.Save(@"D:\testzipEn.gsv");
                #endregion
            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;
            }
        }
        public string SaveFileDialog()
        {
            string folderPath_EXP = string.Empty;
            try
            {
                BExcel _BExcel = new BExcel();
                Microsoft.Win32.SaveFileDialog _saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                _saveFileDialog.DefaultExt = "zip";
                _saveFileDialog.Filter = "Zip File (.zip)|*.zip";
                var result = _saveFileDialog.ShowDialog();
                if (result ?? false)
                {
                    _BExcel.CreateFloder(_saveFileDialog.FileName.Replace(".zip", ""));
                    folderPath_EXP = _saveFileDialog.FileName.Replace(".zip", "");
                }
            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;
            }
            return folderPath_EXP;
        }
        public List<string> OpenFileDialog()
        {
            List<string> path = new List<string>();
            try
            {

                string zipFolderPath_IMP = string.Empty;
                string folderpathzip = string.Empty;
                string folderPath_EXP = string.Empty;
                Microsoft.Win32.OpenFileDialog _openFileDialog = new Microsoft.Win32.OpenFileDialog();
                _openFileDialog.Filter = "gsv Files|*.gsv;*.gsv";
                var result = _openFileDialog.ShowDialog();
                if (result ?? false)
                {
                    zipFolderPath_IMP = _openFileDialog.FileName;
                    folderpathzip = _openFileDialog.FileName + ".gsv";
                    folderPath_EXP = folderpathzip.Replace(".gsv", "");
                    path.Add(zipFolderPath_IMP);
                    path.Add(folderpathzip);
                    path.Add(folderPath_EXP);
                }
            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;
            }
            return path;
        }
        public Tuple<string, string> GetVersionSqlServer()
        {
            string serverVersion = "";
            string VersionSqlServer = "";
            int versionNumber;
            SqlConnection sqlConnection = Connection.connect();
            try
            {

                serverVersion = sqlConnection.ServerVersion;
                VersionSqlServer = serverVersion;
                string[] serverVersionDetails = serverVersion.Split(new string[] { "." }, StringSplitOptions.None);
                versionNumber = int.Parse(serverVersionDetails[0]);
                switch (versionNumber)
                {
                    case 8:
                        serverVersion = "SQL Server 2000";
                        break;
                    case 9:
                        serverVersion = "SQL Server 2005";
                        break;
                    case 10:
                        serverVersion = "SQL Server 2008";
                        break;
                    case 11:
                        serverVersion = "SQL Server 2012";
                        break;
                    case 12:
                        serverVersion = "SQL Server 2014";
                        break;
                    default:
                        serverVersion = string.Format("SQL Server {0}", versionNumber.ToString());
                        break;
                }
            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<string, string>(serverVersion, VersionSqlServer);

        }
        public string GetVS_SQL()
        {
            string versionBD = string.Empty;
            try
            {

                using (SqlConnection cn = Connection.connect())
                {
                    versionBD = cn.Query<string>("select @@version").Single().ToString();
                }

            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;
            }
            return versionBD;
        }
        public string GetVersionDataBase(string Nomtable)
        {
            string version = string.Empty;
            try
            {
                string query = $@"select {Serveur.ColumnName} from {Nomtable}";
                using (SqlCommand cmd = new SqlCommand(query, Connection.connect()))
                {
                    var value = cmd.ExecuteScalar() == null ? "" : cmd.ExecuteScalar();
                    version = value.ToString();
                }
                if (string.IsNullOrEmpty(version))
                {
                    Serveur.ErrorMessage = "la version de la base de base de données est null";
                }

            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;
            }
            return version;
        }
        public DataTable GetAll_Fonction()
        {
            DataTable dtFonction = new DataTable();
            try
            {
                string query = @"SELECT name, definition, type_desc 
                                FROM sys.sql_modules m 
                                INNER JOIN sys.objects o 
                                ON m.object_id=o.object_id
                                WHERE type_desc like '%function%'";
                using (SqlDataAdapter da = new SqlDataAdapter(query, Connection.connect()))
                {
                    da.Fill(dtFonction);
                }
            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;
            }
            return dtFonction;
        }
        public DataTable GetAll_Procedure()
        {
            DataTable dtProc = new DataTable();
            try
            {
                string query = @"SELECT name, definition, type_desc 
                                FROM sys.sql_modules m 
                                INNER JOIN sys.objects o 
                                ON m.object_id=o.object_id
                                WHERE type_desc like '%PROCEDURE%'";
                using (SqlDataAdapter da = new SqlDataAdapter(query, Connection.connect()))
                {
                    da.Fill(dtProc);
                }
            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;
            }
            return dtProc;
        }
        public DataTable GetAll_Views()
        {
            DataTable dtViews = new DataTable();
            try
            {
                string query = @"SELECT name as TABLE_NAME,
                                REPLACE(OBJECT_DEFINITION(id),'VALUE_TO_BE_REPLACED', 'VALUE_TO_REPLACE_WITH') AS VIEW_DEFINITION
                                FROM sys.sysobjects o
                                WHERE xtype = 'V' order by name";
                using (SqlDataAdapter da = new SqlDataAdapter(query, Connection.connect()))
                {
                    da.Fill(dtViews);
                }
            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;
            }
            return dtViews;
        }
        public DataTable GetAll_Trigger()
        {
            DataTable dtTrigger = new DataTable();
            try
            {
                string query = @"select  OBJECT_NAME(o.[object_id]) as Name, m.definition from sys.objects as o inner join sys.sql_modules as m on o.[object_id] = m.[object_id]
                                where OBJECTPROPERTY(o.[object_id], 'IsTrigger') = 1";
                using (SqlDataAdapter da = new SqlDataAdapter(query, Connection.connect()))
                {
                    da.Fill(dtTrigger);
                }
            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;
            }
            return dtTrigger;
        }
        public void DeleteAllScript(SqlConnection con, SqlTransaction transaction)
        {
            try
            {
                string queryFonction = @" Declare @sql NVARCHAR(MAX) = N'';
                                        SELECT @sql = @sql + N' DROP FUNCTION ' 
                                                           + QUOTENAME(SCHEMA_NAME(schema_id)) 
                                                           + N'.' + QUOTENAME(name)
                                        FROM sys.objects
                                        WHERE type_desc LIKE '%FUNCTION%';
                                        Exec sp_executesql @sql";

                string queryProc = @"declare @procName varchar(500)
                                    declare cur cursor 
                                    for select [name] from sys.objects where type = 'p'
                                    open cur
                                    fetch next from cur into @procName
                                    while @@fetch_status = 0
                                    begin
                                        exec('drop procedure [' + @procName + ']')
                                        fetch next from cur into @procName
                                    end
                                    close cur
                                    deallocate cur";
                string queryTrigger = @"DECLARE @TriggerName AS VARCHAR(500) 
                                            DECLARE DropTrigger CURSOR FOR
                                            SELECT TRG.name AS TriggerName
                                            FROM   sys.triggers TRG
                                            INNER JOIN sys.tables TBL
                                            ON TBL.OBJECT_ID = TRG.parent_id 
                                            OPEN DropTrigger
                                             FETCH Next FROM DropTrigger INTO @TriggerName 
                                            WHILE @@FETCH_STATUS = 0
                                              BEGIN
                                                  DECLARE @SQL VARCHAR(MAX)=NULL
                                                  SET @SQL='Drop Trigger ' + @TriggerName
                                                  PRINT 'Trigger ::' + @TriggerName
                                                        + ' Droped Successfully'
                                                  EXEC (@SQL)
                                                  PRINT @SQL
                                                  FETCH Next FROM DropTrigger INTO @TriggerName
                                              END
                                            CLOSE DropTrigger 
                                            DEALLOCATE DropTrigger";
                string queryView = @"DECLARE @sql VARCHAR(MAX) = ''
                                    , @crlf VARCHAR(2) = CHAR(13) + CHAR(10) ;
                                    SELECT @sql = @sql + 'DROP VIEW ' + QUOTENAME(SCHEMA_NAME(schema_id)) + '.' + QUOTENAME(v.name) +';' + @crlf
                                    FROM   sys.views v
                                    PRINT @sql;
                                    EXEC(@sql);";
                using (SqlCommand cmd = new SqlCommand(queryView, con, transaction))
                {
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand(queryFonction, con, transaction))
                {
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand(queryProc, con, transaction))
                {
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand(queryTrigger, con, transaction))
                {
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;
                // transaction.Rollback();
            }
        }
        public void ExecuteQuery(string FileName, SqlConnection con, SqlTransaction transaction)
        {

            try
            {
                string Query = string.Empty;
                string[] lines = File.ReadAllLines(FileName);
                foreach (string line in lines)
                {
                    if (!line.Equals("Go"))
                        Query = Query + "\n" + line;
                    if (line.Equals("Go"))
                    {
                        using (SqlCommand cmd = new SqlCommand(Query, con, transaction))
                        {
                            cmd.Transaction = transaction;
                            cmd.ExecuteNonQuery();
                            Query = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;
                // transaction.Rollback();

            }
        }
        public List<string> GetAllTableFromDataBase()
        {
            List<string> listTableName = null;
            using (SqlCommand cmd = new SqlCommand("", Connection.connect()))
            {
                listTableName = Connection.connect().Query<string>("SELECT Table_Name FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'").ToList();
            }
            return listTableName;
        }
        public void Generation_CreateTable(string TableName)
        {
            try
            {
                string Query = string.Format(@"DECLARE @table_name SYSNAME
                         SELECT @table_name = 'dbo.{0}'
                         DECLARE 
                                                                  @object_name SYSNAME
                                                                , @object_id INT
                                                            SELECT 
                                                                  @object_name = '[' + s.name + '].[' + o.name + ']'
                                                                , @object_id = o.[object_id]
                                                            FROM sys.objects o WITH (NOWAIT)
                                                            JOIN sys.schemas s WITH (NOWAIT) ON o.[schema_id] = s.[schema_id]
                                                            WHERE s.name + '.' + o.name = @table_name
                                                                AND o.[type] = 'U'
                                                                AND o.is_ms_shipped = 0

                                                            DECLARE @SQL NVARCHAR(MAX) = ''

                                                            ;WITH index_column AS 
                                                            (
                                                                SELECT 
                                                                      ic.[object_id]
                                                                    , ic.index_id
                                                                    , ic.is_descending_key
                                                                    , ic.is_included_column
                                                                    , c.name
                                                                FROM sys.index_columns ic WITH (NOWAIT)
                                                                JOIN sys.columns c WITH (NOWAIT) ON ic.[object_id] = c.[object_id] AND ic.column_id = c.column_id
                                                                WHERE ic.[object_id] = @object_id
                                                            ),
                                                            fk_columns AS 
                                                            (
                                                                 SELECT 
                                                                      k.constraint_object_id
                                                                    , cname = c.name
                                                                    , rcname = rc.name
                                                                FROM sys.foreign_key_columns k WITH (NOWAIT)
                                                                JOIN sys.columns rc WITH (NOWAIT) ON rc.[object_id] = k.referenced_object_id AND rc.column_id = k.referenced_column_id 
                                                                JOIN sys.columns c WITH (NOWAIT) ON c.[object_id] = k.parent_object_id AND c.column_id = k.parent_column_id
                                                                WHERE k.parent_object_id = @object_id
                                                            )
                                                            SELECT @SQL = 'CREATE TABLE ' + @object_name + CHAR(13) + '(' + CHAR(13) + STUFF((
                                                                SELECT CHAR(9) + ', [' + c.name + '] ' + 
                                                                    CASE WHEN c.is_computed = 1
                                                                        THEN 'AS ' + cc.[definition] 
                                                                        ELSE UPPER(tp.name) + 
                                                                            CASE WHEN tp.name IN ('varchar', 'char', 'varbinary', 'binary', 'text')
                                                                                   THEN '(' + CASE WHEN c.max_length = -1 THEN 'MAX' ELSE CAST(c.max_length AS VARCHAR(5)) END + ')'
                                                                                 WHEN tp.name IN ('nvarchar', 'nchar', 'ntext')
                                                                                   THEN '(' + CASE WHEN c.max_length = -1 THEN 'MAX' ELSE CAST(c.max_length / 2 AS VARCHAR(5)) END + ')'
                                                                                 WHEN tp.name IN ('datetime2', 'time2', 'datetimeoffset') 
                                                                                   THEN '(' + CAST(c.scale AS VARCHAR(5)) + ')'
                                                                                 WHEN tp.name = 'decimal' 
                                                                                   THEN '(' + CAST(c.[precision] AS VARCHAR(5)) + ',' + CAST(c.scale AS VARCHAR(5)) + ')'
                                                                                ELSE ''
                                                                            END +
                                                                            CASE WHEN c.collation_name IS NOT NULL THEN ' COLLATE ' + c.collation_name ELSE '' END +
                                                                            CASE WHEN c.is_nullable = 1 THEN ' NULL' ELSE ' NOT NULL' END +
                                                                            CASE WHEN dc.[definition] IS NOT NULL THEN ' DEFAULT' + dc.[definition] ELSE '' END + 
                                                                            CASE WHEN ic.is_identity = 1 THEN ' IDENTITY(' + CAST(ISNULL(ic.seed_value, '0') AS CHAR(1)) + ',' + CAST(ISNULL(ic.increment_value, '1') AS CHAR(1)) + ')' ELSE '' END 
                                                                    END + CHAR(13)
                                                                FROM sys.columns c WITH (NOWAIT)
                                                                JOIN sys.types tp WITH (NOWAIT) ON c.user_type_id = tp.user_type_id
                                                                LEFT JOIN sys.computed_columns cc WITH (NOWAIT) ON c.[object_id] = cc.[object_id] AND c.column_id = cc.column_id
                                                                LEFT JOIN sys.default_constraints dc WITH (NOWAIT) ON c.default_object_id != 0 AND c.[object_id] = dc.parent_object_id AND c.column_id = dc.parent_column_id
                                                                LEFT JOIN sys.identity_columns ic WITH (NOWAIT) ON c.is_identity = 1 AND c.[object_id] = ic.[object_id] AND c.column_id = ic.column_id
                                                                WHERE c.[object_id] = @object_id
                                                                ORDER BY c.column_id
                                                                FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, CHAR(9) + ' ')
                                                                + ISNULL((SELECT CHAR(9) + ', CONSTRAINT [' + k.name + '] PRIMARY KEY (' + 
                                                                                (SELECT STUFF((
                                                                                     SELECT ', [' + c.name + '] ' + CASE WHEN ic.is_descending_key = 1 THEN 'DESC' ELSE 'ASC' END
                                                                                     FROM sys.index_columns ic WITH (NOWAIT)
                                                                                     JOIN sys.columns c WITH (NOWAIT) ON c.[object_id] = ic.[object_id] AND c.column_id = ic.column_id
                                                                                     WHERE ic.is_included_column = 0
                                                                                         AND ic.[object_id] = k.parent_object_id 
                                                                                         AND ic.index_id = k.unique_index_id     
                                                                                     FOR XML PATH(N''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, ''))
                                                                        + ')' + CHAR(13)
                                                                        FROM sys.key_constraints k WITH (NOWAIT)
                                                                        WHERE k.parent_object_id = @object_id 
                                                                            AND k.[type] = 'PK'), '') + ')'  + CHAR(13)
                                                                + ISNULL((SELECT (
                                                                    SELECT CHAR(13) +
                                                                         'ALTER TABLE ' + @object_name + ' WITH' 
                                                                        + CASE WHEN fk.is_not_trusted = 1 
                                                                            THEN ' NOCHECK' 
                                                                            ELSE ' CHECK' 
                                                                          END + 
                                                                          ' ADD CONSTRAINT [' + fk.name  + '] FOREIGN KEY(' 
                                                                          + STUFF((
                                                                            SELECT ', [' + k.cname + ']'
                                                                            FROM fk_columns k
                                                                            WHERE k.constraint_object_id = fk.[object_id]
                                                                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '')
                                                                           + ')' +
                                                                          ' REFERENCES [' + SCHEMA_NAME(ro.[schema_id]) + '].[' + ro.name + '] ('
                                                                          + STUFF((
                                                                            SELECT ', [' + k.rcname + ']'
                                                                            FROM fk_columns k
                                                                            WHERE k.constraint_object_id = fk.[object_id]
                                                                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '')
                                                                           + ')'
                                                                        + CASE 
                                                                            WHEN fk.delete_referential_action = 1 THEN ' ON DELETE CASCADE' 
                                                                            WHEN fk.delete_referential_action = 2 THEN ' ON DELETE SET NULL'
                                                                            WHEN fk.delete_referential_action = 3 THEN ' ON DELETE SET DEFAULT' 
                                                                            ELSE '' 
                                                                          END
                                                                        + CASE 
                                                                            WHEN fk.update_referential_action = 1 THEN ' ON UPDATE CASCADE'
                                                                            WHEN fk.update_referential_action = 2 THEN ' ON UPDATE SET NULL'
                                                                            WHEN fk.update_referential_action = 3 THEN ' ON UPDATE SET DEFAULT'  
                                                                            ELSE '' 
                                                                          END 
                                                                        + CHAR(13) + 'ALTER TABLE ' + @object_name + ' CHECK CONSTRAINT [' + fk.name  + ']' + CHAR(13)
                                                                    FROM sys.foreign_keys fk WITH (NOWAIT)
                                                                    JOIN sys.objects ro WITH (NOWAIT) ON ro.[object_id] = fk.referenced_object_id
                                                                    WHERE fk.parent_object_id = @object_id
                                                                    FOR XML PATH(N''), TYPE).value('.', 'NVARCHAR(MAX)')), '')
                                                                + ISNULL(((SELECT
                                                                     CHAR(13) + 'CREATE' + CASE WHEN i.is_unique = 1 THEN ' UNIQUE' ELSE '' END 
                                                                            + ' NONCLUSTERED INDEX [' + i.name + '] ON ' + @object_name + ' (' +
                                                                            STUFF((
                                                                            SELECT ', [' + c.name + ']' + CASE WHEN c.is_descending_key = 1 THEN ' DESC' ELSE ' ASC' END
                                                                            FROM index_column c
                                                                            WHERE c.is_included_column = 0
                                                                                AND c.index_id = i.index_id
                                                                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') + ')'  
                                                                            + ISNULL(CHAR(13) + 'INCLUDE (' + 
                                                                                STUFF((
                                                                                SELECT ', [' + c.name + ']'
                                                                                FROM index_column c
                                                                                WHERE c.is_included_column = 1
                                                                                    AND c.index_id = i.index_id
                                                                                FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') + ')', '')  + CHAR(13)
                                                                    FROM sys.indexes i WITH (NOWAIT)
                                                                    WHERE i.[object_id] = @object_id
                                                                        AND i.is_primary_key = 0
                                                                        AND i.[type] = 2
                                                                    FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)')
                                                                ), '')

                                                            PRINT @SQL
                                                            --EXEC sys.sp_executesql @SQL", TableName);
                using (SqlCommand cmd = new SqlCommand(Query, Connection.connect()))
                {
                    cmd.Connection.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;
            }
        }
        static void InfoMessageHandler(object sender, SqlInfoMessageEventArgs e)
        {
            string myMsg = e.Message;
            Serveur.MessageFromSqlServer = myMsg;
        }
        public void CreateTable(string query, SqlConnection con, SqlTransaction transaction)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, con, transaction))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;
            }
        }
        public void DeleteTable(string tableName, SqlConnection con, SqlTransaction transaction)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand($"DROP TABLE {tableName}", con, transaction))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Serveur.ErrorMessage = ex.Message;
            }
        }
        public void CreateFile(string FileName)
        {
            DataTable dtFonction = new DataTable();
            DataTable dtProcedure = new DataTable();
            DataTable dtTrigger = new DataTable();
            DataTable dtViews = new DataTable();
            dtFonction = GetAll_Fonction();
            dtProcedure = GetAll_Procedure();
            dtTrigger = GetAll_Trigger();
            dtViews = GetAll_Views();

            if (!File.Exists(string.Concat(FileName, "\\ScriptSql.txt")))
            {
                using (var tw = new StreamWriter(FileName + "\\ScriptSql.txt", true))
                {
                    foreach (DataRow dr in dtViews.Rows)
                    {
                        tw.WriteLine(dr["VIEW_DEFINITION"].ToString());
                        tw.WriteLine("");
                        tw.WriteLine("Go");
                    }
                    foreach (DataRow dr in dtFonction.Rows)
                    {
                        tw.WriteLine(dr["definition"].ToString());
                        tw.WriteLine("");
                        tw.WriteLine("Go");
                    }
                    foreach (DataRow dr in dtProcedure.Rows)
                    {
                        tw.WriteLine(dr["definition"].ToString());
                        tw.WriteLine("");
                        tw.WriteLine("Go");
                    }
                    foreach (DataRow dr in dtTrigger.Rows)
                    {
                        tw.WriteLine(dr["definition"].ToString());
                        tw.WriteLine("");
                        tw.WriteLine("Go");
                    }
                }
            }
        }
        public void CreateFileFromTable(string FileName, string FilePath)
        {
            List<string> listTableName = new List<string>();
            listTableName = GetAllTableFromDataBase();
            foreach (string item in listTableName)
            {
                string path = string.Concat(FilePath, "\\", $"{item}.txt");
                if (!File.Exists(path))
                {
                    using (var tw = new StreamWriter(FilePath + "\\" + $"{item}.txt", true))
                    {
                        Generation_CreateTable(item);
                        tw.WriteLine(Serveur.MessageFromSqlServer);
                    }
                }
            }
        }
    }
}
