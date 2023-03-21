using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Configuration;
using System.Reflection;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        PictureBox[] pictureMap = new PictureBox[16];
        string path = "RDDRRU";
        string tresImagePath = @"..\..\resources\treasure.png";
        string startImagePath = @"..\..\resources\start.png";
        string pathImagePath = @"..\..\resources\path.png";
        string stoneImagePath = @"..\..\resources\stone.png";
        string buttonImagePath = @"..\..\resources\button1.png";
        string buttonHoverImagePath = @"..\..\resources\button1hover.png";
        string buttonPressImagePath = @"..\..\resources\button1pressed.png";
        string button2ImagePath = @"..\..\resources\button2.png";
        string button2HoverImagePath = @"..\..\resources\button2hover.png";
        string button2PressImagePath = @"..\..\resources\button2pressed.png";
        string button3ImagePath = @"..\..\resources\buttonSmall.png";
        string button3HoverImagePath = @"..\..\resources\buttonSmallHover.png";
        string button3PressImagePath = @"..\..\resources\buttonSmallPressed.png";
        string mapImagePath = @"..\..\resources\map.png";
        string titleImagePath = @"..\..\resources\title.png";

        int start = -1;
        int startRow = -1;
        int startColumn = -1;
        int[] treasures = new int[16];
        int num_treasure = 0;
        string method = "DFS";
        public Form1()
        {
            InitializeComponent();
            pictureMap[0] = pictureBox1;
            pictureMap[1] = pictureBox2;
            pictureMap[2] = pictureBox3;
            pictureMap[3] = pictureBox4;
            pictureMap[4] = pictureBox5;
            pictureMap[5] = pictureBox6;
            pictureMap[6] = pictureBox7;
            pictureMap[7] = pictureBox8;
            pictureMap[8] = pictureBox9;
            pictureMap[9] = pictureBox10;
            pictureMap[10] = pictureBox11;
            pictureMap[11] = pictureBox12;
            pictureMap[12] = pictureBox13;
            pictureMap[13] = pictureBox14;
            pictureMap[14] = pictureBox15;
            pictureMap[15] = pictureBox16;

            button1.BackgroundImage = Image.FromFile(buttonImagePath);
            button1.BackColor = Color.Transparent;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.ForeColor = System.Drawing.Color.White;

            button2.BackgroundImage = Image.FromFile(button2ImagePath);
            button2.BackColor = Color.Transparent;
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button2.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button2.ForeColor = System.Drawing.Color.White;

            pictureBox17.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox17.BackColor = Color.Transparent;
            dataGridView1.Visible = false;

            
        }


        private void fillData()
        {
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dataGridView1.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dataGridView1, true, null);
            for (int i = 0; i < 4; i++)
            {
                //DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                dataGridView1.Columns.Add("dummy","dummy");
                dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[i].Width = 85;
                /*
                imgCol.Width = 80;
                imgCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
                imgCol.DefaultCellStyle.SelectionBackColor = Color.Black;
                */
            }
            for(int i=0;i < 4; i++)
            {
                
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Text files (*.txt)|*.txt|All Files (*.*)|*.*";

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                string filename = ofd.FileName;
                foreach(char c in filename)
                {
                    if (c == '\\')
                        label1.Text = "Map chosen : \n";
                    else
                    {
                        label1.Text += c;
                    }
                }
                
                string[] map = new string[5];
                int i = 0;
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.Refresh();
                fillData();
                dataGridView1.Visible = true;

                foreach (string lines in File.ReadAllLines(ofd.FileName))
                {
                    int rowId = dataGridView1.Rows.Add();

                    DataGridViewRow row = dataGridView1.Rows[rowId];

                    int j = 0;
                    foreach(char c in lines)
                    {
                        if (c == 'K')
                        {
                            //row.Cells[j].Value = Image.FromFile(startImagePath);
                            row.Cells[j].Value = "START";
                            

                            pictureMap[4*i + j].Visible = true;
                            pictureMap[4 * i + j].ImageLocation = startImagePath;
                            pictureMap[4 * i + j].SizeMode = PictureBoxSizeMode.StretchImage;
                            start = 4 * i + j;
                            startColumn = j;
                            startRow = i;
                            j++;
                        }
                        else if (c == 'R')
                        {
                            row.Cells[j].Value = "";

                            pictureMap[4 * i + j].Visible = true;
                            j++;
                        }
                        else if (c == 'T')
                        {
                            row.Cells[j].Value = "GOAL";

                            pictureMap[4 * i + j].Visible = true;
                            treasures[num_treasure] = 4 * i + j;
                            pictureMap[4 * i + j].ImageLocation = tresImagePath;
                            pictureMap[4 * i + j].SizeMode = PictureBoxSizeMode.StretchImage;
                            j++;
                            num_treasure++;
                        }
                        else if (c == 'X')
                        {
                            row.Cells[j].Value = "";

                            row.Cells[j].Style.BackColor = Color.FromArgb(217, 190, 150);
                            j++;

                        }
                    }
                    i++;
                }
                
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private int colorMap(int end, Color color)
        {
            if (startRow == -1) return 0;
            dataGridView1.Rows[startRow].Cells[startColumn].Style.BackColor = color;

            int curRow = startRow;
            int curColumn = startColumn;

            pictureMap[start].BackColor = color;
            int curBox = start;
            for (int i=0;i<end; i++)
            {
                char c = path[i];
                if (c == 'R')
                {
                    curColumn++;
                    dataGridView1.Rows[curRow].Cells[curColumn].Style.BackColor = color;

                    curBox += 1;
                    pictureMap[curBox].BackColor = color;
                }
                else if (c == 'L')
                {
                    curColumn--;
                    dataGridView1.Rows[curRow].Cells[curColumn].Style.BackColor = color;

                    curBox -= 1;
                    pictureMap[curBox].BackColor = color;
                }
                else if (c == 'U')
                {
                    curRow--;
                    dataGridView1.Rows[curRow].Cells[curColumn].Style.BackColor = color;

                    curBox -= 4;
                    pictureMap[curBox].BackColor = color;
                }
                else if (c == 'D')
                {
                    curRow++;
                    dataGridView1.Rows[curRow].Cells[curColumn].Style.BackColor = color;

                    curBox += 4;
                    pictureMap[curBox].BackColor = color;
                }

            }
            return curBox;
        }

        private void colorBox(int curBox, Color color)
        {
            pictureMap[curBox].BackColor = color;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            trackBar1.Visible = true;
            trackBar1.Maximum = path.Length;
            trackBar1.Value = trackBar1.Maximum;
            //colorMap(path.Length, Color.Green);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            colorMap(path.Length,Color.White);
            int curBox=colorMap(trackBar1.Value, Color.SteelBlue);
            if(trackBar1.Value > 0)
            {
                colorMap(trackBar1.Value-1, Color.Khaki);
            }
            colorBox(curBox, Color.SteelBlue);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            method = "DFS";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            method = "BFS";
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            method = "TSP";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackgroundImage = Image.FromFile(buttonHoverImagePath);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackgroundImage = Image.FromFile(buttonImagePath);
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            button1.BackgroundImage= Image.FromFile(buttonPressImagePath);
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            button1.BackgroundImage = Image.FromFile(buttonImagePath);
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackgroundImage = Image.FromFile(button2ImagePath);
            
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackgroundImage = Image.FromFile(button2HoverImagePath);
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            button2.BackgroundImage = Image.FromFile(button2PressImagePath);
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            button2.BackgroundImage= Image.FromFile(button2ImagePath);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }
}
