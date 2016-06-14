﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteamKit2;
using System.IO;
using Newtonsoft.Json;

namespace SteamBotLite
{
    class Program
    {


        static void Main(string[] args)
        {


            SteamBotData[] Bots = JsonConvert.DeserializeObject<SteamBotData[]>(File.ReadAllText("settings.json"));
            //Get the login Details we'll use to login
            string[] LoginDetails = new string[2];
            Console.WriteLine("Username:");
            LoginDetails[0] = Console.ReadLine();
            Console.WriteLine("Password:");
            LoginDetails[1] = Console.ReadLine();

            

            SteamUser.LogOnDetails LoginData = new SteamUser.LogOnDetails
            {

                Username = LoginDetails[0],
                Password = LoginDetails[1]
            };
            
          //  SteamConnectionHandler FirstBot = new SteamConnectionHandler(new VBot(LoginData));

            SteamConnectionHandler[] SteamConnections = {
                new SteamConnectionHandler(new VBot(LoginData))
            };

            bool Running = true;

            while (Running)
            {
                
                foreach (SteamConnectionHandler Connection in SteamConnections)
                {
                    Connection.Tick();
                }
                
               // Console.ReadKey();
            }
            Console.ReadKey();
        }

    }
}
    