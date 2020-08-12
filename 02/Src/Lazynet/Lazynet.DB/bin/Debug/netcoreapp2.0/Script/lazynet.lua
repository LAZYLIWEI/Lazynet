local lazynet = {}

lazynet.Logger = {
    instance = nil
}

-- 创建实例
function lazynet.Logger:new (o)
  o = o or {}
  setmetatable(o, self)
  self.__index = self
  self.instance = clr.Lazynet.Core.Logger.LazynetLogger("log4net.config");
  return o
end

-- 打印信息
function lazynet.Logger:info (content)
    self.instance.Info(content);
end



return lazynet;