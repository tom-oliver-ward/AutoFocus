using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using MathNet.Numerics;
using System.Linq;

namespace GUI_V2._0
{
    /// <summary>
    /// Class for any data processing / intermediate steps
    /// </summary>
    class Processing
    {
        //Initialises an instance of controls
        Controls controls = new Controls();
        //initialisation of public variables
        public bool focusFound = false; // reference to whether focal point is found
        public double FocalPoint;       //variable for the calculated value of the focal point

        /// <summary>
        /// Creates an array of the three stage positions to test
        /// </summary>
        /// <param name="formObject"></param>
        /// <returns></returns>
        internal double[] stagePosArray(Form1 formObject)
        {
            Form1 form1 = formObject;
            double[] positions = new double[3]; //initialises array for positions            
            double equil = (double.Parse(form1.arg[1])); //extracting equilibrium postion and converting for motor counts,           

            //setting position variable array
            if (equil > form1.space * form1.motorFactor && equil < (form1.stageMax-form1.space) * form1.motorFactor)            //checking there are no min or max errors
            {
                //if not place the centre as the equilibrium and the others a space either side
                double[] posspec = { equil - form1.space * form1.motorFactor, equil, (equil + form1.space * form1.motorFactor) };
                positions = posspec;
            }
            else if (equil <= form1.space * form1.motorFactor)                     //Checking for a zero error due to equilibrium position being too low
            {
                //if min error then set to 0, space, space*2
                double[] posspec = { 0, form1.space * form1.motorFactor, form1.space * form1.motorFactor*2 };
                positions = posspec;
            }
            else if (equil >= (form1.stageMax-form1.space) * form1.motorFactor)                    //Checking for a maximum error due to equilibrium position being too high
            {
                //if max see min but subtracted from 25
                double[] posspec = { (form1.stageMax - form1.space*2) * form1.motorFactor, (form1.stageMax - form1.space) * form1.motorFactor, form1.stageMax * form1.motorFactor };
                positions = posspec;
            }
            else
            {
                //this shouldn't be possible - however if it occurs the equilibrium point is probably something unallowable
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show("Program Calculation Error for EQ point", "Program will close", buttons);
                form1.Close();
            }            
            return positions;   //return the calculated positions array
        }

        /// <summary>
        /// Takes the positions in to take a spctra at each specified position
        /// </summary>
        /// <param name="formObject"></param>
        /// <param name="zPosition"> array of positions for spectra.</param>
        internal void stageiteration(Form1 formObject, double[] zPosition)
        {
            Form1 form1 = formObject;

            for (int i = 0; i < 3; i++)                                                 //take spectrum at each of the 3 specified points
            {
                
                controls.StageMove(Convert.ToInt32(zPosition[i]), form1.arg[0],form1);     //moves stage to position
                form1.textBox_EQStatus.Text = String.Format("Spec {0} of 3", i + 1);            //updates textbox to say which spectr is being taken
                form1.progressBarEQ.Value = (i + 1) * 100 / 3;                                  //jumps progress bar by a third
                Application.DoEvents();

                controls.takeSpectrum(i,form1);                                                 //takes the spectrum
            }   
        }
        
        /// <summary>
        /// Opens up each spectra and processes it to return the average intensity values at the selected range of wavelengths.
        /// </summary>
        /// <returns></returns>
        internal double[] specRead(Form1 form1)
        {
            double[] Data = new double[3]; 
            for (int i = 0; i < 3; i++)                       //for each of three spectra
            {
                string Filename = String.Concat(AppDomain.CurrentDomain.BaseDirectory, "Spectra", i);   //Spectra filename
                double output = ReadFile(Filename, form1);             //Reads and extracts important data from spectrum - extracting average values
                Data[i] = output;                               //Puts each point into a data array
            }
            return Data;                                        //return the average intensity values at the selected range of wavelengths.
        }

        /// <summary>
        /// Reads the input spectra file and finds the range of data used for intensity measurements
        /// </summary>
        /// <param name="Filename"> Spectra name</param>
        /// <returns>average intensity</returns>
        private double ReadFile(string Filename, Form1 form1)
        {
            String input = File.ReadAllText(Filename);

            //initialises variables
            int pos;
            int intensSum = 0;
            int stop;

            pos = input.IndexOf(form1.wavelengthStart);               //finds the point where the 1750 data point is
            for (int i = 0; i < form1.wavelengthLength; i++)            //for 21 data points
            {
                pos = input.IndexOf(";", pos) + 1;   //finds position of start of intensity value
                stop = input.IndexOf("\n", pos);       //finds end of intensity value
                stop = stop - pos;                  //finds length of intensity value
                string intensstring = input.Substring(pos, stop);          //extracts the intensity value at pos into a small string
                int intensNum = 0;                                      //initialises the variable for the integer version of intensity
                Int32.TryParse(intensstring, out intensNum);            //converts string to number
                intensSum = intensNum + intensNum;                      //keeps a running tally of intensities over this region through the loop
            }

            double intenSave = intensSum / 21;                             //calculates the average value of intensities 
            return intenSave;        
        }
        
