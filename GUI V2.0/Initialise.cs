using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_V2._0
{
    /// <summary>
    /// class for initialisation of all components
    /// </summary>
    public class Initialise
    {
        Controls controls = new Controls();

        /// <summary>
        /// Starts the stage
        /// </summary>
        /// <param name="formObject">the main form</param>
        internal void stagestart(Form1 formObject)
        {
            Form1 form1 = formObject;
            int velocity = 0;                       //initialises velocity variable
            if (form1.arg.GetLength(0)>2)                //Checks velocity has been specified in arg
            {
                velocity = int.Parse(form1.arg[2]);      //sets velocity to value specified in arg
            }           
            
                

                if (CKCubeDCServo.TLI_BuildDeviceList() == 0)   // Build list of connected device
                {
                    // get device list size 
                    short n = CKCubeDCServo.TLI_GetDeviceListSize();
                    // get TDC serial numbers
                    string[] serialNos;
                    CKCubeDCServo.TLI_GetDeviceListByTypes(out serialNos, new[] { 27 }, 1);
                    // output list of matching devices
                    foreach (string serialNo in serialNos)
                    {
                        // get device info from device
                        TestStruct s = new TestStruct();
                        int iSize = Marshal.SizeOf(typeof(TestStruct));
                        IntPtr iPtr = Marshal.AllocHGlobal(iSize);
                        CKCubeDCServo.TLI_GetDeviceInfo(serialNo, iPtr);
                        // get strings from device info structure
                        s = (TestStruct)(Marshal.PtrToStructure(iPtr, typeof(TestStruct)));
                        Marshal.FreeHGlobal(iPtr);
                        string x1 = new string(s.description);
                        x1 = x1.Substring(0, x1.IndexOf('\0'));
                        string x2 = new string(s.serialNo);
                        x2 = x2.Substring(0, x2.IndexOf('\0'));
                        // output

                    }
                    // if our device exists, test it
                    if (serialNos.AsEnumerable().Contains(form1.arg[0]))
                    {
                        // open device
                        if (CKCubeDCServo.CC_Open(form1.arg[0]) == 0)
                        {
                            //Console.SetOut(TextWriter.Null);
                            // start the device polling at 200ms intervals
                            CKCubeDCServo.CC_StartPolling(form1.arg[0], 200);
                            //Console.SetIn(TextWriter.Null);

                            float pos = CKCubeDCServo.CC_GetPosition(form1.arg[0]);

                            Thread.Sleep(3000);
                            // Home device
                            CKCubeDCServo.CC_Home(form1.arg[0]);

                            //checks whether stage is in position every second until in place
                            while (pos != 0)
                            {
                                Thread.Sleep(1000);                                 
                                pos = CKCubeDCServo.CC_GetPosition(form1.arg[0]);

                            }
                            form1.TestBox_StagePosition.Text = Convert.ToString(pos / form1.motorFactor);   //outputs position       
                            // set velocity if desired
                            if (velocity > 0)
                            {
                                int currentVelocity = 0, currentAcceleration = 0;
                                CKCubeDCServo.CC_GetVelParams(form1.arg[0], ref currentVelocity, ref currentAcceleration);
                                CKCubeDCServo.CC_SetVelParams(form1.arg[0], velocity, currentAcceleration);
                            }

                        }
                    }
                    else
                    {
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        DialogResult result;
                        result = MessageBox.Show("Motor Not Detected. Please connect motor and then retry", "Program will close", buttons);
                        form1.Close();                        
                    }
                }
            
        }

        /// <summary>
        /// Starts Spectrometer
        /// </summary>
        /// /// <param name="Form1">The main form</param>
        internal void SpecStart(Form1 formObject)
        {
            Form1 form1 = formObject;
            bool retcode = Spectrometer.InitDevices();
            if (retcode == false)
            {

                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show("Spectrometer Not Detected. Please connect Spectrometer and then restart", "Program will close", buttons);
                form1.Close();
            }
            byte[] tmp_channel = new byte[32];
            byte[] spec_channel = new byte[32];
            int retcode1 = Spectrometer.bwtekSetupChannel(-1, tmp_channel);
            if (retcode1 > 0)
            {
                int device_count = Spectrometer.GetDeviceCount();
                for (int i = 0; i < 32; i++)
                {
                    if (tmp_channel[i] < 32)
                    {
                        spec_channel[i] = tmp_channel[i];
                    }
                }
            }
        }

        /// <summary>
        /// initialises spectrometer
        /// </summary>
        internal void SpecInit(Form1 formObject)
        {
            Form1 form1 = formObject;
            //Variable initialisation
            int ret;
            int pixelnumber = 2048;
            int timing_mode = 1;
            int input_mode = 2;
            int inttime_base = 6300;


            double[] coefficeint_a = new double[4];
            coefficeint_a[0] = 768.568237181056;
            coefficeint_a[1] = 0.152779981153354;
            coefficeint_a[2] = -4.32708905592971E-06;
            coefficeint_a[3] = -7.58441172603648E-10;
            double[] coefficeint_b = new double[4];
            coefficeint_b[0] = 6076.59925643135;
            coefficeint_b[1] = 11.2680452636616;
            coefficeint_b[2] = -0.00700388399164194;
            coefficeint_b[3] = 3.42144657631367E-06;

            ret = Spectrometer.bwtekTestUSB(timing_mode, pixelnumber, input_mode, 0, 0);    //tests USB connection
            int inttime_set = Convert.ToInt32(form1.inttime - inttime_base);
            ret = Spectrometer.bwtekSetTimeUSB(inttime_set, 0);                             //Sets correct intergration time for spectrometer

            if (ret < 0)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show("Intergration time failed to set check USB connections and power supply for instabilities", "Program will close", buttons);
                form1.Close();
            }
        }

        /// <summary>
        /// Sets default temperature values for Spectrometer cooling system
        /// </summary>
        /// <param name="Form1">The main form</param>  
        internal void SpecTemp(Form1 formObject)
        {
            int CCDTempValue = -5;          //set cooling values
            int ExternalTempValue = 25;     //set cooling values
            //Setting CCD Temp
            if ((CCDTempValue >= -40) && (CCDTempValue <= 10))      //checks values are within allowable limits
            {
                SetCoolerTemp(0, CCDTempValue, formObject);
            }

            //Setting external temp
            if ((ExternalTempValue >= -40) && (ExternalTempValue <= 10))    //checks values are within allowable limits
            {
                SetCoolerTemp(1, ExternalTempValue, formObject);
            }

        }

        /// <summary>
        /// Sets the cooler temperature of the component specified to the input temperature
        /// </summary>
        /// <param name="DAChannel">Specification of channel, 0 for CCD, 1 for external</param>
        /// <param name="SetTemp">Temperature value to set</param>
        internal void SetCoolerTemp(int DAChannel, int SetTemp, Form1 formObject)
        {
            Form1 form1 = formObject;
            //variable initialisation

            double[] coef = new double[4];
            double Rt_R25;
            double T = SetTemp + 273.15;
            double Rt, R25, Vset;
            int DA_Set;
            if (SetTemp < 0)    //determines coefficient operations to peform depending on positive or negative values
            {
                coef[0] = -1.6165371 * Math.Pow(10, 1);
                coef[1] = 5.9362293 * Math.Pow(10, 3);
                coef[2] = -4.0817384 * Math.Pow(10, 5);
                coef[3] = 2.2340382 * Math.Pow(10, 7);
            }
            else
            {
                coef[0] = -1.5702076 * Math.Pow(10, 1);
                coef[1] = 5.7388897 * Math.Pow(10, 3);
                coef[2] = -4.0470744 * Math.Pow(10, 5);
                coef[3] = 2.6675244 * Math.Pow(10, 7);
            }
            //calculations to produce DA_Set using thermodynamics
            Rt_R25 = coef[0] + coef[1] / T + coef[2] / Math.Pow(T, 2) + coef[3] / Math.Pow(T, 3);
            Rt_R25 = Math.Exp(Rt_R25);
            R25 = 10000; //R25=10K
            Rt = Rt_R25 * R25;
            Vset = (Rt / (Rt + R25) * 1.5);
            DA_Set = (int)(Vset / 2.28 * 4096);
            int ret = Spectrometer.bwtekSetAnalogOut(DAChannel, DA_Set, 0);

            if (ret < 0)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show("Spectrometer cooling failed to inialise, check USB connections and power supply for instabilities", "Program will close", buttons);
                form1.Close();

            }
        }

        /// <summary>
        /// Starts the laser
        /// </summary>
        /// <param name="formObject">takes main form as input</param>
        internal void LasStart(Form1 formObject)
        {
            Form1 form1 = formObject;
            //variable initialisation
            int ret;
            bool bRet;
            int LazUSB_enable;
            int laserChannel;

            bRet = Laser.InitDevices();     //initialises laser
            if (bRet == false)
            {

                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show("Probe Not Detected. Please check probe connection", "Program will close", buttons);
                form1.Close();
            }
            ret = Laser.GetLaserChannel();  //Gets channel of laser
            if (ret >= 0)                   //sets defaults values if function fails
            {
                LazUSB_enable = 1;
                laserChannel = ret;
            }
            else
            {
                LazUSB_enable = 0;
                laserChannel = 0; //laserChannel equal the spectrometer channel, default is 0
            }
            int[] spec = new int[] { LazUSB_enable, laserChannel };    //saves values to secifications variable
            Array.Copy(spec, 0, form1.spfc, 0, 2);

        }

        /// <summary>
        /// initialises the laser Channel
        /// </summary>
        /// <param name="formObject"></param>
        internal void LaserInt(Form1 formObject)
        {
            Form1 form1 = formObject;
            //variable initialisation
            int ret, reta;
            Int32 laserPowerMin = 0;
            Int32 laserPowerMax = 0;
            Int32 laserIO = 0;
            Int32 laserPower = 0;
            int laserChannel = form1.spfc[1];

            ret = Laser.bwtekInitializeLC(0, laserChannel); //laser initialisation            
            reta = Laser.bwtekReadPowerLC(ref laserPowerMin, ref laserPowerMax, ref laserPower, ref laserIO, laserChannel);  //reading reference valuses for each variable from the laser sustem - at the given channel
            if (ret < 0 || reta < 0)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show("Laser failed to initialise. Please check probe connection", "Program will close", buttons);
                form1.Close();

            }
                                                                       //sets laser power variable to zero for initialisation
            controls.LaserSet(form1.laserOff, laserPowerMin, laserPowerMax, laserChannel, formObject);         //sets laser power to zero for initialisation
            int[] spec = new int[] { laserPowerMin, laserPowerMax, laserPower, laserIO };        //saves laser specification variables to files
            Array.Copy(spec, 0, form1.spfc, 2, 4);
        }
    }
}
