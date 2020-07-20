local lazynet = require("lazynet")
local services = require("services")
local test = require("test")

lazynet.core.info(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>")
lazynet.core.info(">>>>> log  app start  >>>>>")
lazynet.core.info(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>")

-- 添加service
lazynet.core.addService(services);

lazynet.core.addService(test);

-- 启动服务器
lazynet.core.start("127.0.0.1", 20000, lazynet.SocketType.TCP_SOCKET);


