﻿using System;
using System.Linq;
using System.IO;
using System.Windows.Forms;

/// Copyright © 2015-2017 nicoco007
///
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///     http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
namespace LoLAutoLogin
{
    public static class Log
    {
        private static readonly DateTime StartTime = DateTime.Now;
        public static string LogFile = $@"{DateTime.Now:yyyy-MM-dd\THH-mm-ss}_LoLAutoLogin.log";

        // write a message using the INFO tag
        public static void Verbose(string message, params object[] arg)
        {
            if (Environment.GetCommandLineArgs().Contains("--verbose"))
                Write("[VERBOSE] " + message, arg);
        }
        
        public static void Debug(string message, params object[] arg)
        {
            if(Environment.GetCommandLineArgs().Contains("--debug") || Environment.GetCommandLineArgs().Contains("--verbose"))
                Write("[DEBUG] " + message, arg);
        }

        // write a message using the INFO tag
        public static void Info(string message, params object[] arg)
        {
            Write("[INFO] " + message, arg);
        }

        // write a message using the WARN tag
        public static void Warn(string message, params object[] arg)
        {
            Write("[WARN] " + message, arg);
        }

        // write a message using the ERROR tag
        public static void Error(string message, params object[] arg)
        {
            Write("[ERROR] " + message, arg);
        }

        // write a message using the FATAL tag
        public static void Fatal(string message, params object[] arg)
        {
            Write("[FATAL] " + message, arg);
        }

        // print a stacktrace string using the FATAL tag
        public static void PrintStackTrace(string st)
        {
            foreach (var str in st.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                Fatal(str);
            }
        }

        // write whatever to the debug log and the log file
        public static void Write(string text, params object[] arg)
        {
            if (arg.Length > 0)
                text = string.Format(text, arg);

            text = (DateTime.Now - StartTime).ToString("G") + " " + text;

            foreach (var str in text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                Console.WriteLine(str);

            try
            {
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "LoL Auto Login Logs");
                var file = Path.Combine(dir, LogFile);

                if (dir != null && !Directory.Exists(dir)) Directory.CreateDirectory(dir);

                using (var sw = new StreamWriter(file, true))
                    sw.WriteLine(text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        public static void PrintException(Exception ex)
        {
            Error($"An error of type {ex.GetType()} occured: {ex.Message}");
            Error(ex.StackTrace);
        }
    }
}
