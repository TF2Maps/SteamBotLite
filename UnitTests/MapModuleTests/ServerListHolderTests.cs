﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SteamBotLite;
using System.Linq;

namespace MapModuleTests
{
    [TestClass]
    public class ServerListHolderTests
    {
        ServerListHolder module;
        ServerListHolder.PlayEntry TestPlayEntry;
        int identifier = 0;
        string MapName = "TESTMAP";

        Dictionary<string, Dictionary<string, object>> MakeConfig()
        {
            Dictionary<string, Dictionary<string, object>> ModuleHolder = new Dictionary<string, Dictionary<string, object>>();
            Dictionary<string, object> ModuleData = new Dictionary<string, object>();
            ModuleData.Add("ListConfigs", "");
            ModuleHolder.Add("ServerListHolder", ModuleData);

            return ModuleHolder;
        }

        void MakeNewModule()
        {
            TestUserHandler tester = new TestUserHandler();
            module = new ServerListHolder(tester, tester, MakeConfig());
        }

        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void Initialize()
        {
            MakeNewModule();
            TestPlayEntry = new ServerListHolder.PlayEntry("25","128.0.0.0","1:00pm");
        }

        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void Cleanup()
        {
            System.IO.File.WriteAllText(module.ModuleSavedDataFilePath(), "");
            MakeNewModule();
        }

        [TestMethod()]
        public void AddEntry()
        {
            module.AddEntry(MapName, TestPlayEntry);
            Assert.IsTrue(module.MapTests[MapName].Contains(TestPlayEntry));
        }
        [TestMethod()]
        public void GroupingEntries()
        {
            module.AddEntry(MapName, TestPlayEntry);
            module.AddEntry(MapName, TestPlayEntry);
            Assert.IsTrue(module.MapTests[MapName].Count == 2);
        }
        [TestMethod()]
        public void Persistancy()
        {
            module.AddEntry(MapName, TestPlayEntry);
            Assert.IsTrue(module.MapTests[MapName].Contains(TestPlayEntry));
            Console.WriteLine("Making new entry");
            MakeNewModule();
            Assert.IsTrue(module.MapTests[MapName].Count > 0);
            Assert.IsTrue(module.MapTests[MapName].First().PlayerCount == TestPlayEntry.PlayerCount);
            Assert.IsTrue(module.MapTests[MapName].First().ServerIP == TestPlayEntry.ServerIP);
            Assert.IsTrue(module.MapTests[MapName].First().TimeEntered == TestPlayEntry.TimeEntered);
            Console.WriteLine("Done");
        }
        [TestMethod()]
        public void DoublePersistancy()
        {
            module.AddEntry(MapName, TestPlayEntry);
            module.AddEntry(MapName, TestPlayEntry);
            Assert.IsTrue(module.MapTests[MapName].Contains(TestPlayEntry));
            Console.WriteLine("Making new entry");
            MakeNewModule();
            Assert.IsTrue(module.MapTests[MapName].Count > 0);
            Assert.IsTrue(module.MapTests[MapName].First().PlayerCount == TestPlayEntry.PlayerCount);
            Assert.IsTrue(module.MapTests[MapName].First().ServerIP == TestPlayEntry.ServerIP);
            Assert.IsTrue(module.MapTests[MapName].First().TimeEntered == TestPlayEntry.TimeEntered);
            Console.WriteLine("Done");
        }

    }
}