class RPCMessageBody{
    static fromString(strData) {
        try{
            // console.log("Parse Data",strData);
            var m= JSON.parse(strData);
        }
        catch(e){
            console.log(e.message);
            let message = new Message();
            message.type = "bad";
            message.time = 0;
            message.body = "";
            return message;
        }
       
        let message = new Message();
        message.time = m.time;
        message.type = m.type;
        message.body = m.body;
        return message; 
    }
}