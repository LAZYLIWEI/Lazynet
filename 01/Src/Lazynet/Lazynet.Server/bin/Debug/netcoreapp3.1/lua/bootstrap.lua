package.path = "./lua/?.lua;" ..  package.path
local lazynet = require "lazynet"


lazynet.start(function ( ... )
    local serviceID = lazynet.getID();
    lazynet.setAlias("bootstrap")
	lazynet.error("bootstrap run");

    -- ����gate����
    local gateServiceID = lazynet.createService("./lua/gate.lua");
    lazynet.startService(gateServiceID);

    -- ��������̨����
    local consoleServiceID = lazynet.createService("./lua/console.lua");
    lazynet.startService(consoleServiceID);

    lazynet.exit()
end)

 
 