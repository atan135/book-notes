markdown的sequence插件来源于js-sequence-diagram(https://bramp.github.io/js-sequence-diagrams/)，使用方式很简洁，官方js甚至提供了化为草稿的模式。本篇通过一个样例展示sequence的全部用法。

**主要使用的标记符号有：**

* title
* participant: 修饰符as
* note: 修饰符 left of, right of, over
* 转移符号：-\\--（代表实线虚线）, >\\>>（实心箭头空心箭头）

   标记词是忽略大小写的，

**样例：**

```markdown
​```sequence
TITLE: this is a title
PARTICIPANT you as y
participant me as m
note left of y: you note sth
note right of other: other say sth
note over m: I say sth
note over m, other: sth of me and other
y -> m: hello
m --> other: who are you
y -->> other: also say hello
​```
```

```sequence
TITLE: this is a title
PARTICIPANT you as y
participant me as m
note left of y: you note sth
note right of other: other say sth
note over m: I say sth
note over m, other: sth of me and other
y -> m: hello
m --> other: who are you
y -->> other: also say hello
```



