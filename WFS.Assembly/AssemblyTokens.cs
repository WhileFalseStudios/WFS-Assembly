/* -------------------------------------------------------------------
*   Copyright © 2017 While False Studios
*   AssemblyTokens.cs created by Andrew on 2017-4-7 
*   Purpose: token string and byte databases.
--------------------------------------------------------------------- */
using System;
using System.Text.RegularExpressions;

namespace WhileFalseStudios.Assembly
{
    /// <summary>
    /// The byte values for the assembly code.
    /// </summary>
    public enum AssemblyTokens
    {
        // Instructions
        /// <summary>
        /// Move
        /// </summary>
        MOV = -255,
        /// <summary>
        /// Addition
        /// </summary>
        ADD,
        /// <summary>
        /// Subtraction
        /// </summary>
        SUB,
        /// <summary>
        /// Compare
        /// </summary>
        CMP,
        /// <summary>
        /// Jump to
        /// </summary>
        JMP,
        /// <summary>
        /// Jump if greater than
        /// </summary>
        JG,
        /// <summary>
        /// Jump if less than
        /// </summary>
        JL,
        /// <summary>
        /// Jump if greater than or equal to
        /// </summary>
        JGE,
        /// <summary>
        /// Jump if less than or equal to
        /// </summary>
        JLE,
        /// <summary>
        /// Jump if equal to
        /// </summary>
        JE,
        /// <summary>
        /// Jump if not equal to
        /// </summary>
        JNE,
        /// <summary>
        /// No operation
        /// </summary>
        NOP,

        // Custom instructions.
        /// <summary>
        /// Print the value specified.
        /// </summary>
        PNT,
        /// <summary>
        /// HALT AND CATCH FIRE! LOOK OUT!
        /// </summary>
        HCF,

        // Registers
        /// <summary>
        /// Register A
        /// </summary>
        ACC,
        /// <summary>
        /// Register B
        /// </summary>
        BCC,
        /// <summary>
        /// Register C
        /// </summary>
        CCC,
        /// <summary>
        /// Register D
        /// </summary>
        DCC,

        // High/low registers
        /// <summary>
        /// High byte of register A
        /// </summary>
        ACH,
        /// <summary>
        /// Low byte of register A
        /// </summary>
        ACL,

        /// <summary>
        /// High byte of register B
        /// </summary>
        BCH,
        /// <summary>
        /// Low byte of register B
        /// </summary>
        BCL,

        /// <summary>
        /// High byte of register C
        /// </summary>
        CCH,
        /// <summary>
        /// Low byte of register C
        /// </summary>
        CCL,

        /// <summary>
        /// High byte of register D
        /// </summary>
        DCH,
        /// <summary>
        /// Low byte of register D
        /// </summary>
        DCL,

        /// <summary>
        /// Special token - used to by tokeniser represent a number, should not appear in compiled code.
        /// </summary>
        Number,
        /// <summary>
        /// Special token - used to by tokeniser represent a label, should not appear in compiled code.
        /// </summary>
        Label,
        /// <summary>
        /// Special token - used to by tokeniser represent a reference to a label, should not appear in compiled code.
        /// </summary>
        LabelReference,

        // Error token, emitted if the token isn't valid.
        /// <summary>
        /// Special token - used to by tokeniser represent a syntax error, should not appear in compiled code.
        /// </summary>
        Error
    }

    /// <summary>
    /// The string tokens for the assembly code.
    /// </summary>
    public static class TokenStrings
    {
        // Instructions
        /// <summary>
        /// Moves a value from the first to the second.
        /// </summary>
        public const string MOV = "mov"; //mov <register>|<number>, <register>|<number>
        /// <summary>
        /// Add the first value to the second. a = a + b
        /// </summary>
        public const string ADD = "add"; //add <register>, <register>|<number>
        /// <summary>
        /// Subtract the second value from the first. a = a - b
        /// </summary>
        public const string SUB = "sub"; //sub <register>, <register>|<number>
        /// <summary>
        /// Compare the two values. The result is stored in the comparator register.
        /// </summary>
        public const string CMP = "cmp"; //cmp <register>|<number>, <register>|<number>
        /// <summary>
        /// Jump to the specified label or memory location.
        /// </summary>
        public const string JMP = "jmp"; //jmp <label>
        /// <summary>
        /// Jump to the specified label or memory location if the result of the last CMP was a > b.
        /// </summary>
        public const string JG = "jg"; //jg <label>
        /// <summary>
        /// Jump to the specified label or memory location if the result of the last CMP was a less than b.
        /// </summary>
        public const string JL = "jl"; //jl <label>
        /// <summary>
        /// Jump to the specified label or memory location if the result of the last CMP was a >= b.
        /// </summary>
        public const string JGE = "jge"; //jge <label>
        /// <summary>
        /// Jump to the specified label or memory location if the result of the last CMP was a less than or equal to b.
        /// </summary>
        public const string JLE = "jle"; //jle <label>
        /// <summary>
        /// Jump to the specified label or memory location if the result of the last CMP was a == b.
        /// </summary>
        public const string JE = "je";
        /// <summary>
        /// Jump to the specified label or memory location if the result of the last CMP was a != b.
        /// </summary>
        public const string JNE = "jne";
        /// <summary>
        /// Does nothing. Blank instruction.
        /// </summary>
        public const string NOP = "nop";

