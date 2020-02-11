class RPCMessageBody{
    isCaller;
    signature;
    variable;
    
    static parse(strData) {
        try{
            var m= JSON.parse(strData);

            let message = new RPCMessageBody();
            message.isCaller = m.isCaller;
            message.signature =JSON.parse(m.seriablzedRPCSignature);
            message.variable = JSON.parse(m.serializedRPCVariable);

            return message;
        }
        catch(e){
            let message = new RPCMessageBody();
            message.isCaller = "bad";
            message.signature = "";
            message.variable = "";
            return message;
        }
    }

    encodeToString(){
        let m = new Object();
        m.isCaller = this.isCaller;
        m.seriablzedRPCSignature = JSON.stringify(this.signature);
        m.serializedRPCVariable = JSON.stringify(this.variable);
        return JSON.stringify(m);
    }
}

module.exports = RPCMessageBody;