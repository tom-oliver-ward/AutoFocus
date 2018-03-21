using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_V2._0
{
    /// <summary>
    /// class to shutdown all components
    /// </summary>
    class SDown
    {
        internal void ShutDown(Form1 formObject)
        {
            Form1 form1 = formObject;  
            stageclose(form1);                //closes stage
            specclose(); 
        }
        /// <summary>
        /// Closes the stage down
        /// </summary>
        /// <param name="args"> argument values for stage</param>
        static void stageclose(Form1 formObject)
        {
            Form1 form1 = formObject;
            string testSerialNo = form1.arg[0];
            // stop polling
            bool ret = false;
            ret = CKCubeDCServo.CC_StopPolling(testSerialNo);
            short retb;
            // close device
            retb = CKCubeDCServo.CC_Close(testSerialNo);
        }

        /// <summary>
        /// closes down the spectrometer
        /// </summary>
        internal void specclose()
        {
            Spectrometer.bwtekCloseUSB(0);
            int y = Spectrometer.CloseDevices();
        }
    }
}
