
<!-- @import "[TOC]" {cmd="toc" depthFrom=1 depthTo=6 orderedList=false} -->

<!-- code_chunk_output -->

- [Vite 使用笔记](#vite-使用笔记)
  - [安装过程](#安装过程)
    - [基本流程](#基本流程)
    - [扩展流程](#扩展流程)
    - [启动流程](#启动流程)
  - [功能介绍](#功能介绍)

<!-- /code_chunk_output -->

# Vite 使用笔记

## 安装过程

### 基本流程

```shell
# vite安装
npm init vite@latest
# 基本启动流程
npm install
npm run dev
```
### 扩展流程
```shell
# 直接选定项目名称和想要的模板，可使用模板vue和vue-ts
npm init vite@latest my-vue-app -- --template vue 
```

### 启动流程
```json
{
    "scripts":{
        "dev": "vite", // 启动开发服务器，别名 vite dev, vite serve
        "build": "vite build", // 为生产环境构建产物
        "preview": "vite preview" // 为本地环境构建产物
    }
}
```
> 除此之外，也可以使用 **--port** 和 **--https**添加额外选项

## 功能介绍

