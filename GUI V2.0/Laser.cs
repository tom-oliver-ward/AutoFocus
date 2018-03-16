using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GUI_V2._0
{
    /// <summary>
    /// imports DLLs for BWTEK laser control
    /// </summary>
    public class Laser
    {
        [DllImport("bwteklz.DLL")]
        public static extern bool InitDevices();

        [DllImport("bwteklz.dll")]
        public static extern Int32 GetLaserChannel();

        [DllImport("bwteklz.dll")]
        public static extern Int32 bwtekSetupChannel(Int32 nChannel, [In, Out] Byte[] pChannelStatus);

        [DllImport("bwteklz.dll")]
        public static extern Int32 bwtekInitializeLC(Int32 nOption, Int32 nChannel);

        [DllImport("bwteklz.dll")]
        public static extern Int32 bwtekGetADCLC(Int32 nAIN, ref Int32 nData, ref double fCalibrate, Int32 nChannel);

        [DllImport("bwteklz.dll")]
        public static extern Int32 bwtekSetDACLC(Int32 nDOut, Int32 nData, Int32 nChannel);

        [DllImport("bwteklz.dll")]
        public static extern Int32 bwtekReadPowerLC(ref Int32 nPowerMin, ref Int32 nPowerMax, ref Int32 nPowerBack, ref Int32 nPortIO, Int32 nChannel);
    }
}
