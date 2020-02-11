const RPCStub = require("./RPCStub");

function processMessage(syncSession, message){
    if(message.type == "sync"){
        processSyncMessage(syncSession,message);
    }
    else if(message.type == "RPC"){
        processRPCMessage(syncSession,message);
    }
    else{
        console.log("process fail- type: "+message.type);
    }
}

function processRPCMessage(syncSession,message){
    RPCStub.processRPCMessage(syncSession,message);
}

function processSyncMessage(syncSession,message){
    try {
        var syncGOSnapshot = JSON.parse(message.body);
    } catch (e) {
        console.log("[Transmit error] - bad body");
        return;
    } 

    console.log("[processing sync message] - sceneUID: "+syncGOSnapshot.sceneUID);

    let isregisted = syncSession.updateEntity(syncGOSnapshot);
    if(!isregisted){
        syncSession.addEntity(syncGOSnapshot);
    }
}

module.exports = processMessage;