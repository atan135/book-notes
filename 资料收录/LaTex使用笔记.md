### 第一章：基础部分

1. 在Latex中所有的空格，tab都等同一个空格，一行开头的空格被忽略，单换行也等同于一个空格

2. 一个空行等同于一个换行

3. 符号# $ % ^ & _ { } ~ \ 都是有特殊意义的符号，需要使用'\'来转义，其中\需要使用\textbackslash转义，因为\\\\ 是换行符号

4. 命令\TeX之类，会对后面的空格忽略，如果需要保留，则使用\TeX{}

5. 有一些命令可以携带参数，参数的格式为\command[optional param]{param}

6. 使用%符号作注释，忽略该行之后所有的文本， 对于长注释可以引入\usepackage{verbatim}然后\begin{comment} .... \end{comment}处理，这种方式在复杂结构中不会生效，例如在数学公式中

7. 一个最简洁的LaTex文本结构如图

   ```latex
   \documentclass{article}
   \begin{document}
   Small is beautiful
   \end{document}
   ```

8. 当需要引入package，使用\usepackage[options]{package name}，在命令行使用texdoc {packagename}打开对应说明文档

9. 常见的文件类型

   a. Latex自带文件

   | 后缀名      | 含义                                                         |
   | ----------- | :----------------------------------------------------------- |
   | <b>.tex</b> | 输入文件，用于格式化                                         |
   | <b>.sty</b> | 宏包文件，\usepackage就是导入这些文件                        |
   | <b>.dtx</b> | documented Tex                                               |
   | <b>.ins</b> | 如果从网上下载一个latex包，一般是包含一个.dtx和一个.ins文件，然后使用latex运行.ins文件，可以解开.dtx文件，生成包文件 |
   | <b>.cls</b> | 类文件，通过\documentclass选择                               |
   | <b>.fd</b>  | 字体文件                                                     |

   b. Latex生成文件

   | 后缀名      | 含义                                                         |
   | ----------- | ------------------------------------------------------------ |
   | <b>.dvi</b> | 设备无关文件，主要的编译结果，可以用于dvi预览器查看或者打印，如果使用的是padfLatex则看不到 |
   | <b>.log</b> | 最近一次编译产生的详细日志                                   |
   | <b>.toc</b> | 保存所有的section header，用于下次compile以及生成目录        |
   | <b>.lof</b> | 类似.toc，用于保存所有figure list                            |
   | <b>.lot</b> | 类似，用于保存所有table list                                 |
   | <b>.aux</b> | 编译过程的中间文件，用于跨引用                               |
   | <b>.idx</b> | 如果文件包含index，这个文件保存，用于之后由makeindex处理     |
   | <b>.ind</b> | 处理后的.idx文件                                             |
   | <b>.ilg</b> | makeindex的log文件                                           |

10. 拆分文件方法：1. \include{filename}，插入另一个tex文件，这样会另起一页。2.  \includeonly{filename, filename, ...}，用于标记那些可插入的tex文件，后面\include 的命令引用文件在这里吗的才能生效。3. \input{filename}不会生成打断。

11. 如果只想检验文件的正确性，不需要生成pdf，可以使用以下两个指令

    ```latex
    \usepackage{syntonly}
    \syntaxonly
    ```



### 第二章：文本格式

