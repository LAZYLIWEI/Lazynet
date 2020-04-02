package.path = "./lua/?.lua;" ..  package.path
local lazynet = require "lazynet"

local config = {
    port = 30000,
    heartbeat = 300,
    type = 0,
    activeEvent = "active", 
    inactiveEvent = "inactive", 
    readEvent = "read", 
    exceptionEvent = "exception"
}

lazynet.start(function ( ... )
    local serviceID = lazynet.getID();
    lazynet.error(serviceID, "current serviceID=" .. serviceID);
    local bootstrapServiceID = lazynet.getServiceID("bootstrap")
    lazynet.error(bootstrapServiceID, "bootstrap service ID=" .. bootstrapServiceID);
    lazynet.sendMessage(bootstrapServiceID, "say", bootstrapServiceID, "ÄãºÃ")

    lazynet.addTrigger(config.activeEvent, function ( ctx, ip )
        lazynet.error(serviceID,  "ip=" .. ip  .. " inline ")
    end)

    lazynet.addTrigger(config.exceptionEvent, function ( ctx, ip )
        lazynet.error(serviceID, "ip=" .. ip  .. " exception ")
    end)

    lazynet.addTrigger(config.inactiveEvent, function ( ctx, ip )
        lazynet.error(serviceID, "ip=" .. ip  .. " offline ")
    end)

     lazynet.addTrigger(config.readEvent, function ( ctx,  ip, msg )
        lazynet.error(serviceID, "ip=" .. ip .. "msg=" .. msg)
    end)

    lazynet.createSocket(config.port, config.heartbeat, config.type);
    lazynet.bind(config.activeEvent, config.inactiveEvent, config.readEvent, config.exceptionEvent);

end) 