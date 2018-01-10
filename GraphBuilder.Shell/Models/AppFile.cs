using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Xml;
using GraphBuilder.Core;

namespace GraphBuilder.Shell.Models
{
    public class AppFile
    {
        private static AppFile _default;

        public static AppFile Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new AppFile();
                }
                return _default;
            }
        }

        public AppFile()
        {
            ImageBytes = LoadResource(new Uri("pack://application:,,,/GraphBuilder.Shell;component/Resources/Userpic.png"));
        }

        public List<Thumb> GetFolders()
        {
            string path = Path.Combine(Environment.CurrentDirectory, Properties.Settings.Default.IconsPath);
            //return ReadDirectoryMetadata(path);
            return EnlistFolders(path);
        }

        public List<Thumb> GetFiles(string path)
        {
            //List<Thumb> data = ReadDirectoryMetadata(path);
            List<Thumb> items = EnlistFiles(path);

            //foreach (Thumb d in data)
            //{
            //    foreach (Thumb item in items)
            //    {
            //        if (d.Path == item.Path)
            //        {
            //            d.Image = item.Image;
            //            break;
            //        }
            //    }
            //}

            return items;
        }

        private List<Thumb> EnlistFolders(string path)
        {
            List<DirectoryInfo> dirList = EnumerateDirectories(path);
            List<Thumb> items = new List<Thumb>();
            foreach (DirectoryInfo di in dirList)
            {
                Thumb thumb = new Thumb();
                thumb.Name = di.Name;
                thumb.Alias = di.Name;
                thumb.Path = di.FullName;
                items.Add(thumb);
            }
            //KeepPersistency(path, items);
            return items;
        }

        public void Refresh(string path)
        {
        }
        
        private void KeepPersistency(string path, List<Thumb> items)
        {
            string filePath = Path.Combine(path, "readme.xml");
            string text = items.SerializeToXml();
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(text);
            xdoc.Save(filePath);
        }

        private List<Thumb> ReadDirectoryMetadata1(string path)
        {
            string filePath = Path.Combine(path, "readme.xml");
            string input = File.ReadAllText(filePath);
            List<Thumb> items = input.Deserialize<List<Thumb>>();
            return items;
        }

        private List<DirectoryInfo> EnumerateDirectories(string path)
        {
            List<DirectoryInfo> dirList = new List<DirectoryInfo>();

            if (Directory.Exists(path))
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                foreach (DirectoryInfo d in dir.GetDirectories())
                {
                    dirList.Add(d);
                }
            }
            return dirList;
        }

        public List<Thumb> EnlistFiles(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            List<Thumb> items = new List<Thumb>();
            foreach (FileInfo fi in dir.GetFiles("*.png"))
            {
                Thumb thumb = new Thumb();
                thumb.Name = fi.Name;
                thumb.Alias = fi.Name;
                thumb.Path = fi.FullName;
                thumb.Image = new BitmapImage(new Uri(fi.FullName));
                items.Add(thumb);
            }

            //KeepPersistency(path, items);
            return items;
        }
        
        private byte[] LoadResource(Uri uri)
        {
            UserImage = new BitmapImage(uri);
            BmpBitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(UserImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                return ms.ToArray();
            }
        }

        private BitmapImage _userImage;

        public BitmapImage UserImage
        {
            get { return _userImage; }
            set { _userImage = value; }
        }

        private byte[] _imageBytes;
        public byte[] ImageBytes
        {
            get { return _imageBytes; }
            set { _imageBytes = value; }
        }

        public static List<string> ReadTextFile(string path, int index, Encoding encoding)
        {
            List<string> lines = new List<string>();
            using (StreamReader sr = new StreamReader(path, encoding))
            {
                string line;
                //int counter = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    //if (index>0 && counter <= index)
                    //{
                        lines.Add(line);
                    //}
                }
            }

            return lines;
        }

        public static void WriteTextFile(List<string> lines, string path, Encoding encoding)
        {
            using (StreamWriter sw = new StreamWriter(path, false, encoding))
            {
                foreach (string line in lines)
                {
                    sw.WriteLine(line);
                }
            }
        }
    }
}