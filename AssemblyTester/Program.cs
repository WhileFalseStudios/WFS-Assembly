/* -------------------------------------------------------------------
*   Copyright © 2017 While False Studios
*   Program.cs created by Andrew on 2017-4-7 
*   Purpose: Command line interface
--------------------------------------------------------------------- */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using WhileFalseStudios.Assembly;

namespace AssemblyTester
{
    class Program
    {
        static AssemblyEngine engine;
        static string arg0 = "";

        static bool quit;

        static string code;

        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                arg0 = args[0];
            }
            Console.Title = "Assembly Tester";
            Console.ForegroundColor = ConsoleColor.White;
            engine = new AssemblyEngine();
            engine.OnAssemblyOutput += AssemblyOut;
            Run();
        }

        static void Run()
        {
            Console.WriteLine("Assembly Tester v1.0. Type 'help' for a list of commands.\n");
            while (!quit)
            {
                Console.Write(">>> ");
                string[] input = Console.ReadLine().ToLower().Split(' ');
                if (input.Length > 0)
                {
                    switch (input[0])
                    {
                        case "quit":
                            quit = true;
                            break;

                        case "execute":
                            try
                            {
                                engine.Execute(code);
                            }
                            catch (AssemblyException e)
                            {
                                AssemblyErr(e.Message);
                            }
                            break;

                        case "edit":
                            var app = new CodeForm(code);
                            app.OnEditorClosed += SetCode;
                            Application.Run(app);
                            app.OnEditorClosed -= SetCode;
                            break;

                        case "help":
                            Console.WriteLine("help: display this help\nquit: quits the program\nexecute: executes the loaded code\nedit: opens the code editor.\npreview: dump the compiled instructions\nsave <file>: save the compiled code to the specified file\n");
                            break;

                        case "preview":
                            string interpreted = engine.GetInterpretedCodeInstructions();
                            if (interpreted == string.Empty)
                            {
                                Console.WriteLine("-- null --");
                            }
                            else
                            {
                                Console.WriteLine(interpreted);
                            }
                            break;

                        case "save":
                            if (input.Length < 2)
                            {
                                break;
                            }
                            string path = input[1];
                            var invalid = Path.GetInvalidPathChars();
                            foreach (char c in path)
                            {
                                foreach (char test in invalid)
                                {
                                    if (test == c)
                                    {
                                        break;
                                    }
                                }
                            }

                            path = Path.ChangeExtension(path, ".bin");

                            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);

                            try
                            {
                                engine.SaveProgram(fs);
                                fs.Dispose();
                                break;
                            }
                            catch (Exception e)
                            {
                                AssemblyErr(e.ToString());
                                break;
                            }

                        default:
                            AssemblyOut("Not a command.");
                            break;
                    }
                }
            }
        }

        static void SetCode(string text)
        {
            code = text;
        }

        static void AssemblyErr(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void AssemblyOut(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
