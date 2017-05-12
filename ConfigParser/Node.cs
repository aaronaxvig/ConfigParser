using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ConfigParser
{
    enum NodeType { txt, boolean, u32, ipv4, ipv4net, ipv6, ipv6net, macaddr }
    class Node
    {
        public string Name { get; }
        public List<Node> Nodes { get; }
        public NodeType? Type { get; }
        public string Help { get; }

        public Node(string path)
        {
            Name = Path.GetFileName(path);
            Nodes = new List<Node>();

            string[] directories = Directory.GetDirectories(path);

            foreach(string directory in directories)
            {
                Nodes.Add(new Node(directory));
            }

            String nodeDefFilePath = Path.Combine(path, "node.def");

            if (File.Exists(nodeDefFilePath))
            {
                using (FileStream fileStream = new FileStream(nodeDefFilePath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader streamReader = new StreamReader(fileStream))
                    {
                        string line;

                        while ((line = streamReader.ReadLine()) != null)
                        {
                            if (line.StartsWith("type: "))
                            {
                                switch(line.Split(' ')[1])
                                {
                                    case "txt":
                                        Type = NodeType.txt;
                                        break;
                                    case "bool":
                                        Type = NodeType.boolean;
                                        break;
                                    case "u32":
                                        Type = NodeType.u32;
                                        break;
                                    case "ipv4":
                                        Type = NodeType.ipv4;
                                        break;
                                    case "ipv4net":
                                        Type = NodeType.ipv4net;
                                        break;
                                    case "ipv6":
                                        Type = NodeType.ipv6;
                                        break;
                                    case "ipv6net":
                                        Type = NodeType.ipv6net;
                                        break;
                                    case "macaddr":
                                        Type = NodeType.macaddr;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else if(line.StartsWith("help: "))
                            {
                                Help = line.Substring(line.IndexOf(' '));
                            }
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
