using GameLib.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Util;
namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            int frameCount = 0;
            bool gameRunning = false;
            NetworkInterface networkInterface = new NetworkInterface();
            GameLib.Server.GameState gs = new GameLib.Server.GameState();
            networkInterface.HostServer();

            while (true)
            {
           
                if (networkInterface.clients.Count > 1 && !gameRunning )
                {
                   
                    for (int i = 0; i < networkInterface.clients.Count;i++)
                    {
                        gs.players[i] = new GameLib.Server.Player();
                        gs.players[i].playerID = i;
                        gs.players[i].Client = networkInterface.clients[i];
                       
                    }
                    gameRunning = true;
                }

                if(gameRunning)
                {
                    
                    foreach(Player p in gs.players)
                    {
                        gs.dataManager.CreateNewMap();  
                        gs.dataManager.getCurrentMap().AddData("GameState:Frame", "Running +" + frameCount);
                        gs.dataManager.getCurrentMap().AddData("GameState:PlayerID", "Player: +" + p.playerID);
                        gs.dataManager.SendData(p.Client);
                        Console.Clear();
                        Console.WriteLine("Running +" + frameCount);

                    }

                    frameCount++;
                }
               
            }
        }
    }
}
