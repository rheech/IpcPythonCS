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
            DownloadZip(DEFAULT_PYTHON_URL);
        }

        private void DownloadZip(string url)
        {
            Uri uri;
            FileInfo file;

            uri = new Uri(url);
            file = new FileInfo(uri.LocalPath);

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(uri, file.Name);
            }

            ZipFile.ExtractToDirectory(file.Name, ".\\");
        }
    }
}
