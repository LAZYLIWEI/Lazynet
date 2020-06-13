-- 文件名为 lazynet.lua
-- 定义一个名为 lazynet 的模块
local lazynet = {}

-- 获取打印函数名
function lazynet.getFuncname()
	return debug.getinfo(2).name
end

-- 获当前行
function lazynet.getCurrentline ()
	return debug.getinfo(2).currentline
end

function lazynet.traceback( ... )
	print(debug.traceback())
end

-- 调用一个函数
function lazynet.pcall(start, ...)
	return xpcall(start, lazynet.traceback, ...)
end

-- lua启用
function lazynet.start(start_func)
	local ok = lazynet.pcall(start_func)
	return ok
end

-- 当前时间
function lazynet.now()
	local datetime = os.date("%Y/%m/%d %H:%M:%S")
	return datetime
end

-- 是字符串
function lazynet.isString(obj)
	return type(obj) == "string"
end

-- 是数字
function lazynet.isNumber(obj)
	return type(obj) == "number"
end

-- 是函数
function lazynet.isFunction(obj)
	return type(obj) == "function"
end

-- 是bool类型
function lazynet.isBoolean(obj)
	return type(obj) == "boolean"
end

-- nil
function lazynet.isNil(obj)
	return type(obj) == "nil"
end

-- 是table
function lazynet.isTable(obj)
	return type(obj) == "table"
end

-- 是userdata
function lazynet.isUserdata(obj)
	return type(obj) == "userdata"
end

-----------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------   api    -----------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------



-- 获取当前服务ID
function lazynet.getID()
	local serviceID = getID()
	return serviceID
end

-- 跟据别名获取ID
function lazynet.getServiceID(alias)
	local serviceID = getServiceID(alias)
	return serviceID
end

-- 获取别名
function lazynet.getAlias()
	return getAlias()
end

-- 设置别名
function lazynet.setAlias(alias)
	setAlias(alias)
end

-- 添加trigger
function lazynet.addTrigger(cmd, cb)
	addTrigger(cmd, cb)
end

-- 删除trigger
function lazynet.removeTrigger(cmd, cb)
	removeTrigger(cmd, cb)
end

-- 创建服务
function lazynet.createService(filename)
	local serviceID = createService(filename)
	return serviceID
end

-- 启动服务
function lazynet.startService(startServiceID)
	if lazynet.isNumber(startServiceID) then
		local ID = lazynet.getID();
		lazynet.call(ID, "startService", startServiceID)
	end
end

-- 退出服务
function lazynet.exit()
	local ID = lazynet.getID();
	lazynet.call(ID, "exit")
end

-- 杀死服务
function lazynet.kill(serviceID)
	if lazynet.isNumber(serviceID) then
		lazynet.call(serviceID,  "exit")
	end
end

-- 打印信息
function lazynet.error(str)
	 local ID = lazynet.getID();
	 lazynet.call(ID, "log", str)
end

function lazynet.error2(ID, str)
	 lazynet.call(ID, "log", str)
end

-- socket类型
lazynet.socketType = {
	tcpSocket = 0,
	webSocket = 1
}

-- 创建socket
-- port: 端口
-- heartbeat: 心跳检测时长(s)
-- type: sokcet类型 0:tcpsocket 1:websocket
function lazynet.createSocket(port, heartbeat, type)
	local ID = lazynet.getID();
	lazynet.call(ID, "createSocket", port, heartbeat, type);
end

-- socket绑定地址
function lazynet.bind(events)
	local ID = lazynet.getID();
	lazynet.call(ID, "bind", events.active, events.inactive, events.read, events.exception);
end

-- 写数据
function lazynet.writeAndFlush(ctx, str)
	local ID = lazynet.getID();
	lazynet.call(ID, "writeAndFlush", ctx, str);
end

-- 添加session
function lazynet.addSession(ctx)
	local ID = lazynet.getID();
	lazynet.call(ID, "addSession", ctx)
end


-- 给服务发消息
function lazynet.send(serviceID, cmd, ...)
	sendMessage(serviceID, cmd, {...})
end

-- 调用系统函数
function lazynet.call(serviceID, cmd, ...)
	sendSystemMessage(serviceID, cmd, {...})
end

return lazynet
 
 