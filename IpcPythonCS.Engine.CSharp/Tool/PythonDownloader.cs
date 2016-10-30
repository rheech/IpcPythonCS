using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IpcPythonCS.Engine.Tool
{
    public class PythonDownloader
    {
        private const string DEFAULT_PYTHON_URL = "https://www.cheonghyun.com/download/Python35.zip";
        private const string DEFAULT_ZIP_FILE = "Python35.zip";

        public PythonDownloader()
        {
        }

        public void DownloadPython()
        {
            DownloadPython(DEFAULT_PYTHON_URL);
        }

        public void DownloadPython(string url)
        {
            DownloadZip(url);
        }

        private void DownloadZip(string url)
        {
            Uri uri;
            FileInfo file;

            uri = new Uri(url);
            file = new FileInfo(uri.LocalPath);

            if (file.Exists)
            {
                file.Delete();
            }

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(uri, file.Name);
            }

            // overwrite zip
            StreamReader sr = new StreamReader(file.Name);
            ZipArchive z = new ZipArchive(sr.BaseStream);
            DirectoryInfo dir = new DirectoryInfo(".\\");

            foreach (ZipArchiveEntry e in z.Entries)
            {
                e.ExtractToFile(Path.Combine(dir.FullName, e.Name), true);
            }

            //ZipFile.ExtractToDirectory(file.Name, ".\\");
        }
    }
}
