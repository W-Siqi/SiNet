'use strict';

var net = require("net");
var Message = require("./Message");
var SyncSession = require("./SyncSession");
var processMessage = require("./MessageProcessing");
var readPackages = require("./PackageReader");
var ServerTime = require("./ServerTime");

// init server time
ServerTime.InitTime();

// the server has THE only session, for now
var syncSession = new SyncSession();
syncSession.start(25);

// create server 
var server = net.createServer(function(socket){
    syncSession.addAddClient(socket);

    // first 
    console.log("one client connected");

    socket.on('data',function(data){
        console.log("buffer size: "+ socket.bufferSize +"the size of data is"+socket.bytesRead);

        let packgeStrs = readPackages(data.toString());
        packgeStrs.forEach(packageStr => {
            let recvMes = Message.parse(packageStr);
            processMessage(syncSession,recvMes);
        });
    })

    socket.on('close',function(haderror){
        console.log('closed, desgtroyed: ',socket.destroyed);
    })
})

// listen
server.listen(8000,function(){
    var address = server.address();
    var message = "the server address is:"+JSON.stringify(address.address);
    console.log(message);
})