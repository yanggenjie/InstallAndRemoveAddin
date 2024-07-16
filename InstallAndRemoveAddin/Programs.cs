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
            if (string.IsNullOrWhiteSpace(addinName)) return;

            if (args[1] == "install")
            {
                CopyAddinFile(args[2]);
            }
            else if (args[0] == "uninstall")
            {
                DeleteAddinFile(args[2]);
            }
        }

        static void CopyAddinFile(string version)
        {
            string curPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // 当前addin模板文件
            string addinFileName = Path.Combine(curPath, "data", addinName);
            if (!File.Exists(addinFileName)) return;

            // 目标文件夹
            string addinFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $"\\Autodesk\\Revit\\Addins\\{version}";
            string destAddinFileName = Path.Combine(addinFolder, addinName);

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
            string destAddinFileName = Path.Combine(addinFolder, addinName);
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
