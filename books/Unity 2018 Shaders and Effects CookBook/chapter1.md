## 1. Post Processing Stack

必须是在Unity 2018才能使用的一个内置功能。如果没有安装，需要首先在Unity编辑器的**Package Manager**内安装，如下图：

![](http://cdn.zergzerg.cn/2018-11-16shader_1_0.png)

通过界面菜单的**Windows -> Package Manager**打开**Packages**界面，在All里面选择**Post-processing**后在右边install。

因为**Post-processing**这个shader是作用于屏幕的，所以需要将相关组件**Post Process Layer**添加进入场景**Camera**。如图，选择带有主场景**Camera**的**GameObject**，使用**Add Component** 输入**Post Process Layer** 后，选择**Layer**为**PostProcessing** 。

![](http://cdn.zergzerg.cn/2018-11-16shader_1_100.png)

然后在**Hierarchy** 下创建一个对象，通过主菜单**GameObject>3D Object>Post Process Volume**，勾选**Is Global** 选项，将Layter设置为**Post Processing** (尤其要注意)。 

![](http://cdn.zergzerg.cn/2018-11-16shader_1_101.png)

然后在创建一个Post Processing Profile，过程是在**Project**下选择**Create>Post Processing Profile** ,在Add Effect下选择**Grain、Vignette、Depth of Field** 这几个效果，用于之后测试。

* **Vignette：** Intensity是分别设置镜头的渲染范围的一个参数，值越大，周围被渲染的面积就越大，smoothness值用于边缘的模糊程度，值越大，界限越清晰，Color是用于周边的渲染基色。
* **Depth of Field：** 用于模糊效果，其中Focus Distance用于聚焦距离，Aperture标识光圈大小，Focal Length用于模糊聚焦距离，Max Blur Size用于整体模糊效果的一个参数。
* **Bloom：** 用于使明亮的场景变得更加的亮，并且会附加上一层光晕
* **Color Grading：** 用于设定整个场景的颜色基调，如冷色调 -> 暖色调，白昼 -> 黑夜
* **Ambient Occlusion：** 用于制造雾的朦胧效果

通过Profile检测使用PostProcessing和不使用做对比，性能没有明显影响。

如下图，是Vignette的smoothness设置0.3，Intensity分别设置为0、0.3、0.6、1.0的效果图

![](http://cdn.zergzerg.cn/2018-11-16shader_1_102.png)

下图是smoothness设置为1，Intensity设置为1的效果图

![](http://cdn.zergzerg.cn/2018-11-16shader_1_103.png)

下图是设置了Depth of Length效果，值分别为10，5，124，medium，可以看出有明显的模糊效果。

![](http://cdn.zergzerg.cn/2018-11-16shader_1_104.png)

下图是使用Color gradling做的处理对比，左图无处理，右图设置temperature为30，Hue shift为-20，Saturation为15.

![](http://cdn.zergzerg.cn/2018-11-16shader_1_105.png)

与之类似，可以尝试后面的一些功能。