        //Custom instructions
        /// <summary>
        /// Prints a string to the output. Reads all bytes from the specified memory address until a null (0) is reached.
        /// </summary>
        public const string PRT = "pnt"; //pnt <register>|<number>. Prints a string to the output? reads all bytes from the specified memory address until a null (0) is reached.
        /// <summary>
        /// Halts execution.
        /// </summary>
        public const string HCF = "hcf";

        //Registers
        public const string ACC = "acc";
        public const string BCC = "bcc";
        public const string CCC = "ccc";
        public const string DCC = "dcc";
        public const string ACH = "ach";
        public const string ACL = "acl";
        public const string BCH = "bch";
        public const string BCL = "bcl";
        public const string CCH = "cch";
        public const string CCL = "ccl";
        public const string DCH = "dch";
        public const string DCL = "dcl";

        //Formatters for hexadecimal numbers
        /// <summary>
        /// Format for a hexadecimal number.
        /// </summary>
        public const string NUMBER_HEX = "0x{0}";
        /// <summary>
        /// Alternate format for a hexadecimal number.
        /// </summary>
        public const string NUMBER_HEX_2 = "{0}h";
        /// <summary>
        /// Format for a binary number.
        /// </summary>
        public const string NUMBER_BINARY = "{0}b";

        // Special symbols
        /// <summary>
        /// Format for a pointer. Unused.
        /// </summary>
        public const string POINTER = "*{0}";
        /// <summary>
        /// Format for a label.
        /// </summary>
        public const string LABEL_SYMBOL = "{0}:";


        static Regex hexMatcher = new Regex(@"(0x[[:xdigit:]]*)|([[:xdigit:]]*h)");
        static Regex binaryMatcher = new Regex(@"(0|1)*b");
        static Regex labelMatcher = new Regex(@"^[A-z]+:$");
        static Regex labelUseMatcher = new Regex(@"^[A-z]+$");

        /// <summary>
        /// Matches a string to its token, if possible.
        /// Otherwise will return the numerical value of the token.
        /// </summary>
        /// <param name="token">The string to find the token for.</param>
        /// <param name="value">Integer the numerical value will be written to.</param>
        /// <returns></returns>
        public static AssemblyTokens Match(string token, out int value) //value must be > 0 so we don't confuse bytecode with hardcoded numeric values. All bytecodes will be < 0, while all numbers will be positive but within the range of ushort.MaxValue (65535).
        {
            value = 0;
            switch (token.ToLower())
            {
                //Instructions
                case MOV:
                    return AssemblyTokens.MOV;
                case ADD:
                    return AssemblyTokens.ADD;
                case SUB:
                    return AssemblyTokens.SUB;
                case CMP:
                    return AssemblyTokens.CMP;
                case JMP:
                    return AssemblyTokens.JMP;
                case JG:
                    return AssemblyTokens.JG;
                case JGE:
                    return AssemblyTokens.JGE;
                case JL:
                    return AssemblyTokens.JL;
                case JLE:
                    return AssemblyTokens.JLE;
                case JE:
                    return AssemblyTokens.JE;
                case JNE:
                    return AssemblyTokens.JNE;
                case NOP:
                    return AssemblyTokens.NOP;

                // Custom
                case HCF:
                    return AssemblyTokens.HCF;

                // Registers
                case ACC:
                    return AssemblyTokens.ACC;
                case ACH:
                    return AssemblyTokens.ACH;
                case ACL:
                    return AssemblyTokens.ACL;
                case BCC:
                    return AssemblyTokens.BCC;
                case BCH:
                    return AssemblyTokens.BCH;
                case BCL:
                    return AssemblyTokens.BCL;
                case CCC:
                    return AssemblyTokens.CCC;
                case CCH:
                    return AssemblyTokens.CCH;
                case CCL:
                    return AssemblyTokens.CCL;
                case DCC:
                    return AssemblyTokens.DCC;
                case DCH:
                    return AssemblyTokens.DCH;
                case DCL:
                    return AssemblyTokens.DCL;

                default:
                    break; //Keep going, stuff below.
            }

            if (hexMatcher.IsMatch(token))
            {
                token = token.Replace("0x", string.Empty).TrimEnd('h');
                try
                {
                    value = Convert.ToUInt16(token, 16);
                    return AssemblyTokens.Number;
                }
                catch
                {
                    return AssemblyTokens.Error;
                }
            }
            else if (binaryMatcher.IsMatch(token))
            {
                token = token.TrimEnd('b');
                try
                {
                    value = Convert.ToUInt16(token, 2);
                    return AssemblyTokens.Number;
                }
                catch
                {
                    return AssemblyTokens.Error;
                }
            }
            else if (labelMatcher.IsMatch(token))
            {
                return AssemblyTokens.Label;
            }
            else if (labelUseMatcher.IsMatch(token))
            {
                return AssemblyTokens.LabelReference;
            }
            else
            {
                try
                {
                    value = Convert.ToUInt16(token, 10);
                    return AssemblyTokens.Number;
                }
                catch
                {
                    return AssemblyTokens.Error;
                }
            }
        }
    }

}
