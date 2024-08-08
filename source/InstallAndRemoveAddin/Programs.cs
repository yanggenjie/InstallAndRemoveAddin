using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace InstallAndRemoveAddin
{
    public class Program
    {
        /// <summary>
        /// exe和addin模板结构：addin 和本exe在同目录下,放在data目录里面
        /// 输入三个参数：参数1：addin文件名，不带后缀；参数2：install或者uninstall，参数3：revit版本:2020、2021等数字
        /// </summary>
        /// <param name="args">args[0]： install or uninstall \n args[1]: Revit version number: 2018、2019...</param>
        public static void Main(string[] args)
        {
            if (args.Length != 2) return;

            string act = args[0];
            string version = args[1];
            Console.WriteLine($"arg[0]: {act}");
            Console.WriteLine($"arg[1]: {version}");

            if (string.IsNullOrWhiteSpace(version))
            {
                Console.WriteLine("args[0]： install or uninstall\n args[1]: Revit version number,ex: 2018、2019...");
                return;
            }

            if (act == "install")
            {
                Console.WriteLine("install ");
                CopyAddinFile(version);
            }
            else if (act == "uninstall")
            {
                Console.WriteLine("uninstall");
                DeleteAddinFile(version);
            }
        }

        static void CopyAddinFile(string version)
        {
            string curPath = Environment.CurrentDirectory;
            // 当前addin模板文件
            string addinFilePath = Directory.GetFiles(curPath, "*.addin", SearchOption.TopDirectoryOnly).FirstOrDefault();
            if (!File.Exists(addinFilePath))
            {
                Console.WriteLine($"File not existed!:{addinFilePath}");
                return;
            }
            string addinFileName = Path.GetFileName(addinFilePath);
            // 目标文件夹
            string addinFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $"\\Autodesk\\Revit\\Addins\\{version}";
            string destAddinFileName = Path.Combine(addinFolder, addinFileName);
            Console.WriteLine($"source file:{addinFilePath}");
            Console.WriteLine($"target file:{destAddinFileName}");
            // 复制
            File.Copy(addinFilePath, destAddinFileName, true);

            // 修改addin文件为具体的目录
            string replacedIndicator = "[InstallerFolder]";

            if (!File.Exists(destAddinFileName)) return;
            var content = File.ReadAllText(destAddinFileName);

            string rootDir = Path.GetDirectoryName(Path.GetDirectoryName(curPath));
            content = content.Replace(replacedIndicator, rootDir);
            File.WriteAllText(destAddinFileName, content);
            Console.WriteLine($"successed! addin file path：{destAddinFileName}");
        }

        static void DeleteAddinFile(string version)
        {
            string curPath = Environment.CurrentDirectory;

            // 从当前同级目录下找到addin文件名
            string addinFilePath = Directory.GetFiles(curPath, "*.addin", SearchOption.TopDirectoryOnly).FirstOrDefault();
            if (!File.Exists(addinFilePath))
            {
                Console.WriteLine($"File not existed!:{addinFilePath}");
                return;
            }
            string addinFileName = Path.GetFileName(addinFilePath);
            // 删除addin文件
            string addinFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $"\\Autodesk\\Revit\\Addins\\{version}";
            string destAddinFileName = Path.Combine(addinFolder, addinFileName);
            if (File.Exists(destAddinFileName))
            {
                try
                {
                    File.Delete(destAddinFileName);
                    Console.WriteLine($"uninstall done! File {destAddinFileName} removed.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"uninstall failed!，{ex.Message}");
                }
            }
        }
    }
}
