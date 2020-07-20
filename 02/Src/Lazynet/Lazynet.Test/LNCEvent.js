// lzynet client event
function LNCEvent(){
    this.events = {};
}

LNCEvent.prototype.register = function(k, fn){
    if (this.events[k]){
        throw new Error(k + " is exists!");
    }
    this.events[k] = fn;
};

LNCEvent.prototype.remove = function(k){
    var fn = this.events[k];
    if (fn){
        delete fn;
    }
}

LNCEvent.prototype.replace = function(k, fn){
    this.events[k] = fn;
}

LNCEvent.prototype.call = function(k, paramArray){
    var fn = this.events[k];
    if (fn){
        return fn.call(this, paramArray)	
    }
};