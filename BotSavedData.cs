﻿using System;
using System.Collections.Generic;
using SteamKit2;
using System.IO;

namespace SteamBotLite
{
    public class SteamBotData
    {
        public SteamUser.LogOnDetails LoginData = new SteamUser.LogOnDetails();
       
        public Type Userhandler
        {
            get; set;
        }
        

    public string username
        {
            get
            {
                return LoginData.Username;
            }
            set
            {
                
                LoginData.Username = value;
                Console.WriteLine("Username {0}", username);
            }
        }
        public string password
        {
            get
            {
                return LoginData.Password;
            }
            set
            {
                LoginData.Password = value;
            }
        }
        public string BotControlClass
        {
            
            get
            {
                return Userhandler.ToString();
            }
            set
            {
                Type T = Type.GetType(value);
                if ((T.GetType() != null ) && (T.BaseType.ToString() == "SteamBotLite.UserHandler"))
                    {
                    Userhandler = Type.GetType(value);
                    }
            }
        }
        

    }
}