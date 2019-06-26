BoneData是存储骨骼基本信息的类，包含有：

* Index：这个bone在Skeleton.Bones的index

* Name：这个bone的名称，在skeleton中是唯一的

* Parent：这个bone的父骨骼，可能为空

* Length：长度

* X：Local X translation

* Y：Local Y translation

* Rotation：Local rotation

* ScaleX：Local scale X

* ScaleY：Local scale Y

* ShearX：Local shearX

* ShearY：Local shearY

* TransformMode：转换形式，包括：

  * Normal
  * OnlyTranslation
  * NoRotationOrReflection
  * NoScale
  * NoScaleOrReflection

  这些模式都在[Bone.cs](Bone.md)中`UpdateWorldTransform` 方法有区分