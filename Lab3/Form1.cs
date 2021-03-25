using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3
{
    public partial class Form1 : Form
    {
        CustomArray MyArray = new CustomArray();
        public Form1()
        {
            InitializeComponent();
            radioButton1.Checked = true;
            button_read_F1.Enabled = false;
            button_read_F2.Enabled = false;
            button_write_F1.Enabled = false;
            button_write_F2.Enabled = false;
        }

        private void CheckInput(TextBox textBox, object sender, KeyPressEventArgs e)
        {
            if ((!char.IsDigit(e.KeyChar)||textBox.Text.Length>9) && e.KeyChar != (char)Keys.Back)
            {
                e.KeyChar = (char)Keys.None;
            }
        }

        private void variable_N_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                CheckInput(variable_N, sender, e);
            }
            catch (System.FormatException)
            {
            }
        }

        private void variable_A_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                CheckInput(variable_A, sender, e);
            }
            catch (System.FormatException)
            {
            }
        }

        private void variable_B_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                CheckInput(variable_B, sender, e);
            }
            catch(System.FormatException)
            {
            }
        }

        private void variable_C_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                CheckInput(variable_C, sender, e);
            }
            catch (System.FormatException)
            {
            }
        }

        private void button_generate_Click(object sender, EventArgs e)
        {
            try
            {
                MyArray.GenerateArray(int.Parse(variable_N.Text),
                    int.Parse(variable_A.Text), int.Parse(variable_B.Text));
                textBox_array.Text = string.Join(" ", MyArray.GetArray());
                dgV.ReadOnly = true;
                if (radioButton1.Checked)
                {
                    dgV.ColumnCount = 2;
                    dgV.RowCount = 2;
                    int[] res = MyArray.SumLessAndGreater(MyArray.GetArray()
                        , int.Parse(variable_C.Text));
                    dgV[0, 0].Value = "Меньше";
                    dgV[0, 1].Value = "Больше";
                    dgV[1,0].Value = res[0];
                    dgV[1,1].Value = res[1];

                }

                if (radioButton2.Checked)
                {
                    dgV.ColumnCount = 2;
                    int[] res = MyArray.SortArray(MyArray.GetArray());
                    dgV[0,0].Value = "i";
                    dgV[1,0].Value= "Value";
                    dgV.RowCount = res.Length+1;
                    for(int i = 0; i < res.Length; i++)
                    {
                        dgV[0, i+1].Value = i;
                        dgV[1, i+1].Value = res[i];
                    }
                }

                if (radioButton3.Checked)
                {
                    dgV.ColumnCount = 2;
                    dgV.RowCount = 2;
                    dgV[0, 0].Value = "Количество";
                    dgV[0, 1].Value = "Сумма";
                    int[] res = MyArray.SumAndCountEven(MyArray.GetArray(),
                        int.Parse(variable_C.Text));
                    dgV[1, 0].Value = res[1];
                    dgV[1, 1].Value = res[0];
                }
                if (radioButton4.Checked)
                {
                    dgV.ColumnCount = 1;
                    dgV.RowCount = 2;
                    int res = MyArray.CountPrimes(MyArray.GetArray());
                    dgV[0, 0].Value = "Количество";
                    dgV[0, 1].Value = res;
                }

                button_read_F1.Enabled = true;
                button_read_F2.Enabled = true;
                button_write_F1.Enabled = true;
                button_write_F2.Enabled = true;
            }
            catch (System.FormatException exception)
            {
                MessageBox.Show("Введены некорректные данные");
            }
        }
    }

    public class CustomArray
    {
        int[] arr;
        public void GenerateArray(int size,int min, int max)
        {
            arr = new int[size];
            Random r = new Random();
            for (int i = 0; i < size; i++)
                arr[i] = r.Next(min, max);
        }

        public int[] GetArray()
        {
            return arr;
        }

        public int[]SumLessAndGreater(int[]array,int C)
        {
            int[] sum = new int[2];
            int sum_less = 0, sum_greater = 0;
            for(int i = 1; i < array.Length; i+=2)
            {
                if (array[i] < C)
                    sum_less += array[i];
                if (array[i] > C)
                    sum_greater += array[i];
            }
            sum[0] = sum_less;
            sum[1] = sum_greater;
            return sum;
        }

        public int[] SortArray(int[] array)
        {
            int[] res = new int[array.Length];
            Array.Copy(array,res,array.Length);
            Array.Sort(res);
            return res;
        }

        public int[] SumAndCountEven(int[] array, int C)
        {
            int[] res = new int[2];
            int count = 0, sum = 0;
            for(int i = 2; i < array.Length; i+=2)
            {
                if (array[i] > C)
                {
                    sum += array[i];
                    count++;
                }
            }
            res[0] = sum;
            res[1] = count;
            return res;
        }

        private bool IsPrimeNumber(uint n)
        {
            var result = true;

            if (n > 1)
            {
                for (var i = 2u; i < n; i++)
                {
                    if (n % i == 0)
                    {
                        result = false;
                        break;
                    }
                }
            }
            else
            {
                result = false;
            }

            return result;
        }

        public int CountPrimes(int[] array)
        {
            int count = 0;
            for(int i = 0; i < array.Length; i++)
            {
                if (array[i] > 0)
                    if (IsPrimeNumber((uint)array[i]))
                        count++;
            }
            return count;
        }
    }
}
