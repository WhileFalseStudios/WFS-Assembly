/* -------------------------------------------------------------------
*   Copyright © 2017 While False Studios
*   AssemblyEngine.cs created by Andrew on 2017-4-7 
*   Purpose: The Assembly Engine class, wraps all the assembly functionality.
--------------------------------------------------------------------- */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhileFalseStudios.Assembly
{
    /// <summary>
    /// Called when the assembly engine outputs any text, usually from the PNT instruction.
    /// </summary>
    /// <param name="text"></param>
    public delegate void AssemblyOutput(string text);

    /// <summary>
    /// An assembly interpreter.
    /// </summary>
    public class AssemblyEngine
    {
        #region Variables
        List<int> bytecode = new List<int>();
        Dictionary<string, uint> labels = new Dictionary<string, uint>();        

        //The registers
        int registerA, registerB, registerC, registerD;
        uint programCounter;
        int comparator;
        Stack<int> programStack = new Stack<int>();

        #endregion

        #region Events
        /// <summary>
        /// Fires when a PNT instruction is executed. You can get the outputted text from this event.
        /// </summary>
        public AssemblyOutput OnAssemblyOutput;
        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new instance of an assembly engine.
        /// </summary>
        public AssemblyEngine()
        {
            ConstructorMain();
        }

        void ConstructorMain()
        {

        }

        #endregion

        #region End-User Accessible Functions

        /// <summary>
        /// The number of individual instructions present in the code. Example: 'mov acc, 1' would be 3.
        /// </summary>
        public int CodeInstructionCount
        {
            get
            {
                return bytecode.Count;
            }
        }

        /// <summary>
        /// Compiles and executes the input code. <see cref="Compile(string)"/> does *not* need to be called first.
        /// </summary>
        /// <param name="code"></param>
        public void Execute(string code)
        {
            Compile(code);
        }

        /// <summary>
        /// Compiles the inputted code.
        /// </summary>
        /// <param name="code"></param>
        public void Compile(string code)
        {
            ResetInterpreter();

            try
            {
                Tokenise(code);
            }
            catch (AssemblyException e)
            {
                ResetInterpreter();        
                throw e;
            }            
        }

        /// <summary>
        /// Returns all the compiled instructions, as a sequential string. Useful for debugging the compilation result of a program.
        /// </summary>
        /// <returns></returns>
        public string GetInterpretedCodeInstructions()
        {
            string code = string.Empty;
            foreach (int b in bytecode)
            {
                string next = string.Empty;
                if (b >= 0)
                {
                    next = b.ToString();
                }
                else
                {
                    next = Enum.GetName(typeof(AssemblyTokens), b);
                }

                code += string.Format("{0} ", next);
            }

            return code;
        }

        /// <summary>
        /// Save a compiled program to the specified stream.
        /// </summary>
        /// <param name="write">The stream to write to.</param>
        /// <exception cref="System.IO.IOException"></exception>
        public void SaveProgram(System.IO.Stream write)
        {
            if (!write.CanWrite)
                throw new System.IO.IOException("Stream not available for write.");

            write.Position = 0;
            foreach (int bc in bytecode)
            {
                byte[] bcAsBytes = BitConverter.GetBytes(bc);
                write.Write(bcAsBytes, 0, bcAsBytes.Length);
            }
        }

        #endregion

        void ResetInterpreter()
        {
            bytecode.Clear();
            labels.Clear();
            programStack.Clear();
            programCounter = 0;
            registerA = 0;
            registerB = 0;
            registerC = 0;
            registerD = 0;
        }

        void Tokenise(string code)
        {
            string currentToken = string.Empty;
            uint tokenPos = 0;
            uint lineNum = 1;
            bool skipChars = false;

            foreach (char c in code)
            {
                if (c == '\n') //End of the line.
                {
                    lineNum++;
                    skipChars = false;
                }

                if (char.IsWhiteSpace(c) || c == ',' || code.LastIndexOf(c) == code.Length - 1)
                {                              
                    if (currentToken != string.Empty || (currentToken != string.Empty && skipChars))
                    {
                        //Interpret what we have here.
                        int numVal = 0;
                        var token = TokenStrings.Match(currentToken, out numVal);
                        switch (token)
                        {
                            case AssemblyTokens.Number:
                                bytecode.Add(numVal);
                                break;

                            case AssemblyTokens.Error:
                                throw new AssemblyException(string.Format("unknown token {0}", currentToken), AssemblyException.ExceptionType.Syntax, lineNum);

                            case AssemblyTokens.Label:
                                if (labels.ContainsKey(currentToken))
                                {
                                    throw new AssemblyException(string.Format("duplicate label {0} defined", currentToken), AssemblyException.ExceptionType.Syntax, lineNum);
                                }
                                else
                                {
                                    labels.Add(currentToken.TrimEnd(':'), tokenPos + 1);
                                }
                                break;

                            case AssemblyTokens.LabelReference:
                                if (labels.ContainsKey(currentToken))
                                {
                                    bytecode.Add((int)labels[currentToken]);
                                }
                                else
                                {
                                    throw new AssemblyException(string.Format("use of undefined label {0}", currentToken), AssemblyException.ExceptionType.Syntax, lineNum);
                                }
                                break;

                            default:
                                bytecode.Add((int)token);
                                break;
                        }                        

                        tokenPos++;
                        currentToken = string.Empty;
                    }                    
                }

                else
                {
                    if (skipChars)
                        continue;

                    if (c == ';' || c == '#') //Start of a comment
                    {
                        skipChars = true;
                        continue;
                    }

                    currentToken += c;
                }                
            }

            if (bytecode.Count < 1)
            {
                throw new AssemblyException("No code to execute", AssemblyException.ExceptionType.Parser, 0);
            }
        }

        #region Assembly Instructions

        // MOV a, b
        void Move(int a, int b)
        {

        }

        // ADD a, b
        void Add(int a, int b)
        {

        }

        // SUB a, b
        void Subtract(int a, int b)
        {

        }

        #endregion


    }
}
