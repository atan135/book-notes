## 7. Controlling the Main Loop

**两种主循环方式：**

![](http://cdn.zergzerg.cn/2018-10-24graphics_7.png)

只把render system分离的原因是，其他子系统功能有互相通信的需求，而图形渲染只有单独的接受信息、显示的需求。