        /// <summary>
        /// Checks whether the focus is found - if not takes an additional data point to use and itterates the process
        /// </summary>
        /// <param name="formObject"></param>
        /// <param name="zPositions"> height / position array</param>
        /// <param name="Data"> average intensity array</param>
        internal void FocusFound(Form1 formObject, double[] zPositions, double[] Data)
        {
            focusFound = false;
            Form1 form1 = formObject;    
            int j = 3;                                          //count variable
            FocalPoint = FitPolynomial(zPositions, Data);       //Find the maxima

            //Loop until the actual focus has been found
            while(focusFound==false)
            {
                //Test whether the focal point is within the range of those where the sectra is taken, otherwise it is likely to be a false maxima/minima. Also checks the middle intensity is the highest for the same reason
                if (FocalPoint < zPositions.Max() && FocalPoint > zPositions.Min() && Data[1] > Data[0] && Data[1] > Data[2])
                {
                    controls.StageMove(Convert.ToInt32(FocalPoint), form1.arg[0], form1);      //moves stage to equilibrium point
                    focusFound = true;                                                              //Confirms that the focus is found, so loop can end
                    form1.textBox_EQStatus.Text = "Found";
                    //Updates textboxes for user feedback
                    form1.Textbox_EQCALC.Text = (FocalPoint / form1.motorFactor).ToString();        
                    form1.TestBox_StagePosition.Text = (FocalPoint / form1.motorFactor).ToString();
                    Application.DoEvents();
                }
                //Test if equilibrium point is below lowest point so far - ie lowest data point is most intense
                else if (Data[0] > Data[2] && zPositions[0] >0)
                {                    
                    bool up = false;    //most intense point is below hence pass up as false through
                    FocalPoint=SpectraIteration(zPositions,Data, form1, FocalPoint,j, up);                    
                }
                //Test if equilibrium point is below highest point so far - ie lowest data point is most intense
                else if (Data[0] < Data[2] && zPositions[2]< form1.stageMax * form1.motorFactor)
                {
                    bool up = true;     //most intense point is above hence pass up as true through
                    FocalPoint = SpectraIteration(zPositions, Data, form1, FocalPoint, j, up);                    
                }
                else
                {
                    //shouldn't happen but.....
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show("AutoFocus failed - measurement error", "Equilibrium Find Failed", buttons);
                    focusFound = true;
                }
                j++;        // increase increment in case additional itteration is required
            }
        }

        /// <summary>
        /// Takes additional spectra then rearranges array of three points to include newest data point and the two (positionally) closest
        /// </summary>
        /// <param name="zPositions">height</param>
        /// <param name="Data">average intensities</param>
        /// <param name="formObject"></param>
        /// <param name="FocalPoint">Previous calculated focal point / variable</param>
        /// <param name="j">count for saving & opening appropriate files to extract data</param>
        /// <param name="up">position for next spectra variable</param>
        /// <returns>new Focus Point</returns>
        private double SpectraIteration(double[] zPositions,double[] Data, Form1 formObject, double FocalPoint, int j, bool up)
        {
            Form1 form1 = formObject;
            //updating user as to status
            form1.textBox_EQStatus.Text = "Failed, taking new spectra below";
            double newPos = NewPosCalc(zPositions, form1, up);   // finding the newposition for the stage
            //checks the NewPosCalc worked
            if (newPos >= 0)
            {
                controls.StageMove(Convert.ToInt32(newPos), form1.arg[0], form1);  //moves stage to newPos
                form1.textBox_EQMeas.Text = Convert.ToString(j + 1);                    //updates user
                Application.DoEvents(); 
                controls.takeSpectrum(j, form1);
                //Chooses where in array newPos should be added, then rearranged both arrays to incorporate the new data - with the new average intensity being read through the ReadFile function
                if (up==false)
                {
                    zPositions[2] = zPositions[1]; zPositions[1] = zPositions[0]; zPositions[0] = newPos;
                    Data[2] = Data[1]; Data[1] = Data[0]; Data[0] = ReadFile(String.Concat(AppDomain.CurrentDomain.BaseDirectory, "Spectra", j),form1);
                }
                else
                {
                    zPositions[0] = zPositions[1]; zPositions[1] = zPositions[2]; zPositions[2] = newPos;
                    Data[0] = Data[1]; Data[1] = Data[2]; Data[2] = ReadFile(String.Concat(AppDomain.CurrentDomain.BaseDirectory, "Spectra", j),form1);
                }                
                FocalPoint = FitPolynomial(zPositions, Data);              //Fits Polynomial to new stage position (x) and intensity (y) arrays
            }
            else
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show("Equilibrium Point is less than minimum point of stage", "Equilibrium Find Failed", buttons);
                focusFound = true;
            }
            return FocalPoint;
        }

