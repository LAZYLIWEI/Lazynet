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
	local instance = clr.Lazynet.GameApp.LazynetAppManager.GetInstance();
	return instance;
end

-- 获得名称
lazynet.core.getName = function()
	local instance = lazynet.core.getInstance();
	return instance.Context.Name;
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

-- 添加定时器
lazynet.core.addTimer = function(repeatCount, interval, cb)
	local instance = lazynet.core.getInstance();
	local name = instance.Context.AddJob(repeatCount, interval, cb, {});
	return name;
end

-- 添加定时器并附带参数
lazynet.core.addTimerWithParam = function(repeatCount, interval, param, cb)
	local instance = lazynet.core.getInstance();
	local name = instance.Context.AddJob(repeatCount, interval, cb, param);
	return name;
end

-- 移除定时器
lazynet.core.removeTimer = function(name)
	local instance = lazynet.core.getInstance();
	instance.Context.RemoveJob(name);
end

-- 发消息
lazynet.core.sendMessage = function(route, ...)
	local arg = {...};
	local instance = lazynet.core.getInstance();
	instance.Context.SendMessage(route, arg)
end


-- 设置配置信息
lazynet.core.start = function(ip, port, socketType)
	local instance = lazynet.core.getInstance();
	instance.Context.Config.IP = ip;
	instance.Context.Config.Port = port;
	instance.Context.Config.SocketType = socketType;
	instance.Start();
end


return lazynet