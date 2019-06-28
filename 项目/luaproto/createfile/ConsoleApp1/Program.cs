using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
        static void Main(string[] args)
        {
            string path = args[0];
            List<string> filename = new List<string>();
            Console.WriteLine("filepath :" + path);
            for (int i = 1;i < args.Length; ++i)
            {
                Console.WriteLine("insertname :" + args[i]);
                filename.Add(args[i]);
            }
            
            var files = Directory.GetFiles(path, "*proto");
            for(int i = 0;i < files.Length; ++i)
            {
                var name = Path.GetFileName(files[i]);
                Console.WriteLine("filename " + name);
                if (!filename.Contains(name))
                {
                    continue;
                }
                using (FileStream fs = File.Create(name + ".txt"))
                {
                    string text = LuaProto.CreateLuaProto(files[i]);
                    AddText(fs, text);
                }
            }
            var txtFiles = Directory.GetFiles(".", "*txt");
            FileStream csFs = File.Create("..\\LuaManager_proto.cs");
            string start = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using XLua;

public partial class LuaManager
{";
            AddText(csFs, start);
            for (int i = 0; i < txtFiles.Length; ++i)
            {
                var text = File.ReadAllText(txtFiles[i]);
                AddText(csFs, text);
            }
            string end = "\r\n}";
            AddText(csFs, end);
            csFs.Close();
        }
    }
}
