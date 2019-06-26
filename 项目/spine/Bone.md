Bone 类继承了一个[IUpdatable](others.md) ，用于存储一个骨骼的当前状态，每一根骨骼都有一个local transform，用于计算运行时的world transform，一个骨骼也有一个applied transform，也能用于集散world transform，这两者之间的区别是如果存在一个constraint或者应用代码在计算后还修改了world transform。

Bone类有几个基本方法：

*  `UpdateWorldTransform` ：用于根据本地位置计算世界坐标
* `SetToSetupPose`：用于将Bone属性设置为初始状态（存储在BoneData里）
* `UpdateAppliedTransform`：用于根据世界坐标计算applied transform，这个方法用于计算在收到外力时，骨骼的transform变动



这些方法基本都是几何化的公式计算，就没必要仔细看了，主要是用于骨骼动作的计算。