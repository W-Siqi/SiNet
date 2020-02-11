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

    encodeToString(){
        let content = JSON.stringify(this);
        let len =  content.length;
        return '%' + len.toString() + content + '%';
    }
}

module.exports = Message;