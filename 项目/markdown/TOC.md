
<!-- @import "[TOC]" {cmd="toc" depthFrom=1 depthTo=6 orderedList=true ignoreLink=false} -->

<!-- code_chunk_output -->

1. [TOC 设置](#toc-设置)
    1. [目录列表生成](#目录列表生成)
    2. [设置](#设置)
        1. [Section 1.2.1](#section-121)
2. [Chapter 2](#chapter-2)

<!-- /code_chunk_output -->

# TOC 设置

## 目录列表生成
Markdown Preview enhanced可以支持自动生成目录，可以通过 `cmd+shift+p` 然后选择 `Markdown Preview Enhanced: Create TOC` 命令来创建TOC，我设置为 `alt+shift+p`, 在文件被保存的时候，TOC会自动更新。

## 设置

* orderedList 设置是否使用有序列表
* depthFrom, depthTo [1,6] 包含哪些
* ignoreLink 如果设置为true,则TOC不会进行点击跳转
### Section 1.2.1
也可以直接通过 `[TOC]` 标签创建 `TOC`, 但是这种方式创建的TOC只会在预览中显示，不会修改 `markdown` 文件
## Section 1.3 {ignore=true}
可以通过编写front-matter 设置边栏(没调试出来)
```markdown
---
toc: 
    depth_from: 1,
    depth_to: 6,
    ordered: false
```
# Chapter 2

