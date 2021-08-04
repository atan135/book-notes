### 新服务器centos更新gcc版本的简洁脚本

```sh
wget https://copr.fedoraproject.org/coprs/hhorak/devtoolset-4-rebuild-bootstrap/repo/epel-7/hhorak-devtoolset-4-rebuild-bootstrap-epel-7.repo -O /etc/yum.repos.d/devtools-4.repo


yum install devtoolset-4-gcc devtoolset-4-binutils devtoolset-4-gcc-c++

scl enable devtoolset-4 bash


ln -s /opt/rh/devtoolset-2/root/usr/bin/* /usr/local/bin/

hash -r

gcc --version

```

