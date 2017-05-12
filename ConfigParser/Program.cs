using System;
using System.IO;

namespace ConfigParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string rootFolder = @"C:\EdgeRouter file dump\opt\vyatta\share\vyatta-cfg\templates";

            Node rootNode = new Node(rootFolder);
        }
    }
}