1. 使用 \\\\ 做新行处理，使用一个完整空行做一个新段落，两者区别是1. 根据结构，如果是一个新的内容，使用完整空行，否则用\\\\， 2. 新段落会有首行缩进
2. 尤其是文本和结构如equation嵌套时，正确处理使用新行还是空行处理，如果在equation后使用新行，会直接后续写下来，如果用空行，则会另起一段落。
3. 使用\\\\ 或者\newline另起新行，使用\newpage另起一页， 使用\linebreak[n], \nolinebreak[n], \pagebreak[n], \nopagebreak[n] 提示需要打断，但是不一定会生效，因为latex系统会根据页面布局使用最合适的布局，可以使用\sloppy降低latex的标准。
4. \mbox{text}和\fbox{text}会保证里面文字不会被换行打断，但是后一个会产生一个可见文字框。
5. 一些通用命令 \today \TeX \LaTeX \LaTeXe， 使用两个``（键盘左上角）开启一个引用，使用 两个单引号结束（好像也可以用左上角的结束）
6. 这个符号 - （减号）会被格式化处理，两连和三连会自动合并为中杠，长杠
7. 符号~的使用，如果要正常使用，使用\\ ~{}，（不需要空格），如果仅使用杠 ~不用{}，那么会生成 $\hat{a}$ ，或者直接使用 \$\sim\$
8. slash符号（/）可以直接使用，也可以使用\slash代替，直接使用，在分词上会认为是来个那个单词，会导致一些其他问题
9. Degree Symbol($^{\circ}$)符号的使用，可以使用\$^{\circ}\mathrm{C}\$ 实现，也可以在引入textcomp包后，使用\textdegree和\textcelsius实现。
10. 欧元符号使用\usepackage{textcomp}后\texteuro，$\ldots$ 符号使用\$\ldots\$，有些单词输出会连接起来，如ff，这时候如果需要可以使用f\mbox{}f将其分隔。
11. 文本上标的实现，可以使用\ ^o, \",\ ~, \ `处理为后缀文字上标
12. 在article中常用的分节命令为 \section{...} \subsection{...} \subsubsection{...} \paragraph{...} \subparagraph{...}，如果想生成不打断这些结构的块，使用\part{}，在report或者book中可以使用更上一级的\chapter{}，使用\tableofcontents对section，subsection，subsubsection做目录，\section等后面加*号，会导致不会进入目录，不产生标号，如果标题太长可以使用类似\section\[short title](whole title) 模式
13. 整个文档标题使用\title, \author, \date标记，然后\maketitle
14. 脚注使用\footnote{text}，下划线使用\underline，em体使用\emph
15. 结构块使用\begin{aaa} \begin{bbb} \end{bbb} \end{aaa}嵌套处理
16. Itemize是符号列表，默认使用$\bullet$ ，如果想用其他符号，使用\item[-]这样的，
17. enumerate是序号列表，默认阿拉伯数字
18. description是key-value类型的，使用时\item[key]处理
19. 文字布局使用\flushleft, \flushright, \center
20. 文字引用，短的用\begin{quote}，长的使用\begin{quotation}或者\begin{verse}，摘要使用\begin{abstract}
21. 保持初始格式不作处理使用\begin{verbatim}，在段落中使用\verb*|text|
22. 个性化表格使用\begin{tabular}处理，只会在一页显示，如果需要多页显示，使用longtable
23. 插入图片，需要引入graphicx包，然后使用\includegraphics[angle=20, height=.., width=\textwidth, scale=...]{path_to_image}
24. 对table和figure需要有布局需求的，样例：\begin{table}[!hbp]，h是here的意思，t是top的意思，b是bottom的意思，p是page的意思，!是不使用其他内置的
25. 对figure和table的类似标记：使用\caption[short title]{full title}命名，使用\listoftables做表目录，使用shortttitle命名该table，使用\label和\ref做引用
26. 使用\clearpage和\cleardoublepage将所有在float queue中还未放置的图表强制清空在当前页布局，后一个命令还会保证下一页是在右边页。

### 第三章： 数学公式

1. 书写数学公式，可以直接使用\$something...\$， 也可以使用\begin{equation}，两种的差异是前一个会写入段落内，后一个是另起段落，且居中显示，默认情况下equation会写上标号，可以通过\label标记这个公式，用于后续引用\eqref，如果不想使用标号，还可以通过\begin{equation\*}\end{equation\*} ,或者直接 \\[some equation \\]
2. 对于在行内高度跨度大的公式，默认情况会影响到行间距，如果需要忽略，可以使用\\ smash{}包起来，维持行间距不受影响。
3. 在公式中，所有文本都会带有特殊的格式，要恢复正常的文本格式，使用\text
4. 开根号使用\sqrt[2]{3}​这样的结构生成$\sqrt[2]{3}$ ,使用\cdot, \cdots, \ldots, \vdots生成不同的点号$\cdot\quad\cdots\quad\ldots\quad\vdots\quad$
5. 使用\underline, \overline生成上划线下划线$0.\overline{3}\quad \underline{underline}$
6. 向量使用\vec或者\overrightarrow，分号使用\frac
7. 对$a \bmod b$ 使用 a \\bmod b， 对$a \equiv b \pmod{b}$ 使用 a \equiv b \pmod{c}
8. \frac \tfrac \dfrac的效果分别为 $\frac{1}{5}\qquad \tfrac{1}{5}\qquad \dfrac{1}{5}$
9. 自定义命令使用\DeclareMathOperator{\argh}{argh}，这个需要用在最前面引言处，和\usepackage位置处
10. 微分符号使用\partial -> $\partial$ 
11. 在一个符号上标另一个符号，使用形式 \stackrel{#1}{#2}，类似 $f_n(x) \stackrel{*}{\approx} 1$
12. 求和、积分、求积符合分别用 \sum \int \prod，如 $\sum_{i=1}^{100}\qquad \int_0^{\frac{\pi}{2}}\qquad \prod_{\epsilon}$

13. 下标叠加使用\substack，例如 $\sum^n_{\substack{0<i<n\\ j\subseteq i}} P(i, j) = Q(i,j)$

14. 小中括号使用正常字符，大括号使用\\{ ,其他的分隔符一般需要使用特殊命令，如\\updownarrow . 如果想让括号和里面内容的高度匹配，分别使用\left， \right 处理，例如 $1 + \left[\left(\dfrac{1}{1+\dfrac{1}{2}}\right) \times 1\right]$ ，也可以使用\big, \bigg手动控制这些符号的高度，例如 $\bigg(1+\big(2 + (3 + 4)\big)\bigg)$ 。

15. 如果要手动换行，一般在等号或者其他操作符之前换行，等号的优先级最高，其次是加减号，再是乘除号，尽量避免在其他操作符前做换行操作。或者使用\begin{multline}处理。

16. 竖直对齐更多是使用align配合& 处理，例如

    $\begin{align} a & = b + c\\ & = d + e\end{align}$

    