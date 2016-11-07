using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopelyTreeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Node tree = null;
            String input = "";
            while (!input.Equals("exit", StringComparison.InvariantCultureIgnoreCase))
            {
                input = Console.ReadLine();

                if (input.Equals("print", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (tree == null)
                    {
                        Console.WriteLine("No tree available. Please build tree.");
                    }
                    else
                    {
                        tree.printTreeFrom(tree);
                    }
                }
                else if(input.Equals("help", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("/[nodename]/...  = creates a tree with the described node structure");
                    Console.WriteLine("print            = prints the full path of every root node in the tree");
                    Console.WriteLine("help             = brings up this guide");
                    Console.WriteLine("exit             = closes this program");
                }
                else if(input.Equals("synonym", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (tree == null)
                    {
                        Console.WriteLine("No tree available. Please build tree.");
                    }
                    else
                    {
                        Console.Write("Which node do you want to find a synonym of?: ");
                        string synonymInput = Console.ReadLine();

                        string[] split = input.Split('/');

                        Node location = tree;
                        foreach (string nodeName in split)
                        {
                            tree.findChild(nodeName);
                        }
                    }
                }
                else if (input.StartsWith("/"))
                {
                    //Split input into node name elements
                    string[] split = input.Split('/');
                    Console.WriteLine("Your input: " + input);
                    Console.WriteLine("Number of elements " + split.Length);
                    //Check if there are enough elements to build tree in desired structure
                    if (split.Length > 1)
                    {
                        //If no existing tree, build new one
                        if (tree == null)
                        {
                            tree = new Node(split[1]);
                        }

                        Node location = tree;   //Save tree starting point (root)

                        for (int elementIndex = 2; elementIndex < split.Length; elementIndex++)
                        {
                            //Split element in case of combinatorial input
                            string[] elementSplit = split[elementIndex].Split('|');
                            if (location.findChild(split[elementIndex]) == null)    //if node already exists, travel there
                            {
                                //add basic node names
                                foreach (var subElement in elementSplit)
                                {
                                    location.addChild(subElement);
                                }

                                //add combined node names
                                for (int combinedIndex = 0; combinedIndex < location.getChildren().Count; combinedIndex++)
                                {
                                    string lastNodeNamePiece = location.getChildren()[combinedIndex].getName().Split('-').Last();    //find name of last element in combined node name
                                    for (int splitIndex = Array.IndexOf(elementSplit, lastNodeNamePiece); splitIndex < elementSplit.Length; splitIndex++)
                                    {
                                        if (!location.getChildren()[combinedIndex].getName().Contains(elementSplit[splitIndex]))
                                        {
                                            string newNodeName = location.getChildren()[combinedIndex].getName() + "-" + elementSplit[splitIndex];
                                            if (newNodeName.Length <= split[elementIndex].Length)
                                            {
                                                location.addChild(newNodeName);
                                            }
                                        }
                                    }
                                }

                            }
                            location = location.findChild(split[elementIndex]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Input must start with '/' to build tree");
                    }
                }
                else
                {
                    Console.WriteLine("Unknown Command");
                }
            }
        }
    }
}