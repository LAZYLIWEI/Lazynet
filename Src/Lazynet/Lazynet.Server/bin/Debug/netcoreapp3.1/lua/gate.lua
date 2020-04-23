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
    lazynet.error(serviceID,  "gate run");

    lazynet.addTrigger(config.activeEvent, function (ctx, ip)
        lazynet.error(serviceID, "ip=" .. ip  .. " inline ")
        lazynet.call(serviceID,  "addSession", ctx)
        for i=10,1,-1 do
            lazynet.call(serviceID,  "writeAndFlush", ctx, "连接成功")
        end
    end)

    lazynet.addTrigger(config.exceptionEvent, function (ctx, ip)
        lazynet.error(serviceID, "ip=" .. ip  .. " exception ")
    end)

    lazynet.addTrigger(config.inactiveEvent, function (ctx, ip)
        lazynet.error(serviceID, "ip=" .. ip  .. " offline ")
    end)

     lazynet.addTrigger(config.readEvent, function (ctx, ip, msg)
        lazynet.error(serviceID, "ip=" .. ip .. "msg=" .. msg)
    end)

    -- 创建socket
    --lazynet.createSocket(serviceID, config.port, config.heartbeat, lazynet.socketType.tcpSocket);
--    lazynet.bind(serviceID, {
--        active = config.activeEvent,
--        inactive = config.inactiveEvent,
--        exception = config.exceptionEvent,
--        read = config.readEvent
--    });
    lazynet.error(serviceID, "bind port=" .. config.port);
    lazynet.kill(serviceID)
end) 