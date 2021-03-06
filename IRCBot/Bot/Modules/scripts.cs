﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

namespace Bot.Modules
{
    class scripts : Module
    {
        // Work in progress
        private ScriptEngine pyEngine = null;
        //private ScriptRuntime pyRuntime = null;
        private ScriptScope pyScope = null;

        public void scripts_control(string[] line, bot ircbot, BotConfig Conf, int conf_id, int nick_access, string nick, string channel, string event_type)
        {
            if (pyEngine == null)
            {
                pyEngine = Python.CreateEngine();
                pyScope = pyEngine.CreateScope();
                
                string msg = "";
                if(line.GetUpperBound(0) > 3)
                {
                    msg = line[3] + " " + line[4];
                }
                else
                {
                    msg = line[3];
                }
                pyScope.SetVariable("line", msg);
                pyScope.SetVariable("event", event_type);
                pyScope.SetVariable("nick", nick);
                pyScope.SetVariable("channel", channel);
                pyScope.SetVariable("bot", ircbot);
                pyScope.SetVariable("conf", Conf);
            }
        }

        public static void Util()
        {

        }
    }
}
