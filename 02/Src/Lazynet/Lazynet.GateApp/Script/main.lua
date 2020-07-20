local lazynet = require("lazynet")
local services = require("services")

lazynet.core.info(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>")
lazynet.core.info(">>>>>  gate app start  >>>>>")
lazynet.core.info(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>")


-- 设置service
 lazynet.core.addService(services)


-- 内部服务器配置
local isc = {
	ip = "127.0.0.1",
	port = 20000,
	socketType = lazynet.SocketType.TCP_SOCKET
}

-- 外部服务器配置
local esc = {
	port = 10000,
	heartbeat = 10000,
	socketType = lazynet.SocketType.WEB_SOCKET,
	path = "/ws"
}

lazynet.core.info(" interior server ip = " .. isc.ip .. " port = " .. isc.port);
lazynet.core.info(" external server port = " .. esc.port);

-- 启动服务器
lazynet.core.start(isc, esc);


