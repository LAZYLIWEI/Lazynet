package.path = "./lua/?.lua;" ..  package.path
local lazynet = require "lazynet"


lazynet.start(function ( ... )
    local serviceID = lazynet.getID();
    lazynet.setAlias("bootstrap")
	lazynet.error("start");

    lazynet.addTrigger('say', function (serviceID, content)
	    lazynet.error("from serviceID = " .. serviceID .. " content=" .. content)
    end)

    -- 创建gate服务
    local gateServiceID = lazynet.createService("./lua/gate.lua");
    lazynet.startService(gateServiceID);
    lazynet.error("run");

    --lazynet.exit()
end)

 
 