Skeleton主要是读取骨骼系统的全量数据，和一些操作这些数据的方法，这些方法包括读取bone、slot、IK constraint、Transform constraint、Path constraint、Skin、Linked mesh、Event、Animation。

所有这些数据一般都会存两份，一份保存在SkeletonData中，作为初始数据记录，另一份存在Skeleton下，作为当前状态下这些属性值。