﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Firewind.Core;
using Firewind.HabboHotel.GameClients;

namespace Firewind.HabboHotel.Misc
{
    class PixelManager
    {
        private const int RCV_EVERY_MINS = 30;
        private const int RCV_AMOUNT = 10;

        internal static Boolean NeedsUpdate(GameClient Client)
        {
            try
            {
                if (Client.GetHabbo() == null)
                    return false;

                Double PassedMins = (FirewindEnvironment.GetUnixTimestamp() - Client.GetHabbo().LastActivityPointsUpdate) / 60;

                if (PassedMins >= RCV_EVERY_MINS)
                    return true;
            }
            catch (Exception e) 
            {
                Logging.HandleException(e, "PixelManager.NeedsUpdate");
            }
            return false;
        }

        internal static void GivePixels(GameClient Client)
        {
            Double Timestamp = FirewindEnvironment.GetUnixTimestamp();

            Client.GetHabbo().LastActivityPointsUpdate = Timestamp;
            Client.GetHabbo().ActivityPoints += RCV_AMOUNT;
            Client.GetHabbo().UpdateActivityPointsBalance(0);
        }

        internal static void GivePixels(GameClient Client, int amount)
        {
            Double Timestamp = FirewindEnvironment.GetUnixTimestamp();

            Client.GetHabbo().LastActivityPointsUpdate = Timestamp;
            Client.GetHabbo().ActivityPoints += amount;
            Client.GetHabbo().UpdateActivityPointsBalance(0);
        }
    }
}
