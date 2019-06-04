首先新建一个项目（我使用了一个ConsoleApp控制台项目）

然后在 **工具/NuGet包管理器/管理解决方案的NuGet程序包** 中搜索 *Google.Protobuf和Google.Protobuf.Tools* ，分别安装，然后查看Tools的位置在该位置下，获取到protoc.exe。

代码中使用pb类的方法为：

1. 生成proto文件，样例如：

   ```protobuf
   syntax = "proto3";						// protoc生成器只能对声明proto3的proto文件解析
   option csharp_namespace = "PackTest";	// 生成的.cs文件的namespace
   package Test;							// 位于哪个包中，也即将会生成的.cs文件名称
   
   message IntTest{
   	uint32 num = 1;						// proto3默认所有成员都是optional属性
   	string str = 2;						// 且不再允许使用required字段
   	repeated uint32 scores = 3;
   }
   ```

2. 在cmd下使用命令`protoc -I=$SRC_DIR --csharp_out=$DEST_DIR pbname.proto ` 

3. 将生成的.cs文件加入项目中

4. 在main方法中得到调用，例如

   ```c#
   using System;
   using System.Diagnostics;
   
   namespace ConsoleApp1
   {
       class Program
       {
           static void Main(string[] args)
           {
               PackTest.IntTest it = new PackTest.IntTest();
               it.Num = 5;
               it.Str = "ted";
               it.Scores.Add(1);
               it.Scores.Add(2);
               Debug.WriteLine(it.ToString());
           }
       }
   }
   
   ```
