const Message = require("./Message");
const ServerLogger = require("./ServerLogger");

const MAX_WAIT_TIME = 6000;
class SyncSession{
    clientIDCounter = 1;
    entities = [];
    clients = [];

    // TBD-Placeholder! 
    boardcastRPC(rpcMessageBody){
        let message = new Message('RPC',rpcMessageBody.encodeToString());
              
        // boardcasr messages to all sockects
        for(let i =0; i < this.clients.length;i++){
            let client = this.clients[i];
            if(client.socket.destroyed){
                this.clients.splice(i,1);
                i--;
            }
            else{
                client.socket.write(message.encodeToString(),function(){
                    console.log("[RPC message to client]");
                })  
            }
        }
    }

    updateEntity(lastestSnapshot){
        for(let i in this.entities){
            let enti = this.entities[i];
            if(enti.lastestSnapshot.sceneUID == lastestSnapshot.sceneUID){
                ServerLogger.logSyncMessage("update- sceneUID: " + lastestSnapshot.sceneUID);
                enti.lastestSnapshot = lastestSnapshot;
                enti.lastUpdateTime = Date.now();
                return true;
            }
        }
        return false;
    }

    addEntity(initSnapshot){
        console.log("new- sceneUID: " + initSnapshot.sceneUID+ " entity count: "+this.entities.length);
        let newEntity = new Entity(initSnapshot);
        this.entities.push(newEntity);
    }

    addAddClient(socket){
        let newClient = new Client(socket, this.clientIDCounter++);
        this.clients.push(newClient);
    }

    async start(frameMseconds){
        while(true){
            // console.log("[server frame] start-----------------------------------------------------------");

            // traverse entities
            // PS: server authority object doesn't sync for now
            let now = Date.now();
            let syncMessages = [];
            for(let i = 0;i<this.entities.length;i++){
                let entity = this.entities[i];
                if(now -  entity.lastUpdateTime>MAX_WAIT_TIME){
                    console.log("time out- sceneUID: "+entity.lastestSnapshot.sceneUID);
                    
                    this.entities.splice(i,1);
                    i--;
                }
                else{
                    ServerLogger.logSyncMessage("encoded - sceneUID: "+entity.lastestSnapshot.sceneUID);
                    
                    let message = new Message('sync',JSON.stringify(entity.lastestSnapshot));
                    syncMessages.push(message);
                }
            }

            // send all messages to all sockects
            for(let i =0; i < this.clients.length;i++){
                let client = this.clients[i];
                if(client.socket.destroyed){
                    this.clients.splice(i,1);
                    i--;
                }
                else{
                    syncMessages.forEach(sMsg => {
                        client.socket.write(sMsg.encodeToString(),function(){
                          // console.log("[message to client]");
                        })   
                    })
                }
            }

            await new Promise((resolve,reject) => {
                setTimeout(resolve, frameMseconds);
            })
        }
    }
}

class Client{
    id = -1;
    socket = null;

    constructor(socket,id){
        this.id = id;
        this.socket = socket;
    }
}

class Entity{
    lastestSnapshot;
    lastUpdateTime;

    constructor(initSnapshot){
        this.lastestSnapshot = initSnapshot;
        this.lastUpdateTime = Date.now();
    }
}

module.exports = SyncSession;