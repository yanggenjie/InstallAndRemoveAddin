# 删除旧的bin
Remove-Item -Recurse ../source/bin
# 编译
Msbuild.exe ../source/InstallAndRemoveAddin.sln -r /t:rebuild /p:Configuration=Release

# 删除pdb、config等无用文件文件
Remove-Item  ../source/bin/*.pdb
Remove-Item  ../source/bin/*.config

# 加密
Confuser.CLI.exe -noPause '.\EncrypDll(ConfuserEx).crproj'                    

# 打包安装包，使用innoSetup制作安装包，需要先安装，然后添加安装路径到环境变量才能使用Iscc命令
# ISCC.exe './RebarSkeleton(InnoSetup).iss'
