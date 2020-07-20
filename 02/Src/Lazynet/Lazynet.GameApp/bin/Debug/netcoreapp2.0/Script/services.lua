local lazynet = require("lazynet")

-- 添加服务
local services = {};

services["/System/Connect"] = function()
	lazynet.core.log(lazynet.LogLevel.INFO, "连接上服务器");
	lazynet.core.sendMessage("/System/AddNode", lazynet.core.getName());
end

services["/System/DisConnect"] = function()
	lazynet.core.log(lazynet.LogLevel.INFO, "断开连接");
end

services["/System/Exception"] = function(ex)
	lazynet.core.log(lazynet.LogLevel.ERROR, ex);
end


return services

