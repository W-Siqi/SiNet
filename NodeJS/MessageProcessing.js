function processMessage(syncSession, message){
    if(message.type == "sync"){
        processSyncMessage(syncSession,message);
    }
    else{
        console.log("process fail");
    }
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