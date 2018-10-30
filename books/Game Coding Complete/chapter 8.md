## 8. Loading and Caching Game Data 

![](http://p9zl5r4hu.bkt.clouddn.com/2018-10-29graphics.png)

资源文件分类：

* **3D Mesh和环境：** 

  包含的是vertex, triangles, and related texture, lighting map.

* **Animation：**

  12bytes for position + 12 bytes for orientation = 24 bytes per sample

  30 samples per second * 24 bytes per sample = 720 bytes per second

  720 bytes per second * 30 bones = 21600 bytes per second

  **关键帧：** 每一秒或者间隔时间，需要保存一次所有的Animation的点位置和方向的全部信息，这种保存的帧称为KeyFrames。作用是可以减少累积误差和方便处理动画切换。

* **Map / Level Data：**

* **Texture Data：**

  注意图像的颜色占用字节，注意压缩是否是无损压缩。mip-map使用

* **Sound and Music Data：**

