### BlendMode

BlendMode是动画融合的类型，包括四种：

* **Normal**
* **Additive**
* **Multiply**
* **Screen**

### ExposedList

是一个封装的数组，内部使用Array形式储存，可以将IEnumerate和IColletion类型的数据转化进来，统一处理。封装了不少list、stack的操作方法，以及使用version保证迭代过程数据没有发生变化。

### IConstraint

集成IUpdatable，增加一个Order接口

### IUpdatable

一个提供Update方法的接口

