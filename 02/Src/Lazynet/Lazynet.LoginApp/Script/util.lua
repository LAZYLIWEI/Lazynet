local util = {}

function util.sleep(n)
	if n > 0 then 
		os.execute("ping -n " .. tonumber(n + 1) .. " localhost > NUL") 
	end
end


function util.getFuncname()
	return debug.getinfo(2).name
end


function util.getCurrentline ()
	return debug.getinfo(2).currentline
end


function util.traceback( ... )
	print(debug.traceback())
end


function util.pcall(start, ...)
	return xpcall(start, util.traceback, ...)
end


function util.start(start_func)
	local ok = util.pcall(start_func)
	return ok
end


function util.now()
	local datetime = os.date("%Y/%m/%d %H:%M:%S")
	return datetime
end


function util.error(str)
    print("[" .. util.now() .. "] " .. str)
end


function util.isString(obj)
	return type(obj) == "string"
end


function util.isNumber(obj)
	return type(obj) == "number"
end


function util.isFunction(obj)
	return type(obj) == "function"
end


function util.isBoolean(obj)
	return type(obj) == "boolean"
end


function util.isNil(obj)
	return type(obj) == "nil"
end


function util.isTable(obj)
	return type(obj) == "table"
end


function util.isUserdata(obj)
	return type(obj) == "userdata"
end


function util.getn(t)
	local idx = 0
	for k, v in pairs(t) do
		idx = idx + 1
	end
	return idx
end


return util
