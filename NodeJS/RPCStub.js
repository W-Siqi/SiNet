const RPCMessageBody = require("./RPCMessageBody");
const ServerTime = require("./ServerTime");
const ServerLogger = require("./ServerLogger");

const GET_SERVER_TIME = "GetServerTime";
const BOARDCAST_EVENT = "BoardcastEvent";

function processRemoteCall(session,rpcMsgBody){
    ServerLogger.logRPC("message body - "+JSON.stringify(rpcMsgBody));
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
    else if(rpcMsgBody.signature.name == BOARDCAST_EVENT){
        let responseRPCMsgBody = new RPCMessageBody();
        responseRPCMsgBody.isCaller = false;
        responseRPCMsgBody.signature = rpcMsgBody.signature;
        responseRPCMsgBody.variable = rpcMsgBody.variable;
        
        session.boardcastRPC(responseRPCMsgBody);
    }
    else{
        console.error("/n/n unimplemented RPC function! /n/n");
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