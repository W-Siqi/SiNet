const isLogPDU = false;
const isLogError = false;
const isLogRPCMessage = true;
const isLogSyncMessage = false;

function logSyncMessage(content){
    if(isLogSyncMessage){
        console.log("[Sync Message] "+content);
    }
}

function logPDU(content){
    if(isLogPDU){
        console.log("[PDU Message] "+content)
    }
}

function logRPC(content){
    if(isLogRPCMessage){
        console.log("[RPC Message] "+content);
    }
}

function logError(content){
    if(isLogError){
        console.logError("[ERROR] "+content);
    }
}

module.exports = {
    logPDU,logRPC,logSyncMessage,logError
}