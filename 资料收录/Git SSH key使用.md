使用SSH可以避免每次连接远端需要输入账号或密码，而且增加了一层额外的安全通道，保护传输安全。

## 1. 检查是否本机已存在SSH keys

**windows**

1. 打开git bash

2. 输入`ls -al ~/.ssh` 查看是否已存在SSH key

   ```shell
   # 公钥是下面四个文件中的一个
   id_dsa.pub
   id_ecdsa.pub
   id_ed25519.pub
   id_rsa.pub
   ```

**Linux**

步骤相同

**MAC**

步骤相同

## 2. 生成SSH key并添加进入ssh agent

**windows**

1. 打开git bash

2. 输入`ssh-keygen.exe -t rsa -b 4096 -C "youremail@**.com"` 

3. 跟随提示框确认密钥存储目录、是否输入secure passphrase

4. 确认ssh-agent正在运行中

   ```shell
   # start the ssh-agent in the background
   $ eval $(ssh-agent -s)
   Agent pid 59566
   ```

5. 将ssh private key添加进入ssh-agent中

   ```shell
   $ ssh-add ~/.ssh/id_rsa
   ```

**Linux**

步骤相同

**MAC**

步骤相同

## 3. 将生成的ssh public key添加进入git account下

**windows**

1. 将ssh public key复制到剪切板

   ```shell
   $ clip < ~/.ssh/id_rsa.pub
   # Copies the contents of the id_rsa.pub file to your clipboar
   ```

2. 在`github>Settings>SSH and GPG keys` 目录下点击 **New SSH key** 或者 **Add SSH key**

3. 在Title下填写描述，使用`ctrl + v` 将已复制的文本复制到Key下

**Linux**

步骤一有不同：

   ```shell
$ sudo apt-get install xclip
# Downloads and installs xclip. If you don't have `apt-get`, you might need to use another installer (like `yum`)
$ xclip -sel clip < ~/.ssh/id_rsa.pub
# Copies the contents of the id_rsa.pub file to your clipboard
   ```

**MAC**

步骤一有不同：

```shell
$ pbcopy < ~/.ssh/id_rsa.pub
# Copies the contents of the id_rsa.pub file to your clipboard
```

## 4. 测试SSH connection

1. 打开git bash

2. 输入如下命令

   ```shell
   $ ssh -T git@github.com
   # Attempts to ssh to GitHub
   ```

   可以看到如下结果：

   ```shel
   The authenticity of host 'github.com (IP ADDRESS)' can't be established.
   RSA key fingerprint is 16:27:ac:a5:76:28:2d:36:63:1b:56:4d:eb:df:a6:48.
   Are you sure you want to continue connecting (yes/no)?
   ```

   在输入`yes` 后可以看到

   ```shell
   Hi username! You've successfully authenticated, but GitHub does not
   provide shell access.
   ```


