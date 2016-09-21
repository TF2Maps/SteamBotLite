﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamBotLite
{
    class ConsoleUserHandler : UserHandler
    {
        public override void ChatMemberInfo(UserIdentifier useridentifier, bool MemberInfo)
        {
        }

        public override void OnLoginCompleted(object sender, EventArgs e)
        {
        }

        public override void ProcessChatRoomMessage(object sender, MessageProcessEventData e)
        {
            Console.WriteLine("{0} | {1}: {2}", e.Chatroom, e.Sender.DisplayName, e.ReceivedMessage);
        }

        public override void ProcessPrivateMessage(object sender, MessageProcessEventData e)
        {
            Console.WriteLine("{0}: {1}", e.Sender.DisplayName, e.ReceivedMessage);
        }
    }
}
