## Spine 源码解读

spine源码划分为三部分，分别为：

* spine-csharp： 

  所有的底层结构代码

* spine-unity：

  所有的在unity中的插件功能代码

*  spine-unity-editor：

  所有的在unity插件中的编辑器功能

重点阅读spine-csharp源码，源码结构为：

**spine-csharp**

* [Attachments](Attachments.md)
  * AtlasAttachmentLoader
  * Attachment
  * AttachmentLoader
  * AttachmentType
  * BoundingBoxAttachment
  * ClippingAttachment
  * MeshAttachment
  * PathAttachment
  * PointAttachment
  * RegionAttachment
  * VertexAttachment
* Animation
* AnimationState
* AnimationStateData
* [Atlas](Atlas.md)
* [BlendMode](others.md)
* [Bone](Bone.md)
* [BoneData](BoneData.md)
* [Event](Event.md)
* [EventData](Event.md)
* [ExposedList](others.md)
* [IConstraint](others.md)
* [IkConstraint](Constraint.md)
* [IkConstraintData](Constraint.md)
* [IUpdatable](others.md)
* [Json](Json.md)
* [MathUtils](MathUtils.md)
* [PathConstraint](Constraint.md)
* [PathConstraintData](Constraint.md)
* [Skeleton](Skeleton.md)
* [SkeletonBinary](Skeleton.md)
* [SkeletonBounds](Skeleton.md)
* [SkeletonClipping](Skeleton.md)
* [SkeletonData](Skeleton.md)
* [SkeletonJson](Skeleton.md)
* [Skin](Skin.md)
* [Slot](Slot.md)
* [SlotData](Slot.md)
* [TransformConstraint](Constraint.md)
* [TransformConstraintData](Constraint.md)
* [Triangulator](Triangulator.md)