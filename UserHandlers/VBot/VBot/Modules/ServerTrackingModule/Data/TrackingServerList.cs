﻿using System.Collections.Generic;

namespace SteamBotLite
{

    public class TrackingServerList
    {
        private ServerTrackingModule ServerTrackingModule;
        private TrackingServerList trackedServers;

        public TrackingServerList(ServerTrackingModule module, List<TrackingServerInfo> TrackingServerList)
        {
            ServerTrackingModule = module;
            TrackingServerListObject = TrackingServerList;
        }

        public TrackingServerList(ServerTrackingModule serverTrackingModule, TrackingServerList trackedServers)
        {
            ServerTrackingModule = serverTrackingModule;
            this.trackedServers = trackedServers;
        }

        public IReadOnlyList<TrackingServerInfo> Servers
        {
            get
            {
                return TrackingServerListObject.AsReadOnly();
            }
        }

        private List<TrackingServerInfo> TrackingServerListObject { get; }

        public void Add(TrackingServerInfo server)
        {
            TrackingServerListObject.Add(server);
            ServerTrackingModule.savePersistentData();
        }

        public void Clear()
        {
            TrackingServerListObject.Clear();
            ServerTrackingModule.savePersistentData();
        }

        public int Count()
        {
            return TrackingServerListObject.Count;
        }

        public IEnumerator<TrackingServerInfo> GetEnumerator()
        {
            return TrackingServerListObject.GetEnumerator();
        }

        public void Remove(TrackingServerInfo server)
        {
            TrackingServerListObject.Remove(server);
            ServerTrackingModule.savePersistentData();
        }
    }
}