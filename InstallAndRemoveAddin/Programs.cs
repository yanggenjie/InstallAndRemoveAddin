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
        public static void Main(string[] args)
        {
            if (args.Length != 1) return;
            if (args[0] == "install")
            {
                CopyAddinFile();
            }
            else if (args[0] == "uninstall")
            {
                DeleteAddinFile();
            }
        }
        const string addinName = "RebarSkeleton.addin";

        static void CopyAddinFile()
        {
            string curPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // 当前版本
            var version = Path.GetFileName(curPath);

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

        static void DeleteAddinFile()
        {
            string curPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // 当前版本
            var version = Path.GetFileName(curPath);
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
