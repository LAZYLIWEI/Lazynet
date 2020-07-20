local lazynet = require("lazynet")

-- 添加服务
local services = {};


local externalServer = {}
externalServer.ctx = nil;
externalServer.distributeMessage = function(soucreName, targetName, routeUrl, parameters)
	local msg = table.ToJson({
		RouteUrl = routeUrl,
		Parameters = parameters
	}, false);
	lazynet.core.info(msg);
	lazynet.core.sendMessage(externalServer.ctx, "/System/Distribute", soucreName, targetName, msg);
end


services["/InteriorServer/Connect"] = function(ctx)
	externalServer.ctx = ctx;
	lazynet.core.info("connect appmgr success");
	lazynet.core.sendMessage(ctx, "/System/AddNode", lazynet.core.getName());
end

services["/InteriorServer/DisConnect"] = function(ctx)
	lazynet.core.info("disconnect appmgr");
end

services["/InteriorServer/Exception"] = function(ctx, ex)
	lazynet.core.log(lazynet.LogLevel.ERROR, ex);
end

services["/InteriorServer/Read"] = function(ctx, routeUrl, sid, ...)
	local str = "recv msg routeUrl = " .. routeUrl .. " sid = " .. sid
	lazynet.core.info(str);

	-- 组成返回给客户端的新包
	local session = lazynet.core.getSessionByID(sid);
	if session then
		lazynet.core.sendMessage(session.Context, routeUrl, ...)
	end
end

-----------------------------------------------------------------------------
-------------------------    外部服务器     ----------------------------------
-----------------------------------------------------------------------------


-- client connect
services["/ExternalServer/Connect"] = function(ctx)
	lazynet.core.info("有客户端连接上来");
	lazynet.core.addSession(ctx);
end

-- read
services["/ExternalServer/Read"] = function(ctx, serverName, routeUrl, ...)
	local session = lazynet.core.getSessionByCtx(ctx);
	local parameters = {...};
	table.insert(parameters, 1, session.ID);
	externalServer.distributeMessage(lazynet.core.getName(), serverName, routeUrl, parameters);
end

-- client disconnect
services["/ExternalServer/DisConnect"] = function(ctx)
	lazynet.core.info("有客户端断开连接");
	lazynet.core.removeSession(ctx);
end

-- client exception
services["/ExternalServer/Exception"] = function(ctx, ex)
	lazynet.core.log(lazynet.LogLevel.ERROR, ex);
end

return services


