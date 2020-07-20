local lazynet = require("lazynet")
local systemServices = require("systemServices")
local userServices = require("userServices")

lazynet.core.info(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>")
lazynet.core.info(">>>>> login  app start  >>>>>")
lazynet.core.info(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>")

-- 添加system service
lazynet.core.addService(systemServices);

-- 添加user service
lazynet.core.addService(userServices);

-- 启动服务器
lazynet.core.start("127.0.0.1", 20000, lazynet.SocketType.TCP_SOCKET);

