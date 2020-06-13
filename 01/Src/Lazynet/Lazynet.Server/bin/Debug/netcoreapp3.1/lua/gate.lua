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
    local ID = lazynet.getID();
    lazynet.setAlias("gate");
    lazynet.error("gate run");

    lazynet.addTrigger("getSessionList", function( ... )
        lazynet.error("getSessionList")
	end);

    lazynet.addTrigger(config.activeEvent, function (ctx, ip)
        lazynet.error("ip=" .. ip  .. " inline ")
        lazynet.addSession(ctx)
        lazynet.writeAndFlush(ctx, "连接成功");
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

--      while true do
--        lazynet.error2(ID, "aaa")
--      end

    lazynet.error("bind port=" .. config.port);
end) 