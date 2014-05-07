function MaxCharLimit(source, toshow,  maxlen)
{  
   var ctr= "ctl00_ContentPlaceHolder1_" + toshow;        
   var ctr1= document.getElementById(ctr);
    
   var len= (source.value).length;   
   ctr1.value = maxlen - (parseInt(len));        
   if( parseInt(len) > maxlen)
   {
        source.value = source.value.substring(0, maxlen);
        ctr1.value=0;
   }       
}