local util = require("util")

local server = {}
local events = {}
events.onConnect = function(ctx)
    -- util.error("onConnect")
	
end

events.onDisConnect = function(ctx)
	-- util.error("onDisConnect")
end

events.onException = function(ctx)
	-- util.error("onException")
end

events.onRead = function(ctx, msg)
	-- util.error("onRead" .. msg)

	

end

function server.start(config)
	local handle = lazynet.NewServer(config);
	lazynet.SetServerSocketEvent(handle, gateServer.NewSocketEvent(events));
	return lazynet.Bind(handle);
end

return server



