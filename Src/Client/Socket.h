#ifndef SOCKET_H
#define SOCKET_H

#include <QObject>
#include <QTcpSocket>
#include <QString>
#include <QByteArray>

class Socket : public QObject
{
    Q_OBJECT
private:
    QTcpSocket* _socket;
    QByteArray _recvBuf;
    bool _disconnect;
    QString _ip;
    int _port;

public:
    explicit Socket(QString ip, int port, QObject *parent = 0);
    ~Socket();

    void connectHost();
private:
    void analyzeData(const QByteArray& buf);
    int byteArrayToInt(const QByteArray &bytes);
    int write(QByteArray byteBuf);
signals:

public slots:
    void slotReadyRead();
    void slotDisconnected();
    void slotConnected();
};

#endif // SOCKET_H
