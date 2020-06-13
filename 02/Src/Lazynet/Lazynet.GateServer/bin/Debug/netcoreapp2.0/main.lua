local interiorServer = require("interiorServer")
local externalServer = require("externalServer")
local util = require("util")


-- 启动内部服务器
interiorServer.start({
	 port = 20000,
	 heartbeat = 3000,
	 socketType = 0
})
util.error("interiorServer bind port = 20000")





-- 开启外部服务
externalServer.start({
	port = 10000,
	heartbeat = 3000,
	socketType = 1,
	websocketPath = "ws"
})
util.error("externalServer bind websocket addr is ws://127.0.0.1:10000")


