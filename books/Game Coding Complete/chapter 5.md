## Initialization and Shutdown

游戏软件有一个主循环，当启动时，主循环会接收并翻译用户输入，更新游戏状态，render游戏界面，知道主循环被打断。主循环被终止后，关闭清理程序会释放游戏资源，关闭文件，退回到操作系统。

**一个windows game的启动过程：**

* 检查系统资源：硬盘空间，内存，输入输出设备
* 检查CPU主频
* 初始化基本随机数生成器
* 加载游戏debug的选项信息
* 初始化内存cache
* 新建游戏窗口
* 初始化音频系统
* 加载游戏玩家信息或者已存档信息
* 创建drawing surface
* 初始化游戏子系统：物理系统，AI系统等
