主要讲述Windows Install

## 安装需求：

为了正常安装、运行Flutter，开发环境必须符合下述最低要求：

* **操作系统**：Windows 7 SP1 或者更高（64bit）

* **磁盘空间**：400MB，不包括后续IDE/tools所使用的

* **工具**：安装 Flutter 需要当前环境中安装有：

  * Windows PowerShell 5.0 或者更新，这个在Win10是预安装的。

  * Git for Windows：在windows的cmd下使用git。

    如果git已经安装，请确认可以在cmd或者powershell下运行git命令。

## 获取Flutter开发工具包

1. 在官网下载Flutter SDK的最新的稳定版本。

   本次下载，地址为https://storage.googleapis.com/flutter_infra/releases/stable/windows/flutter_windows_v1.5.4-hotfix.2-stable.zip

2. 提取zip文件，将解压文件放置在一个不需要特权的文件夹目录下（不要放在C:\Program Files\）这类位置。

3. 在flutter下找到flutter_console.bat文件，双击启动。

现在就可以开始在Flutter控制台运行Flutter命令了！

如果需要更新Flutter的版本，查看[更新Flutter]()。

#### 更新path

如果你希望能够在cmd窗口下也运行Flutter命令，则需要将Flutter目录添加在环境变量中。

* 在开始按钮下，输入env然后在弹出窗口中选择仅为本人账号下设置环境变量（win7测试没有后一步）。
* 在用户变量下，选择Path进行编辑：
  * 如果Path存在，在再最后添加flutter\bin的全路径，用；隔开
  * 如果不存在，则新建一个变量名为Path，然后执行上一步。

为了使以上设置生效，需要重新启动控制台界面，才能看到新修改生效。

#### 运行flutter doctor

在之前 Flutter 目录下，打开控制台运行 flutter doctor命令（如果已经添加flutter\\bin 目录，则可以直接在控制台运行。查看结果，确定是否有一些平台依赖项还没有完成。

```shell
C:\src\flutter>flutter doctor
```

这个命令会检查你的当前环境，展示给你一个flutter整体安装状态，仔细查看是否有一些其他软件需要安装。

接下来几节会讲解如何安装这些缺失的软件。当完成一项后，可以再次运行 `flutter doctor` 指令查看是否正确安装了。

### 启动Android

Flutter需要安装Android studio来提供它的Android平台依赖，但是你可以使用其他文本编辑器来写Flutter apps，这个后续有提及。

#### 安装Android Studio

1. 下载、安装 [Android Studio](https://developer.android.com/studio).
2. 安装Android Studio，在安装流程中，安装好最新的Android SDK，Android SDK Platform-Tools，和Android SDK Build-Tools，这些都是Flutter开发安卓应用时需要的。

#### 启动安卓设备

为了测试、运行Flutter app，需要android设备支持Android 4.1（API level 16）或者更高。

1. 在android设备上勾选开发者选项和USB debugging选项。详细说明在[Android documentation](https://developer.android.com/studio/debug/dev-options).
2. 安装[Google USB Driver](https://developer.android.com/studio/run/win-usb) （仅windows下可用）。
3. 使用一根USB线连接安卓设备和电脑。运行`flutter device` 命令查看设备（没连上还）

Flutter默认使用adb tool的android sdk版本，如果想修改Flutter使用其他版本的android sdk，必须显式设置ANDROID_HOME环境变量。

#### 启动安卓模拟器

在安卓模拟器中启动、运行Flutter app的流程如下：

1. 开启电脑的VM acceleration设置
2. 启动 **Android Studio > Tools > Android > AVD Manager** ,然后选定 **Create Virtual Machine** , **Android** 按钮仅当打开一个android 项目时候存在。
3. 选择一个设备类型，点击 **Next** 。
4. 选择一个或者更多你的模拟器安卓版本下需要的系统镜像，推荐选择x86或者x86_64。
5. 在模拟器performance下，选择 **Hardware - GLE 2.0** 以开启硬件加速。
6. 确认AVD configuration设置正确，点击 **Finish** 。详细说明，查看[Managing AVDs](https://developer.android.com/studio/run/managing-avds).
7. 在安卓虚拟器管理器中，点击 **Run** ，就开始运行了。