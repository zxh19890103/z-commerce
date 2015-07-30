  <!--下面是input的js文件-->

var aInp=document.getElementsByName('myinput');
var i=0;
var sArray=[];
for(i=0; i<aInp.length; i++)
{
aInp[i].index=i;
sArray.push(aInp[i].value);
aInp[i].onfocus=function()
{
if(sArray[this.index]==aInp[this.index].value)
{
aInp[this.index].value='';
}
aInp[this.index].className='';
};

aInp[i].onblur=function()
{
if(aInp[this.index].value=='')
{
aInp[this.index].value=sArray[this.index];
}
aInp[this.index].className='';
};
}