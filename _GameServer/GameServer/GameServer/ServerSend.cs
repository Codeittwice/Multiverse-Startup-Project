using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class ServerSend
    {
        public static void Welcome(int _toClient, string _msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                _packet.Write(_msg);
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);
            }
        }

        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            GameServer.clients[_toClient].tcp.SendData(_packet);
        }
        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i < GameServer.MaxPlayers; i++)
            {
                GameServer.clients[i].tcp.SendData(_packet);
            }
        }
        private static void SendTCPDataToAll(int _exceptClient,  Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i < GameServer.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    GameServer.clients[i].tcp.SendData(_packet);
                }
            }
        }
    }
}
