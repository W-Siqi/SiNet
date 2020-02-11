const RPCMessageBody = require("./RPCMessageBody");
const ServerTime = require("./ServerTime");

const GET_SERVER_TIME = "GetServerTime";

function processRemoteCall(session,rpcMsgBody){
    if(rpcMsgBody.signature.name == GET_SERVER_TIME){
        variable = new Object();
        variable.floatValues = [ServerTime.GetCurrentTime()];
        variable.stringValues = [];

        let responseRPCMsgBody = new RPCMessageBody();
        responseRPCMsgBody.isCaller = false;
        responseRPCMsgBody.signature = rpcMsgBody.signature;
        responseRPCMsgBody.variable = variable;
        
        session.boardcastRPC(responseRPCMsgBody);
    }
    else{
        console.error("unimplemented RPC function");
    }
}

function processRPCMessage(session,message) {
    let rpcMsgBody = RPCMessageBody.parse(message.body);
    if(rpcMsgBody.isCaller){
        processRemoteCall(session,rpcMsgBody)
    }
}

module.exports = {
    processRPCMessage
}