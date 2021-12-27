using OpenWiz;
using System;
using System.Net;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            WizSocket socket = new WizSocket();
            socket.GetSocket().EnableBroadcast = true; // This will enable sending to the broadcast address
            socket.GetSocket().ReceiveTimeout = 1000; // This will prevent the demo from running indefinitely
            WizHandle handle = new WizHandle("000000000000", IPAddress.Broadcast); // MAC doesn't matter here

            WizState state = new WizState
            {
                Method = WizMethod.setPilot,
                Params = new WizParams
                {
                    State = true,
                    Dimming = 100,
                    Speed = 0,
                    R = 255,
                    B = 222,
                    G = 33
                }
            };

            socket.SendTo(state, handle);

            WizResult pilot;
            while (true)
            {
                state = socket.ReceiveFrom(handle);
                pilot = state.Result;
                break;
            }

        }
    }
}
