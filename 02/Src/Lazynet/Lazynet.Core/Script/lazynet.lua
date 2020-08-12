local lazynet = {}

lazynet.Logger = {
    instance = nil
}

-- 创建实例
function lazynet.Logger:new (filename)
  local o = {}
  setmetatable(o, self)
  self.__index = self
  self.instance = clr.Lazynet.Core.Logger.LazynetLogger(filename);
  return o
end

-- 打印信息
function lazynet.Logger:info (content)
    self.instance.Info(content);
end



return lazynet;