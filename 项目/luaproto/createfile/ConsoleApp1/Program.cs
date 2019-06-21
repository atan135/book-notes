using System;
using System.IO;
using System.Text;
using System.Diagnostics;

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
            Console.WriteLine("Hello World!");
            var files = Directory.GetFiles(".", "*proto");
            for(int i = 0;i < files.Length; ++i)
            {
                using (FileStream fs = File.Create(files[i] + ".txt"))
                {
                    string text = LuaProto.CreateLuaProto(files[i]);
                    AddText(fs, text);
                }
            }
            var txtFiles = Directory.GetFiles(".", "*txt");
            FileStream csFs = File.Create("LuaManager_proto.cs");
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
            string end = "\n}";
            AddText(csFs, end);
            csFs.Close();
        }
    }
}
