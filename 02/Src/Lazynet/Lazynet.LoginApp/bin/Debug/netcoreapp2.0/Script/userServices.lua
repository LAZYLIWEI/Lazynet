local lazynet = require("lazynet")
local db = require("db")

local userServices = {}

-- 工具类
local tool = {}
tool.distributeMessage = function(ctx, soucreName, targetName, routeUrl, parameters)
	local msg = table.ToJson({
		RouteUrl = routeUrl,
		Parameters = parameters
	}, false);
	lazynet.core.info(msg);
	lazynet.core.sendMessage(ctx, "/System/Distribute", soucreName, targetName, msg);
end


userServices["/User/Login"] = function(ctx, sid, userName, password)
	lazynet.core.info(sid .. ":" .. userName ..":".. password);
	local result = db.login(userName, password);
	if (result) then
		lazynet.core.info("登录成功");
		tool.distributeMessage(ctx, lazynet.core.getName(), "GateApp", "/User/LoginSuccess", { 
			sid,
			"登录成功"
		});
	else
		lazynet.core.info("登录失败");
		tool.distributeMessage(ctx, lazynet.core.getName(), "GateApp", "/User/LoginFail", { 
			sid,
			"登录失败"
		});
	end
end

userServices["/User/Register"] = function(ctx, sid, userName, password)
	lazynet.core.info(sid .. ":" .. userName ..":".. password);
	local result = db.register(userName, password);
	if (result) then
		lazynet.core.info("注册成功");
		tool.distributeMessage(ctx, lazynet.core.getName(), "GateApp", "/User/RegisterSuccess", { 
			sid,
			"注册成功"
		});
	else
		lazynet.core.info("注册失败");
		tool.distributeMessage(ctx, lazynet.core.getName(), "GateApp", "/User/RegisterFail", { 
			sid,
			"注册失败"
		});
	end
end

return userServices;