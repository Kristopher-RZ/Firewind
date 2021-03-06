﻿using Firewind.HabboHotel.Rooms.Units.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firewind.HabboHotel.Rooms.Units
{
    class OfficialBot : RoomAI
    {
        internal override int GetTypeID()
        {
            // "BOT" - 3
            return 3;
        }

        public OfficialBot(int virtualID, AIBase ai, Room room)
            : base(virtualID, ai, room)
        {
        }
    }
}
