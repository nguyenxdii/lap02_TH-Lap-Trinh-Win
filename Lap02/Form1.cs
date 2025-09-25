using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lap02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                double num1 = double.Parse(txtNum1.Text);
                double num2 = double.Parse(txtNum2.Text);
                double result = num1 + num2;
                txtAnswer.Text = result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi: " + ex.Message, "Thong Bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            try
            {
                double num1 = double.Parse(txtNum1.Text);
                double num2 = double.Parse(txtNum2.Text);
                double result = num1 - num2;
                txtAnswer.Text = result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi: " + ex.Message, "Thong Bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMul_Click(object sender, EventArgs e)
        {
            try
            {
                double num1 = double.Parse(txtNum1.Text);
                double num2 = double.Parse(txtNum2.Text);
                double result = num1 * num2;
                txtAnswer.Text = result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi: " + ex.Message, "Thong Bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDiv_Click(object sender, EventArgs e)
        {
            try
            {
                double num1 = double.Parse(txtNum1.Text);
                double num2 = double.Parse(txtNum2.Text);
                double result = num1 / num2;

                if (num2 == 0)
                {
                    throw new DivideByZeroException("Khong the chia 0");
                }

                txtAnswer.Text = result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi: " + ex.Message, "Thong Bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //luon chay du loi hay k
                Console.WriteLine("Da thuc hien phep chia");
            }
        }
    }
}
