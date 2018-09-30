using System;
using System.Windows.Forms;
using System.IO;


namespace FotoSort
{
    public partial class Form1 : Form
    {
        public string PathFolder { get; set; }
        public Form1()
        {
            InitializeComponent();
            
        }

        private void ButtonFile_Click(object sender, EventArgs e)
        {

            OpenFileDialog f = new OpenFileDialog
            {
                Multiselect = true
            };
            if (f.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in f.FileNames)
                {
                    ReadExifInfo info = new ReadExifInfo(file);
                    string[] s = info.CreateTime.ToString().Split();
                    string newPath = PathFolder + "\\" + s[0].Substring(3);

                    if (Directory.Exists(newPath))
                    {
                        File.Move(file, newPath + "\\" + Path.GetFileName(file));

                    }
                    else
                    {
                        Directory.CreateDirectory(newPath);
                        File.Move(file, newPath + "\\" + Path.GetFileName(file));
                    }
                }
            }

        }

        private void ButtonFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                PathFolder = FBD.SelectedPath;
            }
            

        }
    }

}
