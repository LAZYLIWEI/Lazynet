local lazynet = {}

-- log的级别
lazynet.LogLevel = {
	ERROR = 0,
	WARN = 1,
	DEBUG = 2,
	INFO = 3
}

-- socket的类型
lazynet.SocketType = {
	TCP_SOCKET = 0,
    WEB_SOCKET = 1
}

lazynet.core = {}

-- 获取管理对象
lazynet.core.getInstance = function()
	local instance = clr.Lazynet.GateApp.LazynetAppManager.GetInstance();
	return instance;
end

-- 获得名称
lazynet.core.getName = function()
	local instance = lazynet.core.getInstance();
	return instance.Context.Name;
end

-- 设置名称
lazynet.core.setName = function(name)
	local instance = lazynet.core.getInstance();
	instance.Context.Name = name;
end

-- 打印
lazynet.core.log = function(level, content)
	local instance = lazynet.core.getInstance();
	instance.Context.Log(level, content)
end

lazynet.core.info = function(content)
	local instance = lazynet.core.getInstance();
	instance.Context.Log(lazynet.LogLevel.INFO, content)
end


-- 添加服务
lazynet.core.addService = function(service)
	local instance = lazynet.core.getInstance();
	instance.Context.AddService(service)
end

-- 启动服务器
lazynet.core.start = function(isc, esc)
	local instance = lazynet.core.getInstance();
	instance.Context.SetInteriorServerConfig(isc.ip, isc.port, isc.socketType);
	instance.Context.SetExternalServerConfig(esc.port, esc.heartbeat, esc.socketType, esc.path);
	instance.Start();
end


-- 往内部发消息
lazynet.core.sendMessage = function(ctx, route, ...)
	local arg = {...};
	local instance = lazynet.core.getInstance();
	instance.Context.SendMessage(ctx, route, arg)
end


-- session
lazynet.core.addSession = function(ctx)
	local instance = lazynet.core.getInstance();
	instance.Context.AddSession(ctx)
end

lazynet.core.removeSession = function(ctx)
	local instance = lazynet.core.getInstance();
	instance.Context.RemoveSession(ctx)
end

lazynet.core.getSessionByCtx = function(ctx)
	local instance = lazynet.core.getInstance();
	return instance.Context.GetSessionByCtx(ctx)
end

lazynet.core.getSessionByID = function(id)
	local instance = lazynet.core.getInstance();
	return instance.Context.GetSessionByID(id)
end

return lazynet;