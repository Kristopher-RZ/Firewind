﻿
using Butterfly.Core;
using Butterfly.HabboHotel.GameClients;
using System;
using System.Collections.Generic;
using System.Threading;
using Butterfly.Messages.StaticMessageHandlers;
using HabboEvents;

namespace Butterfly.Messages
{
    partial class GameClientMessageHandler
    {
        //internal static int timeOut = 300;

        private GameClient Session;
        private ClientMessage Request;
        private ServerMessage Response;

        //private delegate void RequestHandler();
        //private Dictionary<uint, RequestHandler> RequestHandlers;

        internal GameClientMessageHandler(GameClient Session)
        {
            this.Session = Session;
            //this.RequestHandlers = new Dictionary<uint, RequestHandler>();
            this.Response = new ServerMessage();
        }

        internal ServerMessage GetResponse()
        {
            return Response;
        }

        internal void Destroy()
        {
            // RequestHandlers.Clear();
            Session = null;
        }

        internal void HandleRequest(ClientMessage request)
        {
            if (Session.GetHabbo() != null && Session.GetHabbo().Username == "Leon")
            {
                Logging.WriteLine("Received request " + request.Id + " [LEON]");
            }

            if (ButterflyEnvironment.diagPackets)
            {
                DateTime start = DateTime.Now;
                this.Request = request;
                StaticClientMessageHandler.HandlePacket(this, request);

                TimeSpan spent = DateTime.Now - start;
                if (spent.TotalMilliseconds > ButterflyEnvironment.timeout)
                {
                    Logging.WriteLine("Packet " + request.Id + " took " + spent.Milliseconds + "ms to run. Packetdata: " + request.ToString());
                }
            }
            else
            {
                this.Request = request;

                StaticClientMessageHandler.HandlePacket(this, request);
            }
            //RequestHandler handler;

            //if (RequestHandlers.TryGetValue(pRequest.Id, out handler))
            //{
            //    //Console.ForegroundColor = ConsoleColor.Green;
            //    //Logging.WriteLine(string.Format("Processing packetID => [{0}]", pRequest.Id));
            //    //DateTime start = DateTime.Now;
            //    handler.Invoke();

            //    //TimeSpan spent = DateTime.Now - start;

            //    //int msSpent = (int)spent.TotalMilliseconds;

            //    //if (msSpent > timeOut)
            //    //{
            //    //    Logging.LogCriticalException(start.ToString() +  " PacketID: " + pRequest.Id + ", total time: " + msSpent);
            //    //}

            //    //Logging.WriteLine(string.Format("[{0}] => Invoked [{1} ticks]", pRequest.Id, spent.Ticks));


            //}
            //else
            //{
            //    //Console.ForegroundColor = ConsoleColor.Red;
            //Logging.WriteLine(string.Format("Unknown packetID => [{0}] data: [{1}]", pRequest.Id, pRequest.Header));
            //}

            //TimeSpan TimeUsed = DateTime.Now - start;
            //if (TimeUsed.Milliseconds > 0 || TimeUsed.Seconds > 0)
            //    Logging.WriteLine("Total used time: " + TimeUsed.Seconds + "s, " + TimeUsed.Milliseconds + "ms");


        }

        internal void SendResponse()
        {
            if (Response != null && Response.Id > 0 && Session.GetConnection() != null)
            {
                //Logging.WriteLine("SENDED [" + Response.Id + "] => " + Response.ToString().Replace(Convert.ToChar(0).ToString(), "{char0}"));
                Session.GetConnection().SendData(Response.GetBytes());
            }
        }

        internal void SendResponseWithOwnerParam()
        {
            if (Response != null && Response.Id > 0 && Session.GetConnection() != null)
            {
                //Logging.WriteLine("SENDED [" + Response.Id + "] => " + Response.ToString().Replace(Convert.ToChar(0).ToString(), "{char0}"));
                Response.AppendBoolean(Session.GetHabbo().CurrentRoom.CheckRights(Session, true));
                Session.GetConnection().SendData(Response.GetBytes());
            }
        }
    }
}