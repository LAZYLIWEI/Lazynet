package.path = "./lua/?.lua;" ..  package.path
local lazynet = require "lazynet"


function analysisCommand( cmd, gateID )
    local idx = string.find(cmd, 'sessionList') ;
    if idx ~= 1 then
        --lazynet.error("sessionList");
        lazynet.send(gateID, "getSessionList")
    end
end



lazynet.start(function (...)
    local ID = lazynet.getID();
    local gateID = lazynet.getServiceID("gate");
    lazynet.error("console run");

    while true do
        local t = io.read('*l');
        analysisCommand(t, gateID);
        io.close();
    end

end) 