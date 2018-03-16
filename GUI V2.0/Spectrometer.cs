using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GUI_V2._0
{
    /// <summary>
    /// class to runn DLL imports to run spectrometer
    /// </summary>
    public class Spectrometer
    {
        [DllImport("BWTEKUSB.dll")]
        public static extern bool InitDevices();

        [DllImport("BWTEKUSB.dll")]
        public static extern int CloseDevices();

        [DllImport("bwtekusb.dll")]
        public static extern Int32 bwtekSetupChannel(Int32 nChannel, [In, Out] Byte[] pChannelStatus);

        [DllImport("BWTEKUSB.dll")]
        public static extern int GetDeviceCount();

        [DllImport("BWTEKUSB.dll")]
        public static extern Int32 GetCCode(byte[] pCCode, Int32 nChannel);

        [DllImport("BWTEKUSB.dll")]
        public static extern Int32 GetUSBType(ref int USBType, Int32 nChannel);

        [DllImport("BWTEKUSB.DLL")]
        public static extern int bwtekReadEEPROMUSB(string filename, int chnnel);

        [DllImport("BWTEKUSB.DLL")]
        public static extern int bwtekTestUSB(int TimngMode, int PixelNumber, int InputMode, int chnnel, int pParam);

        [DllImport("bwtekusb.dll")]
        public static extern Int32 bwtekSetTimeBase0USB(Int32 lTimeBase, Int32 nchannel);

        [DllImport("BWTEKUSB.DLL")]
        public static extern int bwtekSetTimeUSB(int nTime, int channel);

        [DllImport("BWTEKUSB.DLL")]
        public static extern int bwtekDataReadUSB(int TriggerMode, [In, Out] UInt16[] MemHandle, int channel);

        [DllImport("BWTEKUSB.dll")]
        public static extern Int32 bwtekCloseUSB(Int32 nChannel);

        [DllImport("BWTEKUSB.DLL")]
        public static extern int bwtekSetAnalogOut(int nNo, int nSetValue, int chnnel);

        [DllImport("BWTEKUSB.DLL")]
        public static extern int bwtekReadTemperature(int nCommand, int nChannel);

        [DllImport("BWTEKUSB.DLL")]
        public static extern Int32 bwtekDSPDataReadUSB(Int32 nAveNum, Int32 nSmoothing, Int32 nDarkCompensate, Int32 nTriggerMode, UInt16[] pArray, Int32 nChannel);
    }
}
