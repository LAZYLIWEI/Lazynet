local lazynet = require("lazynet");

local services = {};

services["/System/Connect"] = function(ctx)
	lazynet.core.log(lazynet.LogLevel.INFO, "有客户端连接")
end

services["/System/AddNode"] = function(ctx, name)
	lazynet.core.addNode(name, ctx)
	lazynet.core.log(lazynet.LogLevel.INFO, " add node " .. name)
end

services["/System/DisConnect"] = function(ctx)
	lazynet.core.removeNode(ctx)
	lazynet.core.log(lazynet.LogLevel.INFO, "断开连接")
end

services["/System/Exception"] = function(ctx, ex)
	lazynet.core.log(lazynet.LogLevel.ERROR, ex)
end

services["/System/Distribute"] = function(ctx, soucreName, targetName, msg)
	-- 1.判断来源名称在nodes中是否存在
	-- 2.判断目标名称在nodes中是否存在
	-- 3.发送消息
	local soucreNode = lazynet.core.getNodeByName(soucreName);
	if (not soucreNode) then
		lazynet.core.log(lazynet.LogLevel.WARN, "soucre node not exists, name is " .. soucreName);
		return;
	end
	local targetNode = lazynet.core.getNodeByName(targetName);
	if (not targetNode) then
		lazynet.core.log(lazynet.LogLevel.WARN, "target node not exists, name is " .. targetName);
		return;
	end

	lazynet.core.sendMessage(targetNode, msg);
end

return services