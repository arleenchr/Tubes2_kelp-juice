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
using Solver;

using System.Drawing.Text;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font myFont;
        Font myFont2;
        Font myFont3;
        Font myFont4;

        bool mapExist = false;

        string solution = "";
        int cntNode = 0;
        List<Solver.Point> points;
        long timeExec = 0;

        char[,] grid = {{'K', 'R', 'R', 'R'},
                        {'X', 'R', 'X', 'T'},
                        {'X', 'T', 'R', 'R'},
                        {'X', 'R', 'X', 'X'}};

        int startRow = -1;
        int startColumn = -1;
        int[] treasures = new int[16];
        int num_treasure = 0;
        string method = "DFS";
        Map map;
        public Form1()
        {
            InitializeComponent();

            pictureBox17.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox17.BackColor = Color.Transparent;
            dataGridView1.Visible = false;

            map = new Map(grid);

            method = "DFS";

            byte[] fontData = Properties.Resources.icomoon;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.icomoon.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.icomoon.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            myFont = new Font(fonts.Families[0], 18.0F);
            //myFont = new Font(myFont, FontStyle.Bold);

            byte[] fontData2 = Properties.Resources.FranxurterTotally;
            IntPtr fontPtr2 = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData2.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData2, 0, fontPtr2, fontData2.Length);
            uint dummy2 = 0;
            fonts.AddMemoryFont(fontPtr2, Properties.Resources.FranxurterTotally.Length);
            AddFontMemResourceEx(fontPtr2, (uint)Properties.Resources.FranxurterTotally.Length, IntPtr.Zero, ref dummy2);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr2);

            myFont2 = new Font(fonts.Families[0], 26.0F);
            myFont2 = new Font(myFont2, FontStyle.Bold);
            label1.Font= myFont2;

            myFont3 = new Font(fonts.Families[0], 30.0F);
            myFont3 = new Font(myFont3, FontStyle.Bold);
            radioButton1.Font= myFont3;
            radioButton2.Font= myFont3;
            radioButton3.Font= myFont3;

            myFont4 = new Font(fonts.Families[0], 22.0F);
            myFont4 = new Font(myFont4, FontStyle.Regular);
            label3.Font= myFont2;
            label2.Font = myFont4;
            label4.Font= myFont4;
            label5.Font= myFont4;
            label6.Font= myFont4;
        }


        private void fillData()
        {
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.DefaultCellStyle.Font = myFont;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dataGridView1.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dataGridView1, true, null);

            for (int i = 0; i < map.cols; i++)
            {
                dataGridView1.Columns.Add("dummy","dummy");
                dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[i].Width = 70;
                dataGridView1.Columns[i].DividerWidth = 5;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool failOpen = false;
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Text files (*.txt)|*.txt";

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                string filename = ofd.FileName;
                try
                {
                    label2.Visible = false;
                    label3.Visible = false;
                    label4.Visible = false;
                    label5.Visible = false;
                    label6.Visible = false;
                    label7.Visible = false;
                    trackBar1.Visible = false;
                    button3.Visible = false;
                    button4.Visible = false;
                    checkBox2.Visible = false;
                    trackBar2.Visible = false;
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();
                    dataGridView1.Refresh();
                    map = Parser.Parse(filename);
                }
                catch (Exception ex)
                {
                    failOpen = true;
                    mapExist = false;
                }

                if (!failOpen)
                {
                    /*
                    label2.Visible = false;
                    label3.Visible = false;
                    label4.Visible = false;
                    label5.Visible = false;
                    label6.Visible = false;
                    trackBar1.Visible = false;
                    button3.Visible = false;
                    button4.Visible = false;
                    checkBox2.Visible = false;
                    */
                    mapExist = true;


                    foreach (char c in filename)
                    {
                        if (c == '\\')
                            label1.Text = "Map chosen : \n";
                        else
                        {
                            label1.Text += c;
                        }
                    }


                    /*
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();
                    dataGridView1.Refresh();
                    */
                    fillData();
                    dataGridView1.Visible = true;

                    for (int i = 0; i < map.rows; i++)
                    {
                        int rowId = dataGridView1.Rows.Add();

                        DataGridViewRow row = dataGridView1.Rows[rowId];

                        for (int k = 0; k < map.cols; k++)
                        {
                            char c = map.grid[i, k];
                            if (c == 'K')
                            {
                                row.Cells[k].Value = "b";

                                startColumn = k;
                                startRow = i;
                            }
                            else if (c == 'R')
                            {
                                row.Cells[k].Value = "";
                            }
                            else if (c == 'T')
                            {
                                row.Cells[k].Value = "c";
                                num_treasure++;
                            }
                            else if (c == 'X')
                            {
                                row.Cells[k].Value = "";
                                row.Cells[k].Style.BackColor = Color.FromArgb(217, 190, 150);

                            }
                        }
                    }
                }
                else
                {
                    string text = "Map input is invalid!";
                    MessageBox.Show(text);
                    label1.Text = "Map chosen:\nNone";
                }
            }
        }

        private void colorPath(int end, Color color)
        {
            if (startRow == -1) return;
            dataGridView1.Rows[startRow].Cells[startColumn].Style.BackColor = color;

            int curRow = startRow;
            int curColumn = startColumn;

            for (int i = 0; i < end; i++)
            {
                char c = solution[i];
                if (c == 'R')
                {
                    curColumn++;
                }
                else if (c == 'L')
                {
                    curColumn--;
                }
                else if (c == 'U')
                {
                    curRow--;
                }
                else if (c == 'D')
                {
                    curRow++;
                }

                Color curColor = dataGridView1.Rows[curRow].Cells[curColumn].Style.BackColor;
                Color nextColor = color;
                if (curColor != Color.White)
                {
                    byte rval = curColor.R;
                    byte gval = curColor.G;
                    byte bval = curColor.B;

                    if (rval - 30 > 0) rval -= 30;
                    if (gval - 30 > 0) gval -= 30;
                    if (bval - 30 > 0) bval -= 30;

                    nextColor = Color.FromArgb(rval, gval, bval);
                }

                colorBox(curRow, curColumn, nextColor);

            }
        }

        private void colorMapAnimate(Color color)
        {
            foreach (Solver.Point p in points)
            {
                Color curColor = dataGridView1.Rows[p.rowId].Cells[p.colId].Style.BackColor;
                Color nextColor = color;
                if (curColor != Color.White)
                {
                    byte rval = curColor.R;
                    byte gval = curColor.G;
                    byte bval = curColor.B;

                    if (rval - 25 > 0) rval -= 25;
                    if (gval - 25 > 0) gval -= 25;
                    if (bval - 25 > 0) bval -= 25;

                    nextColor = Color.FromArgb(rval, gval, bval);
                }
                colorBox(p, nextColor);
                Thread.Sleep(1000);
            }
        }
        private void colorMap(int end, Color color)
        {
            if (startRow == -1) return ;
            //dataGridView1.Rows[startRow].Cells[startColumn].Style.BackColor = color;

            int curRow = startRow;
            int curColumn = startColumn;

            for (int i=0;i<end; i++)
            {
                Solver.Point p = points[i];
                Color curColor = dataGridView1.Rows[p.rowId].Cells[p.colId].Style.BackColor;
                Color nextColor = color;
                if (curColor != Color.White)
                {
                    byte rval = curColor.R;
                    byte gval = curColor.G;
                    byte bval = curColor.B;

                    if (rval - 25 > 0) rval -= 25;
                    if(gval - 25 > 0) gval -= 25;
                    if(bval - 25 > 0)  bval -= 25;

                    nextColor = Color.FromArgb(rval,gval,bval);
                }
                colorBox(p, nextColor);
            }
        }

        private void resetMap(Color color)
        {
            for(int i = 0; i < map.rows; i++)
            {
                for(int j=0; j < map.cols; j++)
                {
                    if (map.grid[i, j] != 'X')
                    {
                        colorBox(i, j, color);
                    }
                }
            }
        }
        private void checkStars()
        {
            for (int i = 0; i < map.rows; i++)
            {
                for (int j = 0; j < map.cols; j++)
                {
                    if (map.grid[i, j] == 'T')
                    {
                        if (dataGridView1.Rows[i].Cells[j].Style.BackColor == Color.White)
                        {
                            dataGridView1.Rows[i].Cells[j].Value = "c";
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells[j].Value = "d";
                        }

                    }
                }
            }
        }

        private void colorBox(int row,int column, Color color)
        {
            dataGridView1.Rows[row].Cells[column].Style.BackColor = color;
        }
        private void colorBox(Solver.Point point, Color color)
        {
            dataGridView1.Rows[point.rowId].Cells[point.colId].Style.BackColor = color;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (mapExist)
            {
                //colorMap(solution.Length, Color.White);
                resetMap(Color.White);
                checkStars();
                if (method == "DFS")
                {
                    Solver.DFSSolver.CallDFS(map, ref solution, ref cntNode, ref points, ref timeExec);
                }
                else if (method == "BFS")
                {
                   Solver.BFSSolver.BFS(map, ref solution, ref cntNode, ref points, ref timeExec);
                }
                else if(method == "TSP")
                {
                    Solver.TSPSolver.CallTSP(map, ref solution, ref cntNode, ref points, ref timeExec);
                }
                checkBox2.Checked = false;
                resetMap(Color.White);
                colorPath(solution.Length, Color.FromArgb(110, 160, 114));
                checkStars();
                /*
                trackBar1.Visible = true;
                trackBar1.Maximum = points.Count;
                trackBar1.Value = 0;
                trackBar1.Value = trackBar1.Maximum;
                */

                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                
                checkBox2.Visible = true;
                label7.Visible = true;
                trackBar2.Visible = true;

                label3.Text = "Path : ";
                foreach (char c in solution)
                {
                    label3.Text += c;
                    label3.Text += " ";
                }
                label4.Text = "Number\nof Times\nNode \nChecked:\n" + cntNode;
                label5.Text = "Executed\nTime:\n" + timeExec + " ms";
                label6.Text = "Path\nLength:\n" + solution.Length;

                
                //colorMap(path.Length, Color.Green);
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            resetMap(Color.White);
            //Point curBox=colorMap(trackBar1.Value, Color.SteelBlue);
            if(trackBar1.Value > 0)
            {
                
                colorMap(trackBar1.Value-1, Color.FromArgb(255,196,23));
            }

            int end = trackBar1.Value - 1;
            if(end<0)
            {
                end = 0;
            }
            Solver.Point endpoint = points[end];
            colorBox(endpoint, Color.SteelBlue);

            checkStars();
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

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private async void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.BackgroundImage = checkBox2.Checked ? Properties.Resources.checkedbox : Properties.Resources.uncheckedbox;
            if (checkBox2.Checked)
            {
                bool normal = true;
                trackBar1.Maximum = points.Count;
                trackBar1.Value = trackBar1.Maximum;
                trackBar1.Value = 0;
                for(int i= 0; i <= trackBar1.Maximum; i++)
                {
                    trackBar1.Value = i;
                    await Task.Delay(trackBar2.Value);
                    if (checkBox2.Checked == false)
                    {
                        normal = false;
                        break;
                    }
                }
                if (normal)
                {
                    trackBar1.Visible = true;
                    button3.Visible = true;
                    button4.Visible = true;
                }
            }
            else
            {
                trackBar1.Visible = false;
                button3.Visible = false;
                button4.Visible = false;

                resetMap(Color.White);
                colorPath(solution.Length, Color.FromArgb(89, 139, 93));
                checkStars();
            }
        }

        // ........................................
        // CODE BELOW ARE MADE FOR DESIGN PURPOSES
        // ........................................
        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackgroundImage = Properties.Resources.button1hover;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackgroundImage = Properties.Resources.button1;
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            button1.BackgroundImage = Properties.Resources.button1pressed;
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            button1.BackgroundImage = Properties.Resources.button1;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackgroundImage = Properties.Resources.button2;


        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackgroundImage = Properties.Resources.button2hover;
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            button2.BackgroundImage = Properties.Resources.button2pressed;
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            button2.BackgroundImage = Properties.Resources.button2;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (trackBar1.Value != trackBar1.Maximum)
            {
                trackBar1.Value++;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(trackBar1.Value != 0)
            {
                trackBar1.Value--;
            }
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.BackgroundImage = Properties.Resources.button3hover;
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            button3.BackgroundImage = Properties.Resources.button3pressed;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackgroundImage = Properties.Resources.button3;
        }

        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            button3.BackgroundImage = Properties.Resources.button3;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.BackgroundImage = Properties.Resources.button4;
        }

        private void button4_MouseUp(object sender, MouseEventArgs e)
        {
            button4.BackgroundImage = Properties.Resources.button4;
        }

        private void button4_MouseDown(object sender, MouseEventArgs e)
        {
            button4.BackgroundImage = Properties.Resources.button4pressed;
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            button4.BackgroundImage = Properties.Resources.button4hover;
        }

        private void checkBox2_MouseEnter(object sender, EventArgs e)
        {
            checkBox2.BackgroundImage = checkBox2.Checked ? Properties.Resources.checkedboxhover : Properties.Resources.uncheckedboxhover;
        }

        private void checkBox2_MouseLeave(object sender, EventArgs e)
        {
            checkBox2.BackgroundImage = checkBox2.Checked ? Properties.Resources.checkedbox : Properties.Resources.uncheckedbox;
        }

        private void checkBox2_MouseDown(object sender, MouseEventArgs e)
        {
            checkBox2.BackgroundImage = checkBox2.Checked ? Properties.Resources.checkedboxpressed : Properties.Resources.uncheckedboxpressed;
        }

        private void checkBox2_MouseUp(object sender, MouseEventArgs e)
        {
            checkBox2.BackgroundImage = checkBox2.Checked ? Properties.Resources.checkedbox : Properties.Resources.uncheckedbox;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            checkBox2.Checked = checkBox2.Checked ? false : true;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

        }
    }
}
