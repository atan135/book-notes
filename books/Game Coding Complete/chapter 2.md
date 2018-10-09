**Game Architecture:**

* **应用层：** 处理关联游戏和底层硬件、操作系统，如输入事件，网络，多线程，游戏初始化及关闭。
* **游戏逻辑层：** 处理游戏状态，游戏内部世界逻辑，如物理系统
* **游戏显示层：** 处理游戏界面显示、音频等

```flow
st=>start: 应用层
ed=>end: 显示层（可以多层）
op=>operation: 游戏逻辑层
st->op->ed
```

**应用运行逻辑：**

Game Logic层负责接收数据、处理数据、返回数据、维系状态，Game View层负责接收Game Logic数据，展示图像、音频、特效等。这里无论AI对象还是玩家Player，都是会以相同方式接收Game Logic层的状态更新，依据状态更新做出对应的展示反应。

并且屏蔽Game Logic的本地更新和remote更新的差异。

**Main Loop:**

* 获取用户输入
* 处理游戏逻辑
* 展示输出，包括图像、音频和状态发送至其他联机玩家

**子系统：**

* 时钟系统
* 字符串处理
* 动态库加载：例如DirectX和OpenGL运行库的选择
* 线程和线程同步：使用多线程可以充分利用多核处理器、但是也给游戏不同功能同步增加了难度
* 网络通信
* 初始化
* 主循环
* 关机处理



#### 一个样例-----飞行类游戏

飞行类游戏，当飞机停在地表时，可见视野很小，所以GPU需要计算、**render**的图形单元也会很少，但是当飞机飞到一定高度的时候，地表视野是和高度的平方成正比的，导致需要绘制的地表图元非常多，当单个**frame**处理不过来的时候，就会出现卡顿。这里，一种解决方案是，对距离不同的地表图元，引用不同分辨率的美术资源，但是这样的话，如果慢放，会发现某处资源突变为更细致的资源时候，有个加载卡顿的过程。3D引擎有方法使得这个渐变过程更加的平滑。（文章没具体说，应该后面章节有描述）



## GameView

![](http://p9zl5r4hu.bkt.clouddn.com/2018-10-09gameview.png)

在游戏系统中，player和AI有着相同的GameLogic层，处理逻辑，但是Gameview层是有着不同的模块。

AI Agent中：

* **Stimulus Interpreter**：用于处理接收event、过滤event
* **Decision System**：是AI行为决策的处理系统
* **Process Manager**：如果AI的某项行为、处理某些事件需要消耗多个frametime，那么就需要使用这个模块。
* **Options**：选项配置，在开发调试过程中十分重要。

## Game Network Architecture

![](http://p9zl5r4hu.bkt.clouddn.com/2018-10-09network_architecture.png)

**remote game view** ：

位于服务端，被服务端GameLogic认为是和本地Player或者AI一样的模块，它与Game Logic使用事件event交互（和其他AI没有区别），但是对接收到的事件经过处理后使用TCP/UDP将encode的消息发送出去，接收来自remote client的**remote game logic**返回的控制信息。

**remote game logic**：

位于客户端，游戏的最终数据一致性在服务端的geme logic层，客户端的remote game logic用于保存一份数据备份，同时需要一种策略用于管理之间消息传递的延迟。

**样例：** 

射击游戏，网速会拖累玩家，因为玩家接收到一个事件，如果太延迟，remote game logic需要处理适配，让事件和服务端的game logic同步。所以可能会出现，射向你的子弹速度，远快于你发射的子弹速度。同理，网速差的玩家建主，对其他玩家不公平，因为他们都会对你的事件有延迟。



## DirectX和OpenGL的选择

DirectX设计在位于应用层和应用层之间，如果一个Action硬件层能够处理，那么DirectX就会调用驱动完成。如果硬件层没有相关的处理功能，那么DirectX就会在软件层面模拟这个调用，这个的速度就会更慢。