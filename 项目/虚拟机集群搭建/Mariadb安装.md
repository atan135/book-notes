Mariadb是遵循GPL v2协议的开源软件，具体授权规则[参照这里](https://github.com/atan135/book-notes/blob/master/%E8%B5%84%E6%96%99%E6%94%B6%E5%BD%95/%E5%BC%80%E6%BA%90%E5%8D%8F%E8%AE%AE%E4%BB%8B%E7%BB%8D.md)

### 下载安装

使用官方安装流程，源码下载编译安装方式。参见[这里](https://mariadb.com/kb/en/library/source-building-mariadb-on-centos/)

```shell
# 准备工具下载
$ yum-builddep mariadb-server
$ yum install git \
      gcc \
      gcc-c++ \
      bison \
      libxml2-devel \
      libevent-devel \
      rpm-build
$ yum install openssl
# 下载源码，单独保存在一个mariadb文件夹中
$ git clone --branch 10.3 https://github.com/MariaDB/server.git
# 以上方式可能国内太慢，可以从[官网](https://downloads.mariadb.org/mariadb/10.3.9/)下载源码包，解压出来重命名为server，注意使用md5sum校验包文件的完整性。
$ cmake -DRPM=centos7 server/
$ make package
# 如果出现依赖包遗漏导致错误的，更新完依赖包还是会出错，这时候使用下面命令，并重复上两步
$ rm CMakeCache.txt
# 编译成功后
$ make install
```



## Mariadb启动

以上处理完成后，开始运行相关命令启动

```
# 先确保存在mysql用户
$ cat /etc/passwd | grep mysql
# 如果不存在，则创建该用户
$ useradd mysql
$ chown -R mysql /your/path/of/mysql/
$ cd /your/path/of/mysql
# 初始化mysql表
$ ./scripts/mysql_install_db --user=mysql
# 启动mysqld
$ /usr/bin/mysqld_safe --user=mysql &  
$ /usr/bin/mysqladmin -uroot password 'newpassword'
# 完成之后运行test
$ make test
# 没有问题后，kill掉mysql_safe启动的mysql的，将mysql加入自启动选项中，启动mysqld
$ cp ./support-files/mysql.service /usr/lib/systemd/system
$ service mysql start
# 这里要注意，使用root用户可以直接登陆
$ mysql -uroot -ppasswd
# 但是普通用户只能够使用-h登陆，root都可以
$ mysql -h127.0.0.1 -uroot -ppasswd
```

