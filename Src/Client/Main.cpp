#include <QApplication>
#include "Socket.h"


int main(int argc, char* argv[])
{
    QApplication app(argc, argv);

    Socket* socket = new Socket("127.0.0.1", 30001);
    socket->connectHost();

    return app.exec();
}
