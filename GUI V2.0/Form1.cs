using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_V2._0
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Instance creation of sub-classes
        /// </summary>
        Initialise initialise = new Initialise();
        Processing processing = new Processing();
        Controls controls = new Controls();
        SDown sDown = new SDown();

        /// <summary>
        /// initialisation of public parameters
        /// </summary>
        public string[] arg = { "27002103", "0", "100" };       //create argument variable for stagestart
        public double inttime = 10 * 1000 * 1000;               //integration time (first number is seconds) e.g. 10s *1000*1000
        public int[] spfc = new int[6];                         //General parameters for laser use: 0:default number, 1: laser Channel,2:laserPowerMin,3:LaserPowerMax,4:LaserPower,5:LaserIO
        public float space = 3;                                 //spacing between each spectrum point
        public int laserOn=100; public int laserOff=0;          //Setting values for laser on and off
        public double[] zPositions;                             //initialising public variable for array of positions for initial optimisation.
        public int motorFactor = 34304;
        public int stageMax = 25;
        public int stageMin = 0;
        public bool reset = false;
        
        /// <summary>
        /// Initialisation of forms & initial functions to set equilibrium position and setup initial equilibrium finding position array
        /// </summary>
        public Form1()
        {
            //Initialising form
            InitializeComponent();
            buttonInitLaser.Enabled = false;
            buttonFindEQ.Enabled = false;
            buttonSaveEQ.Enabled = false;
            buttonLaserOn.Enabled = false;
            buttonLaserOff.Enabled = false;
            buttonDisconnect.Enabled = false;

            //Preliminary functions to get initial variable for the equilibrium position and the resulting array of positions for the first spectra
            processing.SetEq(this);
            zPositions = processing.stagePosArray(this);            
        }

        private void buttonInitAll_Click(object sender, EventArgs e)
        {
            buttonInitStage_Click(null, EventArgs.Empty);
            buttonInitSpec_Click(null, EventArgs.Empty);
            buttonInitLaser_Click(null, EventArgs.Empty);
        }

        private void buttonInitStage_Click(object sender, EventArgs e)
        {
            initialise.stagestart(this);
            progressBarStageInit.Value = 100;
            buttonInitStage.Enabled = false;
            buttonInitAll.Enabled = false;
            buttonDoAll.Enabled = false;
            buttonDisconnect.Enabled = true;
            Application.DoEvents();
        }

        private void buttonInitSpec_Click(object sender, EventArgs e)
        {
            initialise.SpecStart(this);                            
            progressInitSpec.Value = 33;
            initialise.SpecInit(this);                             
            progressInitSpec.Value = 67;
            initialise.SpecTemp(this);                             
            progressInitSpec.Value = 100;
            buttonInitSpec.Enabled = false;
            if (reset==true)
            {
                buttonFindEQ.Enabled = true;
                buttonLaserOn.Enabled = true;
                buttonLaserOff.Enabled = true;
            }
            else
            {
                buttonInitLaser.Enabled = true;
                buttonDoAll.Enabled = false;
            }
            buttonDisconnect.Enabled = true;
        }

        private void buttonInitLaser_Click(object sender, EventArgs e)
        {
            initialise.LasStart(this);
            progressInitLaser.Value = 40;
            initialise.LaserInt(this);
            progressInitLaser.Value = 100;
            buttonInitLaser.Enabled = false;
            buttonFindEQ.Enabled = true;
            buttonLaserOn.Enabled = true;
            buttonLaserOff.Enabled = true;
        }

        private void buttonFindEQ_Click(object sender, EventArgs e)
        {
            buttonLaserOn.Enabled = false;
            buttonLaserOff.Enabled = false;
            processing.stageiteration(this, zPositions);
            double [] Data = processing.specRead();            
            processing.FocusFound(this, zPositions, Data);
            buttonFindEQ.Enabled = false;
            buttonSaveEQ.Enabled = true;
            buttonLaserOn.Enabled = true;
            buttonLaserOff.Enabled = true;
        }

        private void buttonSaveEQ_Click(object sender, EventArgs e)
        {
            processing.SaveEq(this);
            textBox_EQStatus.Text = String.Format("Equilibrium saved");
            buttonSaveEQ.Enabled = false;
        }

        private void buttonDoAll_Click(object sender, EventArgs e)
        {
            buttonInitAll_Click(null, EventArgs.Empty);
            buttonFindEQ_Click(null, EventArgs.Empty);
            buttonSaveEQ_Click(null, EventArgs.Empty);
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            sDown.specclose();
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;
            result = MessageBox.Show("Disconnect Complete","Disconnect Complete", buttons);

            buttonInitSpec.Enabled = true;
            buttonInitLaser.Enabled = false;
            buttonInitStage.Enabled = false;
            buttonInitAll.Enabled = false;
            buttonDoAll.Enabled = false;
            buttonFindEQ.Enabled = false;
            buttonSaveEQ.Enabled = false;

            progressBarEQ.Value = 0;
            progressInitSpec.Value = 0;
            buttonLaserOn.Enabled = false;
            buttonLaserOff.Enabled = false;

            
            Textbox_EQCALC.Text = "Not Calculated";
            textBox_EQStatus.Text = "EQ Find not started";
            textBox_EQMeas.Text = "0";            

            processing.SetEq(this);
            zPositions = processing.stagePosArray(this);
            reset = true;
        }

        private void buttonLaserOn_Click(object sender, EventArgs e)
        {
            controls.LaserSet(laserOn, spfc[2], spfc[3], spfc[1], this);
        }

        private void buttonLaserOff_Click(object sender, EventArgs e)
        {
            controls.LaserSet(laserOff, spfc[2], spfc[3], spfc[1], this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            sDown.ShutDown(this);
            Application.DoEvents();
        }


    }
}

