#include "Socket.h"
#include <QtEndian>

Socket::Socket(QString ip, int port, QObject *parent) : QObject(parent)
{
    _socket = new QTcpSocket(this);
    connect(_socket, SIGNAL(readyRead()), this, SLOT(slotReadyRead()));
    connect(_socket, SIGNAL(disconnected()), this, SLOT(slotDisconnected()));
    connect(_socket, SIGNAL(connected()), this, SLOT(slotConnected()));

    this->_ip = ip;
    this->_port = port;
}


Socket::~Socket()
{

}


/*
* 连接服务器
*/
void Socket::connectHost()
{
    _socket->connectToHost(this->_ip, this->_port);
}


/*
* 当前数据传输过来时,会调用这个方法
*/
void Socket::slotReadyRead()
{
    /* 接收数据, 判断是否有数据, 如果有, 通过readAll函数获取所有数据 */
    while (_socket->bytesAvailable() > 0)
    {
         _recvBuf.append(_socket->readAll());
         while (_recvBuf.length() > 0)
         {
             int filedLength = sizeof(int);
             int dataLength = qFromBigEndian(byteArrayToInt(_recvBuf));
             int len = filedLength + dataLength;
             if (_recvBuf.length() >= len)
             {
                 analyzeData(_recvBuf.mid(filedLength, dataLength));
                 _recvBuf.remove(0, len);
             }
             else
             {
                 break;
             }
         }
    }
}


/*
* 得到一个完整包之后解析其内容信息
*/
void Socket::analyzeData(const QByteArray& buf)
{
    qDebug() << "recv from server:" << buf;
}


/*
* 远程服务断开连接,直接退出程序
*/
void Socket::slotDisconnected()
{
    qDebug() << "disconnected";
    this->connectHost();
    exit(0);
}


/*
* 连接成功,修改连接状态标志位
*/
void Socket::slotConnected()
{
    _disconnect = false;
    qDebug() << "connected success";
    // 启用心跳机制
}


/*
* 取bytearray前4个字节转成int类型
* 如果bytearray不足4个字节则默认返回0
*/
int Socket::byteArrayToInt(const QByteArray &bytes)
{
    if (bytes.length() < sizeof(int))
    {
        return 0;
    }

    int i = bytes[0] & 0x000000FF;
    i |= ((bytes[1] <<8)&0x0000FF00);
    i |= ((bytes[2] <<16)&0x00FF0000);
    i |= ((bytes[3] <<24)&0xFF000000);
    return i;
}


/*
* 写消息
* byteBuf: 内容数据
*/
int Socket::write(QByteArray byteBuf)
{
    // netty的解码器解析长度是以大端方式
    // 所以需要将小端转换成大端
    int bufLength = qFromBigEndian(byteBuf.length());
    QByteArray wrapByteBuf;
    wrapByteBuf.append((char*)&bufLength, sizeof(bufLength));
    wrapByteBuf.append(byteBuf);
    return _socket->write(wrapByteBuf);
}
