local lazynet = require("lazynet")

-- 添加服务
local systemServices = {};

systemServices["/System/Connect"] = function(ctx)
	lazynet.core.log(lazynet.LogLevel.INFO, "连接上服务器");
	lazynet.core.sendMessage(ctx, "/System/AddNode", lazynet.core.getName());
end

systemServices["/System/DisConnect"] = function(ctx)
	lazynet.core.log(lazynet.LogLevel.INFO, "断开连接");
end

systemServices["/System/Exception"] = function(ctx, ex)
	lazynet.core.log(lazynet.LogLevel.ERROR, ex);
end

return systemServices

