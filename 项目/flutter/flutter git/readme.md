Flutter 是 Google强推的手机app sdk，它在Android和IOS上使用高效的原生接口。Flutter 能够很好的兼容现有代码，目前被很多开发者和组织使用，而且是开源的。

### Documentation

* [Install Flutter](./install.md)
* [Flutter documentation]()
* [Development wiki]()
* [Comtributing to Flutter]()

查看新release和重大修改，跟踪邮件列表  [flutter-announce@googlegroups.com](https://groups.google.com/forum/#!forum/flutter-announce)

### Flutter 简介

我们认为 Flutter 能够帮助开发者生成 beautiful & fast 手机app，同时保持着一个开放的、易扩展的、高效的开发模型。

#### Beautiful apps

我们的目标是让设计者将他们的创造力、灵感集中在他们的产品中，而不需要深入理解框架的实现。Flutter 的分层设计结构能够让开发者控制屏幕的每一个像素点，强大的功能组件能够让开发者控制图形、音频、文本，这个控制能力可以说是无限的。 Flutter有一整套小部件，能够支持在IOS和Android上面完美的像素体验。

#### Fast apps

Flutter运行很快，他和chrome、android同样的使用支持硬件加速的Skia 2D图形库。架构上尽量做到在手机上体验原生的运行速度。 Flutter代码通过Dart 平台编译，同时支持原生32和64位ARM代码（包括Android和IOS）。

#### Productive development

Flutter使用有状态的热加载，允许你在修改代码的同时立即看到结果，而不需要重新启动app，也不会丢失当前的状态。

#### Extensible and open model

Flutter拥有大量的开发工具，包括在VS code和Intellij/Android studio下的大量插件。Flutter 提供大量的package用来加速开发者的开发效率，无论是开发什么平台的app，而且获取平台特性也是很方便的，如下例：

```javascript
Future<void> getBatteryLevel() async{
    var batteryLevel = "unknown";
    try{
        int result = await methodChannel.invokeMethod('getBatteryLevel');
        batteryLevel = 'Battery level: $result%';
    }on PlatformException{
        batteryLevel = 'Failed to get battery level.';
    }
    setState((){
             _batteryLevel = batteryLevel;
             });
}
```

Flutter 是一个完全开源的项目，我们也欢迎开发者贡献代码，如何开始使用Flutter，可以查看[contributor guide](https://github.com/flutter/flutter/blob/master/CONTRIBUTING.md).