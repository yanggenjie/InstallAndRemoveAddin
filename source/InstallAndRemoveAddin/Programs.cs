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
        static string addinName;
        /// <summary>
        /// exe和addin模板结构：exe所在文件夹为当前文件夹,./data/xxx.addin
        /// 输入三个参数：参数1：addin文件名，不带后缀；参数2：install或者uninstall，参数3：revit版本:2020、2021等数字
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            if (args.Length != 3) return;
            addinName = args[0];
            Console.WriteLine($"arg[0]: {args[0]}");
            Console.WriteLine($"arg[1]: {args[1]}");
            Console.WriteLine($"arg[2]: {args[2]}");
            if (string.IsNullOrWhiteSpace(addinName)) return;

            if (args[1] == "install")
            {
                Console.WriteLine("安装");
                CopyAddinFile(args[2]);
            }
            else if (args[1] == "uninstall")
            {
                DeleteAddinFile(args[2]);
            }
        }

        static void CopyAddinFile(string version)
        {
            string curPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // 当前addin模板文件
            string addinFileName = Path.Combine(curPath, "data", $"{addinName}.addin");
            if (!File.Exists(addinFileName))
            {
                Console.WriteLine($"文件不存在:{addinFileName}");
                return;
            }

            // 目标文件夹
            string addinFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $"\\Autodesk\\Revit\\Addins\\{version}";
            string destAddinFileName = Path.Combine(addinFolder, $"{addinName}.addin");
            Console.WriteLine($"源文件:{addinFileName}");
            Console.WriteLine($"目标文件:{destAddinFileName}");
            // 复制
            File.Copy(addinFileName, destAddinFileName, true);

            // 修改addin文件为具体的目录
            string replacedIndicator = "[InstalledFolder]";

            if (!File.Exists(destAddinFileName)) return;
            var content = File.ReadAllText(destAddinFileName);

            content = content.Replace(replacedIndicator, curPath);
            File.WriteAllText(destAddinFileName, content);
        }

        static void DeleteAddinFile(string version)
        {
            string curPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // 当前版本
            string addinFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $"\\Autodesk\\Revit\\Addins\\{version}";
            string destAddinFileName = Path.Combine(addinFolder, $"{addinName}.addin");
            if (File.Exists(destAddinFileName))
            {
                try
                {
                    File.Delete(destAddinFileName);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
