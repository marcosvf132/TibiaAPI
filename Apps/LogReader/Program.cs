using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime;

using OXGaming.TibiaAPI;
using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Network;
using OXGaming.TibiaAPI.Network.ClientPackets;
using OXGaming.TibiaAPI.Network.ServerPackets;
using OXGaming.TibiaAPI.Utilities;

namespace LogReader
{
    class Program
    {
        private static string _log;
        private static string _version;
        private static string _tibiaDirectory = string.Empty;

        private static Client _client;

        static void ParseArgs(string[] args)
        {
            foreach (var arg in args) {
                if (!arg.Contains('=', StringComparison.CurrentCultureIgnoreCase))
                    continue;

                var splitArg = arg.Split('=');
                if (splitArg.Length == 2) {
                    switch (splitArg[0]) {
                        case "-l":
                        case "--log":
                            {
                                _log = splitArg[1].Replace("\"", "");
                            }
                            break;
                        case "-v":
                        case "--version":
                            {
                                _version = splitArg[1].Replace("\"", "");
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            try
            {
                if (args.Length <= 0)
                {
                    Console.WriteLine("Invalid argument.");
                    return;
                }

                ParseArgs(args);
                if (string.IsNullOrEmpty(_log))
                {
                    Console.WriteLine("A log was not specified.");
                    Console.WriteLine("Use -h, or --help, for help.");
                    return;
                }

                if (!File.Exists(_log))
                {
                    Console.WriteLine($"File does not exist: {_log}");
                    return;
                }


                var clientDataDirectory = Environment.CurrentDirectory + "//" + $"ClientData/{_version}";
                if (!Directory.Exists(clientDataDirectory))
                {
                    Console.WriteLine($"ClientData directory for version {_version} doesn't exist. Falling back to default Tibia directory.");
                }
                else
                {
                    _tibiaDirectory = clientDataDirectory;
                }

                _client = new Client(_tibiaDirectory);
                var message = new NetworkMessage(_client);
                message.Write(Convert.FromBase64String(File.ReadAllText(Environment.CurrentDirectory + "//" + _log)));
                message.SetPosition(8);
                IterateLogNetworkMessage(_client, message);

                Console.WriteLine("Extraction complete");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        static void IterateLogNetworkMessage(Client client, NetworkMessage inMessage)
        {
            if (inMessage == null)
                throw new ArgumentNullException(nameof(inMessage));
            
            var packets = new List<(ServerPacketType PacketType, uint Position)>();
            var packetPosition = 0u;
            var currentPacket = ServerPacketType.Invalid;
            var lastKnownPacket = ServerPacketType.Invalid;

            try
            {
                while (inMessage.Position < inMessage.Size) {
                    packetPosition = inMessage.Position;

                    var opcode = inMessage.ReadByte();
                    currentPacket = (ServerPacketType)opcode;

                    Console.WriteLine($"[SERVER:{inMessage.SequenceNumber}] 0x{opcode:X2} - {currentPacket}");

                    var packet = ServerPacket.CreateInstance(client, currentPacket);
                    packet.ParseFromNetworkMessage(inMessage);
                    packets.Add((currentPacket, packetPosition));
                    lastKnownPacket = currentPacket;
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                Console.WriteLine($"Current position: {inMessage.Position}");
                Console.WriteLine($"Current packet: [{(byte)currentPacket:X2}:{packetPosition}]{currentPacket}");
            }
        }
    }
}
