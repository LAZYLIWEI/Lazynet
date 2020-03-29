-- �ļ���Ϊ lazynet.lua
-- ����һ����Ϊ lazynet ��ģ��
local lazynet = {}

-- ��ȡ��ӡ������
function lazynet.getFuncname()
	return debug.getinfo(2).name
end

-- ��ǰ��
function lazynet.getCurrentline ()
	return debug.getinfo(2).currentline
end


function lazynet.traceback( ... )
	print(debug.traceback())
end

-- ����һ������
function lazynet.pcall(start, ...)
	return xpcall(start, lazynet.traceback, ...)
end

-- lua����
function lazynet.start(start_func)
	local ok = lazynet.pcall(start_func)
	return ok
end

-- ��ǰʱ��
function lazynet.now()
	local datetime = os.date("%Y/%m/%d %H:%M:%S")
	return datetime
end

-- ��ӡ
function lazynet.error(serviceID, str)
    print("[" .. serviceID .. "] [" .. lazynet.now() .. "] " .. str)
end

-- ���ַ���
function lazynet.isString(obj)
	return type(obj) == "string"
end

-- ������
function lazynet.isNumber(obj)
	return type(obj) == "number"
end

-- �Ǻ���
function lazynet.isFunction(obj)
	return type(obj) == "function"
end

-- ��bool����
function lazynet.isBoolean(obj)
	return type(obj) == "boolean"
end

-- nil
function lazynet.isNil(obj)
	return type(obj) == "nil"
end

-- ��table
function lazynet.isTable(obj)
	return type(obj) == "table"
end

-----------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------   api    -----------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------

-- ��������
-- �ļ�·��
-- ���ط����ID
function lazynet.createService(filename)
	local serviceID = createService(filename)
	return serviceID
end

-- ��ȡ��ǰ����ID
function lazynet.getID()
	local serviceID = getID()
	return serviceID
end

-- ���ݱ�����ȡID
function lazynet.getServiceID( alias )
	local serviceID = getServiceID(alias)
	return serviceID
end

-- ��ȡ����
function lazynet.getAlias()
	return getAlias()
end

-- ���ñ���
function lazynet.setAlias( alias )
	setAlias(alias)
end

-- ���trigger
function lazynet.addTrigger( cmd, cb )
	addTrigger(cmd, cb)
end

-- ɾ��trigger
function lazynet.removeTrigger( cmd, cb )
	removeTrigger(cmd, cb)
end

-- ��������Ϣ
function lazynet.sendMessage(serviceID, cmd, ...)
	sendMessage(serviceID, cmd, {...})
end

-- �˳�����
function lazynet.exit()
	exit()
end

return lazynet
 
 