        /// <summary>
        /// Takes the position and intensity average arrays and fits a polynomial
        /// </summary>
        /// <param name="z">z position / height</param>
        /// <param name="Data">average intensity</param>
        /// <returns>returns maximum point</returns> 
        internal double FitPolynomial(double[] z, double[] Data)
        {
            //runs Math numerics fit polynomial finction
            double[] p = Fit.Polynomial(z, Data, 2);
            //performs operations to find maxima
            double a = p[1];
            double b = p[2];
            b = b * 2;
            double max = -a / b;
            return max;
        }

        /// <summary>
        /// Calculates the additional position
        /// </summary>
        /// <param name="zPositions">z positions array</param>
        /// <param name="formObject"></param>
        /// <param name="up"></param>
        /// <returns></returns>
         private double NewPosCalc(double [] zPositions, Form1 formObject, bool up)
        {
            Form1 form1 = formObject;
            double newpos;
            //if it is below previous points
            if (up == false)
            {
                double z = zPositions[0];
                //checks for zero error
                if (z > form1.space * form1.motorFactor)
                {
                    newpos = z - (form1.space * form1.motorFactor); //assigns new position by subtracting the spacing from the existing value
                }
                else if (z >= 0 )
                {
                    newpos = 0;                                     // if zero error assigns minimum value
                }
                else { newpos = -1; }
            }
            //else if above previous points
            else if(up==true)
            {
                double z = zPositions[2];                       
                //checks for maximum error
                if (z < ((form1.stageMax - form1.space) * form1.motorFactor))
                {
                    newpos = z + form1.space * form1.motorFactor;                   //assigns new position by adding the spacing from the existing value
                }
                else if (z <= form1.stageMax * form1.motorFactor)
                {
                    newpos = form1.stageMax * form1.motorFactor;            //// if max error assigns maximum value
                }
                else { newpos = -1; }

            }
            else { newpos = -1; }
            return newpos;
        }

        /// <summary>
        /// Saves the Equilibrium value for next time
        /// </summary>
        /// <param name="formObject"></param>
        internal void SaveEq(Form1 formObject)
        {
            Form1 form1 = formObject;
            string Path = AppDomain.CurrentDomain.BaseDirectory;    //Finds Path directory
            Path = String.Concat(Path, "EQ");                       //Creates a full path name
            TextWriter tw = new StreamWriter(Path);                 //creates a text writer
            string FocusS = (FocalPoint / form1.motorFactor).ToString();                       //Converts the focus value to a string
            tw.WriteLine(FocusS);                                   //writes the string focus value to the file
            tw.Close();   
        }

        /// <summary>
        /// Reads the EQ file saved and sets the equilibrium value in the program
        /// </summary>
        /// <param name="formObject"></param>
        internal void SetEq(Form1 formObject)
        {
            Form1 form1 = formObject;

            string Path = String.Concat(AppDomain.CurrentDomain.BaseDirectory, "EQ");                   //Adds EQ, the file name of the equilibrium position to the path                                                       
            try
            {
                TextReader tr = new StreamReader(Path);                         //initialises the text reader
                string line = tr.ReadLine();
                int targetC = (Convert.ToInt32(Convert.ToDouble(line) * form1.motorFactor));    //Reads the line and converts to an integer for multiplication in a two step process
                string targetS = targetC.ToString();                                    //converts back to a string for compatibility with stage ffunctions
                form1.arg[1] = targetS;
                tr.Close();                                                     //closes the text reader stream
                if (line.Length>5)
                {
                    form1.textBox_EQ.Text = line.Substring(0, 5);
                }
                else
                {
                    form1.textBox_EQ.Text = line;
                }
            }
            catch
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show("Equilibrium File not Found", "Equilibrium", buttons);
            }            
        }
    }
}
