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
        private const string DEFAULT_PYTHON_LOCAL_PATH = "C:\\Program Files\\Python35";

        private DirectoryInfo _pythonDirectory;
        private FileInfo _pythonInterpreter;

        public PythonDownloader()
        {
            _pythonDirectory = new DirectoryInfo(DEFAULT_PYTHON_LOCAL_PATH);
        }

        public DirectoryInfo PythonDirectory
        {
            get
            {
                return _pythonDirectory;
            }
        }

        public FileInfo PythonInterpreter
        {
            get
            {
                return _pythonInterpreter;
            }
        }

        public void DownloadAndUnzip()
        {
            DownloadAndUnzip(DEFAULT_PYTHON_URL);
        }

        public void DownloadAndUnzip(string url)
        {
            _downloadAndUnzip(url);
        }

        private void _downloadAndUnzip(string url)
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

            // Set python directory
            dir = new DirectoryInfo(Path.Combine(dir.FullName, Path.GetFileNameWithoutExtension(file.Name)));
            _pythonInterpreter = new FileInfo(Path.Combine(dir.FullName, "python.exe"));
            
            // Remove previously existed directory
            if (System.IO.Directory.Exists(dir.FullName))
            {
                System.IO.Directory.Delete(dir.FullName, true);
            }

            System.IO.Directory.CreateDirectory(dir.FullName);

            _pythonDirectory = dir;

            ZipFile.ExtractToDirectory(file.Name, dir.FullName);
        }
    }
}
