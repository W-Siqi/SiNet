'use strict';

var net = require("net");
var Message = require("./Message");
var SyncSession = require("./SyncSession");
var processMessage = require("./MessageProcessing");

// the server has THE only session, for now
var syncSession = new SyncSession();
syncSession.start(50);
// create server 
var server = net.createServer(function(socket){
    syncSession.addAddClient(socket);

    // first 
    var helloMes = new Message("sync","first connect");
    socket.write(helloMes.encodeToString(),function(){
        var writeSize = socket.bytesWritten;
        console.log("buffer size: "+ socket.bufferSize +"the size of message is： "+writeSize);
    })    
    
    socket.on('data',function(data){
        console.log("buffer size: "+ socket.bufferSize +"the size of data is"+socket.bytesRead);

        // parase body 
        var recvMes = Message.parse(data.toString());

        processMessage(syncSession, recvMes);
        // response test
        // var responseMes = new Message("sync",recvMes.body);
        // socket.write(responseMes.toString(),function(){
        //     var writeSize = socket.bytesWritten;
        //     console.log("the size of message is"+writeSize);
        // })    
    })
    socket.on('close',function(haderror){
        console.log('closed, desgtroyed: ',socket.destroyed);
    })
})

// listen
server.listen(8000,"127.0.0.1",function(){
    var address = server.address();
    var message = "the server address is:"+JSON.stringify(address.address);
    console.log(message);
})