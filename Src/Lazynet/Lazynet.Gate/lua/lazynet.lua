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

-- 打印
function lazynet.error(serviceID, str)
    print("[" .. serviceID .. "] [" .. lazynet.now() .. "] " .. str)
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

-----------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------   api    -----------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------

-- 创建服务
-- 文件路径
-- 返回服务号ID
function lazynet.createService(filename)
	local serviceID = createService(filename)
	return serviceID
end

-- 获取当前服务ID
function lazynet.getID()
	local serviceID = getID()
	return serviceID
end

-- 跟据别名获取ID
function lazynet.getServiceID( alias )
	local serviceID = getServiceID(alias)
	return serviceID
end

-- 获取别名
function lazynet.getAlias()
	return getAlias()
end

-- 设置别名
function lazynet.setAlias( alias )
	setAlias(alias)
end

-- 添加trigger
function lazynet.addTrigger( cmd, cb )
	addTrigger(cmd, cb)
end

-- 删除trigger
function lazynet.removeTrigger( cmd, cb )
	removeTrigger(cmd, cb)
end

-- 给服务发消息
function lazynet.sendMessage(serviceID, cmd, ...)
	sendMessage(serviceID, cmd, {...})
end

-- 退出服务
function lazynet.exit()
	exit()
end

return lazynet
 
 