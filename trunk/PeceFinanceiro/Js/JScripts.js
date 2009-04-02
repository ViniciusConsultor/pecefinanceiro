function roundNumber (rnum) {

return Math.round(rnum*Math.pow(10,2))/Math.pow(10,2);

}
/*
2 - float2moeda

A partir de um valor float ela retorna o valor formatado com separador de milhar e vírgula nos centavos.

*/
function float2moeda(num) {

x = 0;

if(num<0) {
num = Math.abs(num);
x = 1;
} if(isNaN(num)) num = "0";
cents = Math.floor((num*100+0.5)%100);

num = Math.floor((num*100+0.5)/100).toString();

if(cents < 10) cents = "0" + cents;
for (var i = 0; i < Math.floor((num.length-(1+i))/3); i++)
num = num.substring(0,num.length-(4*i+3))+'.'
+num.substring(num.length-(4*i+3)); ret = num + ',' + cents; if (x == 1) ret = ' - ' + ret;return ret;

}
/*
3 - moeda2float

Pega um valor formatado com virgula e separador de milha e o transforma em float
*/
function moeda2float(moeda){

moeda = moeda.replace(".","");

moeda = moeda.replace(",",".");

return parseFloat(moeda);

}




/*
* função para permitir a  digitação de números decimais e inteiros
*/
function ForceNumericInput(event, This, AllowDecimal, AllowMinus)
{
if(arguments.length == 1)
{
var s = This.value;
// garante que o sinal de "-" seja o primeiro do índice
var i = s.lastIndexOf("-");
if(i == -1)
return;
if(i != 0)
This.value = s.substring(0,i)+s.substring(i+1);
return;
}
switch(event.keyCode)
{
case 8:     // backspace
case 9:     // tab
case 37:    // left arrow
case 39:    // right arrow
case 46:    // delete
event.returnValue = true;
return;
}
if(event.keyCode == 189)     // sinal de número de negativo
{
if(AllowMinus == false)
{
CancelEventExecution(event);
return;
}
// aguarda até que o controle tenha sido atualizado
var s = "ForceNumericInput(document.getElementById('"+This.id+"'))";
setTimeout(s, 250);
return;
}
if(AllowDecimal && event.keyCode == 188)
{
if(This.value.indexOf(",") >= 0)
{
// restringe a digitação de apenas uma vírgula
CancelEventExecution(event);
return;
}
event.returnValue = true;
return;
}
// permite caracteres entre 0 e 9
if(event.keyCode >= 48 && event.keyCode <= 57)
{
event.returnValue = true;
return;
}
CancelEventExecution(event);
}
/*
* Cancela a execução de uma function mapeada por um evento
*/
function CancelEventExecution(event)
{
if (navigator.appName == "Netscape")
{
event.preventDefault();
}
else
{
event.returnValue = false;
}
}