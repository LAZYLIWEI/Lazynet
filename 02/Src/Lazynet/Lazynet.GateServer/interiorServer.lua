local util = require("util")
local server = {}
server.encryptKey = "12345678123456781234567812345678"


-- session
sessions = {}
local triggers = {}

-- 保存session
triggers.login = function(args)
	local sessionId = lazynet.GetAddress(args.ctx)
	sessions[sessionId] = {
		name = args.name,
		ctx = args.ctx
	} 
	util.error(args.name .. " connect ")

	server.write("你好");
end

-- 心跳包回调
triggers.heartbeat = function(args)
	
end


local events = {}
events.onConnect = function(ctx)
	
end

events.onDisConnect = function(ctx)
	local sessionId = lazynet.GetAddress(ctx)
	sessions[sessionId] = nil
	util.error(sessionId .. " disconnect")
	util.error("session count = " .. util.getn(sessions))
end

events.onRead = function(ctx, msg)
	-- 注意不能如此使用table.FromLson(lazynet.DESDecrypt(msg, key))
	local decryptStr = lazynet.DESDecrypt(msg, server.encryptKey)
	local packet = table.FromLson(decryptStr)
	packet.parameters.ctx = ctx
	lazynet.CallTrigger(server.triggerHandle, packet.cmd, packet.parameters)
end


local function getKey(idx, sessions)
	local currIndex = 0
	for k, v in pairs(sessions) do
		if (idx == currIndex) then
			return k
		end
		currIndex = currIndex + 1
	end
end


local globaIndex = 0
function server.write(str)
	local sessionCount = util.getn(sessions)
	assert(sessionCount ~= 0, " don't have gameServer ")
	local idx = math.modf(globaIndex / sessionCount)
	local session = sessions[getKey(idx, sessions)]

	if session then
		lazynet.WriteAndFlush(session.ctx, str)
		if (globaIndex == sessionCount - 1) then
			globaIndex = 0
		else
			globaIndex = globaIndex + 1
		end
	else
		util.error("session is nil")
	end

end

function server.init()
	server.triggerHandle = lazynet.NewTriggers()
	server.addTriggers(triggers)
end


function server.start(config)
	server.init()
	local handle = lazynet.NewServer({
		port = config.port,
		heartbeat = config.heartbeat,
		socketType = config.socketType
	});
	lazynet.SetServerSocketEvent(handle, gateServer.NewSocketEvent(events));
	return lazynet.Bind(handle);
end

function server.addTriggers(triggers)
	lazynet.AddTrigger(server.triggerHandle, triggers)
end

return server
