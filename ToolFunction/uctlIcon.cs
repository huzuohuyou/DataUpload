using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ToolFunction
{
    public partial class pe : UserControl
    {
        public pe()
        {
            InitializeComponent();
           
        }

        private void peicon_MouseEnter(object sender, EventArgs e)
        {
            peicon.Location = new Point(peicon.Location.X - 4, peicon.Location.Y - 4);
            peicon.Size = new Size(95, 100);
            peicon.BackColor = System.Drawing.Color.FromArgb(192, 192, 255); ;
        }

        private void peicon_MouseLeave(object sender, EventArgs e)
        {
            peicon.Location = new Point(peicon.Location.X + 4, peicon.Location.Y + 4);
            peicon.Size = new Size(88, 92);
            peicon.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
        }
        public  void setImage(string filepath)
        {
            peicon.Image = BytToImg(getImageByte(filepath));
        }

          /// <summary>
        /// ����ͼƬ·������ͼƬ���ֽ���byte[]
        /// </summary>
        /// <param name="imagePath">ͼƬ·��</param>
        /// <returns>���ص��ֽ���</returns>
        private static byte[] getImageByte(string imagePath)
        {
            FileStream files = new FileStream(imagePath, FileMode.Open);
            byte[] imgByte = new byte[files.Length];
            files.Read(imgByte, 0, imgByte.Length);
            files.Close();
            return imgByte;
        }
        /// <summary>
        /// �ֽ���ת����ͼƬ
        /// </summary>
        /// <param name="byt">Ҫת�����ֽ���</param>
        /// <returns>ת���õ���Image����</returns>
        public static Image BytToImg(byte[] byt)
        {
            MemoryStream ms = new MemoryStream(byt);
            Image img = Image.FromStream(ms);
            return img;
        } 
       
    }
}
