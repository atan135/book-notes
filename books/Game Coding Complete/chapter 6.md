## 6. Game Actors and Component Architecture

不允许使用多继承中基类都含有数据字段，因为这样可能会导致菱形继承，产生未定义的行为。允许使用多继承中，一个基类包含数据字段，其余基类只包含纯虚函数。

当然，如果条件许可，这类缺陷可以通过使用component架构来避免，设计上component应该尽量简洁，只为一个功能服务。这里的GetComponent()方法比较有意思，内部存储的是share_ptr，但是获取的是weak_ptr，原因是可以安全的获得引用，但是又不会增加share_ptr的引用计算，避免影响到component的destroy。

介绍了几种游戏中Actor的组织形式：

* 使用map结构（红黑树），保持增删改查在$$\log(n)$$的时间
* 使用vector结构，保持查找在$$\Omicron(1)$$的时间内，但是不适合频繁增删Actor类型游戏，如FPS
* 使用区域划分结构。也是避免全局查找的一种方法。

Actor中Component的信息传递方法：

1. 直接调用：如果是组件间直接、简单的消息调用
2. 事件机制：如果是需要广播类的消息