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
    public partial class uctlIcon2 : UserControl
    {
        private   string onpath = null;
        private   string offpath = null;
        public uctlIcon2()
        {
            InitializeComponent();
            //peicon.Image = BytToImg(getImageByte(onpath));
        }

        public  void  setPath(string on,string off)
        {
            peicon.Image = BytToImg(getImageByte(off));
            onpath = on;
            offpath = off;
        }

        private void pictureEdit1_MouseEnter(object sender, EventArgs e)
        {
            peicon.Image = BytToImg(getImageByte(onpath));
        }

        private void pictureEdit1_MouseLeave(object sender, EventArgs e)
        {
            peicon.Image = BytToImg(getImageByte(offpath));
        }
        public void setImage(string filepath)
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

        private void peicon_Click(object sender, EventArgs e)
        {

        } 
    }
}
