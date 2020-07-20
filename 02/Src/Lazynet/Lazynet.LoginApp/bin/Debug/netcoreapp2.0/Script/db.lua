local lazynet = require("lazynet")
local db = {}

db.createUserBaseInfoBll = lazynet.core.getDBProxy().CreateUserBaseInfoBll();

-- 登录
db.login = function(userName, password)
	local result = db.createUserBaseInfoBll:Login(userName, password);
	return result;
end


-- 注册
db.register = function(userName, password)
	local result = db.createUserBaseInfoBll:Register(userName, password);
	return result;
end

return db;