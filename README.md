# 教程

制作 Revit 安装包时，自动添加、卸载 addin 文件的脚本

## 使用说明：

`InstallAndRemoveAddin.exe`文件为当前文件夹，
在当前文件夹下新建 data 目录，添加 addin 的模板文件，内容如下

1. `InstallAndRemoveAddin.exe`与`.addin`文件在同级目录
2. `InstallAndRemoveAddin.exe`与`.addin`文件放在`bin/data/scripts`下，类库的 dll 在 bin 下
3. addin文件中，安装路径字符串填充位为：`[InstallerFolder]`
4. 调用命令：

```ps
# 安装 后面的2018是revit的版本号，可改成2019、2020...等
InstallAndRemoveAddin.exe install 2018
# 卸载
InstallAndRemoveAddin.exe uninstall 2018
```

Revit 版本号:

- 2016
- 2017
- 2018
- 2019
- 2020
- ...

## 其他说明

### addin 内容

```xml
<?xml version="1.0" encoding="utf-8"?>
<RevitAddIns>
  <AddIn Type="Application">
    <Name>xxx项目</Name>
    <Assembly>[InstallerFolder]\xxx.dll</Assembly>
    <ClientId>75548b64-64c1-777d-875f-39faeda92e32</ClientId>
    <FullClassName>xxx.xxx.xxx</FullClassName>
    <VendorId>autodesk</VendorId>
    <VendorDescription>www.autodesk.com</VendorDescription>
  </AddIn>
</RevitAddIns>

```

替换其中的一些数据为自己项目的具体数据:

- Name: 项目名称
- Assembly: xxx.dll 替换为具体的 dll 名称，不要改变[InstalledFolder]
- ClientId: 生成一个 GUID
- FullClassName: 实现了 IExternalApplication 接口的完全限定类名，即: 命名空间.类名
