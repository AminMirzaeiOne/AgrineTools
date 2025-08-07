using System;
using System.Runtime.InteropServices;

namespace AgrineCore.OS
{
    public static class Audio
    {
        #region COM Interfaces and GUIDs

        private const uint DEVICE_STATE_ACTIVE = 0x00000001;
        private const int STGM_READ = 0x00000000;

        [ComImport]
        [Guid("5CDF2C82-841E-4546-9722-0CF74078229A")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IAudioEndpointVolume
        {
            int RegisterControlChangeNotify(IntPtr pNotify);
            int UnregisterControlChangeNotify(IntPtr pNotify);
            int GetChannelCount(out uint pnChannelCount);
            int SetMasterVolumeLevel(float fLevelDB, Guid pguidEventContext);
            int SetMasterVolumeLevelScalar(float fLevel, Guid pguidEventContext);
            int GetMasterVolumeLevel(out float pfLevelDB);
            int GetMasterVolumeLevelScalar(out float pfLevel);
            int SetChannelVolumeLevel(uint nChannel, float fLevelDB, Guid pguidEventContext);
            int GetChannelVolumeLevel(uint nChannel, out float pfLevelDB);
            int SetMute([MarshalAs(UnmanagedType.Bool)] bool bMute, Guid pguidEventContext);
            int GetMute(out bool pbMute);
        }

        [ComImport]
        [Guid("1BE09788-6894-4089-8586-9A2A6C265AC5")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IMMDevice
        {
            int Activate(ref Guid iid, int dwClsCtx, IntPtr pActivationParams, out IAudioEndpointVolume ppInterface);
        }

        [ComImport]
        [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IMMDeviceEnumerator
        {
            int EnumAudioEndpoints(int dataFlow, int dwStateMask, out IntPtr ppDevices);
            int GetDefaultAudioEndpoint(int dataFlow, int role, out IMMDevice ppEndpoint);
        }

        [ComImport]
        [Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")]
        private class MMDeviceEnumeratorComObject
        {
        }

        private enum EDataFlow
        {
            eRender = 0,
            eCapture = 1,
            eAll = 2
        }

        private enum ERole
        {
            eConsole = 0,
            eMultimedia = 1,
            eCommunications = 2
        }

        #endregion

        private static IAudioEndpointVolume GetVolumeObject()
        {
            IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumeratorComObject();
            IMMDevice speakers;
            deviceEnumerator.GetDefaultAudioEndpoint((int)EDataFlow.eRender, (int)ERole.eMultimedia, out speakers);

            Guid IID_IAudioEndpointVolume = typeof(IAudioEndpointVolume).GUID;
            IAudioEndpointVolume volume = null;
            speakers.Activate(ref IID_IAudioEndpointVolume, 23 /*CLSCTX_INPROC_SERVER*/, IntPtr.Zero, out volume);
            return volume;
        }

        public static float GetMasterVolume()
        {
            try
            {
                IAudioEndpointVolume volume = GetVolumeObject();
                float level;
                volume.GetMasterVolumeLevelScalar(out level);
                return level * 100f;
            }
            catch
            {
                return -1f;
            }
        }

        public static bool SetMasterVolume(float volumePercent)
        {
            try
            {
                if (volumePercent < 0f) volumePercent = 0f;
                if (volumePercent > 100f) volumePercent = 100f;

                IAudioEndpointVolume volume = GetVolumeObject();
                float level = volumePercent / 100f;
                volume.SetMasterVolumeLevelScalar(level, Guid.Empty);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IncreaseVolume(float incrementPercent)
        {
            try
            {
                float current = GetMasterVolume();
                if (current < 0) return false;

                float newVol = current + incrementPercent;
                if (newVol > 100f) newVol = 100f;
                return SetMasterVolume(newVol);
            }
            catch
            {
                return false;
            }
        }

        public static bool DecreaseVolume(float decrementPercent)
        {
            try
            {
                float current = GetMasterVolume();
                if (current < 0) return false;

                float newVol = current - decrementPercent;
                if (newVol < 0f) newVol = 0f;
                return SetMasterVolume(newVol);
            }
            catch
            {
                return false;
            }
        }

        public static bool Mute()
        {
            try
            {
                IAudioEndpointVolume volume = GetVolumeObject();
                volume.SetMute(true, Guid.Empty);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Unmute()
        {
            try
            {
                IAudioEndpointVolume volume = GetVolumeObject();
                volume.SetMute(false, Guid.Empty);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsMuted()
        {
            try
            {
                IAudioEndpointVolume volume = GetVolumeObject();
                bool muted;
                volume.GetMute(out muted);
                return muted;
            }
            catch
            {
                return false;
            }
        }
    }

}
