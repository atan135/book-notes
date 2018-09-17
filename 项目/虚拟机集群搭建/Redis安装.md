**参考[官方网站](https://redis.io/)**

Redis是使用的BSD协议的内存数据库，不需要担心授权问题，可以放心使用。

## Redis安装

```shell
$ wget http://download.redis.io/releases/redis-4.0.11.tar.gz
$ tar xzf redis-4.0.10.tar.gz
$ cd redis-4.0.10
$ make
$ make test
$ mv redis-4.0.10 /usr/local/redis
```

## Redis的启动

服务端位于`src/redis-server` ，客户端位于`src/redis-cli` ，配置文件为`redis.conf` ，rdb文件为`dump.rdb`

测试开启：

```shell
./src/redis-server &
./src/redis-cli
> set msg hello
> get msg
```

redis配置文件默认为监听内网端口，为了使外网能够访问，需要修改配置文件的配置

```shell
# bind 192.168.1.100 10.0.0.1
# bind 127.0.0.1 ::1
# bind 127.0.0.1
protected-mode no
```

然后在~/bin/startservice.sh中添加这行

```shell
/usr/local/redis/src/redis-server /usr/local/redis/redis.conf &
```

最后，复制一份redis-cli到能识别的快捷项中

```
cp /usr/local/redis/src/redis-cli /usr/local/bin
```

