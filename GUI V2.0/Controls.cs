using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_V2._0
{
    /// <summary>
    /// Class for basic operation controls
    /// </summary>
    public class Controls
    {
        public ushort[] DataArray = new ushort[2048];

        /// <summary>
        /// Sets the Laser to the given power - customised function given by BWTEK
        /// </summary>
        /// <param name="laserPowerRatio"> Desired laser power</param>
        /// <param name="laserPowerMin"> Laser Minimum power</param>
        /// <param name="laserPowerMax"> Laser Maximum power</param>
        /// <param name="laserChannel"> Laser Channel</param>
        internal void LaserSet(int laserPowerRatio, int laserPowerMin, int laserPowerMax, int laserChannel, Form1 formObject)
        {
            Form1 form1 = formObject;
            //variable initialisation
            int laserPowerSet;
            int ret = 0;
            int tmp_int = 0;
            double tmp_double = 0;

            if (laserPowerRatio == 0)   //if desired power is zero then set to 0
            {
                laserPowerSet = 0;
            }
            else if (laserPowerRatio == 1)  //if desired power is one set to the minimum value aailable
            {
                laserPowerSet = laserPowerMin;
            }
            else
            {                               //Calculate set input to match desired power
                laserPowerSet = laserPowerMin + (int)((laserPowerMax - laserPowerMin) * laserPowerRatio / 100);
            }
            ret = Laser.bwtekSetDACLC(4, laserPowerSet, 0);     //set desired power

            //confirm laser power was set
            if (ret < 0)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show("Laser power failed to initialise. Please check probe connection", "Program will close", buttons);
                form1.Close();

            }
            else
            {
                ret = Laser.bwtekGetADCLC(4, ref tmp_int, ref tmp_double, laserChannel);    //check set power
                int laserPower = tmp_int;
                int laserPowerValueRatio = laserPowerRatio;
                if (laserPowerValueRatio != 0)
                {
                    int laserPowerValueRatio_bk = laserPowerValueRatio;
                }

            }
        }

        /// <summary>
        /// Move's stage to the input position
        /// </summary>
        /// <param name="position"> desired position </param>
        /// <param name="testSerialNo"> stage serial number</param>
        /// <param name="formObject"></param>
        internal void StageMove(int position, string testSerialNo, Form1 formObject)
        {
            Form1 form1 = formObject;
            CKCubeDCServo.CC_MoveToPosition(testSerialNo, position);//Move to position
            //read current position and compare to desired position (more detailed explanation see stagepos
            float pos = CKCubeDCServo.CC_GetPosition(testSerialNo);
            while (pos != position)
            {
                Thread.Sleep(1000);
                pos = CKCubeDCServo.CC_GetPosition(testSerialNo);

            }
            form1.TestBox_StagePosition.Text = Convert.ToString(pos / form1.motorFactor);
        }

        /// <summary>
        /// Turns on the laser, takes and saves spectra, turns laser off
        /// </summary>
        /// <param name="i">iteration number</param>
        /// <param name="formObject"></param>
        internal void takeSpectrum(int i, Form1 formObject)
        {
            Form1 form1 = formObject;
            LaserSet(form1.laserOn, form1.spfc[2], form1.spfc[3], form1.spfc[1], form1);        //laserOn
            Thread tid1 = new Thread(new ThreadStart(() => SpecThread.Thread1(i)));             //takes spectra using a secondary thread, seems to be quicker
            tid1.Start();
            tid1.Join();                                                                        //waits for thread to complete before turning laser off
            LaserSet(form1.laserOff, form1.spfc[2], form1.spfc[3], form1.spfc[1], form1);       //Laser Off
        }
    }
}
