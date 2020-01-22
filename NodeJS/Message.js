class Message {
    time;
    type;
    body;

    constructor(type,body){
        this.body = body;
        // Place holder
        this.type = type;
        this.time = 0.0;
    }

    static parse(strData) {
        try{
            var m= JSON.parse(strData.toString());
        }
        catch(e){
            console.log("[Transmit error] --------------------- bad message--------------");
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

    encodeToString(){
        return JSON.stringify(this);
    }
}

module.exports = Message;