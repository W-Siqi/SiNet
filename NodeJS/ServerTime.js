let startTime = 0;

function InitTime(){
    startTime = Date.now();
}

function GetCurrentTime(){
    return Date.now() - startTime;
}

module.exports = {
    InitTime,GetCurrentTime
}