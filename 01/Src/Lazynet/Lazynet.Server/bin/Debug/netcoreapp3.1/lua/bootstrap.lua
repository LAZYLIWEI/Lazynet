package.path = "./lua/?.lua;" ..  package.path
local lazynet = require "lazynet"


lazynet.start(function ( ... )
    local serviceID = lazynet.getID();
    lazynet.setAlias("bootstrap")
	lazynet.error("bootstrap run");

    -- 创建gate服务
    local gateServiceID = lazynet.createService("./lua/gate.lua");
    lazynet.startService(gateServiceID);

    -- 创建控制台服务
    local consoleServiceID = lazynet.createService("./lua/console.lua");
    lazynet.startService(consoleServiceID);

    lazynet.exit()
end)

 
 