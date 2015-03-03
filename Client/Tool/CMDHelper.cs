using System;
using System.Diagnostics;

namespace Hitearth.Tool
{
    public class CMDHelper
    {
        public static string[] ExeCommand(string commandText)
        {
            Process p = new Process();
            p.StartInfo.FileName = Environment.GetEnvironmentVariable("ComSpec");
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;

            p.Start();
            p.StandardInput.WriteLine(commandText);
            p.StandardInput.WriteLine("exit");
            p.WaitForExit();
            string strOutput = p.StandardOutput.ReadToEnd();
            string strError = p.StandardError.ReadToEnd();//无错误返回空字符串
            p.Close();
            return new string[] { strOutput, strError };
        }
    }
}






