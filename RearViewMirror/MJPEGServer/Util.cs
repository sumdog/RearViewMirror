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

    Foobar is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Foobar is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
 *  
 */
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
