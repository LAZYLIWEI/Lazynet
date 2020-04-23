package.path = "./lua/?.lua;" ..  package.path
local lazynet = require "lazynet"


lazynet.start(function ( ... )
    local serviceID = lazynet.getID();
    lazynet.setAlias("bootstrap")
	lazynet.error(serviceID, "bootstrap run");

    -- 创建gate服务
    local gateServiceID = lazynet.createService("./lua/gate.lua");
    lazynet.startService(serviceID, gateServiceID);
    lazynet.kill(serviceID)
end)

 
 