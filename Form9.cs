﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;





namespace Test1
{

    public partial class Form9 : Form
    {
        double current;
        double currentstart;
        double voltage;
        double power;
        double pf, eff;
        string phase;
        string loadtype;
        double hp;
        double pfstart;
        double multiplier;
        double sccurrent;
        double cplxpower;
        string materialname;
        double initialTemp, finalTemp;
        double diameter;
        double temperature;

        double vdrunmax, vdstartmax, vdrun, vdstart;
        double length;
        double maxtemp;


        int n = 1;
        string volSys, powerUnit;
        string tagno, from, to, fromdesc, todesc;
        string material, armour, innersheath, outersheath;
        string breakertype;
        string remarks;
        string ratedvoltage;
        string groupconductor;
        public static string insulation, installation;
        string lengthunit;

        int cores;
        double breakcurrent;

        public static double k1main, k2main, k3main, ktmain;
        public static bool ok_clicked, okSetClicked;

        bool complete, inputValid;
        int i = 0;
        int insulindex = 0;
        int tempindex = 0;

        string readtemp;
        double Rac;
        double X;
        double Rdc;
        double Irated;
        double wirearea;
        double iderated;

        double tbreaker;
        double bLTE;
        double cLTE;
        double k;
        double sc_wiremin;
        double lmax;
        double smin;
        double cablesizemin;
        bool calculated = false;

        int m;

        string voltageLv;

        public static int j = -1;
        Form5 f5 = new Form5();
        Form6 f6 = new Form6();
        FSettings fSettings = new FSettings();

        public static string[] results = new string[38];

        public static char decimalseparator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
        public static bool decimalSeparatorChanged = false;

        public static double[,] inputCableData;
        public static int cableCount;

        public static DataTable dtdiameter = new DataTable();


        public Form9()
        {

            InitializeComponent();

        }

        double[,] xlpe2core = new double[17, 6]
        {
            { 1.5, 15.4300, 0.0999, 15.4287, 26, 26 },
            { 2.5, 9.4500, 0.0961, 9.4485, 34, 36 },
            { 4, 5.8800, 0.0899, 5.8782, 44, 49},
            { 6, 3.9300, 0.0853, 3.9273, 56, 63 },
            { 10, 2.3400, 0.0802, 2.3334, 73, 86 },
            { 16, 1.4700, 0.0781, 1.4664, 95, 115 },
            { 25, 0.9270, 0.0779, 0.9270, 121, 149},
            { 35, 0.6690, 0.0753, 0.6682, 146, 185 },
            { 50, 0.4940, 0.0748, 0.4935, 173, 225 },
            { 70, 0.3430, 0.0734, 0.3417, 213, 289 },
            { 95, 0.2470, 0.0711, 0.2461, 252, 352 },
            { 120, 0.1970, 0.0708, 0.1951, 287, 410 },
            { 150, 0.1600, 0.0712, 0.1581, 324, 473 },
            { 185, 0.1281, 0.0720, 0.1264, 363, 542 },
            { 240, 0.0984, 0.0711, 0.0961, 419, 641 },
            { 300, 0.0799, 0.0704, 0.0766, 474, 741 },
            { 400, 0.0633, 0.0701, 0.0599, 555, 892 }
        };

        double[,] xlpe3core = new double[17, 6]
        {
            {1.5, 15.4300, 0.0999, 15.4281, 22, 23 },
            {2.5,    9.4500,  0.0961,  9.4481,  29,  32 },
            {4,  5.8800,  0.0899,  5.8780,  37,  42 },
            {6,  3.9300,  0.0853,  3.9272,  46, 54 },
            {10, 2.3400,  0.0802,  2.3333,  61,  75 },
            {16, 1.4700,  0.0781,  1.4663,  79,  100 },
            {25, 0.9270,  0.0779,  0.9270,  101, 127 },
            {35, 0.6690,  0.0753,  0.6681,  122, 158 },
            {50, 0.4940,  0.0748,  0.4934,  144, 192 },
            {70, 0.3430,  0.0734,  0.3417,  178, 246 },
            {95, 0.2470,  0.0711,  0.2461,  211, 298 },
            {120, 0.1970,  0.0708,  0.1951,  240, 346 },
            {150, 0.1600,  0.0712,  0.1581,  271, 399 },
            {185, 0.1281,  0.0720,  0.1264,  304, 456 },
            {240, 0.0984,  0.0711,  0.0961,  351, 538 },
            {300, 0.0799,  0.0704,  0.0766,  396, 621 },
            {400, 0.0633,  0.0701,  0.0599,  464, 745 }
        };

        double[,] xlpe4core = new double[17, 6]
        {
            { 1.5, 15.4300, 0.1125,  15.4287, 22,  23 },
            { 2.5, 9.4500,  0.1086,  9.4485,  29,  32 },
            { 4,  5.8800,  0.1025,  5.8782,  37, 42 },
            { 6, 3.93, 0.098, 3.927308, 46, 54 },
            { 10, 2.34, 0.093, 2.333433, 61, 75 },
            { 16, 1.47, 0.091, 1.466365, 79, 100 },
            { 25, 0.927, 0.0908, 0.9269977, 101, 127 },
            { 35, 0.669, 0.0883, 0.6681524, 122, 158 },
            { 50, 0.494, 0.0877, 0.4934637, 144, 192 },
            { 70, 0.343, 0.0864, 0.3417268, 178, 246 },
            { 95, 0.247, 0.0842, 0.2460943, 211, 298 },
            { 120, 0.197, 0.0839, 0.1950903, 240, 346 },
            { 150, 0.16, 0.0843, 0.1581124, 271, 399 },
            { 185, 0.1281, 0.0851, 0.12636241, 304, 456 },
            { 240, 0.0984, 0.0842, 0.09614254, 351, 538 },
            { 300, 0.0799, 0.0835, 0.07663351, 396, 621 },
            { 400, 0.0633, 0.0832, 0.0599297, 464, 745 }
        };

        double[,] pvc2core = new double[16, 6]
        {
            { 1.5, 14.4777, 0.1075, 14.47765, 22, 22 },
            { 2.5, 8.8661, 0.0994, 8.866065, 29, 30 },
            { 4, 5.5159, 0.0983, 5.515865, 38, 40 },
            { 6, 3.6853, 0.0928, 3.68522, 47, 51 },
            { 10, 2.1897, 0.0865, 2.189595, 63, 70 },
            { 16, 1.3761, 0.0818, 1.375975, 81, 94 },
            { 25, 0.8701, 0.0807, 0.8698555, 104, 119 },
            { 35, 0.6273, 0.0778, 0.626966, 125, 148 },
            { 50, 0.4635, 0.0822, 0.4630455, 148, 180 },
            { 70, 0.3213, 0.0789, 0.320662, 183, 232 },
            { 95, 0.2318, 0.0791, 0.2309245, 216, 282 },
            { 120, 0.1842, 0.0772, 0.1830645, 246, 328 },
            { 150, 0.15, 0.0775, 0.148366, 278, 379 },
            { 185, 0.1204, 0.0769, 0.11857315, 312, 434 },
            { 240, 0.0926, 0.0759, 0.0902161, 361, 514 },
            { 300, 0.0749, 0.0756, 0.07190965, 408, 593 }
        };

        double[,] pvc3core = new double[16, 6]
        {
            { 1.5, 14.4777, 0.1075, 14.47721021611, 18, 18.5 },
            { 2.5, 8.8661, 0.0994, 8.86579567779961, 24, 25 },
            { 4, 5.5159, 0.0983, 5.5156974459725, 31, 34 },
            { 6, 3.6853, 0.0928, 3.68510805500982, 39, 43 },
            { 10, 2.1897, 0.0865, 2.18952848722986, 52, 60 },
            { 16, 1.3761, 0.0818, 1.37593320235756, 67, 80 },
            { 25, 0.8701, 0.0807, 0.869829076620825, 86, 101 },
            { 35, 0.6273, 0.0778, 0.62694695481336, 103, 126 },
            { 50, 0.4635, 0.0822, 0.463031434184676, 122, 153 },
            { 70, 0.3213, 0.0789, 0.320652259332024, 151, 196 },
            { 95, 0.2318, 0.0791, 0.230917485265226, 179, 238 },
            { 120, 0.1842, 0.0772, 0.183058939096267, 203, 276 },
            { 150, 0.15, 0.0775, 0.148361493123772, 230, 319 },
            { 185, 0.1204, 0.0769, 0.118569548133595, 258, 364 },
            { 240, 0.0926, 0.0759, 0.0902133595284872, 297, 430 },
            { 300, 0.0749, 0.0756, 0.0719074656188605, 336, 497 }
        };

        double[,] pvc4core = new double[16, 6]
        {
            { 1.5, 14.4777, 0.1199, 14.47765, 18, 18.5 },
            { 2.5, 8.8661, 0.112, 8.866065, 24, 25 },
            { 4, 5.5159, 0.1109, 5.515865, 31, 34 },
            { 6, 3.6853, 0.1054, 3.68522, 39, 43 },
            { 10, 2.1897, 0.0993, 2.189595, 52, 60 },
            { 16, 1.3761, 0.0946, 1.375975, 67, 80 },
            { 25, 0.8701, 0.0936, 0.8698555, 86, 101 },
            { 35, 0.6273, 0.0907, 0.626966, 103, 126 },
            { 50, 0.4635, 0.095, 0.4630455, 122, 153 },
            { 70, 0.3213, 0.0917, 0.320662, 151, 196 },
            { 95, 0.2318, 0.092, 0.2309245, 179, 238 },
            { 120, 0.1842, 0.0901, 0.1830645, 203, 276 },
            { 150, 0.15, 0.0904, 0.148366, 230, 319 },
            { 185, 0.1204, 0.0898, 0.11857315, 258, 364 },
            { 240, 0.0926, 0.0889, 0.0902161, 297, 430 },
            { 300, 0.0749, 0.0886, 0.07190965, 336, 497 }
        };

        double[,] correctionfactor_temperature = new double[10, 6]
        {
            { 1.08, 1.05, 1.04, 1.08, 1.05, 1.04 },
            { 1, 1, 1, 1, 1, 1 },
            { 0.91, 0.94, 0.96, 0.91, 0.94, 0.96 },
            { 0.82, 0.88, 0.91, 0.82, 0.88, 0.91 },
            { 0.71, 0.82, 0.87, 0.71, 0.82, 0.87 },
            { 0.58, 0.75, 0.82, 0.58, 0.75, 0.82 },
            { 0.41, 0.67, 0.76, 0.41, 0.67, 0.76 },
            { 0, 0.58, 0.71, 0, 0.58, 0.71 },
            { 0, 0.33, 0.58, 0, 0.33, 0.58 },
            { 0, 0, 0.41, 0, 0, 0.41 }
        };

        public static double[,] ODxlpe2core = new double[17, 4]
        {
            { 1.5, 10.5, 14, 13 },
            { 2.5, 11.5, 15, 13.5 },
            { 4, 12.5, 16, 15 },
            { 6, 13.5, 17, 16 },
            { 10, 15.5, 20, 18 },
            { 16, 17, 21, 19 },
            { 25, 20, 25, 23 },
            { 35, 22.5, 27.5, 24.5 },
            { 50, 25, 30, 27 },
            { 70, 28.5, 34, 30.5 },
            { 95, 32.5, 39, 35 },
            { 120, 36, 42, 39.5 },
            { 150, 39.5, 46, 43.5 },
            { 185, 44.5, 52, 48.5 },
            { 240, 50, 57.5, 54 },
            { 300, 55.5, 63, 59.5 },
            { 400, 62, 0, 0 }
        };

        public static double[,] ODxlpe3core = new double[17, 4]
        {
            { 1.5, 11, 14.5, 13.5 },
            { 2.5, 12, 15.5, 14 },
            { 4, 13.5, 17, 15.5 },
            { 6, 14.5, 18, 16.5 },
            { 10, 16.5, 20.5, 18.5 },
            { 16, 18, 22, 20 },
            { 25, 21, 26.5, 23.5 },
            { 35, 24, 29, 26 },
            { 50, 26.5, 31.5, 28.5 },
            { 70, 30.5, 37, 33 },
            { 95, 35, 41.5, 37.5 },
            { 120, 38, 45, 42 },
            { 150, 42.5, 50, 46.5 },
            { 185, 47.5, 55, 51.5 },
            { 240, 53.5, 61.5, 58 },
            { 300, 59.5, 67.5, 64 },
            { 400, 67, 0, 0 }
        };

        public static double[,] ODxlpe4core = new double[17, 4]
        {
            { 1.5, 12, 15.5, 14 },
            { 2.5, 13, 16.5, 15 },
            { 4, 14.5, 18, 16.5 },
            { 6, 15.5, 19.5, 17.5 },
            { 10, 18, 22, 20 },
            { 16, 19.5, 23.5, 21.5 },
            { 25, 23.5, 28.5, 25.5 },
            { 35, 26, 31.5, 28.5 },
            { 50, 29.5, 34.5, 31.5 },
            { 70, 34, 40.5, 36 },
            { 95, 38.5, 45, 42.5 },
            { 120, 42.5, 50, 46.5 },
            { 150, 47, 55, 51 },
            { 185, 53, 60.5, 57 },
            { 240, 59.5, 67.5, 64 },
            { 300, 66.5, 74, 70.5 },
            { 400, 0, 0, 0 }
        };

        public static double[,] ODpvc2core = new double[16, 4]
        {
            { 1.5, 11, 14.5, 13 },
            { 2.5, 12, 15.5, 14 },
            { 4, 14, 17.5, 16 },
            { 6, 15, 19, 17 },
            { 10, 17, 21, 19 },
            { 16, 19, 23, 21 },
            { 25, 22, 27.5, 24.5 },
            { 35, 24.5, 30, 27 },
            { 50, 28.5, 34, 31 },
            { 70, 32, 38, 34 },
            { 95, 37, 43.5, 39 },
            { 120, 40.5, 47, 43.5 },
            { 150, 45, 52, 48 },
            { 185, 49.5, 57, 52.5 },
            { 240, 56, 63.5, 58.5 },
            { 300, 62, 70, 64.5 }
        };

        public static double[,] ODpvc3core = new double[16, 4]
        {
            { 1.5, 11.5, 15, 13.5 },
            { 2.5, 12.5, 16, 14.5 },
            { 4, 14.5, 18.5, 16.5 },
            { 6, 15.5, 20, 18 },
            { 10, 18, 22, 20 },
            { 16, 20, 24, 22 },
            { 25, 23.5, 29, 26 },
            { 35, 26, 31.5, 28.5 },
            { 50, 25, 30.5, 27 },
            { 70, 28, 34.5, 30.5 },
            { 95, 33, 39, 36.5 },
            { 120, 35.5, 42, 39.5 },
            { 150, 38.5, 46, 42.5 },
            { 185, 43.5, 51, 47.5 },
            { 240, 49, 57, 53.5 },
            { 300, 54, 62, 58 }
        };

        public static double[,] ODpvc4core = new double[16, 4]
        {
            { 1.5, 12.5, 16, 14.5 },
            { 2.5, 13.5, 17, 15.5 },
            { 4, 16, 20, 18 },
            { 6, 17, 21, 19 },
            { 10, 19.5, 23.5, 21.5 },
            { 16, 22, 27, 24 },
            { 25, 26, 31, 28 },
            { 35, 29, 34, 31 },
            { 50, 28.5, 35, 31 },
            { 70, 32, 38.5, 36 },
            { 95, 37.5, 44.5, 41 },
            { 120, 41, 48.5, 44.5 },
            { 150, 45, 52.5, 49 },
            { 185, 50, 58, 54 },
            { 240, 56, 63.5, 60 },
            { 300, 62.5, 70, 66.5 }
        };



        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void TextBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((TextBox1.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox2.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox3.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox6.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox8.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }
        private void TextBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox9.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox10.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox11.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Input_Enter(object sender, EventArgs e)
        {
            calc_current();
        }

        private void TextBox1_Leave(object sender, EventArgs e)
        {
            calc_current();

        }


        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

            if (TextBox1.Text != "")
            {
                if (cbPower.Text == "HP")
                {
                    hp = double.Parse(TextBox1.Text);
                    power = 0.746 * hp;
                }
                else if (cbPower.Text == "kVA")
                {
                    cplxpower = double.Parse(TextBox1.Text);
                }
                else
                {
                    power = double.Parse(TextBox1.Text);
                }
                panel14.BackColor = Color.Transparent;
            }
            else if ((TextBox1.Text == "") && (!radioButton8.Checked))
            {
                power = 0;
                panel14.BackColor = Color.Red;
            }
            else
            {
                power = 0;
                panel14.BackColor = Color.Transparent;
            }

            calc_current();
            enable_vd_btn();
            enable_result_btn();
        }


        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((comboBox3.Text == "DC") && (comboBox2.Text == "Feeder"))
            {
                pf = 1;
                textBox4.Text = pf.ToString("R");
                textBox4.ReadOnly = true;
            }
            else
            {
                textBox4.ReadOnly = false;
            }

