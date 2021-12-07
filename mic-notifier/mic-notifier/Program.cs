using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using NAudio.CoreAudioApi.Interfaces;

namespace mic_notifier
{
    internal class Program
    {
        public static bool ShouldContinue = true;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MMDevice device = null;
            var enumerator = new MMDeviceEnumerator();
            var originalDevices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
            List<MMDevice> devieds = originalDevices.ToList();
            Console.WriteLine(originalDevices);
            MMDevice headset = devieds.FirstOrDefault(d => d.DeviceFriendlyName == "G433 Gaming Headset");

            SessionCollection coll = headset.AudioSessionManager.Sessions;
            for (int i = 0; i < coll.Count; i++)
            {
                var session = coll[i];
                Console.WriteLine(session);
                if (session.State == NAudio.CoreAudioApi.Interfaces.AudioSessionState.AudioSessionStateActive)
                {
                    Console.WriteLine("It is being used by:");
                    Console.WriteLine(session.GetSessionIdentifier);
                }
            }

            headset.AudioSessionManager.OnSessionCreated += SessionCreate;


            while (ShouldContinue)
            {
                Thread.Sleep(3000);
            }

            Console.WriteLine("The end");

        }

        static void SessionCreate(object sender, IAudioSessionControl newSession)
        {
            Console.WriteLine("The threshold was reached.");
            ShouldContinue = false;
        }

    }
}
