using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mic_notifier
{
    public class SessionConfigurator
    {
        private static void SessionCreate(object sender, IAudioSessionControl newSession)
        {
            newSession.RegisterAudioSessionNotification(SessionEventsHandler.Singleton);
            //Console.WriteLine("The threshold was reached.");
        }

        public static void Start()
        {
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
                else
                {
                    Console.WriteLine("Cleaning up session: " + session.DisplayName);
                    session.Dispose();
                }
            }
            headset.AudioSessionManager.RefreshSessions();

            headset.AudioSessionManager.OnSessionCreated += SessionCreate;
        }
    }
}
