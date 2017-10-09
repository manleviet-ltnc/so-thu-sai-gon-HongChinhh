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
namespace SoThuXiGon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void ListBox_MouseDown(object sender, MouseEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            int index = lb.IndexFromPoint(e.X, e.Y);

            if (index != -1)

                lb.DoDragDrop(lb.Items[index].ToString(),
                                  DragDropEffects.Copy);
            // xu li ham su kien
        }
        private void ListBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.Move;

        }


        private void lstDanhSach_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                bool test = false;
                for (int i = 0; i < lstDanhSach.Items.Count; i++)
                {
                    string st = lstDanhSach.Items[i].ToString();
                    string data = e.Data.GetData(DataFormats.Text).ToString();
                    if (data == st)
                        test = true;
                }
                if (test == false)
                {
                    
                    int indexOfItemUnderMouseToDrop =
                     lstDanhSach.IndexFromPoint(lstDanhSach.PointToClient(
                 new Point(e.X, e.Y)));
                    // lấy tọa độ


                    if (indexOfItemUnderMouseToDrop >= 0 &&
                     indexOfItemUnderMouseToDrop < lstDanhSach.Items.Count)
                    {
                        lstDanhSach.Items.Insert(indexOfItemUnderMouseToDrop,
                         e.Data.GetData(DataFormats.Text));
                    }
                    else
                    {
                        // add the selected string to bottom of list
                        lstDanhSach.Items.Add(e.Data.GetData(DataFormats.Text));
                    }


                   
                }

            }

        }
       
            
        
        // Thêm item vào listbox Danh sách thú tại đúng vị trí con trỏ chuột.

        private void lstDanhSach_DragOver(object sender,
                                    System.Windows.Forms.DragEventArgs e)
            {
            int indexOfItemUnderMouseToDrop =
               lstDanhSach.IndexFromPoint(lstDanhSach.PointToClient(
                 new Point(e.X, e.Y)));
            // lấy tọa độ
            if (indexOfItemUnderMouseToDrop != ListBox.NoMatches)
            {
                label1.Text = "\'" + e.Data.GetData(DataFormats.Text) + "\'" +
                  " will be placed  before item #" + (indexOfItemUnderMouseToDrop + 1) +
                      "\n which is " + lstDanhSach.SelectedItem;
                lstDanhSach.SelectedIndex = indexOfItemUnderMouseToDrop;
            }
            else
            {
                label1.Text = "\'" + e.Data.GetData(DataFormats.Text) + "\'" +
                  " will be added to the bottom of the listBox.";
                lstDanhSach.SelectedIndex = indexOfItemUnderMouseToDrop;
                
            }
            if (e.Effect == DragDropEffects.Move)
                lstDanhSach.Items.Remove((string)e.Data.GetData(DataFormats.Text));
            // fill  the info listBox

        }
        private void Save(object sender, EventArgs e)
        {
            // mo tap tin 
            StreamWriter write = new StreamWriter("danhsachthu.txt");
            if (write == null) return;

            foreach (var item in lstDanhSach.Items)

                write.WriteLine(item.ToString());
            write.Close();

        }


        private void mnuClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuLoad_Click(object sender, EventArgs e)
        {

            StreamReader reader = new StreamReader("thumoi.txt");

            if (reader == null) return;

            string input = null;
            while ((input = reader.ReadLine()) != null)
            {
                lstThuMoi.Items.Add(input);
            }
            reader.Close();

            using (StreamReader rs = new StreamReader("danhsachthu.txt"))
            {

                input = null;
                while ((input = rs.ReadLine()) != null)
                {
                    lstDanhSach.Items.Add(input);
                }
            }



        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = String.Format(" Bây giờ là {0}:{1}:{2} ngày {3} tháng {4} năm {5}",
                                          DateTime.Now.Hour,
                                          DateTime.Now.Minute,
                                          DateTime.Now.Second,
                                          DateTime.Now.Date,
                                          DateTime.Now.Month,
                                          DateTime.Now.Year);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblTime.Enabled = true;
        }

        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstDanhSach.Items.Remove(lstDanhSach.SelectedItem);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            lstDanhSach.Items.Remove(lstDanhSach.SelectedItem);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StreamWriter write = new StreamWriter("danhsachthu.txt");
            if (write == null) return;

            foreach (var item in lstDanhSach.Items)

                write.WriteLine(item.ToString());
            write.Close();
        }
    }
}