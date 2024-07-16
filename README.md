# 教程

制作Revit安装包时，自动添加、卸载addin文件的脚本

使用说明：

`InstallAndRemoveAddin.exe`文件为当前文件夹，
在当前文件夹下新建data目录，添加addin的模板文件，内容如下

```xml
<?xml version="1.0" encoding="utf-8"?>
<RevitAddIns>
  <AddIn Type="Application">
    <Name>xxx项目</Name>
    <Assembly>[InstalledFolder]\xxx.dll</Assembly>
    <ClientId>75548b64-64c1-777d-875f-39faeda92e32</ClientId>
    <FullClassName>xxx.xxx.xxx</FullClassName>
    <VendorId>autodesk</VendorId>
    <VendorDescription>www.autodesk.com</VendorDescription>
  </AddIn>
</RevitAddIns>

```

替换其中的一些数据为自己项目的具体数据:

- Name: 项目名称
- Assembly: xxx.dll替换为具体的dll名称，不要改变[InstalledFolder]
- ClientId: 生成一个GUID
- FullClassName: 实现了IExternalApplication接口的完全限定类名，即: 命名空间.类名

## 传参

安装
```
InstallAndRemoveAddin.exe addin文件的具体名称 install Revit版本号
InstallAndRemoveAddin.exe addin文件的具体名称 uninstall Revit版本号
```

如：addin文件为：`MyPlugin.addin`,安装命令如下
```
InstallAndRemoveAddin.exe MyPlugin install 2020
```

卸载
```
InstallAndRemoveAddin.exe MyPlugin uninstall 2020
```


Revit版本号:
- 2016
- 2017
- 2018
- 2019
- 2020
- ...