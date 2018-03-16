using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_V2._0
{
    /// <summary>
    /// class to run a seperate thread to take a spectrum
    /// </summary>
    public class SpecThread
    {
        public ushort[] DataArray = new ushort[2048];

        /// <summary>
        /// Runs the spectra
        /// </summary>
        /// <param name="i"></param>
        public static void Thread1(int i)
        {
            //ushort[] DataArray = new ushort[2048];
            SpecThread objThread1 = new SpecThread();
            SpecTake(i);

        }

        /// <summary>
        /// Takes Spectrum
        /// </summary>
        /// <param name="i">Spectrum number</param>
        private static void SpecTake(int i)
        {

            //variable initialisation
            int ret = 0;

            SpecThread objThread1 = new SpecThread();
            ret = Spectrometer.bwtekDSPDataReadUSB(1, 0, 0, 0, objThread1.DataArray, 0);   //read data from spectrometer                                   
            if (ret < 0)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show("Spectrum take failed, check USB connections and power supply for instabilities", "Program will close", buttons);
                //this.Close();
            }
            SpecSave(objThread1.DataArray, i);                                             //Saves Spectrum from data
        }

        /// <summary>
        /// Save Spectrum from Data
        /// </summary>
        /// <param name="DataArray">Data aquired from spectrometer</param>
        /// <param name="i"> number position in array </param>
        static void SpecSave(ushort[] DataArray, int i)
        {
            string Path = AppDomain.CurrentDomain.BaseDirectory; //string path of the exe file and hence folder to save
            Path = String.Concat(Path, "Spectra", i);            //Add Spectra and number to the path (file name)

            using (StreamWriter sw = File.CreateText(Path))       //Initialise streamwriter
            {
                sw.WriteLine("Data");                               //add data heading
            }
            string num, data;                                       //variable initialisation
            for (int j = 0; j < DataArray.Length; j++)              //for each point in the data array
            {
                num = DataArray[j].ToString();                      //take the data and convert to string
                using (StreamWriter sw = File.AppendText(Path))
                {
                    data = string.Concat(j.ToString(), ";", num, "\n");//write string with j;num\n, e.g. 1;312\n
                    sw.WriteLine(data);                             //write data into code
                }


            }
        }
    }     
        

}
