﻿//using System.Collections.Generic;
//using System.Data;
//using Firewind;
//using Database_Manager.Database.Session_Details.Interfaces;
//using System;

//namespace Firewind.HabboHotel.NewAchievements
//{
//    static class AchievementFactory
//    {
//        internal static List<Achievement> GetUserAchievements(uint userID)
//        {
//            List<Achievement> achievements = new List<Achievement>();

//            DataTable dTable;
//            using (IQueryAdapter dbClient = FirewindEnvironment.GetDatabaseManager().getQueryreactor())
//            {
//                dbClient.setQuery("SELECT user_achievements2.id, achievement_info.achievementid, achievement_info.level, achievement_info.progress " +
//                                    "FROM user_achievements2 " +
//                                    "JOIN achievement_info " +
//                                    "ON achievement_info.id = user_achievements2.id " +
//                                    "WHERE user_achievements2.userid = " + userID);

//                dTable = dbClient.getTable();
//            }

//            uint ID;
//            uint achievementID;
//            uint level;
//            uint progress;
//            foreach (DataRow dRow in dTable.Rows)
//            {
//                ID = Convert.ToUInt32(dRow[0]);
//                achievementID = Convert.ToUInt32(dRow[1]);
//                level = Convert.ToUInt32(dRow[2]);
//                progress = Convert.ToUInt32(dRow[3]);

//                Achievement achievement = new Achievement(ID, achievementID, level, progress);
//                achievements.Add(achievement);
//            }

//            return achievements;
//        }

//        internal static Achievement CreateAchievement(uint userID, uint category, uint achievementID)
//        {
//            uint id;
//            using (IQueryAdapter dbClient = FirewindEnvironment.GetDatabaseManager().getQueryreactor())
//            {

//                if (dbClient.dbType == Database_Manager.Database.DatabaseType.MSSQL)
//                    dbClient.setQuery("INSERT INTO user_achievements2 OUTPUT INSERTED.* SET userid = " + userID);
//                else
//                    dbClient.setQuery("INSERT INTO user_achievements2 SET userid = " + userID);
//                id = (uint)dbClient.insertQuery();
//                dbClient.setQuery("INSERT INTO achievement_info SET id = " + userID + ", achievementid = "  + achievementID + ", level = 0, progress = 0");
//                dbClient.runQuery();
//            }

//            return new Achievement(id, achievementID, 0, 0);
//        }
//    }
//}
