let startTime = 0;

function InitTime(){
    startTime = Date.now();
}

function GetCurrentTime(){
    return (Date.now() - startTime)/1000.0;
}

module.exports = {
    InitTime,GetCurrentTime
}