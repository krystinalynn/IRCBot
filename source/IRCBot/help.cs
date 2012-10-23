﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IRCBot
{
    class help
    {
        public void help_control(string[] line, string command, Interface ircbot, IRCConfig conf, int nick_access, string nick)
        {
            switch (command)
            {
                case "help":
                    ircbot.spam_count++;
                    if (nick_access >= ircbot.get_command_access(command))
                    {
                        display_help(line, nick, line[2], nick_access, ircbot, conf);
                    }
                    else
                    {
                        ircbot.sendData("NOTICE", nick + " :You do not have permission to use that command.");
                    }
                    break;
            }
        }

        private void display_help(string[] line, string nick, string channel, int access, Interface ircbot, IRCConfig conf)
        {
            string search_term = "";
            string list_file = ircbot.cur_dir + "\\config\\help.txt";
            if (File.Exists(list_file))
            {
                string msg = "";
                string[] file = System.IO.File.ReadAllLines(list_file);
                bool more_info = false;
                foreach (string file_line in file)
                {
                    string[] split = file_line.Split(':');
                    if (access >= Convert.ToInt32(split[1]))
                    {
                        if (line.GetUpperBound(0) > 3)
                        {
                            more_info = true;
                            search_term = line[4];
                            if (search_term.ToLower().Equals(split[2].ToLower()))
                            {
                                ircbot.sendData("NOTICE", nick + " :" + split[0] + " | Usage: " + conf.command + split[2] + " " + split[3] + " | Description: " + split[4]);
                            }
                        }
                        else
                        {
                            msg += " " + conf.command + split[2] + ",";
                        }
                    }
                }
                if (msg != "")
                {
                    ircbot.sendData("NOTICE", nick + " :" + msg.TrimEnd(','));
                    msg = "";
                }
                else
                {
                    ircbot.sendData("NOTICE", nick + " :No help information available.");
                }
                if (more_info == false)
                {
                    ircbot.sendData("NOTICE", nick + " :For more information about a specific command, type .help <command name>");
                }
            }
        }
    }
}
