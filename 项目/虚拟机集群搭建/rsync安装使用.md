## rsync安装使用

#### windows

下载cwrsync地址：https://www.itefix.net/cwrsync

使用：

```shell
C:\tools\cwrsync_6.2.0_x64_free\bin\rsync.exe --port=873 -av ./test username@hostname::testmodule --password-file=./rsyncd.txt --exclude-from=./excludelist.txt
```



#### Linux

1. 安装

   ```shell
   $ wget https://download.samba.org/pub/rsync/src/rsync-3.1.2.tar.gz
   $ tar -xvf rsync-3.1.2.tar.gz
   $ cd rsync-3.1.2
   $ ./configure --prefix=/usr/local/rsync
   $ make
   $ make install
   ```

2. 配置conf文件

   ```shell
   vim /usr/local/rsync/rsync.conf
   # 文件内容如下
   ### 全局参数 ###
   port=873
   motd file=/usr/local/rsync/rsync.motd
   log file=/usr/local/rsync/rsync.log
   pid file=/var/run/rsync.pid
   
   ### 模块参数 ###
   [testmodule]
   path=/home/username/project/test
   use chroot=true
   ## 文件uid和gid
   uid=1000
   gid=1000
   read only=false
   exclude=/readme.txt /runtime
   auth users=username
   secrets file = /usr/local/rsync/rsync.secrets
   ```

3. 配置密钥文件和欢迎语

   ```shell
   vim /usr/local/rsync/rsync.secrets
   username:password			//用户和密码，用 : 隔开
   chmod 600 /usr/local/rsync/rsync.secrets
   vim /usr/local/rsync/rsync.motd		// 配置欢迎语
   ```

4. 启动rsync服务

   ```shell
   /usr/local/rsync/bin/rsync --daemon --config=/usr/local/rsync/rsync.conf
   ```

5. 配置rsync服务开机启动

   ```shell
   vim /etc/rc.local
   	/usr/local/rsync/bin/rsync --daemon --config=/usr/local/rsync/rsync.conf
   ```

6. 测试

   ```shell
   1. 推送数据到远程
   /usr/local/rsync/bin/rsync --port=873 -av ./test username@hostname::testmodule --password-file=./pwd.txt
   ```

常见几个bug：

1. 启动失败，查看rsync.log，可能是目标目录权限没有，也可能是已有启动
2. 远程连接不上，可能是防火墙关闭了873端口，也可能是ip地址拼写错误
3. 远程连接上，报错，可能是没有权限