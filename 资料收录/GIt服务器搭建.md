### Git 服务器搭建流程

##### 1. 安装 git

```shell
$ yum install curl-devel expat-devel gettext-devel openssl-devel zlib-devel perl-devel
$ yum install git
```

##### 2. 创建git账号密码

```shell
$ groupadd git
$ useradd git -g git
$ passwd git            # 设置git用户的密码
```

##### 3. 创建证书登录模式

```shell
$ cd /home/git/
$ mkdir .ssh
$ chmod 755 .ssh
$ touch .ssh/authorized_keys
$ chmod 644 .ssh/authorized_keys
```

> 用户需要或者这个git项目，需要首先复制自己本地的公钥，位于id_rsa.pub，将公钥导入 /home/git/.ssh/authorized_keys

##### 4. 创建git仓库

```shell
$ cd /home
$ mkdir gitrepo
$ chown git:git gitrepo/
$ cd gitrepo

$ git init --bare zerg.git
Initialized empty Git repository in /home/gitrepo/zerg.git/
```

> 用户拉取这个仓库，指令为：git clone git@192.168.0.1:/home/gitrepo/zerg.git