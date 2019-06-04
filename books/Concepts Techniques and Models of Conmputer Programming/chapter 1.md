### 1. Variable

理解变量名、地址、变量值的概念

变量初始assign只能绑定一次地址，之后的赋值操作可以多次进行，绑定后变量-地址的关系不会发生改变，但是可以通过变量名覆盖重新声明一个新的同样名字的变量。

### 2. Functions

没啥好说的，方法调用

### 3. Lists

Pascal的Lists，用L.1表示node，L.2表示next

### 4. Functions over Lists

也没啥说的，使用递归实现了一个pascal三角的计算函数

### 5. Correctness

主要是关于使用递推证明代码运行正确性的。

### 6 Complexity

复杂度，$$O(n) 和\Omega(n)$$ 之类的

### 7. Lazy evaluation

这个有点意思，计算的中间量是存储的，防止反复调用的时候会重复计算。

### 8. Higher-order Programing

能够把函数作为参数传递的编程方法

### 9. Concurrency

讲thread相关的

### 10. Dataflow

线程间有关联时候的等待

## 11. State

就是变量，存储值

### 12. Object

方法+变量，组成对象，能够封装成员变量，防止被外界修改

### 13. Class

。。。

### 14. Nondeterminism and time

并发过程的时序问题导致的数据复写

### 15. Atomicity

原子操作、加锁