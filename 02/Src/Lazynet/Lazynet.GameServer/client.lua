local util = require("util")

local client = {}
client.encryptKey = "12345678123456781234567812345678"

-- 响应
local response = {}

-- 发送登录包
response.login = function(ctx)
	local packet = {
		cmd = "login",
		parameters = {
			name = "gameServer"
		}
	}
	local encryptStr = lazynet.DESEncrypt(table.ToLson(packet), client.encryptKey)
	lazynet.WriteAndFlush(ctx, encryptStr)
end

-- 心跳包
response.heartbeat = function(ctx)
	while(true) 
	do
		local packet = {
			cmd = "heartbeat",
			parameters = {}
		}
		local encryptStr = lazynet.DESEncrypt(table.ToLson(packet), client.encryptKey)
		lazynet.WriteAndFlush(ctx, encryptStr)

		-- sleep
		util.sleep(1)
	end
end


local events = {}
events.onConnect = function(ctx)
	response.login(ctx)
--	lazynet.CreateThread(function()
--		response.heartbeat(ctx)
--	end)
end

events.onDisConnect = function(ctx)
	util.error("onDisConnect")
end

events.onException = function(ctx)
	util.error("onException")
end

events.onRead = function(ctx, msg)
	util.error("onRead" .. msg)
end


client.start = function (config)
	local handle = lazynet.NewClient(config);
	lazynet.SetClientSocketEvent(handle, gameServer.NewClientSocketEvent(events))
	local connectResult = lazynet.ConnectToHost(handle)
	return connectResult;
end


return client;

