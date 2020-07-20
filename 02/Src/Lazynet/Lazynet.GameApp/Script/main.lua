local lazynet = require("lazynet")
local services = require("services")

lazynet.core.info(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>")
lazynet.core.info(">>>>> game app start  >>>>>")
lazynet.core.info(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>")

-- 添加service
lazynet.core.addService(services);


-- 启动服务器
lazynet.core.start("127.0.0.1", 20000, lazynet.SocketType.TCP_SOCKET);



