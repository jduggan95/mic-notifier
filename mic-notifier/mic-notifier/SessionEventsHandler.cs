using Flurl.Http;
using NAudio.CoreAudioApi.Interfaces;
using System;

namespace mic_notifier
{
    public class SessionEventsHandler : IAudioSessionEvents
    {
        #region Singleton

        public readonly static SessionEventsHandler Singleton = new SessionEventsHandler();

        #endregion

        #region Constructor

        private SessionEventsHandler()
        {

        }

        #endregion

        int IAudioSessionEvents.OnChannelVolumeChanged(uint channelCount, IntPtr newVolumes, uint channelIndex, ref Guid eventContext)
        {
            //Console.WriteLine("OnChannelVolumeChanged");
            return 0;
        }

        int IAudioSessionEvents.OnDisplayNameChanged(string displayName, ref Guid eventContext)
        {
            //Console.WriteLine("OnDisplayNameChanged");
            return 0;
        }

        int IAudioSessionEvents.OnGroupingParamChanged(ref Guid groupingId, ref Guid eventContext)
        {
            //Console.WriteLine("OnGroupingParamChanged");
            return 0;
        }

        int IAudioSessionEvents.OnIconPathChanged(string iconPath, ref Guid eventContext)
        {
            //Console.WriteLine("OnIconPathChanged");
            return 0;
        }

        int IAudioSessionEvents.OnSessionDisconnected(AudioSessionDisconnectReason disconnectReason)
        {
            //Console.WriteLine("OnSessionDisconnected");
            return 0;
        }

        int IAudioSessionEvents.OnSimpleVolumeChanged(float volume, bool isMuted, ref Guid eventContext)
        {
            //Console.WriteLine("OnSimpleVolumeChanged");
            return 0;
        }

        int IAudioSessionEvents.OnStateChanged(AudioSessionState state)
        {
            //Console.WriteLine("OnStateChanged");
            //Console.WriteLine(state.ToString());
            if (state == AudioSessionState.AudioSessionStateActive)
            {
                var result = "http://192.168.86.89:8080/hotmic".PostAsync();
            }
            else if (state == AudioSessionState.AudioSessionStateInactive)
            {
                var result = "http://192.168.86.89:8080/text".PostAsync();
            }
            return 0;
        }
    }
}
