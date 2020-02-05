const DVIDE_SIGN = '%'

function readPackages(rawStr){
   let cur = 0;
   let end = rawStr.length;
   let PDUs = []
   
   //console.log("rawStrLen: ",end,"raw data",rawStr);
   while(cur < end && rawStr[cur] == DVIDE_SIGN){
       // skip divide sign
       cur++;

       // read the number
       let numStr = "";

       //console.log(rawStr[cur],end,cur,typeof(rawStr[cur]));
       while(cur < end && 0<= rawStr[cur] && rawStr[cur] < 10){
           numStr+=rawStr[cur++];
       }

       // cur should == '{'
       let len = parseInt(numStr);
       PDUstart = cur;
       // end == '}'
       PDUend = cur + len;
       if(PDUend < end){
            let newPDU = rawStr.substring(PDUstart,PDUend);
            PDUs.push(newPDU);
       }

       // cur should be '%' (begin divide sign of next PDU)
       cur = PDUend + 2;
   }

   return PDUs;
}

module.exports=readPackages;