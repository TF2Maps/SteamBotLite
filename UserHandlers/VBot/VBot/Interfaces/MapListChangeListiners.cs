﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamBotLite
{
    public interface MapListChangeListiner
    {
        void MaplistChange(IReadOnlyList<Map> maplist);
    }
}
