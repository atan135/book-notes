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
