using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace ConsoleApp1
{
    class LuaProto
    {
        static string packName = "";
        public static List<string> SplitLine(string line)
        {
            if(line == "repeated PackBuffer.BufferChange buffer = 7;		//buffer变化")
            {
                Debug.WriteLine("stop");
            }
            List<string> result = new List<string>();
            var words = line.Split(' ', '\t', ';');
            for(int i = 0;i < words.Length; ++i)
            {
                if(words[i].Length > 0)
                {
                    result.Add(words[i]);
                }
            }
            return result;
        }
        public static string CreateLuaProto(string filename)
        {
            var lines = File.ReadAllLines(filename);
            string result = "";
            for (int i = 0;i < lines.Length; ++i)
            {
                string line = lines[i].Trim();
                if (line.Length == 0)
                    continue;
                if (line.StartsWith("//"))
                    continue;
                
                if (line.StartsWith("package"))
                {
                    packName = SplitLine(line)[1];
                    Debug.WriteLine("packName:" + packName);
                    continue;
                }
                if (line.StartsWith("message"))
                {
                    int indexTag = i;
                    {
                        string message = SplitLine(line)[1];
                        string text = string.Format("public LuaTable Create{0}{1}({2}.{3} data)\r\n", packName, message, packName, message);
                        text += "{\r\n";
                        text += string.Format("\tLuaTable t = luaEnv.NewTable();\r\n");

                        while (i < lines.Length)
                        {
                            ++i;
                            string tmpLine = lines[i].Trim();
                            if (tmpLine.Length == 0)
                                continue;
                            if (tmpLine.StartsWith("//"))
                                continue;
                            if (tmpLine.StartsWith("}"))
                                break;
                            var words = SplitLine(tmpLine);
                            if (words[0] == "required")
                            {
                                text += "\t" + CreateField(words, true, 1);
                            }
                            else if (words[0] == "optional")
                            {
                                text += "\t" + string.Format("if (data.{0} != null)\r\n", words[2]);
                                text += "\t{\r\n";
                                text += "\t\t" + CreateField(words, false, 2);
                                text += "\t}\r\n";
                            }
                            else if (words[0] == "repeated")
                            {
                                text += "\t" + string.Format("if (data.{0} != null)\r\n", words[2]);
                                text += "\t{\r\n";
                                text += "\t\t" + string.Format("LuaTable {0}_item = luaEnv.NewTable();\r\n", words[2]);
                                text += "\t\t" + string.Format("for(int i = 0;i < data.{0}.Count; ++i)\r\n", words[2]);
                                text += "\t\t{\r\n";
                                text += "\t\t\t" + CreateRepeatedField(words, 3);
                                text += "\t\t}\r\n";
                                text += "\t\t" + string.Format("t.Set<string, LuaTable>(\"{0}\", {1}_item);\r\n", words[2], words[2]);
                                text += "\t}\r\n";
                            }
                            else
                            {
                                Debug.WriteLine("ERROR!!!! words[0]: " + words[0]);
                                break;
                            }
                        }
                        text += "\treturn t;\r\n";
                        text += "}\r\n";
                        result += text;
                    }
                    i = indexTag;
                    
                    {
                        string message = SplitLine(line)[1];
                        string text = string.Format("public {0}.{1} CreatePB{2}{3}(LuaTable t)\r\n", packName, message, packName, message);
                        text += "{\r\n";
                        text += string.Format("\t{0}.{1} data = new {2}.{3}();\r\n", packName, message, packName, message);

                        while (i < lines.Length)
                        {
                            ++i;
                            line = lines[i].Trim();
                            if (line.Length == 0)
                                continue;
                            if (line.StartsWith("//"))
                                continue;
                            if (line.StartsWith("}"))
                                break;
                            var words = SplitLine(line);
                            if (words[0] == "required")
                            {
                                text += "\t" + CreatePBField(words, true, 1);
                            }
                            else if (words[0] == "optional")
                            {
                                text += "\t" + CreatePBField(words, false, 1);
                            }
                            else if (words[0] == "repeated")
                            {
                                text += "\t" + string.Format("if(t.ContainsKey(\"{0}\"))\r\n", words[2]);
                                text += "\t{\r\n";
                                text += "\t\t" + string.Format("LuaTable t_{0} = t.Get<string, LuaTable>(\"{1}\");\r\n", words[2], words[2]);
                                text += "\t\t" + CreatePBRepeatedList(words, 2);
                                text += "\t\t" + string.Format("for(int i = 0; ;++i)\r\n");
                                text += "\t\t{\r\n";
                                text += "\t\t\t" + string.Format("if(t_{0}.ContainsKey<int>(i + 1))\r\n", words[2]);
                                text += "\t\t\t{\r\n";
                                text += "\t\t\t\t" + CreatePBRepeatedField(words, 4);
                                text += "\t\t\t}\r\n\t\t\telse\r\n\t\t\t{\r\n\t\t\t\tbreak;\r\n\t\t\t}\r\n\t\t}\r\n";
                                text += "\t}\r\n";
                            }
                            else
                            {
                                Debug.WriteLine("ERROR!!!! words[0]: " + words[0]);
                                break;
                            }
                        }
                        text += "\treturn data;\r\n";
                        text += "}\r\n";
                        result += text;
                    }
                }
                
        }
            return result;
        }
        static string CreateRepeatedField(List<string> word, int tap = 0)
        {
            string text = "", pName = "";
            var word1 = word[1].Split('.');
            if (word1.Length > 1)
            {
                Debug.WriteLine("引用了其他的package: " + packName + " --> " + word1[0]);
                word[1] = word1[word1.Length - 1];
                pName = word1[0];
            }
            else
            {
                pName = packName;
            }
            switch (word[1])
            {
                case "int32":
                    text += string.Format("{0}_item.Set<int, int>(i+1, data.{0}[i]);\r\n", word[2]);
                    break;
                case "int64":
                    text += string.Format("{0}_item.Set<int, System.Int64>(i+1, data.{0}[i]);\r\n", word[2]);
                    break;
                case "bool":
                    text += string.Format("{0}_item.Set<int, bool>(i+1, data.{0}[i]);\r\n", word[2]);
                    break;
                case "string":
                    text += string.Format("{0}_item.Set<int, string>(i+1, data.{0}[i]);\r\n", word[2]);
                    break;
                case "buffer":
                    text += string.Format("{0}_item.Set<int, Buffer>(i+1, data.{0}[i]);\r\n", word[2]);
                    break;
                default:
                    text += string.Format("LuaTable t_{0} = Create{1}{2}(data.{3}[i]);\r\n", word[2], pName, word[1], word[2]);
                    for (int i = 0; i < tap; ++i)
                        text += "\t";
                    text += string.Format("{0}_item.Set<int, LuaTable>(i+1, t_{0});\r\n", word[2]);
                    break;
            }
            return text;
        }
        static string CreatePBRepeatedField(List<string> word, int tap = 0)
        {
            string text = "", pName = "", taps = "", rname = "";
            var word1 = word[1].Split('.');
            if (word1.Length > 1)
            {
                rname = word1[word1.Length - 1];
                pName = word1[0];
            }
            else
            {
                rname = word[1];
                pName = packName;
            }
            for (int i = 0; i < tap; ++i)
                taps += "\t";
            switch (word[1])
            {
                case "int32":
                    text += string.Format("data.{0}.Add(t_{0}.Get<int, int>(i + 1));\r\n", word[2], word[2]);
                    break;
                case "int64":
                    text += string.Format("data.{0}.Add(t_{0}.Get<int, System.Int64>(i + 1));\r\n", word[2], word[2]);
                    break;
                case "bool":
                    text += string.Format("data.{0}.Add(t_{0}.Get<int, bool>(i + 1));\r\n", word[2], word[2]);
                    break;
                case "string":
                    text += string.Format("data.{0}.Add(t_{0}.Get<int, string>(i + 1));\r\n", word[2], word[2]);
                    break;
                case "buffer":
                    text += string.Format("data.{0}.Add(t_{0}.Get<int, Buffer>(i + 1));\r\n", word[2], word[2]);
                    break;
                default:
                    text += string.Format("LuaTable subT_{0} = t_{1}.Get<int, LuaTable>(i + 1);\r\n", word[2], word[2]);
                    text += taps + string.Format("{0}.{1} subData_{2} = CreatePB{3}{4}(subT_{5});\r\n", pName, rname, word[2], pName, rname, word[2]);
                    text += taps + string.Format("data.{0}.Add(subData_{1});\r\n", word[2], word[2]);
                    break;
            }
            return text;
        }
        static string CreatePBRepeatedList(List<string> word, int tap = 0)
        {
            string text = "", pName = "", taps = "", rname = "";
            var word1 = word[1].Split('.');
            if (word1.Length > 1)
            {
                Debug.WriteLine("引用了其他的package: " + packName + " --> " + word1[0]);
                rname = word1[word1.Length - 1];
                pName = word1[0];
            }
            else
            {
                rname = word[1];
                pName = packName;
            }
            for (int i = 0; i < tap; ++i)
                taps += "\t";
            switch (word[1])
            {
                case "int32":
                    text += string.Format("data.{0} = new List<int>();\r\n", word[2]);
                    break;
                case "int64":
                    text += string.Format("data.{0} = new List<System.Int64>();\r\n", word[2]);
                    break;
                case "bool":
                    text += string.Format("data.{0} = new List<bool>();\r\n", word[2]);
                    break;
                case "string":
                    text += string.Format("data.{0} = new List<string>();\r\n", word[2]);
                    break;
                case "buffer":
                    text += string.Format("data.{0} = new List<Buffer>();\r\n", word[2]);
                    break;
                default:
                    text += string.Format("data.{0} = new List<{1}.{2}>();\r\n", word[2], pName, rname);
                    break;
            }
            return text;
        }
        static string CreatePBField(List<string> word, bool required, int tap)
        {
            string text = "", pName = "";
            var word1 = word[1].Split('.');
            if (word1.Length > 1)
            {
                Debug.WriteLine("引用了其他的package: " + packName + " --> " + word1[0]);
                word[1] = word1[word1.Length - 1];
                pName = word1[0];
            }
            else
            {
                pName = packName;
            }
            string taps = "";
            for (int i = 0; i < tap; ++i)
                taps += "\t";
            switch (word[1])
            {
                case "int32":
                    text += string.Format("if( t.ContainsKey<string>(\"{0}\"))\r\n", word[2]);
                    text += taps + "{\r\n";
                    text += string.Format("{0}\tdata.{1} = t.Get<string, int>(\"{2}\");\r\n", taps, word[2], word[2]);
                    text += taps + "}\r\n";
                    if (required)
                    {
                        text += taps + "else\r\n";
                        text += taps + "{\r\n";
                        text += taps + string.Format("\tLog.LogError(\"Field {0} Not Exist in LuaTable From Service!!\");\r\n", word[2]);
                        text += taps + "}\r\n";
                    }
                    break;
                case "int64":
                    text += string.Format("if( t.ContainsKey<string>(\"{0}\"))\r\n", word[2]);
                    text += taps + "{\r\n";
                    text += string.Format("{0}\tdata.{1} = t.Get<string, System.Int64>(\"{2}\");\r\n", taps, word[2], word[2]);
                    text += taps + "}\r\n";
                    if (required)
                    {
                        text += taps + "else\r\n";
                        text += taps + "{\r\n";
                        text += taps + string.Format("\tLog.LogError(\"Field {0} Not Exist in LuaTable From Service!!\");\r\n", word[2]);
                        text += taps + "}\r\n";
                    }
                    break;
                case "bool":
                    text += string.Format("if( t.ContainsKey<string>(\"{0}\"))\r\n", word[2]);
                    text += taps + "{\r\n";
                    text += string.Format("{0}\tdata.{1} = t.Get<string, bool>(\"{2}\");\r\n", taps, word[2], word[2]);
                    text += taps + "}\r\n";
                    if (required)
                    {
                        text += taps + "else\r\n";
                        text += taps + "{\r\n";
                        text += taps + string.Format("\tLog.LogError(\"Field {0} Not Exist in LuaTable From Service!!\");\r\n", word[2]);
                        text += taps + "}\r\n";
                    }
                    break;
                case "string":
                    text += string.Format("if( t.ContainsKey<string>(\"{0}\"))\r\n", word[2]);
                    text += taps + "{\r\n";
                    text += string.Format("{0}\tdata.{1} = t.Get<string, string>(\"{2}\");\r\n", taps, word[2], word[2]);
                    text += taps + "}\r\n";
                    if (required)
                    {
                        text += taps + "else\r\n";
                        text += taps + "{\r\n";
                        text += taps + string.Format("\tLog.LogError(\"Field {0} Not Exist in LuaTable From Service!!\");\r\n", word[2]);
                        text += taps + "}\r\n";
                    }
                    break;
                case "buffer":
                    text += string.Format("if( t.ContainsKey<string>(\"{0}\"))\r\n", word[2]);
                    text += taps + "{\r\n";
                    text += string.Format("{0}\tdata.{1} = t.Get<string, Buffer>(\"{2}\");\r\n", taps, word[2], word[2]);
                    text += taps + "}\r\n";
                    if (required)
                    {
                        text += taps + "else\r\n";
                        text += taps + "{\r\n";
                        text += taps + string.Format("\tLog.LogError(\"Field {0} Not Exist in LuaTable From Service!!\");\r\n", word[2]);
                        text += taps + "}\r\n";
                    }
                    break;
                default:
                    text += string.Format("if( t.ContainsKey<string>(\"{0}\"))\r\n", word[2]);
                    text += taps + "{\r\n";
                    text += taps + string.Format("\tLuaTable subTable_{0} = t.Get<string, LuaTable>(\"{1}\");\r\n", word[2], word[2]);
                    text += taps + string.Format("\tdata.{0} = CreatePB{1}{2}(subTable_{3});\r\n", word[2], pName, word[1], word[2]);
                    text += taps + "}\r\n";
                    if (required)
                    {
                        text += taps + "else\r\n";
                        text += taps + "{\r\n";
                        text += taps + string.Format("\tLog.LogError(\"Field {0} Not Exist in LuaTable From Service!!\");\r\n", word[2]);
                        text += taps + "}\r\n";
                    }
                    break;
            }
            return text;
        }
        static string CreateField(List<string> word, bool required, int tap)
        {
            string text = "", pName = "";
            string need = required ? "" : ".GetValueOrDefault()";
            var word1 = word[1].Split('.');
            if (word1.Length > 1)
            {
                Debug.WriteLine("引用了其他的package: " + packName + " --> " + word1[0]);
                word[1] = word1[word1.Length - 1];
                pName = word1[0];
            }
            else
            {
                pName = packName;
            }
            switch (word[1])
            {
                case "int32":
                    text += string.Format("t.Set<string, int>(\"{0}\", data.{1}{2});\r\n", word[2], word[2], need);
                    break;
                case "int64":
                    text += string.Format("t.Set<string, System.Int64>(\"{0}\", data.{1}{2});\r\n", word[2], word[2], need);
                    break;
                case "bool":
                    text += string.Format("t.Set<string, bool>(\"{0}\", data.{1}{2});\r\n", word[2], word[2], need);
                    break;
                case "string":
                    text += string.Format("t.Set<string, string>(\"{0}\", data.{1});\r\n", word[2], word[2]);
                    break;
                case "buffer":
                    text += string.Format("t.Set<string, Buffer>(\"{0}\", data.{1};\r\n", word[2], word[2]);
                    break;
                default:
                    text += string.Format("LuaTable t_{0} = Create{1}{2}(data.{3});\r\n", word[1], pName, word[1], word[2]);
                    for (int i = 0; i < tap; ++i)
                        text += "\t";
                    text += string.Format("t.Set<string, LuaTable>(\"{0}\", t_{1});\r\n", word[2], word[1]);
                    break;
            }
            return text;
        }

    }
}
