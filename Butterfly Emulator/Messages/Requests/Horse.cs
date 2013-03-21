﻿using System;
using System.Data;
using Butterfly.HabboHotel.Misc;
using Butterfly.HabboHotel.Rooms;
using Butterfly.HabboHotel.Users;
using Butterfly.HabboHotel.Users.Badges;
using Database_Manager.Database.Session_Details.Interfaces;
using HabboEvents;
using Database_Manager;
using Butterfly.Messages;
using Butterfly.HabboHotel.GameClients;

namespace Butterfly.Messages
{
    partial class GameClientMessageHandler
    {
        internal void AnyoneRide()
        {
            int ID = Request.PopWiredInt32(); // Get Next ID
            using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
            {
                dbClient.setQuery("SELECT * FROM user_pets WHERE id='" + ID + "'");
                DataRow Row = dbClient.getRow();
                if ((string)Row[3] != "")
                {
                    int Next = 2;

                    if ((string)Row[16] == "0")
                    {
                        Next = 1;
                        Session.SendBroadcastMessage("Users can't Ride Now (Except by You)");
                    }
                    if ((string)Row[16] == "1")
                    {
                        Next = 0;
                        Session.SendBroadcastMessage("Users can Ride Now");
                    }
                    dbClient.runFastQuery("UPDATE user_pets SET everyone_can_ride='" + Next + "' WHERE id='" + ID + "'");
                }

            }
        }
    }
}