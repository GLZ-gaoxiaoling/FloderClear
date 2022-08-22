using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FonderClear
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int i = 0;
        public MainWindow()
        {
            InitializeComponent();
        }



        public string[] director(string dirs,int fl)
        {
            string[] fileName = new string[fl];
            //绑定到指定的文件夹目录
            DirectoryInfo dir = new DirectoryInfo(dirs);
            //检索表示当前目录的文件
           // try
            //{
                FileSystemInfo[] fsinfos = dir.GetFileSystemInfos();
                //遍历检索的文件
                foreach (FileSystemInfo fsinfo in fsinfos)
                {
                    //判断是否为空文件夹　　
                    if (fsinfo is DirectoryInfo)
                    {
                        //递归调用
                        director(fsinfo.FullName, fl); ;
                    }
                    else
                    {
                        Console.WriteLine(fsinfo.FullName);
                        //将得到的文件全路径放入到集合中
                        fileName[i] = fsinfo.FullName;
                        i++;
                    }
                }
                return fileName;
            //}
            //catch (Exception)
           // {
           //     throw;
           // }

        }

        public string wjkzm(string string1)
        {
            string str;
            str = string1.Substring(string1.LastIndexOf(".") + 1, (string1.Length - string1.LastIndexOf(".") - 1));   //获取扩展名（疯狂套娃)
            return str;
        }

        private void Button_file_Click(object sender, RoutedEventArgs e)
        {
            string path = @Texbox1.Text;  //设置路径,@号代表不进行转义
            string[] strs = director(path, Directory.GetFiles(path).Length);   //右半边是doc里找的获取文件数量（逃）
            string files = string.Join(",\n",strs);
            string imanges="";
            string docs = "";
            foreach (string s in strs)
            {
                string ss = wjkzm(s);
                if (ss == "png" || ss == "jpg")
                {
                    imanges += (s + ",\n");
                }
                else if (ss == "doc" || ss == "docx")
                {
                    docs += (s + ",\n");
                }
            }
            MessageBox.Show("当前文件夹中的文件有：\n"+files);
            MessageBox.Show("当前文件夹中的图片有: \n"+imanges);
            MessageBox.Show("当前文件夹中的文档有: \n" + docs);
        }

        private void Button_Get_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "这边选择目录 Nya~~";
            dialog.Filter = "所有文件(*.*)|*.*";
            dialog.ShowDialog();
            Texbox1.Text = System.IO.Path.GetDirectoryName(dialog.FileName);
        }

        private void Texbox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
