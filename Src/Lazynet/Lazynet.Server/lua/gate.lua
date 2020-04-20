package.path = "./lua/?.lua;" ..  package.path
local lazynet = require "lazynet"

local config = {
    port = 30001,
    heartbeat = 300,
    activeEvent = "active", 
    inactiveEvent = "inactive", 
    readEvent = "read", 
    exceptionEvent = "exception"
}

lazynet.start(function ( ... )
    local serviceID = lazynet.getID();
    lazynet.error("current serviceID=" .. serviceID);
    local bootstrapServiceID = lazynet.getServiceID("bootstrap")
    lazynet.error("bootstrap service ID=" .. bootstrapServiceID);
    lazynet.send(bootstrapServiceID, "say", serviceID, "你好")
    lazynet.send(bootstrapServiceID, "say", serviceID, "你好")

    lazynet.addTrigger(config.activeEvent, function (ctx, ip)
        lazynet.error("ip=" .. ip  .. " inline ")
        lazynet.addSession(ctx);
        for i=10,1,-1 do
            lazynet.writeAndFlush(ctx, "连接成功")
        end
    end)

    lazynet.addTrigger(config.exceptionEvent, function (ctx, ip)
        lazynet.error("ip=" .. ip  .. " exception ")
    end)

    lazynet.addTrigger(config.inactiveEvent, function (ctx, ip)
        lazynet.error("ip=" .. ip  .. " offline ")
    end)

     lazynet.addTrigger(config.readEvent, function (ctx, ip, msg)
        lazynet.error("ip=" .. ip .. "msg=" .. msg)
    end)

    -- 创建socket
    lazynet.createSocket(config.port, config.heartbeat, lazynet.socketType.tcpSocket);
    lazynet.bind({
        active = config.activeEvent,
        inactive = config.inactiveEvent,
        exception = config.exceptionEvent,
        read = config.readEvent
    });
    lazynet.error("bind port=" .. config.port);

end) 