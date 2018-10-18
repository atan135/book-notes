## Building your game

大型游戏一般需要使用资源版本管理工具，像SVN，Perforce之类的

游戏资源一般会使用分支功能，如trunk，gold，test分支

其中trunk为测试主分支，gold为稳定版本分支，test是为了通过一个需要修改影响比较多的地方的分支。

build需要自动化脚本，具体过程如下

Open Script:

* 获取trunk分支的当前所有文件
* 解锁 gold分支，并恢复其中所有的修改
* 将trunk的所有文件强制集成到gold分支
* 提交gold分支用于build

Close Script：

* 获取gold分支的所有当前资源
* 将gold分支集成到trunk分支中
* resolve所有的修改冲突
* 提交trunk分支
* lock所有gold分支，防止后续修改

这样可以保证gold代码的稳定性，防止被随意篡改

**作家建议：**

> My parting advice: Always automate the monkey work, give the test team a good
> build every time, and never ever get in the way of a developer in the zone. 

对重复性的工作一定要使用自动化脚本处理，并且让其他开发者适应这样的模式，这样才能方便测试人员时刻拥有一个无故障的版本构建。