## Coding Tidbits and Style that Saved Me

> 记住一点，一个问题的解决方案不是非此即彼的，也可能多种方案都是可行的，只是要给选择问题而已，没必要在这上面太多争论。

**通用编程规范：**

1. **大括号**

   作者建议使用严格换行的方式，我的做法是函数内小于5行的代码块，使用K&R的方式，大于5行的代码块，大括号都严格换行写。所有的函数方法的大括号都严格执行换行。例如：

   ```cpp
   // 所有的方法后大括号，都同意换行
   void FunctionA()
   {
       for(int i = 0;i < 5;++i){
           cout << "小于5行的代码块，使用K&R方式" << endl;
       }
       
       if(true)
       {
           int a1;
           int a2;
           int a3;
           cout << "大于等于五行的代码块，使用严格换行方式" << endl;
           return;
       }
       
       return;
   }
   ```

2. **格式一致性**

   作者建议以下三种形式的都可行，但是重点是，在一个工程内的代码，都严格执行这种格式规范。我是使用第一种。对于所有第三方代码，都需要将其放置在单独一个目录内管理，防止和主程序代码混杂。

   ```cpp
   Action* FindBestAction(void);
   Action* findBestAction(void);
   Action* find_best_action(void);
   ```

   此外，对于不同类的相似功能的函数，建议使用相同命名。类似STL中，所有尾插入的容器，都是用push_back插入元素，而对于非尾插入类型容器，如unordered_map，则使用insert插入元素。

