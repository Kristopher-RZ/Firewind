﻿using Firewind.HabboHotel.Rooms;
using Firewind.HabboHotel.Users;
using Firewind.HabboHotel.Users.UserDataManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Firewind.HabboHotel.Groups.Types
{
    class Group
    {
        private Habbo _owner;
        private RoomData _room;

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BadgeCode { get; set; }
        public string DateCreated { get; set; }

        public int OwnerID { get; set; }
        public string OwnerName 
        {
            get
            {
                if (_owner == null)
                    _owner = FirewindEnvironment.getHabboForId((uint)OwnerID);
                return _owner.Username;
            }
        }
        public int RoomID { get; set; }
        public string RoomName
        {
            get
            {
                if (_room == null)
                    _room = FirewindEnvironment.GetGame().GetRoomManager().GenerateRoomData((uint)RoomID);
                return _room.Name;
            }
        }

        public int Color1 { get; set; }
        public int Color2 { get; set; }

        public string HtmlColourOne { get; set; }
        public string HtmlColourTwo { get; set; }

        public List<int> PendingMembers { get; set; }
        public List<uint> Members { get; set; }

        public int Type { get; set; }
        public int RightsType { get; set; }

        public Group(DataRow Data, DataTable Members)
        {
            this.ID = (int)Data["id"];
            this.Name = (string)Data["name"];
            this.Description = (string)Data["description"];
            this.BadgeCode = (string)Data["badge"];
            this.DateCreated = (string)Data["date_created"];
            this.OwnerID = Convert.ToInt32(Data["owner_id"]);
            this.RoomID = (int)Data["room_id"];
            this.Color1 = (int)Data["colour_one"];
            this.Color2 = (int)Data["colour_two"];
            this.Type = (int)Data["type"];
            this.RightsType = (int)Data["rights_type"];

            this.Members = new List<uint>();

            foreach (DataRow Member in Members.Rows)
            {
                this.Members.Add((uint)Member["user_id"]);
            }
        }

        public Group()
        {
            // TODO: Complete member initialization
        }

        public static string GenerateBadgeImage(List<Tuple<int, int, int>> parts)
        {
            // b 22 13  s03044  s27044  s1701  s01051
            StringBuilder image = new StringBuilder();
            image.Append("b");
            image.Append(parts[0].Item1.ToString("D2"));
            image.Append(parts[0].Item2.ToString("D2"));

            for (int i = 1; i < parts.Count; i++)
            {
                var part = parts[i];
                if (part.Item1 == 0)
                    continue;
                image.Append("s");
                image.Append((part.Item1 - 20).ToString("D2"));
                image.Append(part.Item2.ToString("D2"));
                image.Append(part.Item3);
            }

            return image.ToString();
        }
    }
}
