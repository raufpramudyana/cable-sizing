﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test1
{
    public partial class Form6 : Form
    {
        public static double[,] cabledata = new double[17, 5];
        public static double[,] confirmedcabledata;
        int cablecount;
        bool[] datAvailable = new bool[17]; 
        int nMax = 17;
        int nValid = 0;
        bool inValid;
        public static bool okclicked = false;

        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            int nValid = 0;
            okclicked = false;

            cabledata[0, 0] = 1.5;
            cabledata[1, 0] = 2.5;
            cabledata[2, 0] = 4;
            cabledata[3, 0] = 6;
            cabledata[4, 0] = 10;
            cabledata[5, 0] = 16;
            cabledata[6, 0] = 25;
            cabledata[7, 0] = 35;
            cabledata[8, 0] = 50;
            cabledata[9, 0] = 70;
            cabledata[10, 0] = 95;
            cabledata[11, 0] = 120;
            cabledata[12, 0] = 150;
            cabledata[13, 0] = 185;
            cabledata[14, 0] = 240;
            cabledata[15, 0] = 300;
            cabledata[16, 0] = 400;

            for (int i = 0; i <17; i++)
            {
                dataGridView1.RowCount++;
                dataGridView1.Rows[i].Cells[0].Value = cabledata[i, 0];
                dataGridView1.Rows[i].Cells[0].ReadOnly = true;
            }

        }

        private void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.KeyPress += new KeyPressEventHandler(DataGridView1_KeyPress);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }

        private void DataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Form1.decimalseparator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == Form1.decimalseparator) && ((sender as TextBox).Text.IndexOf(Form1.decimalseparator) > -1))
            {
                e.Handled = true;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            tableToArray();
            inValid = cekValidasiTable();
            if (!inValid)
            {
                Form1.cableCount = cablecount;
                okclicked = true;
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid: All cells in a row must be either filled entirely or left empty!", "Input Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void DataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void tableToArray()
        {
            bool terisi;
            for (int i = 0; i < nMax; i++)
            {
                int j = 1;
                terisi = true;
                while (j < 5)
                {
                    cabledata[i, j] = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                    if (cabledata[i, j] == 0)
                    {
                        terisi = false;
                    }
                    j++;
                }
                if (terisi)
                {
                    datAvailable[i] = true;
                }
                else
                {
                    datAvailable[i] = false;
                }
            }
        }
        private bool cekValidasiTable()
        {
            bool inVal;
            int count;
            inVal = false;
            int i = 0;
            while ((i <nMax) && !inVal)
            {
                int j = 1;
                count = 0;
                while (j < 5)
                {
                    if(cabledata[i, j] == 0)
                    {
                        count++;
                    }
                    j++;
                }
                if ((count < 4) && (count > 0))
                {
                    inVal = true;
                }
                i++;
            }
            return inVal;
        }

        private void finalCableData()
        {
            cablecount = 0;
            for (int i = 0; i <nMax; i++)
            {
                if (datAvailable[i])
                {
                    for (int j = 0; j < 5; j++)
                    {
                        confirmedcabledata = new double[17, 5];
                        confirmedcabledata[cablecount, j] = cabledata[i, j];
                        cablecount++;
                    }
                }
            }
        }
    }
}