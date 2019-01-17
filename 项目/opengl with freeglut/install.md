## OpenGL环境配置

### 1. 安装MinGW（Windows）

官网：http://www.mingw.org/ 进入Downloads下载对应的下载器

点击安装后，在MinGw Installation Manager中选择base/gcc/g++/tools这四个选项更新下载

在windows的环境配置中添加Path（这里默认下载位置为C:\MinGW\bin）

### 2. 配置对应的Visual Studio Code

在.vscode文件夹下分别新建launch.json和tasks.json

launch.json:

```json
{
    // 使用 IntelliSense 了解相关属性。 
    // 悬停以查看现有属性的描述。
    // 欲了解更多信息，请访问: https://go.microsoft.com/fwlink/?linkid=830387
    "version": "0.2.0",
    "configurations": [
        {
            // 配置名称，将会在启动配置的下拉菜单中显示
            "name": "C++ Launch (GDB)",        
            // 配置类型，这里只能为cppdbg
            "type": "cppdbg",     
            // 请求配置类型，可以为launch（启动）或attach（附加）
            "request": "launch",          
            // 调试器启动类型，这里只能为Local
            //"launchOptionType": "Local",    
            // 生成目标架构，一般为x86或x64，可以为x86, arm, arm64, x64, amd64, x86_64
            "targetArchitecture": "x86",      
            // 将要进行调试的程序的路径
            "program": "${file}.exe",     
            // miDebugger的路径，注意这里要与MinGw的路径对应
            "miDebuggerPath":"c:\\MinGW\\bin\\gdb.exe", 
            // 程序调试时传递给程序的命令行参数，一般设为空即可
            "args": ["name",  "zerg", "# #"],    
            // 设为true时程序将暂停在程序入口处，一般设置为false
            "stopAtEntry": false,          
            // 调试程序时的工作目录，一般为${workspaceRoot}即代码所在目录
            "cwd": "${workspaceRoot}",      
            // 调试时是否显示控制台窗口，一般设置为true显示控制台
            "externalConsole": true,       
            // 调试会话开始前执行的任务，一般为编译程序，c++为g++, c为gcc
            "preLaunchTask": "g++"　　                  
        }
    ]
}
```

tasks.json

```json
{
    "version": "2.0.0",
    "command": "g++",
    "args": ["-g","${file}","-o","${file}.exe"],    // 编译命令参数
    "problemMatcher": {
        "owner": "cpp",
        "fileLocation": ["relative", "${workspaceRoot}"],
        "pattern": {
            "regexp": "^(.*):(\\d+):(\\d+):\\s+(warning|error):\\s+(.*)$",
            "file": 1,
            "line": 2,
            "column": 3,
            "severity": 4,
            "message": 5
        }
    }
}
```

### 3. 下载freeglut

下载地址：http://freeglut.sourceforge.net/index.php#download

选择Prepackaged Releases的for MinGW，解压文件中的include/GL放入MinGW的include目录下，

x64版本的freeglut.dll 放入C:\Windows\SysWOW64下

或者在编译、运行时关联到对应的文件头和lib库，这种方式可能更好

