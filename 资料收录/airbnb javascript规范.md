
<!-- @import "[TOC]" {cmd="toc" depthFrom=1 depthTo=6 orderedList=false} -->

<!-- code_chunk_output -->

- [Types](#types)
- [引用类型](#引用类型)
- [Objects 对象](#objects-对象)
- [Destructuring 结构化赋值](#destructuring-结构化赋值)
- [Strings 数组](#strings-数组)
- [Functions 方法](#functions-方法)
- [Arrow Functions 箭头方法](#arrow-functions-箭头方法)
- [Classes & constructors 类和构造器](#classes-constructors-类和构造器)
- [Modules 模块](#modules-模块)
- [Iterators and Generators](#iterators-and-generators)
- [Properties 属性](#properties-属性)
- [Variables 变量](#variables-变量)
- [Hoisting 变量提升](#hoisting-变量提升)
- [Comparison Operation & Equality 比较运算符和相等](#comparison-operation-equality-比较运算符和相等)
- [Blocks](#blocks)
- [Control Statements](#control-statements)
- [Comments 注释](#comments-注释)
- [Whitespace 空格](#whitespace-空格)
- [Commas 逗号](#commas-逗号)
- [Semicolons 分号](#semicolons-分号)
- [Type Casting & Coercion 类型转换](#type-casting-coercion-类型转换)
- [Naming Conventions 命名规则](#naming-conventions-命名规则)
- [Accessors](#accessors)
- [Events 事件](#events-事件)
- [jQuery](#jquery)
- [ECMAScript 5 适配](#ecmascript-5-适配)
- [ECMAScript 6+ (ES 2015+) Styles](#ecmascript-6-es-2015-styles)
- [Standard Library](#standard-library)
- [Testing 测试](#testing-测试)
- [Performance 性能](#performance-性能)
- [Resouces 资料](#resouces-资料)
- [License](#license)

<!-- /code_chunk_output -->

## Types
* 1.1 基本类型
  * `string`
  * `number`
  * `boolean`
  * `null`
  * `undefined`
  * `symbol`
  * `bigint`
```javascript
const foo = 1;
let bar = foo;

bar = 9;

console.log(foo, bar);  // => 1, 9
```
> Symbols 和 Bigint 慎重使用，不是所有环境都能支持

* 1.2 Complex 类型: 获取的都是值的引用
  * `object`
  * `array`
  * `function`
```javascript
const foo = [1, 2];
const bar = foo;

bar[0] = 9;

console.log(foo[0], bar[0]); // => 9, 9
```

## 引用类型
* 2.1 所有引用类型，使用 `const` 而避免使用 `var`， eslint： `prefer-const`, `no-const-assign`
  > 原因： 确保不会错误的赋值

```javascript
// bad
var a = 1;
var b = 2;

// good
const a = 1;
const b = 2;
```

* 2.2 如果确认要重新赋值引用， 使用 `let` 代替 `var`，eslint: `no-var`
> 原因： `let` 是块结构变量， `var` 是函数范围变量
```javascript
// bad
var count = 1;
if(true){
    const += 1;
}

// good, use the let
let count = 1;
if(true){
    count += 1;
}
```
* 2.3 注意， `let` 和 `const` 都是块结构变量， `var` 是函数内变量
```javascript
// const and let only exist in the blocks they are defined in.
{
    let a = 1;
    const b = 1;
    var c = 1;
}
console.log(a); // ReferenceError
console.log(b); // ReferenceError
console.log(c); // Prints 1
```

## Objects 对象
* 3.1 对象生成使用直接生成，eslint: `no-new-object`
```javascript
// bad
const item = new Object();

// good
const item = {}
```
* 3.2 在生成包含动态属性的对象时，使用计算属性名称
> 这样可以在一个地方定义所有属性名
```javascript
function getKey(k){
    return `a key named ${k}`;
}
// bad
const obj = {
    id: 5,
    name: 'San Francisco',
};
obj[getKey('enabled')] = true

// good
const obj = {
    id: 5,
    name: 'San Francisco',
    [getKey('enabled')]: true,
};
```
* 3.3 使用对象成员方法缩写，eslint: `object-shorthand`
```javascript
// bad
const atom = {
    value: 1,
    addValue: function(value){
        return atom.value + value;
    },
};

// good
const atom = {
    value: 1,
    addValue(value){
        return atom.value + value;
    },
};

* 3.4 使用对象属性缩写，eslint：`object-shorthand`
> 原因：代码自描述，且更简洁
```javascript
const lukeSkywalker = 'Luke Skywalker';
// bad
const obj = {
    lukeSkywalker: lukeSkywalker,
};
// good
const obj = {
    lukeSkyluker,
};
```

* 3.5 将缩写的对象属性组织在对象的开头处
```javascript
const anakinSkywalker = 'Anakin Skywalker';
const lukeSkywalker = 'Luke Skywalker';
// bad
const obj = {
    episodeOne: 1,
    anakinSkywalker,
    episodeTwo: 2,
    lukeSkywalker,
};

// good
const obj = {
    lukeSkywalker,
    anakinSkywalker,
    episodeOne: 1,
    episodeTwo: 2,
};
```

* 3.6 属性不支持使用分号包含，eslint: `quote-props`
> 因为这样可以使语言高亮支持更好
```javascript
// bad
 const bad = {
     'foo': 3,
     'bar': 4,
 };

 // good
 const good = {
     foo: 3,
     bar: 4,
 };
 ```

 *3.7 不要直接使用 `Object.prototype` 方法，例如 `hasOwnProperty`, `propertyIsEnumerable`, `isPrototypeOf` 。eslint: `no-prototype-builtins`
 > 直接调用对象的这些方法，可能会被覆盖。
 ```javascript
 // bad
 console.log(object.hasOwnProperty(key));

 // good
 console.log(Object.prototype.hasOwnProperty.call(object, key));

 // best
 const has = Object.prototype.hasOwnProperty;  // 在模块范围内缓存这个引用
 console.log(has.call(object, key));
 // or
 import has from 'has'; // https://www.npmjs.com/package/has
 console.log(has(object, key));
 ```

 * 3.8 `Object.assign` 的使用方法选择。eslint：`prefer-object-spread`
 ```javascript
 // very bad
 const original = {a: 1, b: 2};
 const copy = Object.assign(original, {c:3});   // this mutates 'original' 同时修改了original
 delete copy.a; // so does this
 
 // bad
 const original = {a:1, b:2};
 const copy = Object.assign({}, original, {c:3});   // copy => {a:1, b:2, c:3}

 // good
 const original = {a:1, b:2};
 const copy = {...original, c: 3};  
 const {a, ...noA} = copy   // noA => {b:2, c:3}
 ```

 ## Arrays 数组

 * 4.1 使用字符串创建方式，而不是new. eslint: `no-array-constructor`
 ```javascript
 // bad
 const items = new Array();

 // good
 const items = [];
 ```

 * 4.2 使用 <font color=blue>Array#push</font> 代替直接尾部添加

```javascript
const someStack = []
// bad
someStack[someStack.length] = 'abc';

// good
someStack.push('abc');

```

* 4.3 使用延展符合 `...` 复制数组
```javascript
// bad
const len = items.length;
const itemsCopy = [];
let i;

for(i = 0;i < len; i+= 1){
    itemsCopy[i] = items[i];
}
// good
const itemsCopy = [...items];
```

* 4.4 将一个可迭代的对象转换为数组，使用延展符号 `...` 代替 `Array.from`
```javascript
const foo = document.querySelectorAll('.foo');
// good
const nodes = Array.from(foo);
// best 
const nodes = [...foo];
```

* 4.5 使用 `Array.from` 讲一个类Array对象转换为Array

```javascript
const arrLike = {0: 'foo', 1: 'bar', 2: 'baz', len: 3};
// bad
const arr = Array.prototype.slice.call(arrLike);
// good
const arr = Array.from(arrLike);
```

* 4.6 在可迭代对象使用mapping映射时候，使用 `Array.from` 代替 `...` ，因为可以避免生成中间数组。
```javascript
// bad 
const baz = [...foo].map(bar);

// good
const baz = Array.from(foo, bar);
```

* 4.7 在array的回调方法中，适当使用return, eslint: `array-callback-return`

```javascript
// good
[1, 2, 3].map((x)=>{
    const y = x + 1;
    return x * y;
});

// good
[1, 2, 3].map((x) => x + 1);

// bad no return meas acc becomes undefined after first iteration
[[0, 1], [2, 3], [4, 5]].reduce((acc, item, index) => {
    const flatten = acc.concat(item);
});

// good
[[0, 1], [2, 3], [4, 5]].reduce((acc, item, index) => {
    const flatten = acc.concat(item);
    return flatten;
});


// bad
inbox.filter((msg) => {
    const { subject, author } = msg;
    if(subject == 'Mockingbird'){
        return author === 'Harper Lee';
    }else{
        return false;
    }
});

// good
inbox.filter((msg) => {
    const {subject, author} = msg;
    if(subject === 'Mockingbird'){
        return author === 'Harper Lee';
    }
    return false;
})
```

* 4.8 如果一个数组有多行，适当使用断行符

```javascript
// bad
const arr = [
    [0, 1], [2, 3], [4, 5],
];

const objectInArray = [{
    id: 1,
}, {
    id: 2,
}];
const numberInArray = [
    1, 2,
];

// good
const arr = [[0, 1], [2, 3], [4, 5]];
const objectInArray = [
    {
        id: 1,
    },
    {
        id: 2,
    },
];
const numberInArray = [
    1,
    2,
];
```

## Destructuring 结构化赋值
* 5.1 使用对象的多个属性时，使用结构化赋值。 eslint: `prefer-destructuring`
> 结构化赋值能够减少新生成中间变量，
```javascript
// bad
function getFullName(user){
    const firstName = user.firstName;
    const lastName = user.lastName;
    return `${firstName}${lastName}`;
}

// good
function getFullName(user){
    const {firstName, lastName} = user;
    return `${firstName} ${lastName}`;
}

// best
function getFullName({firstName, lastName}){
    return `${firstName} ${lastName}`;
}
```

* 5.2 使用数组结构化赋值， eslint: `prefer-destructuring`
```javascript
const arr = [1, 2, 3, 4];
// bad
const first = arr[0];
const second = arr[1];

// good
const [first, second] = arr;
```

* 5.3 使用对象结构化赋值返回多个值，而不是使用数组结构化赋值
> 这样可以保证当需要多返回属性时，避免出错
```javascript
// bad
function processInput(input){
    // then a miracle occurs
    return [left, right, top, bottom];
}
// the caller must remember the order about the return values
const [left, __, top] = processInput(input);

// good
function processInput(input){
    return {left, right, top, bottom};
}
const {left, top} = processInput(input);
```

## Strings 数组
* 6.1 字符串使用单引号， eslint: `quotes`
```javascript
// bad
const name = "Cap. Janv";

// bad
const name = `Cap. Janv`;

// good
const name = 'Cap. Janv';
```

* 6.2 超过100个字符的字符串不应该使用字符串连接拼成
> 难以阅读，而且可能导致搜索不到
```javascript
// bad
const errMsg = 'fkjdasfjsfkdjsfasdfjkfjksdjfkdjfkajsfkjskfjsdkfj \
fjdksjfkjsdkfjksadfjksdjafksafsad \
fjkdsjfkadjsfkjdskfjasdfjdskfj';

// bad
const errMsg = 'fkasdfjsdfsddsf ' + 
    'fksdfjkdsjkdsfds' + 
    'fjkdsfjdksfjkdsjfk';

// good
const errMsg = 'fjksdajfkdjsfkdjskfjdksfjdasjkfjskdfjkdsafjdksalfjaksdflsdfsdlfjsa';
```

* 6.3 需要拼字符串时候，使用模板字符串代替连接。eslint: `prefer-template`, `template-curly-spacing`
> 模板字符串更具有可读性
```javascript
// bad
function sayHi(name){
    return 'How are you, ' + name + '?';
}

// bad
function sayHi(name){
    return ['How are you, ', name, '?'].join();
}

// good
function sayHi(name){
    return `How are you, ${name}?`;
}
```

* 6.4 绝不使用 `eval()` 执行字符串指令， eslint: `no-eval`

* 6.5 不使用多余的 escape characters, eslint: `no-useless-escape`
> 提高可读性
```javascript
// bad
const foo = '\'this\' \i\s \"quoted\"';

// good
const foo = '\'this\' is "quoted"';
const foo = `my name is '${name}``;
```

## Functions 方法

* 7.1 使用具名方法，而不是方法声明，eslint: `func-style`
> 好处很多，听从就好
```javascript
// bad
function foo(){
    //
};
// bad
const foo = function(){
    // ...
};

// good
// lexical name distinguished from the variable-referenced invocations
const short = function longUniqueMoreDescriptiveLexicalFoo(){
    //
};
```
  
* 7.2 将立即执行的函数表达式包含在括号内，eslint: `wrap-iife`
> 自成一模块。
```javascript
// immediately-invoked function expression
(function(){
    console.log('ok');
}());
```

* 7.3 禁止在非函数块中声明一个函数，替换为使用一个变量引用一个函数。eslint: `no-loop-func`

* 7.4 ECMA-262 定义block 为一系列的语句。一个函数声明不是一个语句。
```javascript
// bad
if(currentUser){
    function test(){
        console.log('Nope.');
    }
}

// good
let test
if(currentUser){
    test = ()=>{
        console.log('Nope.');
    };
}
```

* 7.5 参数禁止命名为 `arguments`
```javascript
// bad
function foo(name, options, arguments){

}

// good
function foo(name, options, args){

}
```

* 7.6 禁止使用 `arguments`, 使用 `...` 替代，eslint: `prefer-rest-params`
> `...` 明确定义想获取的参数，而且是一个真实的Array， `arguments` 只是一个类Array
```javascript
// bad
function concatenateAll(){
    const args = Array.prototype.slice.call(arguments);
    return args.join('');
}

// good
function concatenateAll(...args){
    return args.join('');
}
```

* 7.7 使用默认参数语法
```javascript
// really bad
function handleThings(opts){
    // 
    opts = opts || {}
}
// bad
function handleThings(opts){
    if(opts === void 0){
        opts = {};
    }
}

// good
function handleThings(opts = {}){

}
```

* 7.8 默认参数不要含有数据操作
```javascript
var b = 1;
// bad
function count(a = b++){
    console.log(a);
}
count();    // 1
count();    // 2
count(3);   // 3
count();    // 3
```

* 7.9 默认参数放置在最后, Eslint: `default-param-last`
```javascript
// bad
function handleThins(opts = {}, name){

}

// good
function handleThings(name, opts = {}){

}
```

* 7.10 禁止使用 `Function constructor` 去新建函数。Eslint: `no-new-func`
```javascript
// bad
var add = new Function('a', 'b', 'return a + b');

// still bad
var subtract = Function('a', 'b', 'return a- b');
```

* 7.11 函数的命名空格使用
```javascript
// bad
const f = function(){};
const g = function (){};
const h = function() {};

// good
const x = function () {};
const y = function a() {};
```

* 7.12 不要修改传入参数. Eslint: `no-param-reassign`
> 操作传入参数容易导致不可预知的结果
```javascript
// bad
function f1(obj){
    obj.key = 1;
}

// good
function f2(obj){
    const key = Object.prototype.hasOwnProperty.call(obj, 'key') ? obj.key : 1;
}
```

* 7.13 不要重新赋值传入参数 Eslint: `no-param-reassign`
> 容易导致代码参数误用，以及在V8引擎下影响优化
```javascript
// bad
function f1(a) {
    a = 1;
}

// good
function f3(a) {
    const b = a || 1
}
function f4(a = 1) {

}
```

* 7.14 优先考虑使用 `...` 传入可变参数， eslint: `prefer-spread`
> 这样更简洁，不需要提供一个context
```javascript
// bad
const x = [1, 2, 3, 4, 5];
console.log.apply(console, x);

// good
const x = [1, 2, 3, 4, 5];
console.log(...x);

// bad
new (Function.prototype.bind.apply(Date, [null, 2016, 8, 5]));

// good
new Date(...[2016, 8, 5]);
```

* 7.15 如果有多个参数需要换行，每个参数独立一行 Eslint: `function-paren-newline`
```javascript
// bad
function foo(bar,
             baz,
             quux){

}

// good
function foo(
    bar,
    baz,
    quux,
) {

}

// bad
console.log(foo,
    bar,
    baz);

// good
console.log(
    foo,
    bar,
    baz,
);
```

## Arrow Functions 箭头方法
* 8.1 如果需要使用匿名函数，就是用箭头函数。Eslint: `prefer-arrow-callback`, `arrow-spacing`
> 使用箭头函数，this是当前作用域的
```javascript
// bad
[1, 2, 3].map(function (x) {
    const y = x + 1;
    return x * y;
});

// good
[1, 2, 3].map((x) => {
    const y = x + 1;
    return x * y;
});
```

* 8.2 匿名函数中 `return` 是否使用细节， Eslint: `arrow-parens`, `arrow-body-style`
```javascript
// bad
[1, 2, 3].map((number) => {
    const nextNumber = number + 1;
    `A string containing the ${nextNumber}`;
});

// good
[1, 2, 3].map((number) => `A string containing the ${nextNumber + 1}.`);

// good
[1, 2, 3].map((number) => {
    const nextNumber = number + 1;
    return `A string containing the ${nextNumber}`;
});

// 
function foo(callback) {
    const val = callback();
    if(val === true) {
        //
    }
}

let bool = false;

// bad
foo(() => bool = true);

// good
foo(() => {
    bool = true;
});
```

* 8.3 表达式如果太长，注意换行
```javascript
// bad
['get', 'post', 'put'].map((httpMethod) => Object.prototype.hasOwnProperty.call(
    httpMaticObjectWithAVeryLongName,
    httpMethod,
));

// good
['get', 'post', 'put'].map((httpMethod) => (
    Object.prototype.hasOwnProperty.call(
        httpMagicObjectAVeryLongName,
        httpMethod,
    )
));
```

* 8.4 参数一定要用括号包含。 Eslint: `arrow-parens`
```javascript
// bad
[1, 2, 3].map(x => x * x);

// good
[1, 2, 3].map((x) => x * x);

// bad
[1, 2, 3].map(number => (
    `A string with ${number}`;
));

// good
[1, 2, 3].map((number) => (
    `A string with ${number}`;
));

// bad 
[1, 2, 3].map(x => {
    const y = x + 1;
    return x * y;
});
// good
[1, 2, 3].map((x) => {
    const y = x + 1;
    return x * y;
});
```

* 8.5 避免 `=>` 和 `<=` `>=` 混淆 Eslint: `no-confusing-arrow`
```javascript
// bad
const itemHeight = (item) => item.height <= 256 ? item.largeSize : item.smallSize;

// good
const itemHeight = (item) => (item.height <= 256 ? item.largeSize : item.smallSize)

// good
const itemHeight = (item) => {
    const {height, largeSize, smallSize } = item;
    return height <= 256 ? largeSize : smallSize;
}
```

* 8.6 箭头符要有隐含的返回。 Eslint: `implicit-arrow-linebreak`
```javascript
// bad
(foo) => 
    bar;
(foo) => 
    (bar);

// good
(foo) => bar;
(foo) => (bar);
(foo) => (
    bar
)
```

## Classes & constructors 类和构造器

* 9.1 总是使用 `class` 而避免使用 `prototype` 直接操作。

```javascript
// bad
function Queue(contents = []) {
    this.queue = [...contents];
}
Queue.prototype.pop = function () {
    const value = this.queue[0];
    this.queue.splice(0, 1);
    return value;
};

// good
class Queue {
    constructor(contents = []) {
        this.queue = [...contents];
    }

    pop() {
        const value = this.queue[0];
        this.queue.splice(0, 1);
        return value;
    }
}
```

* 9.2 使用 `extends` 代表继承
> 这个内置方法是不会打断 `instanceof` 的识别
```javascript
const inherits = require('inherits');
function PeekableQueue(contents) {
    Queue.apply(this, contents);
}
inherits(PeekableQueue, Queue);
PeekableQueue.prototype.peek = function () {
    return this.queue[0];
};

// good
class PeekableQueue extends Qeueu {
    peek() {
        return this.queue[0];
    }
}
```

* 9.3 方法可以返回 `this` 来使用方法链

```javascript
// bad 
Jedi.prototype.jump = function () {
    this.jumping = true;
    return true;
};

Jedi.prototype.setHeight = function(height) {
    this.height = height;
};

const luke = new Jedi();
luke.jump();    // return true;
luke.setHeight(20);     // => undefined

// good
class Jedi {
    jump() {
        this.jumping = true;
        return this;
    }

    setHeight(height) {
        this.height = height;
        return this;
    }
}

const luke = new Jedi();
luke.jump()
    .setHeight(20);
```

* 9.4 可以重写 `toString()` 方法，但是注意不要有副作用
```javascript
class Jedi {
    constructor(options = []) {
        this.name = options.name || 'no name';
    }

    getName() {
        return this.name;
    }

    toString() {
        return `Jedi -${this.getName()}`;
    }
}
```

* 9.5 类会有自带的默认构造器，所有不需要写一个空构造器。eslint: `no-useless-constructor`

```javascript
class Jedi {
    constructor() {}

    getName() {
        return this.name;
    }
}

// bad
class Rey extends Jedi {
    constructor(..args)  {
        super( ...args);
    }
}

// good
class Rey extends Jedi {
    constructor(...args) {
        super(...args);
        this.name = 'Rey';
    }
}
```

* 9.6 避免重复的方法 Eslint: `no-dupe-class-members`
> 重复的方法会默认使用最后一个。
```javascript
// bad 
class Foo {
    bar() { return 1;}
    bar() { return 2;}
}

// good
class Foo {
    bar() { return 1;}
}
```

* 9.7 类成员方法应该使用 `this` 调用内部属性 Eslint: `class-methods-use-this`

```javascript
// bad
class Foo {
    bar() {
        console.log('bar');
    }
}
// good
class Foo {
    bar() {
        console.log(this.bar);
    }
}

class Foo {
    static bar() {
        console.log('bar');
    }
}
```

## Modules 模块

* 10.1 总是使用模块化系统，`import` / `export`

```javascript
// bad
const AirbnbStyleGuide = require('./AirbnbStyleGuide');
module.exports = AirbnbStyleGuide.es6;

// ok
import AirbnbStyleGuide from './AirbnbStyleGuide';
export default AirbnbStyleGuide.es6;

// best
import {es6} from './AirbnbStyleGuide';
export default es6;
```

* 10.2 不要使用 * 引用模块
> 这样可以保证有一个单独的default export
```javascript
// bad
import * as AirbnbStyleGuide from './AirbnbStyleGuide';

// good
import AirbnbStyleGuide from './AirbnbStyleGuide';
```

* 10.3 不要直接从 import 内容执行 export
```javascript
// bad 
// filename es6.js
export {es6 as default} from './AirbnbStyleGuide';

// good
import {es6}  from './AirbnbStyleGuide';
export default es6;
```

* 10.4 从一个文件引用的内容，只在一行出现. Eslint: `no-duplicate-imports`
> 便于代码维护
```javascript
// bad
import foo from 'foo';
// ...
import {named1, named2} from 'foo';

// good
import foo, {
    named1,
    named2,
} from 'foo';
```

* 10.5 禁止export 可变内容。 eslint: `import/no-mutable-exports`
> 惯例
```javascript
// bad
let foo = 3;
export {foo};

// good
const foo = 3;
export {foo};
```

* 10.6 如果模块只有一个单一的导出，尽量使用 `default export`, Eslint: `import/prefer-default-export`
> 利于代码可读性和维护
```javascript
// bad
export function foo() {}

// good
export default function foo() {}
```

* 10.7 将import放入最前面, Eslint: `import/first`
```javascript
// bad
import foo from 'foo';
foo.init();

import bar from 'bar';

// good
import foo from 'foo';
import bar from 'bar';

foo.init();
```

* 10.8 多个对象引入，应该写成类似对象换行的写法。 Eslint: `object-curly-newline`
```javascript
// bad 
import {longNameA, longNameB, longNameC, longNameD} from 'path';

// good
import {
    longNameA,
    longNameB,
    longNameC,
    longNameD,
    longNameE,
} from 'path';
```

* 10.9 模块引用中禁止使用webpack loader语法。
> 有需要，建议在 `webpack.config.js` 中处理
```javascript
// bad
import fooSass from 'css!sass!foo.scss';
import barCss from 'style!css!bar.css';

// good
import fooSass from 'foo.scss';
import barCss from 'bar.css';
```

* 10.10 在import中js文件不要写入后缀名
```javascript
// bad
import foo from './foo.js';
import bar from './bar.jsx';
import baz from './baz/index.jsx';

// good
import foo from './foo';
import bar from './bar';
import baz from './baz';
```

## Iterators and Generators
* 11.1 不使用iterators。 优先使用js的更高级的方法代替循环如 `for-in` `for-of`. Eslint: `no-iterator`, `no-restricted-syntax`
> 使用 `map()` / `every()` / `filter()` / `find()` / `findIndex()` / `reduce()` / `some()` / ... 在数组上迭代。使用 `Object.keys()` / `Object.values()` / `Object.entries()` 在对象上迭代。
```javascript
const numbers = [1, 2, 3, 4, 5];

// bad
let sum = 0;
for (let num of numbers){
    sum += num;
}
sum === 15;

// good
let sum = 0;
numbers.forEach((num) => {
    sum += num;
});
sum === 15;

// bad
const increasedByOne = [];
for (let i = 0;i < numbers.length; i++) {
    increasedByOne.push(numbers[i] + 1);
}

// good
const increasedByOne = [];
numbers.forEach((num) => {
    increasedByOne.push(num + 1);
});

// best
const increasedByOne = numbers.map((num) => num + 1);
```

* 11.2 暂时不使用generators，因为转换为ES5还有些问题。
* 11.3 如果必须使用generators， 确认函数的`*`符号位置正确, Eslint: `generator-star-spacing`
```javascript
// bad
function * foo() {

}

// bad
function bar = function * () {

}

// bad
const baz = function *() {

}

// bad
const quux = function*() {

}
// bad 
function*foo() {

}

// bad
function *foo() {

}

// very bad
function
*
foo() {

}
// very bad
const wat = function
*
() {

}
// good
function* foo() {

}
const foo = function* () {

}
```

## Properties 属性

* 12.1 使用 `.` 符号获取属性值，eslint: `dot-notation`

```javascript
const luke = {
    jedi: true,
    age: 28,
};

// bad
const isJedi = luke['jedi'];
// good
const isJedi = luke.jedi;
```

* 12.2 使用 `[]` 符号获取包含变量的属性
```javascript
const luke = {
    jedi: true,
    age: 28,
};

function getProp(prop) {
    return luke[prop];
}

const isJedi = getProp('jedi');
```

* 12.3 使用 `**` 获取指数倍, Eslint: `no-restricted-properties`
```javascript
// bad
const binary = Math.pow(2, 10);

// good
const binary = 2 ** 10;
```

## Variables 变量

* 13.1 总是使用 `const` 或者 `let` 修饰变量。 Eslint: `no-undef`, `prefer-const`
```javascript
// bad
superPower = new SUperPower();

// good
const superPower = new SuperPower();
```

* 13.2 使用 `const` 或者 `let` 每个变量修饰一次 Eslint: `one-var`
```javascript
// bad
const items = getItems(),
    goSportsTeam = true,
    dragonball = 'z';

// bad
const items = getItems(),
    goSportsTeam = true;
    dragonball = 'z';

// good
const items = getItems();
const goSportsTeam = true;
const dragonball = 'z';
```
* 13.3 将 `const` 和 `let` 都分类放置， `const` 的放置在前
```javascript
// bad
let i, len, dragonball,
items = getItems(),
goSportsTeam = true;

// bad
let i;
const items = getItems();
let dragonball;
const goSportsTeam = true;
let len;

// good
const goSportsTeam = true;
const items = getItems();
let dragonball;
let i;
let length;
```

* 13.4 变量在需要的时候才给赋值
```javascript
// bad
function checkName(hasName) {
    const name = getName();

    if (hasName === 'test') {
        return false;
    }

    if (name === 'test') {
        this.setName('');
        return false;
    }

    return name;
}

// good
function checkName(hasName) {
    if (hasName === 'test') {
        return false;
    }

    const name = getName();
    if (name === 'test') {
        this.setName('');
        return false;
    }

    return name;
}
```

* 13.5 不要使用多个等于号赋值。 Eslint: `no-multi-assign`

```javascript
// bad
(function example() {
    // => let a = (b = (c = 1));
    // b 和 c 都成为了全局变量
    let a = b = c = 1;
}());

console.log(a); // referenceError
console.log(b); // 1
console.log(c); // 1

// good
(function example() {
    let a = 1;
    let b = 1;
    let c = 1;
}());

console.log(a); // referenceError
console.log(b); // referenceError
console.log(c); // referenceError
// const同样有效
```

* 13.6 禁止使用 `++` 和 `--`. Eslint: `no-plusplus`

```javascript
// bad
const array = [1, 2, 3];
let num = 1;
num++;
--num;

let sum = 0;
let truthyCount = 0;
for (let i = 0;i < array.length; i++) {
    let value = array[i];
    sum += value;
    if (value) {
        truthyCount++;
    }
}

// good
const array = [1, 2, 3];
let num = 1;
num += 1;
num -= 1;

const sum = array.forEach((x) => sum += x);
const truthyCount = array.filter(Boolean).length;
```

* 13.7 不要在 `=` 附近断行， eslint: `operator-linebreak`

```javascript
// bad
const foo = 
    superLongLongLongLongLongLongFunctionName();

// bad 
const foo
    = 'superLongLongLongLongLongLongString';

// good
const foo = (
    superLongLongLongLongLongLongLongLongLongFunctionName()
);

// good
const foo = 'superLongLongLongLongLongLongLongLongLongString';
```

* 13.8 不允许存在未被使用的变量。 Eslint: `no-unused-vars`
```javascript
// bad
var some_unused_var = 42;

var y = 10;
y = 5;

// 
var z = 0;
z = z + 1;

// 未使用的传参
function getX(x, y) {
    return x;
}

```

## Hoisting 变量提升

* 14.1 `var` 定义的变量，会被提升到函数域的顶部, 但是赋值部分不会被提升, `const` 和 `let` 定义的变量不会，概念：暂时性死区（Temporal Dead Zone)
```javascript
function example() {
    console.log(notDefined);  // => ReferenceError
}

function example() {
    console.log(declaredButNotAssigned);    // => undefined
    var declaredButNotAssigned = true;
}

// 上面的等同于
function example() {
    let declaredButNotAssigned;
    console.log(declaredButNotAssigned);
    declaredButNotAssigned = true;
}

// 使用const的区别
function example() {
    console.log(declaredButNotAssigned);    // ReferenceError
    console.log(typeof declaredButNotAssigned); // ReferenceError
    const declaredButNotAssigned = true;
}
```

* 14.2 匿名函数会提升变量名，但是不会包含其实现.
```javascript
function example() {
    console.log(anonymous);   // undefined

    anonymous();        // TypeError anonymous is not a function

    var anonymous = function () {
        console.log('anonymous function expression');
    };
}
```

* 14.3 有名函数会提升变量名，而不是函数名或者函数实体
```javascript
function example() {
    console.log(named);     // undefined

    named();        // TypeError

    superPower();   // ReferenceError

    var named = function superPower() {
        console.log('Fly');
    };
}

function example() {
    console.log(named);  // undefined

    named();         // TypeError

    var named = function named() {
        console.log('named');
    };
}
```

* 14.4 函数定义可以提升函数名和实现
```javascript
function example() {
    superPower();   // Fly

    function superPower() {
        console.log('Fly');
    }
}
```

## Comparison Operation & Equality 比较运算符和相等

* 15.1 使用 `===` 或者 `!==` 而不是 `==` 和 `!=`. Eslint: `eqeqeq`

* 15.2 条件语句遵循这些规则：
  * **Object** 判定为 **true**
  * **Undefined** 判定为 **false**
  * **Null** 判定为 **false**
  * **Boolean** 判定为 **the value of boolean** 
  * **Numbers** 判定为 如果为 **+0**，**-1**，**NAN** 判定为 **false**, 其他为 **true**
  * **String** 判定为 **false** 如果是 `''`

```javascript
if ([0] && []) {
    // true 都是对象object
}
```

* 15.3 boolean对象直接使用，其他的，使用明确的false/true 判定
```javascript
// bad
if (isValid === true) {

}

// good
if (isValid) {

}

// good
if (name !== '') {

}

// bad
if (collection.length) {

}

// good
if (collection.length > 0) {

}
```
* 15.4 更多信息，查阅 [Truth Equality and javascript](https://javascriptweblog.wordpress.com/2011/02/07/truth-equality-and-javascript/#more-2108)

* 15.5 使用大括号生成代码块处理 `case` 和 `default` 语句的内容，如( `let`, `const`, `function`, `class`) Eslint: `no-case-declarations`

```javascript
// bad
switch (foo) {
    case 1:
        let x = 1;
        break;
    case 2:
        const y = 2;
        break;
    case 3:
        function f() {

        }
        break;
    default: 
        class C {}
}

// bood
switch (foo) {
    case 1: {
        let x = 1;
        break;
    }
    case 2: {
        const y = 2;
        break;
    }
    case 3: {
        function f() {
            //
        }
        break;
    }
    case 4: 
        bar();
        break;
    default: {
        class C {}
    }
}
```

* 15.6 二元选择符不能嵌套，需要拆开 Eslint: `no-nested-ternary`
```javascript
// bad
const foo = maybe1 > maybe2 
    ? "bar" 
    : value1 > value2 ? "baz" : null;

// 先做拆分
const maybeNull = value1 > value2 ? 'baz' : null;
// better
const foo = maybe1 > maybe2 
    ? 'bar' 
    : maybeNull;

// best
const foo = maybe1 > maybe2 ? 'baz' : maybeNull;
```
* 15.7 避免不需要的额外二元选择， Eslint: `no-unneeded-ternary`
```javascript
// bad
const foo = a ? a : b;
const bar = c ? true : false;
const baz = c ? false : true;

// good
const foo = a || b;
const bar = !!c;
const baz = !c;
```

* 15.8 除了 `+`, `-`, `**` 符号外，其他符号混用，都建议使用括号包括，以避免误解。Eslint: `no-mixed-operators`
```javascript
// bad
const foo = a && b < 0 || c > 0 || d + 1 === 0;

// bad
const bar = a ** b - 5 % d;

// bad
if (a || b && c) {
    return d;
}

// bad
const bar = a + b / c * d;

// good
const foo = (a && b < 0 ) || c > 0 || (d + 1 === 0);

// good
const bar = a ** b - (5 % d);

// good
if (a || (b && c)) {
    return d;
}

// good
const bar = a + (b / c) * d;
```

## Blocks 
* 16.1 所有多行代码块都使用括号包含. Eslint: `nonblock-statement-body-position`
```javascript
// bad
if (test)
    return false;

// good
if (test) return false;

// good
if (test) {
    return false;
}

// bad
function foo() { return false; }

// good
function bar() {
    return false;
}
```

* 16.2 如果在 `if` 后面有 `else`，将 `else` 放置在 `if` 的大括号同行后面。 Eslint: `brace-style`
```javascript
// bad
if (test) {
    thing1();
    thing2();
}
else {
    thing3();
}

// good
if (test) {
    thing1();
    thing2();
} else {
    thing3();
}
```
* 16.3 如果 `if` 语句只是会执行一个 `return` 语句，随后的 `else` 语句是不需要的。 Eslint: `no-else-return`

```javascript
// bad
function foo() {
    fi (x) {
        return x;
    } else {
        return y;
    }
}

// bad 
function cats() {
    if (x) {
        return x;
    } else if (y) {
        return y;
    }
}

// bad
function dogs() {
    if (x) {
        return x;
    } else {
        if (y) {
            return y;
        }
    }
}

// good
function foo() {
    if (x) {
        return x;
    }

    return y;
}

// good
function cats() {
    if (x) {
        return x;
    }

    if (y) {
        return y;
    }
}

// good
function dogs(x) {
    if (x) {
        if (z) {
            return y;
        }
    } else {
        return z;
    }
}
```

## Control Statements

* 17.1 控制语句如果太长，可以考虑里面每个条件一个新行，逻辑运算符放置最前。
```javascript
// bad
if ((foo === 123 || bar === 'abc') && doesItLookGoodWhenItBecomesThatLong() && isRight()) {
    thing1();
}

// bad
if (foo === 123 &&
    bar == 'abc') {
        thing1();
}

// bad
if (foo === 123 
    && bar === 'abc') {
        thing1();
}

// bad
if (
    foo === 123 &&
    bar === 'abc'
) {
    thing1();
}

// good
if (
    foo == 123
    && bar === 'abc'
) {
    thing1();
}

// good
if (
  (foo === 123 || bar === 'abc')
  && doesItLookGoodWhenItBecomesThatLong()
  && isThisReallyHappening()
) {
    thing1();
}

// good
if (foo === 123 && bar === 'abc') {
    thing1();
}
```

* 17.2 不要使用 逻辑符的短路原理
```javascript
// bad
!isRunning && startRunning();

// good
if (!isRunning) {
    startRunning();
}
```

## Comments 注释

* 18.1 多行注释使用 `/** ... */`
```javascript
// bad
// make() reutrns a new element
// based on the passed in tag name
//
// @param {String} tag
// @return {Element} element
function make(tag) {
    // ...
    return element;
}

// good
/**
 * make() returns a new element 
 * based on the passed-in tag name
 */
function make(tag) {
    // ...
    return element;
}
```

* 18.2 单行注释使用 `//`, 除非是在代码块首行，或者新增一个空行然后再单行注释
```javascript
// bad
const active = true;    // is current tab

// good
// is current tab
const active = true;

// bad
function getType() {
    console.log('fetching type...');
    // set the default type to no type
    const type = this.type || 'no type';

    return type;
}

// good
function getType() {
    console.log('fetching type...');

    // set the default type to no type
    const type = this.type || 'no type';

    return type;
}
// also good
function getType() {
    // set the default type to no type
    const type = this.type || 'no type';

    return type;
}
```

* 18.3 所有注释，都需要隔开一个空格。 Eslint: `spaced-comment`

```javascript
// bad
//is current tab
const active = true;

// good
// is currrent tab
const active = true;

// bad
/** 
 *make() returns a new element
 *based on the passed-in tag name
 */
function make(tag) {
    // ...

    return element;
}

// good
/** 
 * make() returns a new element
 * based on the passed-in tag name
 */
function make(tag) {
    // ...

    return element;
}
```

* 18.4 在需要的场景，添加 `FIXME` or `TODO`， `FIXME: -- need to figure` `TODO: -- need to implement`
```javascript
class Calculator extends Abacus {
    constructor() {
        super();

        // FIXME: should't use a global here
        total = 0;
    }
}

class Callculator extends Abacus {
    constructor() {
        super();

        // TODO: total should be configuiable by an option param
        this.total = 0;
    }
}
```

## Whitespace 空格

* 19.1 将tab设置为2空格。Eslint: `indent`

```javascript
// bad
function foo() {
....let name;
}

// bad
function bar() {
.let name;
}

// good
function bar() {
..let name;
}
```

* 19.2 左大括号前使用一个空格。Eslint: `space-before-blocks`
```javascript
// bad
function test(){
    console.log('test');
}

// good
function test() {
    console.log('test');
}

// bad
dog.set('attr',{
    age: '1 year',
    breed: 'Bernese Mountain Dog',
});

// good
dog.set('attr', {
    age: '1 year',
    breed: 'Bernese Moutain Dog',
});
```

* 19.3 在 `if` `while` 等控制语句的后面，放置一个空格。在参数列表和函数名之间不需要空格。
```javascript
// bad
if(isJedi) {
    fight();
}

// good
if (isJedi) {
    fight();
}

// bad
function fight () {
    console.log('ok');
}

// good
function fight() {
    console.log('ok');
}
```

* 19.4 所有的操作符都用空格分开. Eslint: `space-infix-ops`

```javascript
// bad
const x=y+5;

// good
const x = y + 5;
```

* 19.5 文件最后添加一个新行. Eslint: `eol-last`

```javascript
// good
import { es6 } from './AirbnbStyleGuide';
// ...
export default es6; 

```
* 19.6 函数链调用次数超过两个，则使用换行缩进，首部使用 `.`， Eslint: `newline-per-chained-call`, `no-whitespace-before-property`
```javascript
// bad
$('#items').find('.selected').highlight().end().find('.open').updateCount();

// bad
$('#items').
    find('.selected').
        highlight().
        end().
    find('.open').
        updateCount();

// good
$('#items')
    .find('.selected')
        .highlight()
        .end()
    .find('.open')
        .updateCount();

// good
const leds = stage.selectAll('.led').data(data);
const svg = leds.enter().append('svg:svg');
svg.classed('led', true).attr('width', (radius + margin) * 2);
const g = svg.append('svg:g');
g.attr('transform', `translate(${radius + margin}, ${radius + margin})`).call(tron.led);
```

* 19.7 结构块和之后新的语句之间加一个空行
```javascript
// bad
if (foo) {
    return bar;
}
return baz;

// good
if (foo) {
    return bar;
}

return baz;

// bad
const obj = {
    foo() {
    },
    bar() {
    },
};
return obj;

// good
const obj = {
    foo() {
    },

    bar() {
    },
};

return obj;
```

* 19.8 代码块内不需要添加无用的新行。Eslint: `padded-blocks`

```javascript
// bad
function bar() {

    console.log(foo);

}

// bad
if (baz) {

    console.log(quzx);
} else {
    console.log(foo);

}

// bad
class Foo {

    constructor(bar) {
        this.bar = bar;
    }
}

// good
function bar() {
    console.log(foo);
}

// good
if (baz) {
    console.log(qux);
} else {
    console.log(foo);
}
```

* 19.9 不要使用多个空行隔开代码。 Eslint: `no-multiple-empty-lines`

```javascript
// bad
class Person {
    constructor(fullName, email, birthday) {
        this.fullName = fullName;


        this.email = email;


        this.setAge(birthday);
    }


    setAge(birthday) {
        const today = new Date();


        const age = this.getAge(today, birthday);


        this.age = age;
    }

}

// good
class Person {
    constructor(fullName, email, birthday) {
        this.fullName = fullName;
        this.email = email;
        this.setAge(birthday);
    }

    setAge(birthday) {
        const today = new Date();
        const age = this.getAge(today, birthday);
        this.age = age;
    }

}
```

* 19.10 小括号内侧不需要添加空格， Eslint: `space-in-parens`
```javascript
// bad
function bar( foo ) {
    reutrn foo;
}

// good
function bar(foo) {
    return foo;
}

// bad
if ( foo ) {
    console.log(foo);
}

// good
if (foo) {
    console.log(foo);
}
```

* 19.11 中括号的内侧不需要添加空格。Eslint: `array-bracket-spacing`
```javascript
// bad
const foo = [ 1, 2, 3 ];
console.log(foo[ 0 ]);

// good
const foo = [1, 2, 3];
console.log(foo[0]);
```

* 19.12 大括号的内侧需要添加空格. Eslint: `object-curly-spacing`

```javascript
// bad
const foo = {clark: 'kent'};

// good
const foo = { clark: 'kent' };
```

* 19.13 每行代码最好不要超过100个字符，单长字符串除外, Eslint: `max-len`

```javascript
// bad
const foo = jsonData && jsonData.foo && jsonData.foo.bar && jsonData.foo.bar.baz && jsonData.foo.bar.baz.quux && jsonData.foo.bar.baz.quux.xyzzy;

// good
const foo = jsonData
    && jsonData.foo
    && jsonData.foo.bar
    && jsonData.foo.bar.baz
    && jsonData.foo.bar.baz.quux
    && jsonData.foo.bar.baz.quux.xyzzy;

// bad
$.ajax({ method: 'POST', url: 'https://airbnb.com/', data: { name: 'John' }, }) .done(() => console.log('Congratulations!')); .fail(() => console.log('You have failed this city.'));

// good
$.ajax({
    method: 'POST',
    url: 'https://airbnb.com/',
    data: { name: 'John' },
})
    .done(() => console.log('Congratulations!'));
    .fail(() => console.log('You have failed this city.'));
```

* 19.14 -- 看了下和19.12重复了

* 19.15 在逗号的前面避免空格，后面添加空格 Eslint: `comma-spacing`
```javascript
// bad
var foo = 1,bar = 2;
var arr = [1 , 2];

// good
var foo = 1, bar = 2;
var arr = [1, 2];
```

* 19.16 对象属性中间空格取舍 Eslint: `computed-property-spacing`
```javascript
// bad
obj[foo ]
obj[ 'foo']
var x = {[ b ]: a}
obj[foo[ bar ]]

// good
obj[foo]
obj['foo']
var x = { [b]: a }
obj[foo[bar]]
```

* 19.17 方法和其调用之间避免空格。Eslint: `func-call-spacing`

```javascript
// bad
func ();

func
();

// good
func();
```

* 19.18 对象的 `key-value` 之间留有空格 Eslint: `key-spacing`
```javascript
// bad
var obj = { foo : 42 };
var obj2 = { foo:42 };

// good
var obj = { foo: 42 };
```

* 19.19 行尾注意不要有多余的空格 Eslint: `no-trailing-spaces`

* 19.20 空行只留一行、行尾只多留一行空行，首行不要留空行 Eslint: `no-multiple-empty-lines`
```javascript
// bad multiple empty lines
var x = 1;



var y = 2;
```
## Commas 逗号
* 20.1 禁止行首逗号 Eslint: `comma-style`
```javascript
// bad
const stroy = [
    once
    , upon
    , aTime
];

// good
const story = [
    once,
    upon,
    aTime,
];
```

* 20.2 行尾逗号添加 Eslint: `comma-dangle`
> 原因是在 `git diff` 的时候，减少行变化数
```javascript
// bad - git diff without trailing comma
const hero = {
    firstName: 'Floria',
    lastName: 'Night'
};

// good
const hero = {
    firstName: 'Floria',
    lastName: 'Night',
};

// bad
function createHero(
    firstName,
    lastName,
    inventorOf
) {

}

// good
function createHero(
    firstName,
    lastName,
    inventorOf,
) {

}

// bad
createHero(
    firstName,
    lastName,
    inventorOf
);

// good
createHero(
    firstName,
    lastName,
    inventorOf,
);

// good
// 注意在 ... 行不能加逗号
createHero(
    firstName,
    lastName,
    inventorOf,
    ...heroArgs
);
```

## Semicolons 分号
* 21.1 建议使用分号 eslint: `semi`
> 自己添加分号防止换行逻辑解析器误判
```javascript
// bad - raise exception
const luke = {}
const leia = {}
[luke, leia].forEach((jedi) => jedi.father = 'vader')

// bad - raise exception
const reaction = "No ! That's impossible!"
(async function meanwhileOnTheFalcon() {
    // 
}())

// bad - return 'undefined' instead of the value on the next line 
function foo() {
    return 
        'ok'
}

// good
const luke = {};
const leia = {};
[luke, leia].forEach((jedi) => {
    jedi.father = 'vader';
});

// good
const reaction = "No! That's impossible!";
(async function meanwhileOnTheFalcon() {
    // ...
}());

// good
function foo() {
    return 'ok';
}
```

## Type Casting & Coercion 类型转换
* 22.1 在表达式的开头使用类型转换

* 22.2 字符串 Eslint: `no-new-wrappers`
```javascript
// => this.reviewScore = 9

// bad
const totalScore = new String(this.reviewScore);    // type of totalScore is 'object' not String

// bad
const totalScore = this.reviewScore + '';   // invokes this.reviewScore.valueOf()

// bad
const totalScore = this.reviewScore.toString(); // isn't guaranteed to return a string

// good
const totalScore = String(this.reviewScore);
```

* 22.3 Numbers 数值类型，使用 `Number` 执行类型转换，使用 `parseInt` 转换字符串。 Eslint: `radix` `no-new-wrappers`
> `parseInt` 根据第二个参数 `radix` 确认数值的进制，如果是 `undefined` 或者 `0`，则默认为10进制，如果字符串开头有空行，被忽略，如果开头是0x或者0X，则默认为16进制
```javascript
const inputValue = '4';

// bad
const val = new Number(inputValue);

// bad
const val = +inputValue;

// bad
const val = inputValue >> 0;

// bad
const val = parseInt(inputValue);

// good
const val = Number(inputValue);

// good -- 使用parseInt，明确进制数值
const val = parseInt(inputValue, 10);
```

* 22.4 如果认为 `parseInt` 是性能瓶颈，要使用二进制计算值方法提升性能，使用注释明确说明
```javascript
// good
/*
 * parseInt was the reason my code was slow
 * Bitshifting the String to coerce it to
 * Number made it a lot better
 */
const val = inputValue >> 0;
```

* 22.5 使用二进制位移操作时，需要注意 `Number` 类型是64位数值，但是位移操作只返回一个32位数值。大于32位的数操作结果是未定义的

```javascript
2147483647 >> 0;    // 2147483647
2147483648 >> 0;    // -2147483648
2147483649 >> 0;    // -2147483647
```

* 22.6 Booleans: Eslint: `no-new-wrappers`
```javascript
const age = 0;

// bad
const hasAge = new Boolean(age);

// good
const hasAge = BOolean(age);

// best
const hasAge = !!age;
```

## Naming Conventions 命名规则

* 23.1 禁止使用单字母的命名 Eslint: `id-length`
```javascript
// bad
function q() {
    // ...
}

// good
function query() {
    // ...
}
```

* 23.2 使用 ·camelCase` 驼峰规则 `object` `function` `instances` Eslint: `camelcase`
```javascript
// bad
const OBJEcttssss = {};
const this_is_my_object = {};
function c() {}

// good
const thisIsMyObject = {};
function thisIsMyFunction() {}
```

* 23.3 使用 `PascalCase` 命名 `constructors` or `classes` Eslint: `new-cap`

```javascript
// bad
function user(options) {
    this.name = options.name;
}

const bad = new user({
    name: 'nope',
});

// good
class User {
    constructor(options) {
        this.name = options.name;
    }
}

const good = new User({
    name: 'yup',
});
```

* 23.4 不要使用前置或者后置的下划线. Eslint: `no-underscore-dangle`
> 因为javascript没有private、public属性之分
```javascript
// bad
this.__firstName__ = 'panda';
this.firstName_ = 'panda';
this._firstName = 'panda';

// good
this.firstName = 'panda';

// 弱引用
const firstNames = new WeakMap();
firstNames.set(this, 'panda');
```

* 23.5 不要保存 `this` 的引用。 可以改为使用箭头方法。
```javascript
// bad
function foo() {
    const self = this;
    return function () {
        console.log(self);
    }
}

// bad
function foo() {
    return () => {
        console.log(this);
    };
}
```
* 23.6 文件名字应该和default export对象名相同
```javascript
// file 1 content
class CheckBox {
    // ...
}
export default CheckBox;

// file 2 content
export default function fortyTwo() { return 42; }

// file 3 content
export default function insideDirectory() {}

// in some files
// bad
import CheckBox from './checkBox';  
import FortyTwo from './FortyTwo';
import InsideDirectory from './InsideDirectory';

// good
import CheckBox from './CheckBox';
import fortyTwo from './fortyTwo';
import insideDirectory from './insideDirectory';
```

* 23.7 default export的是一个方法, 使用驼峰规则，文件名应该和方法名相同
```javascript
function makeStyleGuide() {

}

export default makeStyleGuide;
```

* 23.8 default export 一个 `constructor` `class` `singleton` `function library` `object` 时，使用PascalCase规则
```javascript
const AirbnbStyleGuide = {
    es6: {

    },
};

export default AirbnbStyleGuide;
```

* 23.9 缩写统一全大写，或者全小写。
```javascript
// bad
import SmsContainer from './containers/SmsContainer';

// bad
const HttpRequests = [
    // ...
];

// good
import SMSContainer from './containers/SMSContainer';

// good
const HTTPRequest = [
    // ...
];

// also good
const httpRequest = [
    // ...
];

// best
import TextMessageContainer from './containers/TextMessageContainer';

// best
const requests = [
    // ...
];
```

* 23.10 变量名全大写的场景： 1. export内使用 2. 是一个 `const` 3. 确信不会发生变化
```javascript
// bad
const PRIVATE_VARIABLE = 'should not be unnecessarily uppercased within a file';

// bad
export const THING_TO_BE_CHANGED = 'should obviously not be uppercased';

// bad
export let REASSIGNABLE_VARIABLE = 'do not use let with uppercase variables';

// ---

// allowed but does not supply semantic value
export const apiKey = 'SOMEKEY';

// better in most cases
export const API_KEY = 'SOMEKEY';

// --

// bad - unnecessarily uppercases key while adding no semantic value
export const MAPPING = {
    KEY: 'value',
};

// good
export const MAPPING = {
    key: 'value',
};
```

## Accessors

* 24.1 对属性值使用 `Accessor function` 没有必要
* 24.2 不要使用javscript的 `getters/setters` ， 而是使用 `getVal()` 和 `setVal('hello')`
```javascript
// bad
class Dragon {
    get age() {
        // ...
    }

    set age() {
        // ...
    }
}

// good
class Dragon {
    getAge() {
        // ...
    }

    setAge() {
        // ...
    }
}
```

* 24.3 如果一个方法返回值是 `boolean` ，则使用 `isVal()` 或者 `hasVal()`
```javascript
// bad
if (!dragon.age()) {
    return false;
}

// good
if (!dragon.hasAge()) {
    return false;
}
```

* 24.4 添加 `get()` 和 `set()` 方法，要就都添加
```javascript
class Jedi {
    constructor(options = {}) {
        const lightsaber = options.lightsaber || 'blue';
        this.set('lightsaber', lightsaber);
    }

    set(key, val) {
        this[key] = val;
    }

    get(key) {
        return this[key];
    }
}
```

## Events 事件
*25.1 触发事件时，如果需要带出数据，使用对象，而不是基础类型。这样方便后续添加
```javascript
// bad
$(this).trigger('listingUpdated', listing.id);

$(this).on('listingUpdated', (e, listingID) => {
    // ...
});

// good
$(this).trigger('listingUpdated', { listingID: listing.id });

$(this).on('listingUpdated', (e, data) => {
    // ...
});
```

## jQuery
* 26.1 jQuery对象开头用 `$` 前缀
```javascript
// bad
const sidebar = $('.sidebar');

// good
const $sidebar = $('.sidebar');

// good
const $sidebarBtn = $('.sidebar-btn');
```

* 26.2 缓存jQuery的查询结果
```javascript
// bad
function setSidebar() {
    $('.sidebar').hide();

    // ...

    $('.sidebar').css({
        'background-color': 'pink',
    });
}

// good
function setSidebar() {
    const $sidebar = $('.sidebar');
    $sidebar.hide();

    // ...

    $sidebar.css({
        'background-color': 'pink',
    });
}
```

* 26.3 `DOM` 查询使用层叠结构 如 $('.sidebar ul')`

* 26.4 使用 `find` 来处理jQuery查询
```javascript
// bad
$('ul', '.sidebar').hide();

// bad
$('.sidebar').find('ul').hide();

// good
$('.sidebar ul').hide();

// good
$('.sidebar > ul').hide();

// good
$sidebar.find('ul').hide();
```

## ECMAScript 5 适配
* 27.1 Refer to [Kangax's ES5 compatibility table](http://kangax.github.io/compat-table/es5/)

## ECMAScript 6+ (ES 2015+) Styles

* 28.1 一下特性属于ES6+的
  1. [Arrow Function](https://github.com/airbnb/javascript#arrow-functions)
  2. [Classes](https://github.com/airbnb/javascript#classes--constructors)
  3. [Object Shorthand](https://github.com/airbnb/javascript#es6-object-shorthand)
  4. [Object Concise](https://github.com/airbnb/javascript#es6-object-concise)
  5. [Object Computed Properties](https://github.com/airbnb/javascript#es6-computed-properties)
  6. [Template String](https://github.com/airbnb/javascript#es6-template-literals)
  7. [Destructuring](https://github.com/airbnb/javascript#destructuring)
  8. [Default Parameters](https://github.com/airbnb/javascript#es6-default-parameters)
  9. [Rest](https://github.com/airbnb/javascript#es6-rest)
  10. [Array Spreads](https://github.com/airbnb/javascript#es6-array-spreads)
  11. [Let and Const](https://github.com/airbnb/javascript#references)
  12. [Exponentiation Operation](https://github.com/airbnb/javascript#es2016-properties--exponentiation-operator)
  13. [Iterators and Generators](https://github.com/airbnb/javascript#iterators-and-generators)
  14. [Modules](https://github.com/airbnb/javascript#modules)

* 28.2 不要使用 [TC39 proposals](https://github.com/tc39/proposals) ，还没标准化

## Standard Library
* 29.1 使用 `Number.isNaN` 代替 `isNaN`, Eslint: `no-restricted-globals`
> 全局的 `isNaN` 会先把非数值类型转换为数值类型，再做判定，这里最好明确指出
```javascript
// bad
isNaN('1.2');       // false
isNaN('1.2.3');     // true

// good
Number.isNaN('1.2.3');  // false
Number.isNaN(Number('1.2.3'));  // true
```

* 29.2 使用 `Number.isFinite` 代替 `isFinite`, Eslint: `no-restricted-globals`
> 相同原理
```javascript
// bad
isFinite('2e3') ;       // true

// good
Number.isFinite('2e3');     // false
Number.isFinite(parseInt('2e3', 10));   // true
```

## Testing 测试
* 30.1 Yup.
```javascript
function foo() {
    return true;
}
```

* 30.2 一些总结
  * 无论使用哪个测试框架，都需要自己写测试样例
  * 坚持写一些简单函数，尽量减少变化产生
  * 对stubs和mocks仔细使用，有时候会让测试变得更脆弱
  * Airbnb使用 `mocha` 和 `jest` 做测试框架
  * 100%的测试覆盖度是一个好的目标，但是一般很难实现
  * 无论什么时候修复了一个bug，都写一个测试样例，一个没有回归测试的bug修复，都是很脆弱的


## Performance 性能

* [On Layout & Web Performance](https://www.kellegous.com/j/2013/01/26/layout-performance/)
* [String vs Array Concat](https://jsperf.com/string-vs-array-concat/2)
* [Try/Catch Cost in a Loop](https://jsperf.com/try-catch-in-loop-cost/12)
* [Bang Function](https://jsperf.com/bang-function)
* [jQuery Find vs Context, Selector](https://jsperf.com/jquery-find-vs-context-sel/164)
* [innerHTML vs textContent for script text](https://jsperf.com/innerhtml-vs-textcontent-for-script-text)
* [Long String Concatenation](https://jsperf.com/ya-string-concat/38)
* [Are JavaScript functions like `map()`, `reduce()`, and `filter()` optimized for traversing arrays](https://www.quora.com/JavaScript-programming-language-Are-Javascript-functions-like-map-reduce-and-filter-already-optimized-for-traversing-array/answer/Quildreen-Motta)

## Resouces 资料
**Learning ES6+**
* [Latest ECMA spec](https://tc39.github.io/ecma262/)
* [ExploringJS](https://exploringjs.com/)
* [ES6 Compatibility Table](https://kangax.github.io/compat-table/es6/)
* [Comprehensive Overview of ES6 Features](http://es6-features.org/)

**Read This**
* [Standard ECMA-262](https://www.ecma-international.org/ecma-262/6.0/index.html)

**Tools**
* Code Style Linters
  * [ESlint](https://eslint.org/) - [Airbnb Style .eslintrc](https://github.com/airbnb/javascript/blob/master/linters/.eslintrc)
  * [JSHint](https://jshint.com/) - [Airbnb Style.jshintrc](https://github.com/airbnb/javascript/blob/master/linters/.jshintrc)
* Neutrino Preset - [@neutrinojs/airbnb](https://neutrinojs.org/packages/airbnb/)

**Other Style Guides**
* [Google JavaScript Style Guide](https://google.github.io/styleguide/jsguide.html)
* [Google JavaScript Style Guide(Old)](https://google.github.io/styleguide/javascriptguide.xml)
* [JQuery Core Style Guidelines](https://contribute.jquery.org/style-guide/js/)
* [Principles of Writing Consistent, Idiomatic JavaScript](https://github.com/rwaldron/idiomatic.js)
* [StandardJS](https://standardjs.com/)

**Other Styles**
* [Naming this in nested functions](https://gist.github.com/cjohansen/4135065) - Christian Johansen
* [Conditional Callbacks](https://github.com/airbnb/javascript/issues/52) - Ross Allen
* [Popular JavaScript Coding Conventions on GitHub](http://sideeffect.kr/popularconvention/#javascript) - JeongHoon Byun
* [Multiple var statements in JavaScript, not superfluous](https://benalman.com/news/2012/05/multiple-var-statements-javascript/) - Ben Alman

**Further Reading**
* [Understanding JavaScript Closures](https://javascriptweblog.wordpress.com/2010/10/25/understanding-javascript-closures/) - Angus Croll
* [Basic JavaScript for the impatient Programmer](https://www.2ality.com/2013/06/basic-javascript.html) - Dr. Axel Rauschmayer
* [You Might Not Need jQuery](https://youmightnotneedjquery.com/) - Zaqck Bloom & Adam Schwartz
* [ES6 Features](https://github.com/lukehoban/es6features) - Luke Hoban
* [Frontend Guidelines](https://github.com/bendc/frontend-guidelines) - Benjamin De Cock

**Books**
* [JavaScript: The Good Parts](https://www.amazon.com/JavaScript-Good-Parts-Douglas-Crockford/dp/0596517742) - Douglas Crockford
* [JavaScript Patterns](https://www.amazon.com/JavaScript-Patterns-Stoyan-Stefanov/dp/0596806752) - Stoyan Stefanov
* [Pro JavaScript Design Patterns](https://www.amazon.com/JavaScript-Design-Patterns-Recipes-Problem-Solution/dp/159059908X) - Ross Harmes and Dustin Diaz
* [High Performance Web Sites: Essential Knowledge for Front-End Engineers](https://www.amazon.com/High-Performance-Web-Sites-Essential/dp/0596529309) - Steve Souders
* [Maintainable JavaScript](https://www.amazon.com/Maintainable-JavaScript-Nicholas-C-Zakas/dp/1449327680) - Nicholas C.Zakas
* [JavaScript Web Applications](https://www.amazon.com/JavaScript-Web-Applications-Alex-MacCaw/dp/144930351X) - Alex MacCaw
* [Pro JavaScript Techniques](https://www.amazon.com/Pro-JavaScript-Techniques-John-Resig/dp/1590597273) - John Resig
* [Smashing Node.js: JavaScript Everywhere](https://www.amazon.com/Smashing-Node-js-JavaScript-Everywhere-Magazine/dp/1119962595) - Guillermo Rauch
* [Secrets of the JavaScript Ninja](https://www.amazon.com/Secrets-JavaScript-Ninja-John-Resig/dp/193398869X) - John Resig and Bear Bibeault
* [Human JavaScript](http://humanjavascript.com/) - Henrik Joreteg
* [Superhero.js](http://superherojs.com/) - Kim Joar Bekkelund, Mads Mobak, & Olav Bjorkoy
* [JSBooks](https://jsbooks.revolunet.com/) - Julien Bouquillon
* [Third Party JavaScript](https://www.manning.com/books/third-party-javascript) - Ben Vinegar and Anton Kovalyov
* [Effective JavaScript: 68 Specific Ways to Harness the Power of JavaScript](https://amzn.com/0321812182) - David Herman
* [Eloquent JavaScript](https://eloquentjavascript.net/) - Marijn Haverbeke
* [You Don't Know JS: ES6 & Beyond](https://shop.oreilly.com/product/0636920033769.do) - Kyle Simpson

**Blogs**
* [JavaScript Weekly](https://javascriptweekly.com/);
* [JavaScript, javaScript ...](https://javascriptweblog.wordpress.com/)
* [Bocoup Weblog](https://bocoup.com/weblog)
* [Adequately Good](https://www.adequatelygood.com/)
* [NCZOnline](https://www.nczonline.net/)
* [Perfection Kills](http://perfectionkills.com/)
* [Ben Alman](https://benalman.com/)
* [Dmitry Baranovskiy](http://dmitry.baranovskiy.com/)
* [nettuts](https://code.tutsplus.com/?s=javascript)

## License
(The MIT License)