            if (comboBox3.Text != "")
            {
                phase = comboBox3.Text;
                panel17.BackColor = Color.Transparent;
            }
            else
            {
                panel17.BackColor = Color.Red;
            }


            comboBox2.Items.Clear();
            comboBox5.Items.Clear();
            if (comboBox3.Text == "Three-Phase AC")
            {
                comboBox5.Items.Insert(0, 3);
                comboBox5.Items.Insert(1, 4);

                comboBox2.Items.Insert(0, "Feeder");
                comboBox2.Items.Insert(1, "Motor");
            }
            else if (comboBox3.Text == "Single-Phase AC")
            {
                comboBox5.Items.Insert(0, 2);

                comboBox2.Items.Insert(0, "Feeder");
                comboBox2.Items.Insert(1, "Motor");
            }
            else if (comboBox3.Text == "DC")
            {
                comboBox5.Items.Insert(0, 2);

                comboBox2.Items.Insert(0, "Feeder");
            }

            calc_current();
            enable_vd_btn();
            enable_result_btn();
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((comboBox3.Text == "DC") && (comboBox2.Text == "Feeder"))
            {
                pf = 1;
                textBox4.Text = pf.ToString("R");
                textBox4.ReadOnly = true;
            }
            else
            {
                textBox4.ReadOnly = false;
            }

            if (comboBox2.Text != "")
            {
                loadtype = comboBox2.Text;
                panel18.BackColor = Color.Transparent;
            }
            else
            {
                panel18.BackColor = Color.Red;
            }


            if (comboBox2.Text == "Motor")
            {
                if (textBox14.Text == "")
                {
                    panel10.BackColor = Color.Red;
                }
                if (textBox25.Text == "")
                {
                    panel12.BackColor = Color.Red;
                }
                if (textBox11.Text == "")
                {
                    panel27.BackColor = Color.Red;
                }
            }
            else
            {
                panel10.BackColor = Color.Transparent;
                panel12.BackColor = Color.Transparent;
                panel27.BackColor = Color.Transparent;
            }



            if (comboBox2.Text == "Motor")
            {
                label18.Enabled = true;
                label19.Enabled = true;
                textBox10.Enabled = true;
                textBox11.Enabled = true;
                label24.Enabled = true;
                label25.Enabled = true;
                label45.Enabled = true;
                label46.Enabled = true;
                textBox14.Enabled = true;
                label28.Enabled = true;
                label29.Enabled = true;
                textBox7.Enabled = true;
                textBox25.Enabled = true;
                label59.Enabled = true;

            }
            else
            {
                label18.Enabled = false;
                label19.Enabled = false;
                textBox10.Enabled = false;
                textBox11.Enabled = false;
                label24.Enabled = false;
                label25.Enabled = false;
                label45.Enabled = false;
                label46.Enabled = false;
                textBox14.Enabled = false;
                label28.Enabled = false;
                label29.Enabled = false;
                textBox7.Enabled = false;
                textBox25.Enabled = false;
                label59.Enabled = false;

                textBox25.Text = "";
                textBox14.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox7.Text = "";
                vdstart = 0;
                vdstartmax = 0;
                pfstart = 0;
                currentstart = 0;
            }

            calc_current();
            enable_vd_btn();
            enable_result_btn();
        }

