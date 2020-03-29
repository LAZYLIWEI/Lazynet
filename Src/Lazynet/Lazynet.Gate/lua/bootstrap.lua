package.path = "./lua/?.lua;" ..  package.path
local lazynet = require "lazynet"


lazynet.start(function ( ... )
    local serviceID = lazynet.getID();
    lazynet.setAlias("bootstrap")
	lazynet.error(serviceID, "start");

    -- ע��trigger
    lazynet.addTrigger('say', function ( serviceID, content )
	    lazynet.error(serviceID, content)
    end)

    -- ����gate��������
    local gateServiceID = lazynet.createService("./lua/gate.lua");

    lazynet.exit()
end)

 
 