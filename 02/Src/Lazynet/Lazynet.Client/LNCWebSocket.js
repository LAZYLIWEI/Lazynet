// 封装一些原生的websocket事件, 方便于开发
function LNCWebSocket(url){
    this.ws = new WebSocket(url);
    this.recvBeforefn = null; // 接收事件之前运行的函数, 可用于进行数据加密
    this.recvAfterfn = null; // 接收事件之后运行的函数
    this.sendBeforefn = null; // 发送事件之前运行的函数, 可用于进行数据加密
    this.connectState = 1; // web socket处理可进行通信息状态
}

// 注册相应事件
// startfn: 连接成功事件
// recvfn: 有接收数据事件
// closefn: 连接关闭数据事件
LNCWebSocket.prototype.register = function(startfn, recvfn, closefn){
    this.ws.onopen = startfn;
    this.ws.onmessage = function (ev) {
        var msg = ev.data;
        if (this.recvBeforefn){
            msg = this.recvBeforefn(msg);
        }
        if (recvfn){
            msg = recvfn(msg);
        }
        if (this.recvAfterfn){
            this.recvAfterfn(msg);
        }
    };
    this.ws.onclose = closefn;
}

// 设置接收数据事件之前要运行的函数
LNCWebSocket.prototype.setRecvBeforefn = function(fn){
    if (!fn){
        throw new Error("fn is null");
    }

    this.recvBeforefn = fn;
}


// 设置接收数据事件之后要运行的函数
LNCWebSocket.prototype.setRecvAfterfn = function(fn){
    if (!fn){
        throw new Error("fn is null");
    }

    this.setRecvAfterfn = fn;
}

// 设置发送数据事件之后要运行的函数
LNCWebSocket.prototype.setSendBeforefn = function(fn){
    if (!fn){
        throw new Error("fn is null");
    }

    this.sendBeforefn = fn;
}

// 发送消息
LNCWebSocket.prototype.send = function(msg){
    if (this.ws.readyState == this.connectState){
        if (this.sendBeforefn){
            msg = this.sendBeforefn(msg);
        }
        this.ws.send(msg);
    }
}