        private void CbPower_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPower.Text != "")
            {
                powerUnit = cbPower.Text;
            }
            calc_current();
        }


        private void TextBox4_Leave(object sender, EventArgs e)
        {
            calc_current();
        }

        private void TextBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text != "")
            {
                if (lengthunit == "m")
                {
                    length = 3.28084 * double.Parse(textBox6.Text);
                }
                else
                {
                    length = double.Parse(textBox6.Text);
                }
                panel25.BackColor = Color.Transparent;
            }
            else
            {
                length = 0;
                panel25.BackColor = Color.Red;
            }

            Updatek3();

            Updatekt();

            enable_vd_btn();
            enable_result_btn();
        }

        private void TextBox12_TextChanged(object sender, EventArgs e)
        {
            if (textBox12.Text != "")
            {
                n = int.Parse(textBox12.Text);
                panel31.BackColor = Color.Transparent;
            }
            else
            {
                panel31.BackColor = Color.Red;
            }
            enable_vd_btn();
            enable_result_btn();
        }

        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.Text != "") 
            {
                cores = int.Parse(comboBox5.Text);
                panel30.BackColor = Color.Transparent;
            }
            else
            {
                panel30.BackColor = Color.Red;
            }

            enable_vd_btn();
            enable_result_btn();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox8.Text = "PVC";
            dtdiameter.Columns.Add("Diameter");


            cbPower.Text = "kW";
            comboBox1.SelectedIndex = 1;

        }

        /*
        private void TextBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox15.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }
        */

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        

        

        private void ComboBox4_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            material = comboBox4.Text;
            if (comboBox4.Text != "")
            {
                panel19.BackColor = Color.Transparent;
            }
            else
            {
                panel19.BackColor = Color.Red;
            }
            Calc_k();
            enable_vd_btn();
            enable_result_btn();
        }

        private void ComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            insulation = comboBox6.Text;

            if ((insulation == "TW") || (insulation == "UF"))
            {
                insulindex = 1;
                comboBox17.Items.Clear();
                comboBox17.Items.Insert(0, "60");
                comboBox17.SelectedIndex = 0;
                //textBox15.Text = "60";
                //textBox24.Text = "160";
                panel20.BackColor = Color.Transparent;
            }
            else if ((insulation == "RHW") || (insulation == "THW") || (insulation == "THWN") 
                || (insulation == "USE") || (insulation == "ZW"))
            {
                insulindex = 2;
                comboBox17.Items.Clear();
                comboBox17.Items.Insert(0, "75");
                comboBox17.SelectedIndex = 0;
                //textBox15.Text = "75";
                //textBox24.Text = "160";
                panel20.BackColor = Color.Transparent;
            }
            else if ((insulation == "TBS") || (insulation == "SA") || (insulation == "SIS") || (insulation == "FEP")
                || (insulation == "FEPB") || (insulation == "MI") || (insulation == "RHH")
                || (insulation == "RHW-2") || (insulation == "THHN") || (insulation == "THW-2")
                || (insulation == "THWN-2") || (insulation == "USE-2") || (insulation == "XHH")
                || (insulation == "XHHW-2") || (insulation == "ZW-2"))
            {
                insulindex = 3;
                comboBox17.Items.Clear();
                comboBox17.Items.Insert(0, "90");
                comboBox17.SelectedIndex = 0;
                //textBox15.Text = "90";
                //textBox24.Text = "250";
                panel20.BackColor = Color.Transparent;
            }
            else if ((insulation == "XHHW") || (insulation == "THHW"))
            {
                insulindex = 4;
                comboBox17.SelectedIndex = -1;
                comboBox17.Items.Clear();
                comboBox17.Items.Insert(0, "75");
                comboBox17.Items.Insert(1, "90");
                panel20.BackColor = Color.Transparent;
            }
            else
            {
                insulindex = 0;
                comboBox17.SelectedIndex = -1;
                comboBox17.Items.Clear();
                //textBox15.Text = "";
                //textBox24.Text = "";
                textBox21.Text = "";
                panel20.BackColor = Color.Red;
            }

            maxtemp_calc();

            Updatek1();
            Updatekt();

            enable_vd_btn();
            enable_result_btn();
        }

        private void maxtemp_calc()
        {
            if (insulindex == 1)
            {
                maxtemp = 55;
            }
            else if (insulindex == 2)
            {
                maxtemp = 70;
            }
            else if (insulindex == 3)
            {
                maxtemp = 80;
            }
            else
            {
                maxtemp = 0;
            }
        }

        private void ComboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            armour = comboBox7.Text;
            if (comboBox7.Text != "")
            {
                panel21.BackColor = Color.Transparent;
            }
            else
            {
                panel21.BackColor = Color.Red;
            }
            enable_vd_btn();
            enable_result_btn();
        }

        private void ComboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            installation = comboBox9.Text;

            correctionfactor_fill();
            Updatek1();
            Updatekt();

            enable_vd_btn();
            enable_result_btn();
        }

        private void correctionfactor_fill()
        {
            if (installation == "Raceways")
            {
                comboBox18.SelectedIndex = -1;
                comboBox19.SelectedIndex = -1;
                comboBox20.SelectedIndex = -1;
                comboBox18.Enabled = true;
                comboBox19.Enabled = true;
                comboBox20.Enabled = false;
                label93.Text = "Ambient Temperature\nCorrection Factor";
                label94.Text = "No. of Grouped Conductor\nCorrection Factor";
                label95.Text = "";
                label93.Visible = true;
                label94.Visible = true;
                label95.Visible = false;
                panel22.BackColor = Color.Transparent;
            }
            else if (installation == "Cable Tray / Ladder")
            {
                comboBox18.SelectedIndex = -1;
                comboBox19.SelectedIndex = -1;
                comboBox20.SelectedIndex = -1;
                comboBox18.Enabled = true;
                comboBox19.Enabled = false;
                comboBox20.Enabled = true;
                label93.Text = "Ambient Temperature\nCorrection Factor";
                label94.Text = "No. of Grouped Conductor\nCorrection Factor";
                label95.Text = "Cable Tray/Ladder Cover\nCorrection Factor";
                label93.Visible = true;
                label94.Visible = true;
                label95.Visible = true;
                panel22.BackColor = Color.Transparent;
            }
            else if (installation == "Earth (Direct Buried)")
            {
                comboBox18.SelectedIndex = -1;
                comboBox19.SelectedIndex = -1;
                comboBox20.SelectedIndex = -1;
                comboBox18.Enabled = true;
                comboBox19.Enabled = false;
                comboBox20.Enabled = false;
                label93.Text = "Ambient Temperature\nCorrection Factor";
                label94.Text = "No. of Grouped Conductor\nCorrection Factor";
                label95.Text = "";
                label93.Visible = true;
                label94.Visible = true;
                label95.Visible = false;
                panel22.BackColor = Color.Transparent;
            }
            else if (installation == "Free Air")
            {
                comboBox18.SelectedIndex = -1;
                comboBox19.SelectedIndex = -1;
                comboBox20.SelectedIndex = -1;
                comboBox18.Enabled = true;
                comboBox19.Enabled = false;
                comboBox20.Enabled = false;
                label93.Text = "Ambient Temperature\nCorrection Factor";
                label94.Text = "No. of Grouped Conductor\nCorrection Factor";
                label95.Text = "";
                label93.Visible = true;
                label94.Visible = true;
                label95.Visible = false;
                panel22.BackColor = Color.Transparent;
            }
            else
            {
                comboBox18.SelectedIndex = -1;
                comboBox19.SelectedIndex = -1;
                comboBox20.SelectedIndex = -1;
                comboBox18.Enabled = false;
                comboBox19.Enabled = false;
                comboBox20.Enabled = false;
                label93.Text = "";
                label94.Text = "";
                label95.Text = "";
                label93.Visible = false;
                label94.Visible = false;
                label95.Visible = false;
                panel22.BackColor = Color.Red;
            }

            if (comboBox18.Enabled == true)
            {
                label89.Enabled = true;
                panel34.BackColor = Color.Red;
            }
            else
            {
                label89.Enabled = false;
                panel34.BackColor = Color.Transparent;
            }

            if (comboBox19.Enabled == true)
            {
                label90.Enabled = true;
                panel35.BackColor = Color.Red;
            }
            else
            {
                label90.Enabled = false;
                panel35.BackColor = Color.Transparent;
            }

            if (comboBox20.Enabled == true)
            {
                label91.Enabled = true;
                panel36.BackColor = Color.Red;
            }
            else
            {
                label91.Enabled = false;
                panel36.BackColor = Color.Transparent;
            }
        }

        // calculate wire size based on vd and fl current
        private void vd_size_calc()
        {
            n = int.Parse(textBox12.Text);
            complete = false;

            while (!complete)
            {
                i = 0;
                if (radioButton4.Checked)
                {
                    if (insulation == "XLPE")
                    {
                        if (cores == 2)
                        {
                            while ((!complete) && (i < 17))
                            {
                                wirearea = xlpe2core[i, 0];
                                if (phase == "DC")
                                {
                                    Rdc = xlpe2core[i, 3];
                                    vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                }
                                else //AC
                                {
                                    Rac = xlpe2core[i, 1];
                                    X = xlpe2core[i, 2];

                                    if (phase == "Single-Phase AC")
                                    {
                                        vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                        / (n * 1000 * voltage);

                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                            (X * Math.Sqrt(1 - pf * pf))) * 100);

                                        if (loadtype == "Motor")
                                        {
                                            vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                            / (n * 1000 * voltage);
                                        }
                                    }
                                    else if (phase == "Three-Phase AC")
                                    {
                                        vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                        / (n * 1000 * voltage);

                                        lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                            (X * Math.Sqrt(1 - pf * pf))) * 100);

                                        if (loadtype == "Motor")
                                        {
                                            vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                            / (n * 1000 * voltage);
                                        }
                                    }
                                }

                                if (armour == "Non Armoured")
                                {
                                    diameter = ODxlpe2core[i, 1];
                                }
                                else if (armour == "SWA")
                                {
                                    diameter = ODxlpe2core[i, 2];
                                }
                                else if (armour == "DSTA")
                                {
                                    diameter = ODxlpe2core[i, 3];
                                }


                                if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                                {
                                    Irated = xlpe2core[i, 4] * n;
                                }
                                else //above ground
                                {
                                    Irated = xlpe2core[i, 5] * n;
                                }

                                iderated = Irated * ktmain;

                                cable_lte();

                                // Validasi
                                validasi_vd();

                                i++;
                            }
                        }
                        else if (cores == 3)
                        {
                            while ((!complete) && (i < 17))
                            {
                                wirearea = xlpe3core[i, 0];
                                if (phase == "DC")
                                {
                                    Rdc = xlpe3core[i, 3];
                                    vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                }
                                else //AC
                                {
                                    Rac = xlpe3core[i, 1];
                                    X = xlpe3core[i, 2];

                                    if (phase == "Single-Phase AC")
                                    {
                                        vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                        / (n * 1000 * voltage);

                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                            (X * Math.Sqrt(1 - pf * pf))) * 100);

                                        if (loadtype == "Motor")
                                        {
                                            vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                            / (n * 1000 * voltage);
                                        }
                                    }
                                    else if (phase == "Three-Phase AC")
                                    {
                                        vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                        / (n * 1000 * voltage);

                                        lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                            (X * Math.Sqrt(1 - pf * pf))) * 100);

                                        if (loadtype == "Motor")
                                        {
                                            vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                            / (n * 1000 * voltage);
                                        }
                                    }
                                }

                                if (armour == "Non Armoured")
                                {
                                    diameter = ODxlpe3core[i, 1];
                                }
                                else if (armour == "SWA")
                                {
                                    diameter = ODxlpe3core[i, 2];
                                }
                                else if (armour == "DSTA")
                                {
                                    diameter = ODxlpe3core[i, 3];
                                }

                                if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                                {
                                    Irated = xlpe3core[i, 4] * n;
                                }
                                else //above ground
                                {
                                    Irated = xlpe3core[i, 5] * n;
                                }

                                iderated = Irated * ktmain;
                                cable_lte();

                                // Validasi
                                validasi_vd();

                                i++;
                            }
                        }
                        else if (cores == 4)
                        {
                            while ((!complete) && (i < 17))
                            {
                                wirearea = xlpe4core[i, 0];
                                if (phase == "DC")
                                {
                                    Rdc = xlpe4core[i, 3];
                                    vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                }
                                else //AC
                                {
                                    Rac = xlpe4core[i, 1];
                                    X = xlpe4core[i, 2];

                                    if (phase == "Single-Phase AC")
                                    {
                                        vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                        / (n * 1000 * voltage);

                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                            (X * Math.Sqrt(1 - pf * pf))) * 100);

                                        if (loadtype == "Motor")
                                        {
                                            vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                            / (n * 1000 * voltage);
                                        }
                                    }
                                    else if (phase == "Three-Phase AC")
                                    {
                                        vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                        / (n * 1000 * voltage);

                                        lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                            (X * Math.Sqrt(1 - pf * pf))) * 100);

                                        if (loadtype == "Motor")
                                        {
                                            vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                            / (n * 1000 * voltage);
                                        }
                                    }
                                }

                                if (armour == "Non Armoured")
                                {
                                    diameter = ODxlpe4core[i, 1];
                                }
                                else if (armour == "SWA")
                                {
                                    diameter = ODxlpe4core[i, 2];
                                }
                                else if (armour == "DSTA")
                                {
                                    diameter = ODxlpe4core[i, 3];
                                }


                                if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                                {
                                    Irated = xlpe4core[i, 4] * n;
                                }
                                else //above ground
                                {
                                    Irated = xlpe4core[i, 5] * n;
                                }

                                iderated = Irated * ktmain;
                                cable_lte();

                                // Validasi
                                validasi_vd();

                                i++;
                            }
                        }
                    }
                    else if (insulation == "PVC")
                    {
                        if (cores == 2)
                        {
                            while ((!complete) && (i < 16))
                            {
                                wirearea = pvc2core[i, 0];
                                if (phase == "DC")
                                {
                                    Rdc = pvc2core[i, 3];
                                    vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                }
                                else //AC
                                {
                                    Rac = pvc2core[i, 1];
                                    X = pvc2core[i, 2];

                                    if (phase == "Single-Phase AC")
                                    {
                                        vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                        / (n * 1000 * voltage);

                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                            (X * Math.Sqrt(1 - pf * pf))) * 100);

                                        if (loadtype == "Motor")
                                        {
                                            vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                            / (n * 1000 * voltage);
                                        }
                                    }
                                    else if (phase == "Three-Phase AC")
                                    {
                                        vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                        / (n * 1000 * voltage);

                                        lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                            (X * Math.Sqrt(1 - pf * pf))) * 100);

                                        if (loadtype == "Motor")
                                        {
                                            vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                            / (n * 1000 * voltage);
                                        }
                                    }
                                }

                                if (armour == "Non Armoured")
                                {
                                    diameter = ODpvc2core[i, 1];
                                }
                                else if (armour == "SWA")
                                {
                                    diameter = ODpvc2core[i, 2];
                                }
                                else if (armour == "DSTA")
                                {
                                    diameter = ODpvc2core[i, 3];
                                }

                                if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                                {
                                    Irated = pvc2core[i, 4] * n;
                                }
                                else //above ground
                                {
                                    Irated = pvc2core[i, 5] * n;
                                }

                                iderated = Irated * ktmain;
                                cable_lte();

                                // Validasi
                                validasi_vd();

                                i++;
                            }
                        }
                        else if (cores == 3)
                        {
                            while ((!complete) && (i < 16))
                            {
                                wirearea = pvc3core[i, 0];
                                if (phase == "DC")
                                {
                                    Rdc = pvc3core[i, 3];
                                    vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                }
                                else //AC
                                {
                                    Rac = pvc3core[i, 1];
                                    X = pvc3core[i, 2];

                                    if (phase == "Single-Phase AC")
                                    {
                                        vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                        / (n * 1000 * voltage);

                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                            (X * Math.Sqrt(1 - pf * pf))) * 100);

                                        if (loadtype == "Motor")
                                        {
                                            vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                            / (n * 1000 * voltage);
                                        }
                                    }
                                    else if (phase == "Three-Phase AC")
                                    {
                                        vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                        / (n * 1000 * voltage);

                                        lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                            (X * Math.Sqrt(1 - pf * pf))) * 100);

                                        if (loadtype == "Motor")
                                        {
                                            vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                            / (n * 1000 * voltage);
                                        }
                                    }
                                }

                                if (armour == "Non Armoured")
                                {
                                    diameter = ODpvc3core[i, 1];
                                }
                                else if (armour == "SWA")
                                {
                                    diameter = ODpvc3core[i, 2];
                                }
                                else if (armour == "DSTA")
                                {
                                    diameter = ODpvc3core[i, 3];
                                }

                                if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                                {
                                    Irated = pvc3core[i, 4] * n;
                                }
                                else //above ground
                                {
                                    Irated = pvc3core[i, 5] * n;
                                }

                                iderated = Irated * ktmain;
                                cable_lte();

                                // Validasi
                                validasi_vd();

                                i++;
                            }
                        }
                        else if (cores == 4)
                        {
                            while ((!complete) && (i < 16))
                            {
                                wirearea = pvc4core[i, 0];
                                if (phase == "DC")
                                {
                                    Rdc = pvc4core[i, 3];
                                    vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                }
                                else //AC
                                {
                                    Rac = pvc4core[i, 1];
                                    X = pvc4core[i, 2];

                                    if (phase == "Single-Phase AC")
                                    {
                                        vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                        / (n * 1000 * voltage);

                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                            (X * Math.Sqrt(1 - pf * pf))) * 100);

                                        if (loadtype == "Motor")
                                        {
                                            vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                            / (n * 1000 * voltage);
                                        }
                                    }
                                    else if (phase == "Three-Phase AC")
                                    {
                                        vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                        / (n * 1000 * voltage);

                                        lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                            (X * Math.Sqrt(1 - pf * pf))) * 100);


                                        if (loadtype == "Motor")
                                        {
                                            vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                            / (n * 1000 * voltage);
                                        }
                                    }
                                }

                                if (armour == "Non Armoured")
                                {
                                    diameter = ODpvc4core[i, 1];
                                }
                                else if (armour == "SWA")
                                {
                                    diameter = ODpvc4core[i, 2];
                                }
                                else if (armour == "DSTA")
                                {
                                    diameter = ODpvc4core[i, 3];
                                }

                                if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                                {
                                    Irated = pvc4core[i, 4] * n;
                                }
                                else //above ground
                                {
                                    Irated = pvc4core[i, 5] * n;
                                }

                                iderated = Irated * ktmain;
                                cable_lte();

                                // Validasi
                                validasi_vd();

                                i++;
                            }
                        }
                    }
                    else if (insulation == "EPR")
                    {
                        MessageBox.Show("Vendor data cable specification for EPR insulation doesn't exist at the time, please insert cable specification data manually!",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        complete = true;
                    }
                    if (!complete)
                    {
                        solvableOrNPlus_Vd();
                    }
                }
                else if (radioButton3.Checked) //manual cable database input
                {
                    while ((!complete) && (i < cableCount))
                    {
                        wirearea = inputCableData[i, 0];

                        if (phase == "DC")
                        {
                            Rdc = inputCableData[i, 2];
                            Rac = 0;

                            vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                            lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                        }
                        else
                        {
                            Rac = inputCableData[i, 2];
                            Rdc = 0;
                            X = inputCableData[i, 3];

                            if (phase == "Single-Phase AC")
                            {
                                vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                / (n * 1000 * voltage);

                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                    (X * Math.Sqrt(1 - pf * pf))) * 100);

                                if (loadtype == "Motor")
                                {
                                    vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                    / (n * 1000 * voltage);
                                }
                            }
                            else if (phase == "Three-Phase AC")
                            {
                                vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                / (n * 1000 * voltage);

                                lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                    (X * Math.Sqrt(1 - pf * pf))) * 100);

                                if (loadtype == "Motor")
                                {
                                    vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                    / (n * 1000 * voltage);
                                }
                            }
                        }

                        Irated = inputCableData[i, 4];
                        diameter = inputCableData[i, 5];

                        iderated = Irated * ktmain;
                        cable_lte();

                        // Validasi
                        validasi_vd();

                        i++;
                    }
                    if (!complete)
                    {
                        solvableOrNPlus_Vd();
                    }
                }

            }

            if (inputValid)
            {
                textBox12.Text = n.ToString();
                textBox8.Text = vdrun.ToString("0.##");
                textBox29.Text = lmax.ToString("0.##");
                textBox22.Text = cLTE.ToString("0.##");

                if (material == "Copper")
                {
                    materialname = "Cu";
                }

                if (loadtype == "Motor")
                {
                    textBox10.Text = vdstart.ToString("0.##");
                }

                readtemp = "";

                readtemp += n.ToString() + "  ×  " + cores.ToString("0.##") + "/C  #  " + wirearea.ToString() +
                    " mm²    " + ratedvoltage + " / " + materialname + " / " + insulation;

                if (armour != "Non Armoured")
                {
                    readtemp += " / " + armour;
                }

                readtemp += " / " + outersheath;

                tbResult.Text = readtemp;

                save_vd_result(); //MUNGKIN DUIBAH

                Update_size();

                enable_save();
                if (phase == "DC")
                {
                    textBox34.Text = Rdc.ToString("0.####");
                    textBox33.Text = "";
                }
                else //AC
                {
                    textBox34.Text = Rac.ToString("0.####");
                    textBox33.Text = X.ToString("0.####");
                }
                textBox32.Text = Irated.ToString("0.##");

                label86.Visible = true;
                timer1.Enabled = true;

                //enable sc/lte panel
                panel4.Enabled = true;

                // reset s.c & breaker
                textBox28.Text = "";
                textBox23.Text = "";
                textBox30.Text = "";
                textBox20.Text = "";

                comboBox12.SelectedIndex = -1;
                comboBox10.SelectedIndex = -1;
                comboBox11.SelectedIndex = -1;
                comboBox10.Text = "";
                comboBox11.Text = "";
                comboBox5.Enabled = false;
                textBox12.ReadOnly = true;

                button8.Enabled = true;
                panel5.Enabled = false;
                panel6.Enabled = false;

                label87.Text = "Since Vd run is lower than Vd run max, therefore cable size of \n" + wirearea + " mm²  is acceptable";
                label87.Visible = true;
                timer2.Enabled = true;
            }
            else
            {
                tbResult.Text = "Failed to get a suitable cable size";
                button4.Enabled = false;
                textBox8.Text = "";
                textBox10.Text = "";
                textBox29.Text = "";
                textBox22.Text = "";
            }
        }

        //validate vd and fl current
        private void validasi_vd()
        {
            if ((current < iderated) && (vdrun < vdrunmax))
            {
                if (loadtype == "Motor")
                {
                    if (vdstart < vdstartmax)
                    {
                        complete = true;
                        inputValid = true;

                    }
                    else
                    {
                        complete = false;
                        inputValid = false;
                    }
                }
                else
                {
                    complete = true;
                    inputValid = true;
                }

            }
            else
            {
                complete = false;
                inputValid = false;
            }
        }

        //calculate wire size based on all data
        public void area_calc()
        {
            if (finalTemp < initialTemp)
            {
                MessageBox.Show("Invalid value on following input: \n- Final temperature must be greated than the initial temperature", 
                    "Input Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                n = int.Parse(textBox12.Text);
                complete = false;
                while (!complete)
                {
                    i = m;
                    if (radioButton4.Checked)
                    {
                        if (insulation == "XLPE")
                        {
                            if (cores == 2)
                            {
                                while ((!complete) && (i < 17))
                                {
                                    wirearea = xlpe2core[i, 0];
                                    if (phase == "DC")
                                    {
                                        Rdc = xlpe2core[i, 3];
                                        vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                    }
                                    else //AC
                                    {
                                        Rac = xlpe2core[i, 1];
                                        X = xlpe2core[i, 2];

                                        if (phase == "Single-Phase AC")
                                        {
                                            vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                            / (n * 1000 * voltage);

                                            lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                                (X * Math.Sqrt(1 - pf * pf))) * 100);

                                            if (loadtype == "Motor")
                                            {
                                                vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                / (n * 1000 * voltage);
                                            }
                                        }
                                        else if (phase == "Three-Phase AC")
                                        {
                                            vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                            / (n * 1000 * voltage);

                                            lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                                (X * Math.Sqrt(1 - pf * pf))) * 100);

                                            if (loadtype == "Motor")
                                            {
                                                vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                / (n * 1000 * voltage);
                                            }
                                        }
                                    }

                                    if (armour == "Non Armoured")
                                    {
                                        diameter = ODxlpe2core[i, 1];
                                    }
                                    else if (armour == "SWA")
                                    {
                                        diameter = ODxlpe2core[i, 2];
                                    }
                                    else if (armour == "DSTA")
                                    {
                                        diameter = ODxlpe2core[i, 3];
                                    }

                                    if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                                    {
                                        Irated = xlpe2core[i, 4] * n;
                                    }
                                    else //above ground
                                    {
                                        Irated = xlpe2core[i, 5] * n;
                                    }

                                    iderated = Irated * ktmain;

                                    cable_lte();

                                    // Validasi
                                    validasi();

                                    i++;
                                }
                            }
                            else if (cores == 3)
                            {
                                while ((!complete) && (i < 17))
                                {
                                    wirearea = xlpe3core[i, 0];
                                    if (phase == "DC")
                                    {
                                        Rdc = xlpe3core[i, 3];
                                        vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                    }
                                    else //AC
                                    {
                                        Rac = xlpe3core[i, 1];
                                        X = xlpe3core[i, 2];

                                        if (phase == "Single-Phase AC")
                                        {
                                            vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                            / (n * 1000 * voltage);

                                            lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                                (X * Math.Sqrt(1 - pf * pf))) * 100);

                                            if (loadtype == "Motor")
                                            {
                                                vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                / (n * 1000 * voltage);
                                            }
                                        }
                                        else if (phase == "Three-Phase AC")
                                        {
                                            vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                            / (n * 1000 * voltage);

                                            lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                                (X * Math.Sqrt(1 - pf * pf))) * 100);

                                            if (loadtype == "Motor")
                                            {
                                                vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                / (n * 1000 * voltage);
                                            }
                                        }
                                    }

                                    if (armour == "Non Armoured")
                                    {
                                        diameter = ODxlpe3core[i, 1];
                                    }
                                    else if (armour == "SWA")
                                    {
                                        diameter = ODxlpe3core[i, 2];
                                    }
                                    else if (armour == "DSTA")
                                    {
                                        diameter = ODxlpe3core[i, 3];
                                    }

                                    if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                                    {
                                        Irated = xlpe3core[i, 4] * n;
                                    }
                                    else //above ground
                                    {
                                        Irated = xlpe3core[i, 5] * n;
                                    }

                                    iderated = Irated * ktmain;
                                    cable_lte();

                                    // Validasi
                                    validasi();

                                    i++;
                                }
                            }
                            else if (cores == 4)
                            {
                                while ((!complete) && (i < 17))
                                {
                                    wirearea = xlpe4core[i, 0];
                                    if (phase == "DC")
                                    {
                                        Rdc = xlpe4core[i, 3];
                                        vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                    }
                                    else //AC
                                    {
                                        Rac = xlpe4core[i, 1];
                                        X = xlpe4core[i, 2];

                                        if (phase == "Single-Phase AC")
                                        {
                                            vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                            / (n * 1000 * voltage);

                                            lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                                (X * Math.Sqrt(1 - pf * pf))) * 100);

                                            if (loadtype == "Motor")
                                            {
                                                vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                / (n * 1000 * voltage);
                                            }
                                        }
                                        else if (phase == "Three-Phase AC")
                                        {
                                            vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                            / (n * 1000 * voltage);

                                            lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                                (X * Math.Sqrt(1 - pf * pf))) * 100);

                                            if (loadtype == "Motor")
                                            {
                                                vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                / (n * 1000 * voltage);
                                            }
                                        }
                                    }

                                    if (armour == "Non Armoured")
                                    {
                                        diameter = ODxlpe4core[i, 1];
                                    }
                                    else if (armour == "SWA")
                                    {
                                        diameter = ODxlpe4core[i, 2];
                                    }
                                    else if (armour == "DSTA")
                                    {
                                        diameter = ODxlpe4core[i, 3];
                                    }

                                    if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                                    {
                                        Irated = xlpe4core[i, 4] * n;
                                    }
                                    else //above ground
                                    {
                                        Irated = xlpe4core[i, 5] * n;
                                    }

                                    iderated = Irated * ktmain;
                                    cable_lte();

                                    // Validasi
                                    validasi();

                                    i++;
                                }
                            }
                        }
                        else if (insulation == "PVC")
                        {
                            if (cores == 2)
                            {
                                while ((!complete) && (i < 16))
                                {
                                    wirearea = pvc2core[i, 0];
                                    if (phase == "DC")
                                    {
                                        Rdc = pvc2core[i, 3];
                                        vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                    }
                                    else //AC
                                    {
                                        Rac = pvc2core[i, 1];
                                        X = pvc2core[i, 2];

                                        if (phase == "Single-Phase AC")
                                        {
                                            vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                            / (n * 1000 * voltage);

                                            lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                                (X * Math.Sqrt(1 - pf * pf))) * 100);

                                            if (loadtype == "Motor")
                                            {
                                                vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                / (n * 1000 * voltage);
                                            }
                                        }
                                        else if (phase == "Three-Phase AC")
                                        {
                                            vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                            / (n * 1000 * voltage);

                                            lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                                (X * Math.Sqrt(1 - pf * pf))) * 100);

                                            if (loadtype == "Motor")
                                            {
                                                vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                / (n * 1000 * voltage);
                                            }
                                        }
                                    }

                                    if (armour == "Non Armoured")
                                    {
                                        diameter = ODpvc2core[i, 1];
                                    }
                                    else if (armour == "SWA")
                                    {
                                        diameter = ODpvc2core[i, 2];
                                    }
                                    else if (armour == "DSTA")
                                    {
                                        diameter = ODpvc2core[i, 3];
                                    }

                                    if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                                    {
                                        Irated = pvc2core[i, 4] * n;
                                    }
                                    else //above ground
                                    {
                                        Irated = pvc2core[i, 5] * n;
                                    }

                                    iderated = Irated * ktmain;
                                    cable_lte();

                                    // Validasi
                                    validasi();

                                    i++;
                                }
                            }
                            else if (cores == 3)
                            {
                                while ((!complete) && (i < 16))
                                {
                                    wirearea = pvc3core[i, 0];
                                    if (phase == "DC")
                                    {
                                        Rdc = pvc3core[i, 3];
                                        vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                    }
                                    else //AC
                                    {
                                        Rac = pvc3core[i, 1];
                                        X = pvc3core[i, 2];

                                        if (phase == "Single-Phase AC")
                                        {
                                            vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                            / (n * 1000 * voltage);

                                            lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                                (X * Math.Sqrt(1 - pf * pf))) * 100);

                                            if (loadtype == "Motor")
                                            {
                                                vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                / (n * 1000 * voltage);
                                            }
                                        }
                                        else if (phase == "Three-Phase AC")
                                        {
                                            vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                            / (n * 1000 * voltage);

                                            lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                                (X * Math.Sqrt(1 - pf * pf))) * 100);

                                            if (loadtype == "Motor")
                                            {
                                                vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                / (n * 1000 * voltage);
                                            }
                                        }
                                    }

                                    if (armour == "Non Armoured")
                                    {
                                        diameter = ODpvc3core[i, 1];
                                    }
                                    else if (armour == "SWA")
                                    {
                                        diameter = ODpvc3core[i, 2];
                                    }
                                    else if (armour == "DSTA")
                                    {
                                        diameter = ODpvc3core[i, 3];
                                    }

                                    if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                                    {
                                        Irated = pvc3core[i, 4] * n;
                                    }
                                    else //above ground
                                    {
                                        Irated = pvc3core[i, 5] * n;
                                    }

                                    iderated = Irated * ktmain;
                                    cable_lte();

                                    // Validasi
                                    validasi();

                                    i++;
                                }
                            }
                            else if (cores == 4)
                            {
                                while ((!complete) && (i < 16))
                                {
                                    wirearea = pvc4core[i, 0];
                                    if (phase == "DC")
                                    {
                                        Rdc = pvc4core[i, 3];
                                        vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);

                                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                                    }
                                    else //AC
                                    {
                                        Rac = pvc4core[i, 1];
                                        X = pvc4core[i, 2];

                                        if (phase == "Single-Phase AC")
                                        {
                                            vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                            / (n * 1000 * voltage);

                                            lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                                (X * Math.Sqrt(1 - pf * pf))) * 100);

                                            if (loadtype == "Motor")
                                            {
                                                vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                / (n * 1000 * voltage);
                                            }
                                        }
                                        else if (phase == "Three-Phase AC")
                                        {
                                            vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                            / (n * 1000 * voltage);

                                            lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                                (X * Math.Sqrt(1 - pf * pf))) * 100);


                                            if (loadtype == "Motor")
                                            {
                                                vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                                / (n * 1000 * voltage);
                                            }
                                        }
                                    }

                                    if (armour == "Non Armoured")
                                    {
                                        diameter = ODpvc4core[i, 1];
                                    }
                                    else if (armour == "SWA")
                                    {
                                        diameter = ODpvc4core[i, 2];
                                    }
                                    else if (armour == "DSTA")
                                    {
                                        diameter = ODpvc4core[i, 3];
                                    }

                                    if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                                    {
                                        Irated = pvc4core[i, 4] * n;
                                    }
                                    else //above ground
                                    {
                                        Irated = pvc4core[i, 5] * n;
                                    }

                                    iderated = Irated * ktmain;
                                    cable_lte();

                                    // Validasi
                                    validasi();

                                    i++;
                                }
                            }
                        }
                        else if (insulation == "EPR")
                        {
                            MessageBox.Show("Vendor data cable specification for EPR insulation doesn't exist at the time, please insert cable specification data manually!",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            complete = true;
                        }
                        if (!complete)
                        {
                            solvableOrNPlus();
                        }
                    }
                    else if (radioButton3.Checked) //manual cable database input
                    {
                        while ((!complete) && (i < cableCount))
                        {
                            wirearea = inputCableData[i, 0];

                            if (phase == "DC")
                            {
                                Rdc = inputCableData[i, 2];
                                Rac = 0;

                                vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                            }
                            else
                            {
                                Rac = inputCableData[i, 2];
                                Rdc = 0;
                                X = inputCableData[i, 3];

                                if (phase == "Single-Phase AC")
                                {
                                    vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);

                                    if (loadtype == "Motor")
                                    {
                                        vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                }
                                else if (phase == "Three-Phase AC")
                                {
                                    vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);

                                    if (loadtype == "Motor")
                                    {
                                        vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                }
                            }

                            Irated = inputCableData[i, 4];
                            diameter = inputCableData[i, 5];

                            iderated = Irated * ktmain;
                            cable_lte();

                            // Validasi
                            validasi();

                            i++;
                        }
                        if (!complete)
                        {
                            solvableOrNPlus();
                        }
                    }

                }
                if (inputValid)
                {
                    calculated = true;
                    textBox12.Text = n.ToString();
                    textBox8.Text = vdrun.ToString("0.##");
                    textBox29.Text = lmax.ToString("0.##");

                    if (material == "Copper")
                    {
                        materialname = "Cu";
                    }

                    if (loadtype == "Motor")
                    {
                        textBox10.Text = vdstart.ToString("0.##");
                    }

                    readtemp = "";

                    readtemp += n.ToString() + "  ×  " + cores.ToString("0.##") + "/C  #  " + wirearea.ToString() +
                        " mm²    " + ratedvoltage + " / " + materialname + " / " + insulation;

                    if (armour != "Non Armoured")
                    {
                        readtemp += " / " + armour;
                    }

                    readtemp += " / " + outersheath;

                    tbResult.Text = readtemp;
                    cable_lte();
                    textBox22.Text = cLTE.ToString("0.##");
                    save_result();
                    Update_size();
                    enable_save();
                    if (phase == "DC")
                    {
                        textBox34.Text = Rdc.ToString("0.####");
                        textBox33.Text = "";
                    }
                    else //AC
                    {
                        textBox34.Text = Rac.ToString("0.####");
                        textBox33.Text = X.ToString("0.####");
                    }
                    textBox32.Text = Irated.ToString("0.##");

                    label86.Visible = true;
                    timer1.Enabled = true;


                    if (radioButton1.Checked)
                    {
                        label88.Text = "Since withstand energy level of cable is larger than the LTE of the \nprotection device," +
                            " therefore cable size of" + wirearea + " mm²  is acceptable";
                    }
                    else if (radioButton2.Checked)
                    {
                        label88.Text = "Since the minimum cable size due to S.C. is lower than the \nselected cable size," +
                            " therefore cable size of " + wirearea + " mm²  is acceptable";
                    }

                    label88.Visible = true;
                    timer3.Enabled = true;

                }
                else
                {
                    tbResult.Text = "Failed to get a suitable cable size";
                    button4.Enabled = false;
                    i = m;
                }
            }
        }

        private void solvableOrNPlus_Vd()
        {
            if (n > 2147482)
            {
                MessageBox.Show("Failed to get a suitable cable: Maximum number of run exeeded (2147483)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                complete = true;
                inputValid = false;
            }
            else
            {
                n++;
            }
        }

        // show selected size + 2 size above
        private void Update_size()
        {
            textBox37.Text = wirearea.ToString();
            i--;
            comboBox15.Items.Clear();
            comboBox15.Items.Insert(0, "Update Size");
            if (radioButton4.Checked)
            {
                if (insulation == "XLPE")
                {
                    if (cores == 2)
                    {
                        comboBox15.Items.Insert(1, xlpe2core[i, 0]);
                        if (i < 16)
                        {
                            comboBox15.Items.Insert(2, xlpe2core[i + 1, 0]);
                        }
                        if (i < 15)
                        {
                            comboBox15.Items.Insert(3, xlpe2core[i + 2, 0]);
                        }
                    }
                    else if (cores == 3)
                    {
                        comboBox15.Items.Insert(1, xlpe3core[i, 0]);
                        if (i < 16)
                        {
                            comboBox15.Items.Insert(2, xlpe3core[i + 1, 0]);
                        }
                        if (i < 15)
                        {
                            comboBox15.Items.Insert(3, xlpe3core[i + 2, 0]);
                        }
                    }
                    else if (cores == 4)
                    {
                        comboBox15.Items.Insert(1, xlpe4core[i, 0]);
                        if (i < 16)
                        {
                            comboBox15.Items.Insert(2, xlpe4core[i + 1, 0]);
                        }
                        if (i < 15)
                        {
                            comboBox15.Items.Insert(3, xlpe4core[i + 2, 0]);
                        }
                    }
                }
                else if (insulation == "PVC")
                {
                    if (cores == 2)
                    {
                        comboBox15.Items.Insert(1, pvc2core[i, 0]);
                        if (i < 15)
                        {
                            comboBox15.Items.Insert(2, pvc2core[i + 1, 0]);
                        }
                        if (i < 14)
                        {
                            comboBox15.Items.Insert(3, pvc2core[i + 2, 0]);
                        }
                    }
                    else if (cores == 3)
                    {
                        comboBox15.Items.Insert(1, pvc3core[i, 0]);
                        if (i < 15)
                        {
                            comboBox15.Items.Insert(2, pvc3core[i + 1, 0]);
                        }
                        if (i < 14)
                        {
                            comboBox15.Items.Insert(3, pvc3core[i + 2, 0]);
                        }
                    }
                    else if (cores == 4)
                    {
                        comboBox15.Items.Insert(1, pvc3core[i, 0]);
                        if (i < 15)
                        {
                            comboBox15.Items.Insert(2, pvc3core[i + 1, 0]);
                        }
                        if (i < 14)
                        {
                            comboBox15.Items.Insert(3, pvc3core[i + 2, 0]);
                        }
                    }
                }
            }
            else if (radioButton3.Checked)
            {
                comboBox15.Items.Insert(1, inputCableData[i, 0]);
                if (i + 1 < cableCount)
                {
                    comboBox15.Items.Insert(2, inputCableData[i + 1, 0]);

                }
                if (i + 2 < cableCount)
                {
                    comboBox15.Items.Insert(3, inputCableData[i + 2, 0]);
                }
            }
            comboBox15.SelectedIndex = 0;
            m = i;
        }

        private void solvableOrNPlus()
        {
            if (breakcurrent < current)
            {
                MessageBox.Show("Failed to get a suitable cable size: Breaker current value must be greater than the full load current value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                complete = true;
                inputValid = false;
            }
            else if ((radioButton2.Enabled) && (cablesizemin > wirearea))
            {
                MessageBox.Show("Failed to get a suitable cable size: Minimum cable size exceeds the maximum available cable size!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                complete = true;
                inputValid = false;
            }
            else if ((radioButton1.Enabled) && (cLTE < bLTE))
            {
                MessageBox.Show("Failed to get a suitable cable size: Breaker LTE exceeds the maximum cable LTE!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                complete = true;
                inputValid = false;
            }
            else if (n > 2147482)
            {
                MessageBox.Show("Failed to get a suitable cable: Maximum number of run exeeded (214783)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                complete = true;
                inputValid = false;
            }
            else
            {
                n++;
            }
        }

        //validate i fl < i break < i derated, validate vd < vdmax, validate sizemin sc < selected size, validate cLTE < bLTE
        private void validasi()
        {
            if ((current < breakcurrent) && (breakcurrent < iderated) && (vdrun < vdrunmax) &&
                (((radioButton1.Checked) && (cLTE > bLTE)) || ((radioButton2.Checked) && (wirearea > smin))))
            {
                if (loadtype == "Motor")
                {
                    if (vdstart < vdstartmax)
                    {
                        complete = true;
                        inputValid = true;

                    }
                    else
                    {
                        complete = false;
                        inputValid = false;
                    }
                }
                else
                {
                    complete = true;
                    inputValid = true;
                }

            }
            else
            {
                complete = false;
                inputValid = false;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            area_calc();
        }

        private void TextBox5_Leave(object sender, EventArgs e)
        {
            calc_current();
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                voltage = double.Parse(textBox2.Text);
                panel7.BackColor = Color.Transparent;
            }
            else
            {
                voltage = 0;
                panel7.BackColor = Color.Red;
            }
            breaker_fill();
            calc_current();
            enable_vd_btn();
            enable_result_btn();
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                pf = double.Parse(textBox4.Text);
                panel9.BackColor = Color.Transparent;
            }
            else
            {
                pf = 0;
                panel9.BackColor = Color.Red;
            }
            calc_current();
            enable_vd_btn();
            enable_result_btn();
        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text != "")
            {
                eff = double.Parse(textBox5.Text);
                panel8.BackColor = Color.Transparent;
            }
            else
            {
                eff = 0;
                panel8.BackColor = Color.Red;
            }
            calc_current();
            enable_vd_btn();
            enable_result_btn();
        }

        private void TextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox4.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox14_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox14.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox5.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox14_TextChanged(object sender, EventArgs e)
        {
            if (textBox14.Text != "")
            {
                pfstart = double.Parse(textBox14.Text);
                panel10.BackColor = Color.Transparent;
            }
            else if ((textBox14.Text == "") && (comboBox2.Text == "Motor"))
            {
                pfstart = 0;
                panel10.BackColor = Color.Red;
            }
            else
            {
                pfstart = 0;
                panel10.BackColor = Color.Transparent;
            }
            enable_vd_btn();
            enable_result_btn();

        }

        private void TextBox14_Leave_1(object sender, EventArgs e)
        {
            pfstart = double.Parse(textBox14.Text);
        }

        private void TextBox23_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox23.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if ((radioButton1.Checked))
            {
                label53.Enabled = false;
                label54.Enabled = false;
                textBox23.Enabled = false;
                textBox28.Enabled = false;
                label63.Enabled = false;
                label64.Enabled = false;
                label67.Enabled = false;
                label68.Enabled = false;
                textBox30.Enabled = false;
                textBox20.ReadOnly = false;
                label48.Enabled = true;
                label49.Enabled = true;
                label69.Enabled = true;
                label70.Enabled = true;
                textBox20.Enabled = true;
                textBox22.Enabled = true;

                if (radioButton6.Checked)
                {
                    textBox20.ReadOnly = false;
                }
                else if (radioButton5.Checked)
                {
                    textBox20.ReadOnly = true;
                }
            }
            else if ((radioButton2.Checked))
            {
                comboBox11.DropDownStyle = ComboBoxStyle.DropDown;
                label53.Enabled = true;
                label54.Enabled = true;
                textBox23.Enabled = true;
                textBox28.Enabled = true;
                label63.Enabled = true;
                label64.Enabled = true;
                label67.Enabled = true;
                label68.Enabled = true;
                textBox30.Enabled = true;
                textBox20.ReadOnly = true;
                label48.Enabled = false;
                label49.Enabled = false;
                label69.Enabled = false;
                label70.Enabled = false;
                textBox20.Enabled = false;
                textBox22.Enabled = false;
            }

            break_lte();
            enable_result_btn();
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if ((radioButton1.Checked))
            {
                label53.Enabled = false;
                label54.Enabled = false;
                textBox23.Enabled = false;
                textBox28.Enabled = false;
                label63.Enabled = false;
                label64.Enabled = false;
                label67.Enabled = false;
                label68.Enabled = false;
                textBox30.Enabled = false;
                textBox20.ReadOnly = false;
                label48.Enabled = true;
                label49.Enabled = true;
                label69.Enabled = true;
                label70.Enabled = true;
                textBox20.Enabled = true;
                textBox22.Enabled = true;

                if (radioButton6.Checked)
                {
                    textBox20.ReadOnly = false;
                }
                else if (radioButton5.Checked)
                {
                    textBox20.ReadOnly = true;
                }
            }
            else if ((radioButton2.Checked))
            {
                comboBox11.DropDownStyle = ComboBoxStyle.DropDown;
                label53.Enabled = true;
                label54.Enabled = true;
                textBox23.Enabled = true;
                textBox28.Enabled = true;
                label63.Enabled = true;
                label64.Enabled = true;
                label67.Enabled = true;
                label68.Enabled = true;
                textBox30.Enabled = true;
                textBox20.ReadOnly = true;
                label48.Enabled = false;
                label49.Enabled = false;
                label69.Enabled = false;
                label70.Enabled = false;
                textBox20.Enabled = false;
                textBox22.Enabled = false;
            }

            break_lte();
            enable_result_btn();
        }

        private void ComboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            breakcurrent = double.Parse(comboBox11.Text);
            SCLTEchanged();
            enable_result_btn();
        }

        private void ComboBox11_TextChanged(object sender, EventArgs e)
        {
            if (comboBox11.Text != "")
            {
                breakcurrent = double.Parse(comboBox11.Text);
            }
            SCLTEchanged();
            break_lte();
            enable_result_btn();
        }

        private void ComboBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((comboBox11.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as ComboBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox13_Leave(object sender, EventArgs e)
        {
            tagno = textBox13.Text;
        }


        private void ComboBox10_TextChanged(object sender, EventArgs e)
        {
            SCLTEchanged();
            breaker_fill();
            enable_result_btn();
        }

        private void ComboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            SCLTEchanged();
            breaker_fill();
            enable_result_btn();
        }

        private void TextBox16_TextChanged(object sender, EventArgs e)
        {
            breaker_fill();
        }

        private void TextBox23_TextChanged(object sender, EventArgs e)
        {
            if (textBox23.Text != "")
            {
                tbreaker = double.Parse(textBox23.Text);
            }
            else
            {
                tbreaker = 0;
            }
            SCLTEchanged();
            calc_smin();
            break_lte();
            enable_result_btn();
        }

        private void TextBox9_TextChanged(object sender, EventArgs e)
        {
            if (textBox9.Text != "")
            {
                vdrunmax = double.Parse(textBox9.Text);
                panel26.BackColor = Color.Transparent;
            }
            else
            {
                vdrunmax = 0;
                panel26.BackColor = Color.Red;
            }
            enable_vd_btn();
            enable_result_btn();
        }


        private void ComboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            outersheath = comboBox8.Text;
            if (comboBox8.Text != "")
            {
                panel23.BackColor = Color.Transparent;
            }
            else
            {
                panel23.BackColor = Color.Red;
            }
            enable_vd_btn();
            enable_result_btn();
        }

        private void buttonReset(object sender, EventArgs e)
        {
            calculated = false;
            textBox13.Text = "";
            textBox26.Text = "";
            textBox27.Text = "";
            TextBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox25.Text = "";
            textBox14.Text = "";
            textBox9.Text = "";
            textBox11.Text = "";
            textBox12.Text = "1"; //no of run default = 1
            textBox12.ReadOnly = false;
            textBox6.Text = "";
            //textBox15.Text = "";
            textBox24.Text = "";
            textBox17.Text = "";
            textBox36.Text = "";
            textBox18.Text = "";
            textBox19.Text = "";
            textBox23.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox20.Text = "";
            textBox21.Text = "";
            textBox22.Text = "";
            tbResult.Text = "";
            textBox10.Text = "";
            textBox28.Text = "";
            textBox29.Text = "";
            textBox30.Text = "";
            textBox16.Text = "";
            textBox31.Text = "";
            textBox32.Text = "";
            textBox33.Text = "";
            textBox34.Text = "";
            textBox35.Text = "";
            textBox37.Text = "";


            cbPower.Text = "kW";
            comboBox1.SelectedIndex = 1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            comboBox5.Enabled = true;
            comboBox6.SelectedIndex = -1;
            comboBox7.SelectedIndex = -1;
            comboBox8.SelectedIndex = -1;
            comboBox9.SelectedIndex = -1;
            comboBox10.SelectedIndex = -1;
            comboBox11.SelectedIndex = -1;
            comboBox12.SelectedIndex = -1;
            comboBox11.Text = "";
            comboBox14.SelectedIndex = -1;
            comboBox13.SelectedIndex = -1;
            comboBox15.SelectedIndex = -1;
            comboBox15.Items.Clear();
            comboBox16.SelectedIndex = -1;
            comboBox17.SelectedIndex = -1;
            comboBox18.SelectedIndex = -1;
            comboBox19.SelectedIndex = -1;
            comboBox20.SelectedIndex = -1;

            panel4.Enabled = false;
            panel5.Enabled = true;
            panel6.Enabled = true;
            panel30.BackColor = Color.Red;

            label93.Text = "";
            label94.Text = "";
            label95.Text = "";

            label93.Visible = false;
            label94.Visible = false;
            label95.Visible = false;
        }

        private void TextBox25_TextChanged(object sender, EventArgs e)
        {
            if (textBox25.Text != "")
            {
                multiplier = double.Parse(textBox25.Text);
                panel12.BackColor = Color.Transparent;
            }
            else if ((textBox25.Text == "") && (comboBox2.Text == "Motor"))
            {
                multiplier = 0;
                panel12.BackColor = Color.Red;
            }
            else
            {
                panel12.BackColor = Color.Transparent;
            }
            calc_current();
            enable_vd_btn();
            enable_result_btn();
        }

        private void TextBox25_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox25.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox28_TextChanged(object sender, EventArgs e)
        {
            if (textBox28.Text != "")
            {
                sccurrent = double.Parse(textBox28.Text);
            }
            else
            {
                sccurrent = 0;
            }
            SCLTEchanged();
            calc_smin();
            break_lte();
            enable_result_btn();
        }

        //disable add when SC / LTE data changed after being calculated
        private void SCLTEchanged()
        {
            if (calculated)
            {
                button4.Enabled = false;
            }
        }


        private void TextBox28_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox28.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox17_TextChanged(object sender, EventArgs e)
        {
            if (textBox17.Text != "")
            {
                k1main = double.Parse(textBox17.Text);
            }
            else
            {
                k1main = 0;
            }

            if ((textBox17.Text != "") && (textBox18.Text != ""))
            {
                textBox19.Location = new Point(kttextboxX, kttextboxY);
                label43.Location = new Point(ktlabelX, ktlabelY);
                panel24.Location = new Point(textBox19.Location.X - 4, textBox19.Location.Y);
            }
            else if ((textBox17.Text != "") && (radioButton7.Checked) && (k1main != 0))
            {
                label43.Location = label80.Location;
                textBox19.Location = textBox36.Location;

                panel24.Location = new Point(textBox36.Location.X - 4, textBox36.Location.Y);

                textBox18.Visible = true;
                label42.Visible = true;
            }
            else if ((k1main == 0) && (radioButton7.Checked))
            {
                k1main = 0;
                k2main = 0;
                k3main = 0;
                ktmain = 0;

                textBox18.Text = "";
                textBox36.Text = "";
                textBox19.Text = "";

                label43.Location = label42.Location;
                textBox19.Location = textBox18.Location;
                panel24.Location = new Point(textBox18.Location.X - 4, textBox18.Location.Y);

                textBox18.Visible = false;
                label42.Visible = false;
            }
            Updatekt();
            enable_result_btn();
        }

        private void TextBox18_TextChanged(object sender, EventArgs e)
        {
            if ((textBox18.Text != "") && (radioButton7.Checked))
            {
                k2main = double.Parse(textBox18.Text);
                textBox19.Location = new Point(kttextboxX, kttextboxY);
                label43.Location = new Point(ktlabelX, ktlabelY);

                textBox36.Visible = true;
                label80.Visible = true;

            }
            else if ((textBox18.Text == "") && (radioButton7.Checked))
            {
                k2main = 0;
                k3main = 0;

                textBox36.Text = "";

                label43.Location = label80.Location;
                textBox19.Location = textBox36.Location;

                textBox36.Visible = false;
                label80.Visible = false;


            }
            Updatekt();
            enable_result_btn();
        }

        private void TextBox19_TextChanged(object sender, EventArgs e)
        {
            if (textBox19.Text != "")
            {
                panel24.BackColor = Color.Transparent;
            }
            else
            {
                panel24.BackColor = Color.Red;
            }
            enable_vd_btn();
            enable_result_btn();
        }

        private void Updatekt()
        {
            kttextboxX = 433;
            kttextboxY = 458;
            ktlabelX = 310;
            ktlabelY = 460;

            if (radioButton7.Checked)
            {
                label93.Text = "";
                label94.Text = "";
                label95.Text = "";

                label93.Visible = false;
                label94.Visible = false;
                label95.Visible = false;

                if ((textBox17.Text != "") && (textBox18.Text != "") && (textBox36.Text != ""))
                {
                    ktmain = k1main * k2main * k3main;
                }
                else if ((textBox17.Text != "") && (textBox18.Text != "") && (textBox36.Text == ""))
                {
                    ktmain = k1main * k2main;
                }
                else if ((textBox17.Text != "") && (textBox18.Text == "") && (textBox36.Text == ""))
                {
                    ktmain = k1main;
                }
                else
                {
                    textBox19.Text = "";
                }

                if (ktmain != 0)
                {
                    textBox19.Text = ktmain.ToString("0.##");
                }

            }
            else //radiobutton7 unchecked
            {
                if (installation == "Raceways")
                {
                    ktmain = k1main * k2main;
                }
                else if (installation == "Cable Tray / Ladder")
                {
                    ktmain = k1main * k2main * k3main;
                }
                else if (installation == "Earth (Direct Buried)")
                {
                    ktmain = k1main * k2main;
                }
                else if (installation == "Free Air")
                {
                    ktmain = k1main * k2main;
                }
                else
                {
                    ktmain = 0;
                }

                if (k1main != 0)
                {
                    textBox17.Text = k1main.ToString("0.##");
                }
                else
                {
                    textBox17.Text = "";
                }

                if (k2main != 0)
                {
                    textBox18.Text = k2main.ToString("0.##");
                }
                else
                {
                    textBox18.Text = "";
                }

                if (k3main != 0)
                {
                    textBox36.Text = k3main.ToString("0.##");
                }
                else
                {
                    textBox36.Text = "";
                }

                if (ktmain != 0)
                {
                    textBox19.Text = ktmain.ToString("0.##");
                }
                else
                {
                    textBox19.Text = "";
                }
            }
        }

        private void TextBox26_TextChanged(object sender, EventArgs e)
        {
            from = textBox26.Text;
        }

        private void ComboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            breakertype = comboBox12.Text;
            SCLTEchanged();
            breaker_fill();
            break_lte();
            enable_result_btn();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            DataRow dtr = dtdiameter.NewRow();
            f5.dataGridView1.RowCount++;
            j++;
            f5.dataGridView1.Rows[j].Cells[0].Value = j + 1;

            for (int k = 0; k < 38; k++)
            {
                f5.dataGridView1.Rows[j].Cells[k + 1].Value = results[k];
            }

            OpenDataTable();
            
            //cable OD
            dtr[0] = diameter;

            dtdiameter.Rows.Add(dtr);
            
            f5.Update_summary();
        }

        private void TextBox16_TextChanged_1(object sender, EventArgs e)
        {
            fromdesc = textBox16.Text;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
            if (Form5.cancelexit)
            {
                e.Cancel = true;
                Form5.cancelexit = false;
            }
        }

        private void TextBox31_TextChanged(object sender, EventArgs e)
        {
            todesc = textBox31.Text;
        }

        private void TextBox30_TextChanged(object sender, EventArgs e)
        {
            if (textBox30.Text != "")
            {
                cablesizemin = double.Parse(textBox30.Text);
            }
            else
            {
                cablesizemin = 0;
            }
        }

        private void Button5_Click_1(object sender, EventArgs e)
        {
            OpenDataTable();
        }

        private void OpenDataTable()
        {
            if (!f5.Visible)
            {
                f5.Show();
            }
            else if (f5.WindowState == FormWindowState.Minimized)
            {
                f5.WindowState = FormWindowState.Normal;
            }
            else
            {
                f5.BringToFront();
            }
        }

        private void RadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                if (voltageLv == "MV")
                {
                    MessageBox.Show("Vendor data for Medium Voltage (MV) cable is not available, please input cable data manually.",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    radioButton3.Checked = true;
                }
                else
                {
                    button6.Enabled = false;
                    label78.Enabled = false;
                }
            }
            else
            {
                button6.Enabled = true;
                label78.Enabled = true;
            }
            enable_vd_btn();
            enable_result_btn();
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                button6.Enabled = true;
                label78.Enabled = true;
            }
            else
            {
                button6.Enabled = false;
                label78.Enabled = false;
            }
            enable_vd_btn();
            enable_result_btn();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            f6.FormClosed += F6_FormClosed;
            f6.ShowDialog();

        }
        private void F6_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Form6.okclicked)
            {
                enable_result_btn();
                inputCableData = new double[cableCount, 6];
                for (int i = 0; i < cableCount; i++)
                {
                    for (int q = 0; q < 6; q++)
                    {
                        inputCableData[i, q] = Form6.confirmedcabledata[i, q];
                    }
                }
            }
        }


        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fSettings.FormClosed += fSettings_FormClosed;
            fSettings.ShowDialog();
        }
        private void fSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (decimalSeparatorChanged && okSetClicked)
            {
                refreshInputData();
                refreshDataTable();
                decimalSeparatorChanged = false;
            }
            okSetClicked = false;
        }

        //change decimal separator of data in data table based on the new selected decimal separator
        private void refreshDataTable()
        {
            for (int i = 0; i < j + 1; i++)
            {
                for (int k = 0; k < 39; k++)
                {
                    if (((k == 8) || ((k >= 10) && (k <= 16)) || ((k >= 18) && (k <= 23)) ||
                       ((k >= 25) && (k <= 36))))
                    {
                        if (decimalseparator == '.')
                        {
                            f5.dataGridView1.Rows[i].Cells[k].Value = Convert.ToString(f5.dataGridView1.Rows[i].Cells[k].Value).Replace(',', '.');
                        }
                        else //decimalseparator == ','
                        {
                            f5.dataGridView1.Rows[i].Cells[k].Value = Convert.ToString(f5.dataGridView1.Rows[i].Cells[k].Value).Replace('.', ',');
                        }
                    }
                }
            }

            foreach (DataRow row in dtdiameter.Rows)
            {
                if (decimalseparator == ',')
                {
                    row[0] = Convert.ToString(row[0]).Replace('.', ',');
                }
                else
                {
                    row[0] = Convert.ToString(row[0]).Replace(',', '.');
                }
            }
            f5.Update_summary();
        }

        //change decimal separator of data input to the new selected decimal separator
        private void refreshInputData()
        {
            if (power != 0)
            {
                TextBox1.Text = power.ToString();
            }
            if (voltage != 0)
            {
                textBox2.Text = voltage.ToString();
            }
            if (multiplier != 0)
            {
                textBox25.Text = multiplier.ToString();
            }
            if (eff != 0)
            {
                textBox5.Text = eff.ToString();
            }
            if (pf != 0)
            {
                textBox4.Text = pf.ToString();
            }
            if (pfstart != 0)
            {
                textBox14.Text = pfstart.ToString();
            }
            if (k1main != 0)
            {
                textBox17.Text = k1main.ToString();
            }
            if (k2main != 0)
            {
                textBox18.Text = k2main.ToString();
            }
            if (k3main != 0)
            {
                textBox36.Text = k3main.ToString();
            }
            if (ktmain != 0)
            {
                textBox19.Text = ktmain.ToString();
            }
            if (sccurrent != 0)
            {
                textBox28.Text = sccurrent.ToString();
            }
            if (tbreaker != 0)
            {
                textBox23.Text = tbreaker.ToString();
            }
            if (cablesizemin != 0)
            {
                textBox30.Text = cablesizemin.ToString("0.##");
            }
            if (vdrun != 0)
            {
                textBox8.Text = vdrun.ToString("0.##");
            }
            if (vdrunmax != 0)
            {
                textBox9.Text = vdrunmax.ToString();
            }
            if (vdstart != 0)
            {
                textBox10.Text = vdstart.ToString("0.##");
            }
            if (vdstartmax != 0)
            {
                textBox11.Text = vdstartmax.ToString();
            }
            if (length != 0)
            {
                textBox6.Text = length.ToString();
            }
            if (lmax != 0)
            {
                textBox29.Text = lmax.ToString("0.##");
            }
            if (initialTemp != 0)
            {
                comboBox17.Text = initialTemp.ToString();
                //textBox15.Text = initialTemp.ToString();
            }
            if (finalTemp != 0)
            {
                textBox24.Text = finalTemp.ToString();
            }
            if (k != 0)
            {
                textBox21.Text = k.ToString("0.###");
            }
            if (Rac != 0)
            {
                textBox34.Text = Rac.ToString("0.####");
            }
            else if (Rdc != 0)
            {
                textBox34.Text = Rdc.ToString("0.####");
            }
            if (X != 0)
            {
                textBox33.Text = X.ToString("0.####");
            }
            if (Irated != 0)
            {
                textBox32.Text = Irated.ToString("0.##");
            }
            if (bLTE != 0)
            {
                textBox20.Text = bLTE.ToString();
            }
            if (cLTE != 0)
            {
                textBox22.Text = cLTE.ToString("0.##");
            }
            if (breakcurrent != 0)
            {
                comboBox11.Text = breakcurrent.ToString();
            }

        }

        private void TextBox35_TextChanged(object sender, EventArgs e)
        {
            remarks = textBox35.Text;
        }

        private void RadioButton2_Click_1(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                radioButton2.Checked = false;
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
                radioButton1.Checked = false;
            }
        }

        private void RadioButton1_Click_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                radioButton1.Checked = false;
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
            }
        }

        private void TextBox20_TextChanged(object sender, EventArgs e)
        {
            if (textBox20.Text != "")
            {
                bLTE = double.Parse(textBox20.Text);
            }
            SCLTEchanged();
            enable_result_btn();
        }

        private void TextBox20_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox20.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void ComboBox13_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox13.Text != "")
            {
                volSys = comboBox13.Text;
                panel15.BackColor = Color.Transparent;
            }
            else
            {
                panel15.BackColor = Color.Red;
            }
            voltageLv = comboBox13.Text;
            comboBox14.Items.Clear();
            if (comboBox13.Text == "MV")
            {
                comboBox14.Items.Insert(0, "3.6/6kV");
                comboBox14.Items.Insert(1, "6/10kV");
                comboBox14.Items.Insert(2, "8.7/15kV");
                comboBox14.Items.Insert(3, "12/20kV");
                comboBox14.Items.Insert(4, "18/30kV");

                toolTip1.SetToolTip(comboBox14, null);
            }
            else if (comboBox13.Text == "LV")
            {
                comboBox14.Items.Insert(0, "0.6/1kV");
                comboBox14.Text = "0.6/1kV";

                toolTip1.SetToolTip(comboBox14, null);
            }
            else
            {
                panel14.BackColor = Color.Red;
                toolTip1.SetToolTip(comboBox14, "Voltage system needs to be chosen first");
            }
            if ((radioButton4.Checked) && (voltageLv == "MV"))
            {
                MessageBox.Show("Vendor data for Medium Voltage (MV) cable is not available, please input cable data manually.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                radioButton3.Checked = true;
            }
            enable_vd_btn();
            enable_result_btn();
        }

        private void ComboBox14_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox14.Text != "")
            {
                panel16.BackColor = Color.Transparent;
            }
            else
            {
                panel16.BackColor = Color.Red;
            }
            ratedvoltage = comboBox14.Text;
            enable_vd_btn();
            enable_result_btn();
        }

        private void TextBox10_TextChanged(object sender, EventArgs e)
        {
            if (textBox10.Text != "")
            {
                vdstart = double.Parse(textBox10.Text);
            }
        }

        private void TextBox8_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox8.Text != "")
            {
                vdrun = double.Parse(textBox8.Text);
            }
        }

        private void RadioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                comboBox11.DropDownStyle = ComboBoxStyle.DropDownList;
                textBox20.ReadOnly = true;
            }
            else if (radioButton6.Checked)
            {
                comboBox11.DropDownStyle = ComboBoxStyle.DropDown;
                textBox20.ReadOnly = false;
            }
        }

        private void RadioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                comboBox11.DropDownStyle = ComboBoxStyle.DropDownList;
                textBox20.ReadOnly = true;
                comboBox10.DropDownStyle = ComboBoxStyle.DropDownList;

            }
            else if (radioButton6.Checked)
            {
                comboBox11.DropDownStyle = ComboBoxStyle.DropDown;
                textBox20.ReadOnly = false;
                comboBox10.DropDownStyle = ComboBoxStyle.DropDown;
            }
        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        int kttextboxX, kttextboxY, ktlabelX, ktlabelY;


        private void TextBox24_TextChanged(object sender, EventArgs e)
        {
            if (textBox24.Text != "")
            {
                finalTemp = double.Parse(textBox24.Text);
            }
            else
            {
                finalTemp = 0;
            }
            SCLTEchanged();
            Calc_k();
            enable_result_btn();

        }

        private void Button7_Click(object sender, EventArgs e)
        {
            calculated = false;
            if ((voltage == 0) || (eff == 0) || (length == 0) || ((power == 0) && (!radioButton8.Checked)) || (current == 0) ||
                ((comboBox2.Text == "Motor") && (multiplier == 0)) || (pf > 1) || (pfstart > 1) || 
                (vdrunmax > 100) || (vdrunmax <= 0) || ((comboBox2.Text == "Motor") && ((vdstartmax > 100) || (vdstartmax <= 0)))) 
            {
                string msgbox;
                msgbox = "Invalid value on following input: ";
                if (voltage == 0)
                {
                    msgbox += "\n- Voltage: voltage input can't be 0";
                }
                if (power == 0)
                {
                    msgbox += "\n- Power";
                }
                if (eff == 0)
                {
                    msgbox += "\n- Efficiency";
                }
                if (current == 0)
                {
                    msgbox += "\n- Full load Current";
                }
                if ((comboBox2.Text == "Motor") && (multiplier == 0))
                {
                    msgbox += "\n- Multiplier";
                }
                    if (pf > 1)
                {
                    msgbox += "\n- P.F. full load";
                }
                if (pfstart > 1)
                {
                    msgbox += "\n- P.F. start";
                }
                if (length == 0)
                {
                    msgbox += "\n- Length: cable length can't be 0";
                }
                if ((vdrunmax > 100) || (vdrunmax <= 0))
                {
                    msgbox += "\n- Vd Run Max";
                }
                if ((comboBox2.Text == "Motor") && ((vdstartmax > 100) || (vdstartmax <= 0)))
                {
                    msgbox += "\n- Vd Start Max";
                }
                MessageBox.Show(msgbox, "Input Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                vd_size_calc();
            }
        }

        private void TextBox36_TextChanged(object sender, EventArgs e)
        {
            if((textBox36.Text != "") && (radioButton7.Checked))
            {
                k3main = double.Parse(textBox36.Text);
            }
             
            Updatekt();
            enable_result_btn();
        }


        private void TextBox17_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox17.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox18_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox18.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox36_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox36.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        /*
        private void TextBox15_TextChanged(object sender, EventArgs e)
        {
            if (textBox15.Text != "")
            {
                initialTemp = double.Parse(textBox15.Text);
            }
            else
            {
                initialTemp = 0;
            }
            SCLTEchanged();
            Calc_k();
            enable_result_btn();
        }
        */

        private void ComboBox15_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!((comboBox15.Text == "Update Size") || (comboBox15.Text == "")))
            {
                textBox37.Text = comboBox15.Text;
                if (comboBox15.SelectedIndex == 1)
                {
                    m = i;
                }
                else if (comboBox15.SelectedIndex == 2)
                {
                    m = i + 1;
                }
                else if (comboBox15.SelectedIndex == 3)
                {
                    m = i + 2;
                }
            }

            if (!((textBox37.Text == wirearea.ToString()) || (comboBox15.Text == "Update Size") || (textBox37.Text == "") || 
                (comboBox15.Text == "")))
            {
                if (radioButton4.Checked)
                {
                    if (insulation == "XLPE")
                    {
                        if (cores == 2)
                        {
                            wirearea = xlpe2core[m, 0];
                            if (phase == "DC")
                            {
                                Rdc = xlpe2core[m, 3];
                                vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                            }
                            else //AC
                            {
                                Rac = xlpe2core[m, 1];
                                X = xlpe2core[m, 2];

                                if (phase == "Single-Phase AC")
                                {
                                    vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);

                                    if (loadtype == "Motor")
                                    {
                                        vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                }
                                else if (phase == "Three-Phase AC")
                                {
                                    vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);

                                    if (loadtype == "Motor")
                                    {
                                        vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                }
                            }

                            if (armour == "Non Armoured")
                            {
                                diameter = ODxlpe2core[m, 1];
                            }
                            else if (armour == "SWA")
                            {
                                diameter = ODxlpe2core[m, 2];
                            }
                            else if (armour == "DSTA")
                            {
                                diameter = ODxlpe2core[m, 3];
                            }

                            if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                            {
                                Irated = xlpe2core[m, 4] * n;
                            }
                            else //above ground
                            {
                                Irated = xlpe2core[m, 5] * n;
                            }

                            iderated = Irated * ktmain;

                            cable_lte();

                            // Validasi
                            validasi_vd();

                        }
                        else if (cores == 3)
                        {
                            
                            wirearea = xlpe3core[m, 0];
                            if (phase == "DC")
                            {
                                Rdc = xlpe3core[m, 3];
                                vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                            }
                            else //AC
                            {
                                Rac = xlpe3core[m, 1];
                                X = xlpe3core[m, 2];

                                if (phase == "Single-Phase AC")
                                {
                                    vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);

                                    if (loadtype == "Motor")
                                    {
                                        vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                }
                                else if (phase == "Three-Phase AC")
                                {
                                    vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);

                                    if (loadtype == "Motor")
                                    {
                                        vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                }
                            }

                            if (armour == "Non Armoured")
                            {
                                diameter = ODxlpe3core[m, 1];
                            }
                            else if (armour == "SWA")
                            {
                                diameter = ODxlpe3core[m, 2];
                            }
                            else if (armour == "DSTA")
                            {
                                diameter = ODxlpe3core[m, 3];
                            }


                            if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                            {
                                Irated = xlpe3core[m, 4] * n;
                            }
                            else //above ground
                            {
                                Irated = xlpe3core[m, 5] * n;
                            }

                            iderated = Irated * ktmain;
                            cable_lte();

                            // Validasi
                            validasi_vd();

                        }
                        else if (cores == 4)
                        {
                           
                            wirearea = xlpe4core[m, 0];
                            if (phase == "DC")
                            {
                                Rdc = xlpe4core[m, 3];
                                vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                            }
                            else //AC
                            {
                                Rac = xlpe4core[m, 1];
                                X = xlpe4core[m, 2];

                                if (phase == "Single-Phase AC")
                                {
                                    vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);

                                    if (loadtype == "Motor")
                                    {
                                        vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                }
                                else if (phase == "Three-Phase AC")
                                {
                                    vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);

                                    if (loadtype == "Motor")
                                    {
                                        vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                }
                            }

                            if (armour == "Non Armoured")
                            {
                                diameter = ODxlpe4core[m, 1];
                            }
                            else if (armour == "SWA")
                            {
                                diameter = ODxlpe4core[m, 2];
                            }
                            else if (armour == "DSTA")
                            {
                                diameter = ODxlpe4core[m, 3];
                            }

                            if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                            {
                                Irated = xlpe4core[m, 4] * n;
                            }
                            else //above ground
                            {
                                Irated = xlpe4core[m, 5] * n;
                            }

                            iderated = Irated * ktmain;
                            cable_lte();

                            // Validasi
                            validasi_vd();

                            
                        }
                    }
                    else if (insulation == "PVC")
                    {
                        if (cores == 2)
                        {
                            wirearea = pvc2core[m, 0];
                            if (phase == "DC")
                            {
                                Rdc = pvc2core[m, 3];
                                vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                            }
                            else //AC
                            {
                                Rac = pvc2core[m, 1];
                                X = pvc2core[m, 2];

                                if (phase == "Single-Phase AC")
                                {
                                    vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);

                                    if (loadtype == "Motor")
                                    {
                                        vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                }
                                else if (phase == "Three-Phase AC")
                                {
                                    vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);

                                    if (loadtype == "Motor")
                                    {
                                        vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                }
                            }

                            if (armour == "Non Armoured")
                            {
                                diameter = ODpvc2core[m, 1];
                            }
                            else if (armour == "SWA")
                            {
                                diameter = ODpvc2core[m, 2];
                            }
                            else if (armour == "DSTA")
                            {
                                diameter = ODpvc2core[m, 3];
                            }

                            if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                            {
                                Irated = pvc2core[m, 4] * n;
                            }
                            else //above ground
                            {
                                Irated = pvc2core[m, 5] * n;
                            }

                            iderated = Irated * ktmain;
                            cable_lte();

                        }
                        else if (cores == 3)
                        {
                            wirearea = pvc3core[m, 0];
                            if (phase == "DC")
                            {
                                Rdc = pvc3core[m, 3];
                                vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                            }
                            else //AC
                            {
                                Rac = pvc3core[m, 1];
                                X = pvc3core[m, 2];

                                if (phase == "Single-Phase AC")
                                {
                                    vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);

                                    if (loadtype == "Motor")
                                    {
                                        vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                }
                                else if (phase == "Three-Phase AC")
                                {
                                    vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);

                                    if (loadtype == "Motor")
                                    {
                                        vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                }
                            }

                            if (armour == "Non Armoured")
                            {
                                diameter = ODpvc3core[m, 1];
                            }
                            else if (armour == "SWA")
                            {
                                diameter = ODpvc3core[m, 2];
                            }
                            else if (armour == "DSTA")
                            {
                                diameter = ODpvc3core[m, 3];
                            }

                            if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                            {
                                Irated = pvc3core[m, 4] * n;
                            }
                            else //above ground
                            {
                                Irated = pvc3core[m, 5] * n;
                            }

                            iderated = Irated * ktmain;
                            cable_lte();
                            
                        }
                        else if (cores == 4)
                        {
                            wirearea = pvc4core[m, 0];
                            if (phase == "DC")
                            {
                                Rdc = pvc4core[m, 3];
                                vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);

                                lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                            }
                            else //AC
                            {
                                Rac = pvc4core[m, 1];
                                X = pvc4core[m, 2];

                                if (phase == "Single-Phase AC")
                                {
                                    vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);

                                    if (loadtype == "Motor")
                                    {
                                        vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                }
                                else if (phase == "Three-Phase AC")
                                {
                                    vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                                    / (n * 1000 * voltage);

                                    lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                        (X * Math.Sqrt(1 - pf * pf))) * 100);


                                    if (loadtype == "Motor")
                                    {
                                        vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                        / (n * 1000 * voltage);
                                    }
                                }
                            }

                            if (armour == "Non Armoured")
                            {
                                diameter = ODpvc4core[m, 1];
                            }
                            else if (armour == "SWA")
                            {
                                diameter = ODpvc4core[m, 2];
                            }
                            else if (armour == "DSTA")
                            {
                                diameter = ODpvc4core[m, 3];
                            }

                            if ((installation == "D2 (Under Ground)") || (installation == "D1 (Under Ground)"))
                            {
                                Irated = pvc4core[m, 4] * n;
                            }
                            else //above ground
                            {
                                Irated = pvc4core[m, 5] * n;
                            }

                            iderated = Irated * ktmain;
                            cable_lte();
                            
                        }
                    }
                }
                else if (radioButton3.Checked) //manual cable database input
                {
                    wirearea = inputCableData[m, 0];

                    if (phase == "DC")
                    {
                        Rdc = inputCableData[m, 2];
                        Rac = 0;

                        vdrun = 2 * current * (Rdc * pf) * length * 100 / (n * 1000 * voltage);
                        lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * Rdc * 100);
                    }
                    else
                    {
                        Rac = inputCableData[m, 2];
                        Rdc = 0;
                        X = inputCableData[m, 3];

                        if (phase == "Single-Phase AC")
                        {
                            vdrun = 2 * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                            / (n * 1000 * voltage);

                            lmax = (n * vdrunmax * 1000 * voltage) / (2 * current * ((Rac * pf) +
                                (X * Math.Sqrt(1 - pf * pf))) * 100);

                            if (loadtype == "Motor")
                            {
                                vdstart = 2 * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                / (n * 1000 * voltage);
                            }
                        }
                        else if (phase == "Three-Phase AC")
                        {
                            vdrun = Math.Sqrt(3) * current * (Rac * pf + X * Math.Sqrt(1 - pf * pf)) * length * 100
                            / (n * 1000 * voltage);

                            lmax = (n * vdrunmax * 1000 * voltage) / (Math.Sqrt(3) * current * ((Rac * pf) +
                                (X * Math.Sqrt(1 - pf * pf))) * 100);

                            if (loadtype == "Motor")
                            {
                                vdstart = Math.Sqrt(3) * currentstart * (Rac * pfstart + X * Math.Sqrt(1 - pfstart * pfstart)) * length * 100
                                / (n * 1000 * voltage);
                            }
                        }
                    }

                    Irated = inputCableData[m, 4];
                    diameter = inputCableData[m, 5];

                    iderated = Irated * ktmain;
                    cable_lte();
                }

                textBox12.Text = n.ToString();
                textBox8.Text = vdrun.ToString("0.##");
                textBox29.Text = lmax.ToString("0.##");
                textBox22.Text = cLTE.ToString("0.##");

                if (material == "Copper")
                {
                    materialname = "Cu";
                }
                else if (material == "Aluminium")
                {
                    materialname = "Al";
                }

                if (loadtype == "Motor")
                {
                    textBox10.Text = vdstart.ToString("0.##");
                }

                readtemp = "";

                readtemp += n.ToString() + "  ×  " + cores.ToString("0.##") + "/C  #  " + wirearea.ToString() +
                    " mm²    " + ratedvoltage + " / " + materialname + " / " + insulation;

                if (armour != "Non Armoured")
                {
                    readtemp += " / " + armour;
                }

                readtemp += " / " + outersheath;

                tbResult.Text = readtemp;

                save_vd_result();

                enable_save();
                if (phase == "DC")
                {
                    textBox34.Text = Rdc.ToString("0.####");
                    textBox33.Text = "";
                }
                else //AC
                {
                    textBox34.Text = Rac.ToString("0.####");
                    textBox33.Text = X.ToString("0.####");
                }
                textBox32.Text = Irated.ToString("0.##");
                if (IsCalculateNeeded())
                {
                    disable_save();
                }
            }
            
        }


        private void Button8_Click(object sender, EventArgs e)
        {
            calculated = false;
            panel6.Enabled = true;
            panel5.Enabled = true;

            textBox8.Text = "";
            textBox10.Text = "";
            textBox29.Text = "";
            textBox12.Text = "1";
            textBox37.Text = "";
            textBox34.Text = "";
            textBox33.Text = "";
            textBox32.Text = "";
            textBox28.Text = "";
            textBox23.Text = "";
            textBox30.Text = "";
            textBox22.Text = "";
            textBox20.Text = "";


            comboBox5.Enabled = true;
            textBox12.ReadOnly = false;
            comboBox15.SelectedIndex = -1;
            comboBox15.Items.Clear();
            comboBox10.SelectedIndex = -1;
            comboBox11.SelectedIndex = -1;
            comboBox12.SelectedIndex = -1;

            panel4.Enabled = false;
            button8.Enabled = false;
        }

        private void TextBox4_Leave_1(object sender, EventArgs e)
        {
            if (pf > 1)
            {
                MessageBox.Show("Power Factor can't be greater than 1!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label86.Visible = false;
            timer1.Enabled = false;
        }

        private void RadioButton7_CheckedChanged(object sender, EventArgs e)
        {
            reset_correction();

            if (!radioButton7.Checked)
            {
                Updatek1();
                Updatek2();
                Updatek3();
                Updatekt();

                textBox17.ReadOnly = true;
                textBox18.ReadOnly = true;
                textBox36.ReadOnly = true;

                label42.Visible = true;
                label80.Visible = true;
                textBox18.Visible = true;
                textBox36.Visible = true;

                textBox19.Location = new Point(kttextboxX, kttextboxY);
                label43.Location = new Point(ktlabelX, ktlabelY);
                panel24.Location = new Point(textBox19.Location.X - 4, textBox19.Location.Y);
            }
            else if (radioButton7.Checked)
            {

                label93.Text = "";
                label94.Text = "";
                label95.Text = "";

                label93.Visible = false;
                label94.Visible = false;
                label95.Visible = false;

                kttextboxX = 433;
                kttextboxY = 458;
                ktlabelX = 310;
                ktlabelY = 460;

                textBox17.ReadOnly = false;
                textBox18.ReadOnly = false;
                textBox36.ReadOnly = false;

                if ((textBox17.Text == "") && (textBox18.Text == "") && (textBox36.Text == ""))
                {
                    label42.Visible = false;
                    label80.Visible = false;
                    textBox18.Visible = false;
                    textBox36.Visible = false;
                    textBox19.Location = textBox18.Location;
                    label43.Location = label42.Location;
                    panel24.Location = new Point(textBox18.Location.X - 4, textBox18.Location.Y);

                }
                /*
                else if ((textBox17.Text != "") && (textBox18.Text == ""))
                {
                    textBox36.Visible = false;
                    label80.Visible = false;

                    textBox19.Location = textBox36.Location;
                    label43.Location = label80.Location;
                }
                */

            }
        }

        private void RadioButton7_Click(object sender, EventArgs e)
        {
            if (!radioButton7.Checked)
            {
                radioButton7.Checked = true;
            }
            else if (radioButton7.Checked)
            {
                radioButton7.Checked = false;
            }
        }

        private void RadioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
            {
                TextBox1.Text = "";
                power = 0;

                TextBox1.ReadOnly = true;
                cbPower.Enabled = false;

                textBox3.ReadOnly = false;
                panel14.BackColor = Color.Transparent;
                if (textBox3.Text == "")
                {
                    panel11.BackColor = Color.Red;
                }

            }
            else
            {
                textBox3.Text = "";
                current = 0;
                TextBox1.ReadOnly = false;
                cbPower.Enabled = true;

                textBox3.ReadOnly = true;
                panel11.BackColor = Color.Transparent;
                if (textBox2.Text == "")
                {
                    panel14.BackColor = Color.Red;
                }
            }
        }

        private void RadioButton8_Click(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
            {
                radioButton8.Checked = false;
            }
            else
            {
                radioButton8.Checked = true;
            }
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                current = double.Parse(textBox3.Text);
                panel11.BackColor = Color.Transparent;
            }
            else if ((textBox3.Text == "") && (radioButton8.Checked))
            {
                current = 0;
                panel11.BackColor = Color.Red;
            }


            if (radioButton8.Checked)
            {
                enable_vd_btn();
                enable_result_btn();
            }
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            label87.Visible = false;
            timer2.Enabled = false;
        }

        private void Timer3_Tick(object sender, EventArgs e)
        {
            label88.Visible = false;
            timer3.Enabled = false;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TextBox14_Leave(object sender, EventArgs e)
        {
            if (pfstart > 1)
            {
                MessageBox.Show("Starting Power Factor can't be greater than 1!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CableDataTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDataTable();
        }

        private void CableSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5.OpenSummary();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                lengthunit = comboBox1.Text;
            }
        }

        private void ComboBox16_SelectedIndexChanged(object sender, EventArgs e)
        {
            tempindex = comboBox16.SelectedIndex + 1;

            if (comboBox16.SelectedIndex != -1)
            {
                panel32.BackColor = Color.Transparent;
            }
            else
            {
                panel32.BackColor = Color.Red;
            }

            Updatek1();
            Updatekt();

            SCLTEchanged();
            Calc_k();
            enable_result_btn();
        }

        private void ComboBox17_TextChanged(object sender, EventArgs e)
        {
            if (comboBox17.Text != "")
            {
                initialTemp = double.Parse(comboBox17.Text);
                panel33.BackColor = Color.Transparent;
                comboBox16.Enabled = true;
            }
            else
            {
                initialTemp = 0;
                panel33.BackColor = Color.Red;
                comboBox16.Enabled = false;
            }
            SCLTEchanged();
            Calc_k();
            enable_result_btn();
        }


        private void ComboBox17_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox17.SelectedIndex != -1)
            {
                initialTemp = double.Parse(comboBox17.Text);
                panel33.BackColor = Color.Transparent;
                if ((insulation == "XHHW") || (insulation == "THHW"))
                {
                    if (initialTemp == 75)
                    {
                        insulindex = 2;
                    }
                    else if (initialTemp == 90)
                    {
                        insulindex = 3;
                    }
                }
                comboBox16.Enabled = true;
            }
            else
            {
                initialTemp = 0;
                panel33.BackColor = Color.Red;
                comboBox16.Enabled = false;
            }

            maxtemp_calc();

            Updatek1();
            Updatekt();

            SCLTEchanged();
            Calc_k();
            enable_result_btn();
        }

        private void ComboBox18_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox18.SelectedIndex != -1)
            {
                groupconductor = comboBox18.Text;
                panel34.BackColor = Color.Transparent;
            }
            else
            {
                groupconductor = "";
                panel34.BackColor = Color.Red;
            }

            Updatek2();

            Updatekt();
            
        }

        private void Updatek2()
        {
            if (comboBox18.SelectedIndex == 0)
            {
                k2main = 1;
            }
            else if (comboBox18.SelectedIndex == 1)
            {
                k2main = 0.8;
            }
            else if (comboBox18.SelectedIndex == 2)
            {
                k2main = 0.7;
            }
            else if (comboBox18.SelectedIndex == 3)
            {
                k2main = 0.5;
            }
            else if (comboBox18.SelectedIndex == 4)
            {
                k2main = 0.45;
            }
            else if (comboBox18.SelectedIndex == 5)
            {
                k2main = 0.4;
            }
            else if (comboBox18.SelectedIndex == 6)
            {
                k2main = 0.35;
            }
            else
            {
                k2main = 0;
            }
        }

        private void ComboBox19_SelectedIndexChanged(object sender, EventArgs e)
        {
            Updatek1();
            Updatekt();

            if (comboBox19.SelectedIndex != -1)
            {
                panel35.BackColor = Color.Transparent;
            }
            else
            {
                panel35.BackColor = Color.Red;
            }
        }

        private void Updatek1()
        {
            if (comboBox16.Text != "")
            {
                temperature = double.Parse(comboBox16.Text);
            }
            else
            {
                temperature = 0;
            }

            if (comboBox19.SelectedIndex == 0)
            {
                temperature = temperature + 33;
            }
            else if (comboBox19.SelectedIndex == 1)
            {
                temperature = temperature + 22;
            }
            else if (comboBox19.SelectedIndex == 2)
            {
                temperature = temperature + 17;
            }
            else if (comboBox19.SelectedIndex == 3)
            {
                temperature = temperature + 14;
            }
            else
            {
                if (comboBox16.Text != "")
                {
                    temperature = double.Parse(comboBox16.Text);
                }
                else
                {
                    temperature = 0;
                }
            }

            if ((insulindex != 0) && (tempindex != 0))
            {
                if (temperature > maxtemp)
                {
                    MessageBox.Show("Temperature + added temperature can't be greater than " + maxtemp + " °C !", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    comboBox16.SelectedIndex = -1;
                    comboBox19.SelectedIndex = -1;
                    k1main = 0;
                }
                else
                {
                    k1main = correctionfactor_temperature[(tempindex - 1), (insulindex - 1)];
                }
            }
        }

        private void ComboBox20_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox20.SelectedIndex == 0)
            {
                k3main = 1;
                panel36.BackColor = Color.Transparent;
            }
            else if (comboBox20.SelectedIndex == 1)
            {
                Updatek3();
                panel36.BackColor = Color.Transparent;
            }
            else
            {
                k3main = 0;
                panel36.BackColor = Color.Red;
            }
            Updatekt();
        }

        private void Updatek3()
        {
            if (comboBox20.SelectedIndex != -1)
            {
                if (length >= 6)
                {
                    k3main = 0.95;
                }
                else
                {
                    k3main = 1;
                }
            }
        }

        private void TextBox9_Leave(object sender, EventArgs e)
        {
            if (vdrunmax > 100)
            {
                MessageBox.Show("Vd run can't be greater than 100%!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if ((vdrunmax <= 0) && (textBox9.Text != ""))
            {
                MessageBox.Show("Vd run can't be 0%!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TextBox11_Leave(object sender, EventArgs e)
        {
            if (vdstartmax > 100)
            {
                MessageBox.Show("Vd start can't be greater than 100%!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if ((vdstartmax <= 0) && (textBox11.Text != ""))
            {
                MessageBox.Show("Vd start can't be 0%!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TextBox24_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != decimalseparator))
            {
                e.Handled = true;
            }

            if ((textBox24.Text == "") && (e.KeyChar == decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == decimalseparator) && ((sender as TextBox).Text.IndexOf(decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void Calc_k()
        {
            if (material == "Copper")
            {
                k = 226 * Math.Sqrt(Math.Log(1 + (finalTemp - initialTemp)/(234.5 + initialTemp)));
            }
            else if (material == "Alumunium")
            {
                k = 148 * Math.Sqrt(Math.Log(1 + (finalTemp - initialTemp) / (228 + initialTemp)));
            }

            if (k != 0)
            {
                textBox21.Text = k.ToString("0.###");
            }
            else
            {
                textBox21.Text = "";
            }
        }

        private void TextBox27_TextChanged(object sender, EventArgs e)
        {
            to = textBox27.Text;
        }

        private void TextBox13_TextChanged(object sender, EventArgs e)
        {
            tagno = textBox13.Text;
            if (textBox13.Text == "")
            {
                textBox26.Enabled = false;
                textBox27.Enabled = false;
                label61.Enabled = false;
                label62.Enabled = false;
                textBox16.Enabled = false;
                textBox31.Enabled = false;
                label26.Enabled = false;
                label60.Enabled = false;
                panel13.BackColor = Color.Red;
            }
            else
            {
                textBox26.Enabled = true;
                textBox27.Enabled = true;
                label61.Enabled = true;
                label62.Enabled = true;
                textBox16.Enabled = true;
                textBox31.Enabled = true;
                label26.Enabled = true;
                label60.Enabled = true;
                panel13.BackColor = Color.Transparent;
            }

            enable_vd_btn();
            enable_result_btn();
        }

        private void TextBox21_TextChanged(object sender, EventArgs e)
        {
            if (textBox21.Text != "")
            {
                k = double.Parse(textBox21.Text);
            }
            calc_smin();
        }

        private void TextBox11_TextChanged(object sender, EventArgs e)
        {
            if (textBox11.Text != "")
            {
                vdstartmax = double.Parse(textBox11.Text);
                panel27.BackColor = Color.Transparent;
            }
            else if ((textBox11.Text == "") && (comboBox2.Text == "Motor"))
            {
                vdstartmax = 0;
                panel27.BackColor = Color.Red;
            }
            else
            {
                vdstartmax = 0;
                panel27.BackColor = Color.Transparent;
            }
            enable_vd_btn();
            enable_result_btn();
        }

        private void calc_current()
        {
            if ((TextBox1.Text != "") && (textBox2.Text != "") && (comboBox3.Text != "")
            && (comboBox2.Text != "") && (textBox4.Text != "") && (textBox5.Text != "") && (!radioButton8.Checked))
            {
                if ((phase == "DC") && (loadtype == "Feeder"))
                {
                    current = power * 1000 / voltage;
                    textBox3.Text = current.ToString("0.##");
                    textBox7.Text = "";
                }
                else if (phase == "Single-Phase AC")
                {
                    if (cbPower.Text == "kVA")
                    {
                        power = cplxpower * pf;
                        current = power * 1000 / (voltage * eff * pf);
                    }
                    else
                    {
                        current = power * 1000 / (voltage * eff * pf);
                    }
                    textBox3.Text = current.ToString("0.##");

                    if ((loadtype == "Motor") && (textBox25.Text != ""))
                    {
                        currentstart = current * multiplier;
                        textBox7.Text = currentstart.ToString("0.##");
                    }
                    else
                    {
                        textBox7.Text = "";
                        currentstart = 0;
                    }
                }
                else if (phase == "Three-Phase AC")
                {
                    if (cbPower.Text == "kVA")
                    {
                        current = power * 1000 / (Math.Sqrt(3) * voltage * eff);
                    }
                    else
                    {
                        current = power * 1000 / (Math.Sqrt(3) * voltage * eff * pf);
                    }
                    textBox3.Text = current.ToString("0.##");

                    if ((loadtype == "Motor") && (textBox25.Text != ""))
                    {
                        currentstart = current * multiplier;
                        textBox7.Text = currentstart.ToString("0.##");
                    }
                    else
                    {
                        textBox7.Text = "";
                        currentstart = 0;
                    }
                }
                else
                {
                    textBox3.Text = "";
                    textBox7.Text = "";
                    currentstart = 0;
                }
            }
            else if (!radioButton8.Checked)
            {
                textBox3.Text = "";
            }
            else if (radioButton8.Checked)
            {
                if ((phase == "DC") && (loadtype == "Feeder"))
                {
                    textBox7.Text = "";
                    currentstart = 0;
                }
                else if (((phase == "Single-Phase AC") || (phase == "Three-Phase AC")) && (loadtype == "Motor") && (textBox25.Text != ""))
                {
                    currentstart = current * multiplier;
                    textBox7.Text = currentstart.ToString("0.##");
                }
                else
                {
                    textBox7.Text = "";
                    currentstart = 0;
                }
            }
            
        }

        

        private void reset_correction()
        {
            if (!radioButton7.Checked)
            {
                k1main = 0;
                k2main = 0;
                k3main = 0;
                ktmain = 0;

                textBox17.Text = "";
                textBox18.Text = "";
                textBox36.Text = "";
                textBox19.Text = "";
            }
        }

        private void enable_vd_btn()
        {
            if ((comboBox2.Text != "") && (comboBox3.Text != "") && (comboBox13.Text != "")&& 
                (textBox2.Text != "") && (textBox3.Text != "") && (textBox4.Text != "") && (textBox5.Text != "") && 
                (comboBox6.Text != "") &&(comboBox7.Text != "") && (comboBox8.Text != "") && (textBox9.Text != "") && 
                (comboBox14.Text != "") &&(comboBox9.Text != "") && (textBox19.Text != "") && (textBox6.Text != "") && 
                (textBox12.Text != "") && (comboBox5.Text != "")  && (comboBox4.Text != "") && (textBox13.Text != ""))
            {
                if (((radioButton3.Checked) && (cableCount > 0)) || (radioButton4.Checked))
                {
                    if (loadtype == "Motor")
                    {
                        if ((textBox7.Text != "") && (textBox14.Text != "") && (textBox11.Text != "") && (textBox25.Text != ""))
                        {
                            button7.Enabled = true;
                        }
                        else
                        {
                            button7.Enabled = false;
                        }
                    }
                    else
                    {
                        button7.Enabled = true;
                    }
                }
                else
                {
                    button7.Enabled = false;
                }
            }
            else
            {
                button7.Enabled = false;
            }
        }
        
        private void enable_result_btn()
        {
            if ((comboBox2.Text != "") && (comboBox3.Text != "")&& (textBox2.Text != "") && 
                (textBox3.Text != "") && (textBox4.Text != "") &&
                (textBox5.Text != "") && (textBox9.Text != "") && (textBox6.Text != "") && (textBox12.Text != "") && 
                (comboBox5.Text != "") && (comboBox6.Text != "") && (comboBox9.Text != "") && (textBox19.Text != "") && 
                (comboBox11.Text != "") && (comboBox4.Text != "") && (comboBox7.Text != "") && (comboBox8.Text != "") && 
                (comboBox10.Text != "") && (comboBox12.Text != "")&& (comboBox13.Text != "") && (comboBox14.Text != "") && 
                (textBox13.Text!= "") && (comboBox17.Text != "") && (textBox24.Text != ""))
                {
                    if (((radioButton3.Checked) && (cableCount > 0)) || (radioButton4.Checked))
                    {
                        if (((radioButton2.Checked) && (textBox23.Text != "") && (textBox28.Text != "")) ||
                                ((radioButton1.Checked) && (textBox20.Text != "")))
                        {
                            if (loadtype == "Motor")
                            {
                                if ((textBox7.Text != "") && (textBox14.Text != "") && (textBox11.Text != "") && (textBox25.Text != ""))
                                {
                                    button2.Enabled = true;
                                }
                                else
                                {
                                    button2.Enabled = false;
                                }
                            }
                            else
                            {
                                button2.Enabled = true;
                            }
                        }
                        else
                        {
                            button2.Enabled = false;
                        }
                    }
                    else
                    {
                        button2.Enabled = false;
                    }
                }
                else
                {
                    button2.Enabled = false;
                }
        }

        private bool IsCalculateNeeded()
        {
            if ((textBox23.Text != "") || (textBox28.Text != "") || (textBox20.Text != ""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void breaker_fill()
        {
            comboBox11.Items.Clear();
            if (comboBox12.Text == "MCB")
            {
                if ((comboBox10.Text == "10") && ((voltage == 230) || (voltage == 400)))
                {
                    comboBox11.Items.Insert(0, "2");
                    comboBox11.Items.Insert(1, "4");
                    comboBox11.Items.Insert(2, "6");
                    comboBox11.Items.Insert(3, "10");
                    comboBox11.Items.Insert(4, "16");
                    comboBox11.Items.Insert(5, "25");
                    comboBox11.Items.Insert(6, "32");
                    comboBox11.Items.Insert(7, "40");
                    comboBox11.Items.Insert(8, "50");
                    comboBox11.Items.Insert(9, "63");
                }
                else if ((comboBox10.Text == "25") && ((voltage == 230) || (voltage == 400)))
                {
                    comboBox11.Items.Insert(0, "2");
                    comboBox11.Items.Insert(1, "4");
                    comboBox11.Items.Insert(2, "6");
                    comboBox11.Items.Insert(3, "10");
                    comboBox11.Items.Insert(4, "16");
                    comboBox11.Items.Insert(5, "25");
                    comboBox11.Items.Insert(6, "32");
                    comboBox11.Items.Insert(7, "40");
                    comboBox11.Items.Insert(8, "50");
                    comboBox11.Items.Insert(9, "63");
                }
            }
            else if (comboBox12.Text == "MCCB")
            {
                if ((comboBox10.Text == "10") && (voltage == 230))
                {
                    comboBox11.Items.Insert(0, "4");
                    comboBox11.Items.Insert(1, "6");
                    comboBox11.Items.Insert(2, "10");
                    comboBox11.Items.Insert(3, "16");
                    comboBox11.Items.Insert(4, "25");
                    comboBox11.Items.Insert(5, "32");
                    comboBox11.Items.Insert(6, "40");
                    comboBox11.Items.Insert(7, "50");
                    comboBox11.Items.Insert(8, "63");
                    comboBox11.Items.Insert(9, "80");
                    comboBox11.Items.Insert(10, "100");
                    comboBox11.Items.Insert(11, "125");
                    comboBox11.Items.Insert(12, "160");
                    comboBox11.Items.Insert(13, "250");
                    comboBox11.Items.Insert(14, "400");
                    comboBox11.Items.Insert(15, "630");
                }
                else if ((comboBox10.Text == "25") && (voltage == 400))
                {
                    comboBox11.Items.Insert(0, "4");
                    comboBox11.Items.Insert(1, "6");
                    comboBox11.Items.Insert(2, "10");
                    comboBox11.Items.Insert(3, "16");
                    comboBox11.Items.Insert(4, "20");
                    comboBox11.Items.Insert(5, "25");
                    comboBox11.Items.Insert(6, "32");
                    comboBox11.Items.Insert(7, "40");
                    comboBox11.Items.Insert(8, "50");
                    comboBox11.Items.Insert(9, "63");
                    comboBox11.Items.Insert(10, "80");
                    comboBox11.Items.Insert(11, "100");
                    comboBox11.Items.Insert(12, "125");
                    comboBox11.Items.Insert(13, "160");
                }
            }
        }


        private void break_lte()
        {
            if ((radioButton1.Checked) && (radioButton5.Checked) && (comboBox11.Text != ""))
            {
                if (comboBox12.Text == "MCB")
                {
                    if ((comboBox10.Text == "10") && ((voltage == 230) || (voltage == 400)))
                    {
                        switch (breakcurrent)
                        {
                            case 2:
                                bLTE = 800;
                                break;
                            case 4:
                                bLTE = 6800;
                                break;
                            case 6:
                                bLTE = 13000;
                                break;
                            case 10:
                                bLTE = 35000;
                                break;
                            case 16:
                                bLTE = 40000;
                                break;
                            case 25:
                                bLTE = 46000;
                                break;
                            case 32:
                                bLTE = 56000;
                                break;
                            case 40:
                                bLTE = 56000;
                                break;
                            case 50:
                                bLTE = 88000;
                                break;
                            case 63:
                                bLTE = 88000;
                                break;
                        }
                        sc_wiremin = 2.5;
                        textBox20.Text = bLTE.ToString();
                    }
                    else if ((comboBox10.Text == "25") && ((voltage == 230) || (voltage == 400)))
                    {
                        switch (breakcurrent)
                        {
                            case 2:
                                bLTE = 800;
                                sc_wiremin = 2.5;
                                break;
                            case 4:
                                bLTE = 8500;
                                sc_wiremin = 2.5;
                                break;
                            case 6:
                                bLTE = 15000;
                                sc_wiremin = 2.5;
                                break;
                            case 10:
                                bLTE = 51000;
                                sc_wiremin = 2.5;
                                break;
                            case 16:
                                bLTE = 54000;
                                sc_wiremin = 2.5;
                                break;
                            case 25:
                                bLTE = 66000;
                                sc_wiremin = 2.5;
                                break;
                            case 32:
                                bLTE = 130000;
                                sc_wiremin = 4;
                                break;
                            case 40:
                                bLTE = 130000;
                                sc_wiremin = 4;
                                break;
                            case 50:
                                bLTE = 160000;
                                sc_wiremin = 4;
                                break;
                            case 63:
                                bLTE = 160000;
                                sc_wiremin = 4;
                                break;
                        }
                        textBox20.Text = bLTE.ToString();
                    }
                    else
                    {
                        textBox20.Text = "";
                    }
                }
                else if (comboBox12.Text == "MCCB")
                {
                    if ((comboBox10.Text == "10") && (voltage == 230))
                    {
                        switch (breakcurrent)
                        {
                            case 4:
                                bLTE = 5300;
                                sc_wiremin = 2.5;
                                break;
                            case 6:
                                bLTE = 9400;
                                sc_wiremin = 2.5;
                                break;
                            case 10:
                                bLTE = 18000;
                                sc_wiremin = 2.5;
                                break;
                            case 16:
                                bLTE = 44000;
                                sc_wiremin = 2.5;
                                break;
                            case 25:
                                bLTE = 50000;
                                sc_wiremin = 2.5;
                                break;
                            case 32:
                                bLTE = 50000;
                                sc_wiremin = 2.5;
                                break;
                            case 40:
                                bLTE = 55000;
                                sc_wiremin = 2.5;
                                break;
                            case 50:
                                bLTE = 55000;
                                sc_wiremin = 2.5;
                                break;
                            case 63:
                                bLTE = 55000;
                                sc_wiremin = 2.5;
                                break;
                            case 80:
                                bLTE = 130000;
                                sc_wiremin = 4;
                                break;
                            case 100:
                                bLTE = 130000;
                                sc_wiremin = 4;
                                break;
                            case 125:
                                bLTE = 130000;
                                sc_wiremin = 4;
                                break;
                            case 160:
                                bLTE = 130000;
                                sc_wiremin = 4;
                                break;
                            case 250:
                                bLTE = 1300000;
                                sc_wiremin = 6;
                                break;
                            case 400:
                                bLTE = 1300000;
                                sc_wiremin = 6;
                                break;
                            case 630:
                                bLTE = 1300000;
                                sc_wiremin = 6;
                                break;
                        }
                        textBox20.Text = bLTE.ToString();
                    }
                    else if ((comboBox10.Text == "25") && (voltage == 400))
                    {
                        switch (breakcurrent)
                        {
                            case 4:
                                bLTE = 18000;
                                sc_wiremin = 2.5;
                                break;
                            case 6:
                                bLTE = 30000;
                                sc_wiremin = 2.5;
                                break;
                            case 10:
                                bLTE = 53000;
                                sc_wiremin = 2.5;
                                break;
                            case 16:
                                bLTE = 130000;
                                sc_wiremin = 4;
                                break;
                            case 20:
                                bLTE = 140000;
                                sc_wiremin = 4;
                                break;
                            case 25:
                                bLTE = 150000;
                                sc_wiremin = 4;
                                break;
                            case 32:
                                bLTE = 150000;
                                sc_wiremin = 4;
                                break;
                            case 40:
                                bLTE = 160000;
                                sc_wiremin = 4;
                                break;
                            case 50:
                                bLTE = 160000;
                                sc_wiremin = 4;
                                break;
                            case 63:
                                bLTE = 160000;
                                sc_wiremin = 4;
                                break;
                            case 80:
                                bLTE = 330000;
                                sc_wiremin = 4;
                                break;
                            case 100:
                                bLTE = 330000;
                                sc_wiremin = 4;
                                break;
                            case 125:
                                bLTE = 330000;
                                sc_wiremin = 4;
                                break;
                            case 160:
                                bLTE = 330000;
                                sc_wiremin = 4;
                                break;
                        }
                        textBox20.Text = bLTE.ToString();
                    }
                    else
                    {
                        textBox20.Text = "";
                    }
                }
                else
                {
                    textBox20.Text = "";
                }
            }
            else if ((radioButton1.Checked) && (radioButton6.Checked))
            {
                if (bLTE != 0)
                {
                    textBox20.Text = bLTE.ToString();
                }
            }
        }


        private void cable_lte()
        {
            cLTE = wirearea * wirearea * k * k;
        }

        private void calc_smin()
        {
            if ((textBox28.Text != "") && (textBox23.Text != "") && (textBox21.Text != ""))
            {
                smin = sccurrent * 1000 * Math.Sqrt(tbreaker) / k;
                textBox30.Text = smin.ToString("0.##");
            }
            else
            {
                smin = 0;
                textBox30.Text = "";
            }
        }
        
        private void save_vd_result()
        {
            results[0] = tagno;
            results[1] = from;
            results[2] = fromdesc;
            results[3] = to;
            results[4] = todesc;
            results[5] = phase;
            results[6] = loadtype;
            results[7] = voltage.ToString("0.##");
            results[8] = installation;
            results[9] = power.ToString("0.##");
            results[10] = eff.ToString("0.##");
            results[11] = pf.ToString("0.##");
            results[12] = Math.Sqrt(1 - pf * pf).ToString("0.##");
            results[13] = pfstart.ToString("0.##");
            results[14] = current.ToString("0.##");
            results[15] = currentstart.ToString("0.##");
            results[16] = n.ToString("0.##");
            results[17] = wirearea.ToString("0.##");
            results[18] = Rac.ToString("0.####");
            results[19] = X.ToString("0.####");
            results[20] = Irated.ToString("0.##");
            results[21] = ktmain.ToString("0.##");
            results[22] = iderated.ToString("0.##");
            results[23] = "";
            results[24] = "";
            results[25] = length.ToString("0.##");
            results[26] = lmax.ToString("0.##");
            results[27] = vdrun.ToString("0.##");
            results[28] = vdrunmax.ToString("0.##");
            results[29] = vdstart.ToString("0.##");
            results[30] = vdstartmax.ToString("0.##");
            results[31] = "";
            results[32] = "";
            results[33] = "";
            results[34] = "";
            results[35] = "";
            results[36] = readtemp;
            results[37] = remarks;

            for (int i = 0; i < 37; i++)
            {
                if ((results[i] == "0") || (results[i] == null) || (results[i] == ""))
                {
                    results[i] = "N/A";
                }
            }
        }

        private void save_result()
        {
            results[0] = tagno;
            results[1] = from;
            results[2] = fromdesc;
            results[3] = to;
            results[4] = todesc;
            results[5] = phase;
            results[6] = loadtype;
            results[7] = voltage.ToString("0.##");
            results[8] = installation;
            results[9] = power.ToString("0.##");
            results[10] = eff.ToString("0.##");
            results[11] = pf.ToString("0.##");
            results[12] = Math.Sqrt(1 - pf * pf).ToString("0.##");
            results[13] = pfstart.ToString("0.##");
            results[14] = current.ToString("0.##");
            results[15] = currentstart.ToString("0.##");
            results[16] = n.ToString("0.##");
            results[17] = wirearea.ToString("0.##");
            results[18] = Rac.ToString("0.####");
            results[19] = X.ToString("0.####");
            results[20] = Irated.ToString("0.##");
            results[21] = ktmain.ToString("0.##");
            results[22] = iderated.ToString("0.##");
            results[23] = breakertype;
            results[24] = breakcurrent.ToString("0.##");
            results[25] = length.ToString("0.##");
            results[26] = lmax.ToString("0.##");
            results[27] = vdrun.ToString("0.##");
            results[28] = vdrunmax.ToString("0.##");
            results[29] = vdstart.ToString("0.##");
            results[30] = vdstartmax.ToString("0.##");
            results[31] = sccurrent.ToString("0.##");
            results[32] = tbreaker.ToString("0.##");
            results[33] = cablesizemin.ToString("0.##");
            results[34] = bLTE.ToString("0.##");
            results[35] = cLTE.ToString("0.##");
            results[36] = readtemp;
            results[37] = remarks;

            for (int i = 0; i < 37; i++)
            {
                if ((results[i] == "0") || (results[i] == null) || (results[i] == ""))
                {
                    results[i] = "N/A";
                }
            }

            /*
            //update data
            DataRow dtr = dtdiameter.NewRow();

            //full data
            dtr[1] = tagno;
            dtr[2] = from;
            dtr[3] = fromdesc;
            dtr[4] = to;
            dtr[5] = todesc;
            dtr[6] = phase;
            dtr[7] = loadtype;
            dtr[8] = volSys;
            if (!radioButton8.Checked)
            {
                dtr[9] = false; ;
                dtr[10] = cbPower; //powerdata
                if (Convert.ToString(dtr[9]) == "kW") //power
                {
                    dtr[11] = power;
                }
                else if (Convert.ToString(dtr[9]) == "kV")
                {
                    dtr[11] = cplxpower;
                }
                else if (Convert.ToString(dtr[9]) == "HP")
                {
                    dtr[11] = hp;
                }
                dtr[12] = "";
            }
            else
            {
                dtr[9] = true;
                dtr[10] = "";
                dtr[11] = "";
                dtr[12] = current;
            }
            dtr[13] = voltage;
            dtr[14] = eff;
            dtr[15] = pf;
            dtr[16] = ratedvoltage;
            dtr[17] = material;
            dtr[18] = insulation;
            dtr[19] = armour;
            dtr[20] = outersheath;
            dtr[21] = installation;
            if(radioButton7.Checked)
            {
                dtr[22] = true;
            }
            else
            {
                dtr[22] = false;
            }
            dtr[23] = k1main;
            dtr[24] = k2main;
            dtr[25] = k3main;
            dtr[26] = length;
            dtr[27] = vdrunmax;
            dtr[28] = vdstartmax;
            if (radioButton4.Checked)
            {
                dtr[29] = "vendor";
            }
            else if (radioButton3.Checked)
            {
                dtr[29] = "manual";
            }
            dtr[30] = vdrun;
            dtr[31] = vdstart;
            dtr[32] = lmax;
            dtr[33] = n;
            dtr[34] = cores;
            dtr[35] = */

        }

        private void enable_save()
        {
            button4.Enabled = true;
            toolTip1.SetToolTip(button4, "Add calculated cable size data to data table");
        }

        private void disable_save()
        {
            button4.Enabled = false;
            toolTip1.SetToolTip(button4, null);
        }

    }
}