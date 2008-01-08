using System;
using System.Collections.Generic;
using System.Text;

namespace MJPEGServer
{

    public class Log
    {
        public static void trace(String s)
        {
            consoleWrite(s);
        }

        public static void debug(String s)
        {
            consoleWrite(s);
        }

        public static void info(String s)
        {
            consoleWrite(s);
        }

        public static void warn(String s)
        {
            consoleWrite(s);
        }

        public static void error(String s)
        {
            consoleWrite(s);
        }

        public static void fatal(String s)
        {
            consoleWrite(s);
        }

        private static void consoleWrite(String s)
        {
            Console.WriteLine(s);
        }
    }
}
