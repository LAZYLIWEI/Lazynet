local services = require("services");
local lazynet = require("lazynet");

lazynet.core.log(lazynet.LogLevel.INFO, ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>")
lazynet.core.log(lazynet.LogLevel.INFO, ">>>>>  app manager start  >>>>>")
lazynet.core.log(lazynet.LogLevel.INFO, ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>")

-- 添加service
lazynet.core.addService(services)

-- 启动服务器
local port = 20000
local heart = 30000
lazynet.core.info("bind port = " .. port)
lazynet.core.start(port, heart, lazynet.SocketType)



