/*
 * RearViewMirror - Sumit Khanna <sumit@penguindreams.org>
 * Copyleft 2007-2011, Some rights reserved
 * http://penguindreams.org/projects/rearviewmirror
 * 
 * Based on work by Andrew Kirillov:
 * http://code.google.com/p/aforge/
 * http://www.codeproject.com/KB/audio-video/Motion_Detection.aspx
 * 
 * 
    This file is part of RearViewMirror.

    RearViewMirror is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    RearViewMirror is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with RearViewMirror.  If not, see <http://www.gnu.org/licenses/>.
 *  
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace MJPEGServer
{

    public class Log
    {

        public enum LogLevel { TRACE, DEBUG, INFO, WARN, ERROR, FATAL };

        public static void trace(String s)
        {
            consoleWrite(s);
            SendEvent(s, LogLevel.TRACE);
        }

        public static void debug(String s)
        {
            consoleWrite(s);
            SendEvent(s, LogLevel.DEBUG);
        }

        public static void info(String s)
        {
            consoleWrite(s);
            SendEvent(s, LogLevel.INFO);
        }

        public static void warn(String s)
        {
            consoleWrite(s);
            SendEvent(s, LogLevel.WARN);
        }

        public static void error(String s)
        {
            consoleWrite(s);
            SendEvent(s, LogLevel.ERROR);
        }

        public static void fatal(String s)
        {
            consoleWrite(s);
            SendEvent(s, LogLevel.FATAL);
        }

        private static void consoleWrite(String s)
        {
            Console.WriteLine(s);
        }


        private static void SendEvent(String msg, LogLevel loglevel)
        {
            if (LogEvent != null)
            {
                LogEvent(msg, loglevel);
            }
        }

        public delegate void LogEventHandler(string msg, LogLevel loglevel);

        public static event LogEventHandler LogEvent;

    }


}
