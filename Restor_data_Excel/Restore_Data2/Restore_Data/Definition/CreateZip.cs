using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restore_Data
{
    class CreateZip
    {
        public void ZipFile(string chemin, string currentExtension)
        {
            if (String.IsNullOrWhiteSpace(chemin))
                return;

            if (System.IO.Directory.Exists(chemin))
                System.IO.File.Delete(chemin);
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(chemin, "");
                    zip.Comment = String.Format("This zip archive was created by GeneraFi \non machine '{0}'", System.Net.Dns.GetHostName());
                    string CheminZip = chemin.Replace(currentExtension, ".zip");
                    zip.Save(CheminZip);
                }
            }
            catch (System.Exception )
            {
                //UCGeneraFi.GeneraFiMessageBox.Show("Erreur : " + ex1.Message, "Erreur..", UCGeneraFi.GeneraFiMsgBoxButton.OK, UCGeneraFi.GeneraFiMsgBoxImage.Erreur);
                //System.Console.Error.WriteLine("exception: " + ex1);
            }
        }
        public void ExtractFile(string CheminZip, string RepertoireXml)
        {
            using (ZipFile zip = Ionic.Zip.ZipFile.Read(CheminZip))
            {
                foreach (ZipEntry e in zip)
                {
                    zip.Password = Serveur.MotDePass;
                    e.Extract(RepertoireXml, ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }
    }
}
