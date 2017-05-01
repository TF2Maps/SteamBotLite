﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.GData;
using Google.Apis.YouTube.v3;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Net;

namespace SteamBotLite
{
    public class MediaBot : UserHandler
    {

        string ApiKey;

        void SetApiKey (string value)
        {
            System.IO.File.WriteAllText(ApiKeySaveFile, value);
            ApiKey = value;
        }
       
        
        
        public MediaBot() {
            GetVideoData("xrbrQhpvn8E");
            ApiKey = GetConfig();
        }

        string ApiKeySaveFile = "Media.txt";
        string APICommand = "!YoutubeAPIKEY";
        string GetConfig()
        {
            if (File.Exists(ApiKeySaveFile)) {
                return System.IO.File.ReadAllText(@ApiKeySaveFile);
            }
            else {
                Console.WriteLine("API Functionality will be disabled until a user sends the command: " + APICommand + " <Key>" );
                return null;
            }
        }

        
        public override void ChatMemberInfo(object sender, Tuple<ChatroomEntity, bool> e){
        }

        public override void OnLoginCompleted(object sender, EventArgs e) {
        }
        
        public override void ProcessChatRoomMessage(object sender, MessageEventArgs e) {
            string VideoID = ExtractID(e.ReceivedMessage);
            
            if (string.IsNullOrEmpty(VideoID)) {
                //Do Nothing
            } else {
                string VideoData = GetVideoData(VideoID);
                if (string.IsNullOrEmpty(VideoData)) {
                    //Do Nothing
                } else {

                    string item = VideoData;//.Replace("\n", string.Empty);

                    Console.WriteLine(item);
                    
                    dynamic red = JsonConvert.DeserializeObject(item);
                    RootObject data = JsonConvert.DeserializeObject<RootObject>(item);
                    dynamic blue = JsonConvert.DeserializeObject(item);
                   
            
                 
                   
                    string Duration = "0" ;
                    //e.ReplyMessage = data["Title"] + "[" + Duration + "]";
                    e.InterfaceHandlerDestination.SendChatRoomMessage(this, e);
                }
               Console.WriteLine(GetVideoData(VideoID));
            }
                
        }
        public class RegionRestriction
        {
            public List<string> allowed { get; set; }
        }

        public class ContentDetails
        {
            public string duration { get; set; }
            public string dimension { get; set; }
            public string definition { get; set; }
            public string caption { get; set; }
            public bool licensedContent { get; set; }
            public RegionRestriction regionRestriction { get; set; }
            public string projection { get; set; }
        }

        public class Item
        {
            public ContentDetails contentDetails { get; set; }
        }

        public class RootObject
        {
            public List<Item> items { get; set; }
        }

        string GetVideoData (string ID)
        {
            try
            {
                string header = "https://www.googleapis.com/youtube/v3/videos?id=";

                string Key = "&key=" + ApiKey + "";
                string trailer = "&fields=items(contentDetails,snippet(title))&part=snippet,contentDetails";
                string WebData = SearchClass.GetWebPageAsString(header + ID + Key + trailer);
                Console.WriteLine(WebData);
                return WebData;
            }
            catch
            {
                return null;
            }
        }

        public override void ProcessPrivateMessage(object sender, MessageEventArgs e) {
            if (e.ReceivedMessage.StartsWith(APICommand, StringComparison.OrdinalIgnoreCase))
            {
                int StartIndex = APICommand.Length;
                int CharactersRemaining = e.ReceivedMessage.Length - APICommand.Length;

                string Key = e.ReceivedMessage.Substring(StartIndex, CharactersRemaining);
                SetApiKey(Key);
            }
        }
        string TrimOpening (string MainString, int Trimmer)
        {
            int StartIndex = Trimmer;
            int CharactersRemaining = MainString.Length - Trimmer;
            return MainString.Substring(StartIndex, CharactersRemaining);
        }

        string ExtractID (string Message) {
            string[] YoutubeRepresentations = new string[] {
                "https://youtu.be/",
                "https://www.youtube.com/watch?v=",
                "https://www.m.youtube.com/watch?v="
            };

            for (int i = 0 ; i < YoutubeRepresentations.Length;i++)
            {
                if (Message.ToLower().Contains(YoutubeRepresentations[i])) {
                    string Value = TrimOpening(Message , YoutubeRepresentations[i].Length);

                    if (Value.EndsWith("/")) {
                        Value = Value.Substring(0, Value.Length - 1);
                    }

                    return Value;
                }
            }
            return null;
        }
        string ExtractURL (string Message) {
            return Message;
        }
    }
}