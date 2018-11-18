1. 在官网http://nginx.org/download/处下载源码tar包
2. 使用tar -xvf nginx-1.8.0.tar.gz解压
3. 进入解压后的文件夹，./configure
4. make & make install
5. 默认安装路径在/usr/local/nginx
6. 进入/usr/local/nginx/sbin,启动nginx(./nginx)
7. 测试，外部curl 80端口，如果返回nginx启动页，说明启动成功