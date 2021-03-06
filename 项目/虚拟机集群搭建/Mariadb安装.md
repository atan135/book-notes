Mariadb是遵循GPL v2协议的开源软件，具体授权规则[参照这里](https://github.com/atan135/book-notes/blob/master/%E8%B5%84%E6%96%99%E6%94%B6%E5%BD%95/%E5%BC%80%E6%BA%90%E5%8D%8F%E8%AE%AE%E4%BB%8B%E7%BB%8D.md)

### 下载安装

#### 使用yum下载安装

使用官网推荐安装流程

1. 点击链接<https://downloads.mariadb.org/mariadb/repositories/#mirror=supportex>

2. 选择对应的版本，本机使用CentOS yum安装

3. 更新yum目的仓库，新建`/etc/yum.repos.d/MariaDB.repo` , 内容如下：

   ```shell
   # MariaDB 10.3 CentOS repository list - created 2019-08-26 15:06 UTC
   # http://downloads.mariadb.org/mariadb/repositories/
   [mariadb]
   name = MariaDB
   baseurl = http://yum.mariadb.org/10.3/centos7-amd64
   gpgkey=https://yum.mariadb.org/RPM-GPG-KEY-MariaDB
   gpgcheck=1
   ```

4. 使用yum下载，`yum install MariaDB-server MariaDB-client`

5. root下执行`mysql_secure_installation` ，根据提示操作更新root密码及其他配置



### 使用源码安装

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

客户机只需要安装mysql 客户端即可 `yum install mysql`

因为mysql安装时的账号只允许本地的root账号，所以需要新增账号来用于客户连接，处理客户请求

1. 打开特定机器的root登陆特权

   ```mysql
    GRANT ALL PRIVILEGES ON *.* TO 'root'@'%'IDENTIFIED BY 'passwd' WITH GRANT OPTION;
    FLUSH PRIVILEGES;
   ```

   如此处理后，root可以在特定机器上登陆了

   `mysql -uroot -ppasswd -h192.168.0.100 -P3306`

2. 新增用户用于普通事务处理，为了方便处理，不限制host。

   ```mysql
   CREATE USER 'username'@'%' IDENTIFIED BY 'passwd'
   CREATE DATABASE dbname DEFAULT CHARSET=utf8mb4;
   GRANT ALL PRIVILEGES ON dbname.* TO 'username'@'%'IDENTIFIED BY 'passwd' WITH GRANT OPTION;
   FLUSH PRIVILEGES;
   ```

   如此处理后，所有网络上的机器，均可以通过该账号登陆Mariadb。

   ` mysql -uusername -ppasswd -h192.168.0.101 -P3306`

