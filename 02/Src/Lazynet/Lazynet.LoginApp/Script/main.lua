local util = require "util"


local logLevel = {
	Error = 0,
	Warn = 1,
	Debug = 2,
	Info = 3
}

local appManager = clr.Lazynet.AppCore.LazynetAppManager.GetInstance();
appManager.Log("test:" .. util.now(), logLevel.Info)


local prames = {
	"李伟",
	"小红"
}
local job1Name = appManager.Context.Timer.AddJob(-1, 3, function( parameters ): int
	appManager.Log("CreateLuaJob1" .. parameters[1] .. "|" .. parameters[2], logLevel.Info)
end, prames);

appManager.Context.Timer.RemoveJob(job1Name)

local prames2 = {
	name1 = "李伟1",
	name2 = "小红2"
}
appManager.Context.Timer.AddJob(-1, 3, function( obj ): int
	appManager.Log("CreateLuaJob2" .. obj.name1 .. "|" .. obj.name2, logLevel.Debug)
	prames2.name1 = "智能abc";
end, prames2);



local services = {};
services["sayHello"] = function( msg )
	appManager.Log(msg, logLevel.Info)
	return "不好"
end

appManager.Context.Service.AddService(services)



