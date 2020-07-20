local lazynet = require("lazynet")
local test = {}

lazynet.core.log(lazynet.LogLevel.INFO, lazynet.core.getName(), " test lua ")

-- 添加有参定时器
local prames = {
	"李伟",
	"小红"
}
local job1Name = lazynet.core.addTimerWithParam(-1, 3, prames, function(parameters): int
	lazynet.core.log(lazynet.LogLevel.INFO, "create first job" .. parameters[1] .. "|" .. parameters[2])
end);

-- 添加无参定时器
local prames2 = {
	name1 = "李伟1",
	name2 = "小红2"
}
local job2Name = lazynet.core.addTimer(-1, 3, function(parameter): int
	lazynet.core.log(lazynet.LogLevel.INFO, "create secend job" .. prames2.name1 .. "|" .. prames2.name2)
end);

--lazynet.core.removeTimer(job2Name)


return test;