using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV; //opencv 主要函式
using Emgu.CV.CvEnum; //CV列舉項
using Emgu.CV.Cvb; //CVblob 功能
using Emgu.CV.Structure; //色彩形態定義
using Emgu.CV.UI; //imgbox顯示功能
using Emgu.CV.Util; //特殊形別定義 CV使用
using Emgu.Util;

namespace LayerSepartion_2023_12_22
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //使用Image<>中 包含的色彩模型
        //     Color type of this image (either Gray, Bgr, Bgra, Hsv, Hls, Lab, Luv, Xyz, Ycc,
        //     Rgb or Rbga)


        Image<Bgr, byte> img = null;    //設定原影像


        //讀取影像
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog Openfile = new OpenFileDialog();
            Openfile.Filter = "BMP 檔 (*.bmp) |*.bmp|" + "JPG 檔 (*.jpg) |*.jpg|" + "PNG 檔 (*.png) |*.png";
            Openfile.Title = "選擇一個圖片檔案 ";
            if (Openfile.ShowDialog() == DialogResult.OK)
            {
                img = new Image<Bgr, byte>(Openfile.FileName);
                imageBox1.Image = img;
                imageBox6.Image = img;

                Image<Gray, byte> B = new Image<Gray, byte>(img[0].ToBitmap());
                Image<Gray, byte> G = new Image<Gray, byte>(img[1].ToBitmap());
                Image<Gray, byte> R = new Image<Gray, byte>(img[2].ToBitmap());

                imageBox3.Image = R;
                imageBox4.Image = G;
                imageBox5.Image = B;

            }
        }

        //啟動時設定
        private void Form1_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = true;   //設定RGB = true
            //checkBox2.Checked = false;  //設定HLS = false
            //checkBox3.Checked = false;  //設定HSV = false
            //checkBox4.Checked = false;  //設定Lab = false
            //checkBox5.Checked = false;  //設定Luv = false
            //checkBox6.Checked = false;  //設定Xyz = false
            //checkBox7.Checked = false;  //設定Ycc = false
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;  //設定HLS = false
                checkBox3.Checked = false;  //設定HSV = false
                checkBox4.Checked = false;  //設定Lab = false
                checkBox5.Checked = false;  //設定Luv = false
                checkBox6.Checked = false;  //設定Xyz = false
                checkBox7.Checked = false;  //設定Ycc = false

                if (img == null)
                    return;

                //灰階直與文字名稱更改
                Image<Gray, byte> ImgB = new Image<Gray, byte>(img[0].ToBitmap());
                Image<Gray, byte> ImgG = new Image<Gray, byte>(img[1].ToBitmap());
                Image<Gray, byte> ImgR = new Image<Gray, byte>(img[2].ToBitmap()); 
                imageBox3.Image = ImgR;
                label2.Text = "R";
                imageBox4.Image = ImgG;
                label3.Text = "G";
                imageBox5.Image = ImgB;
                label4.Text = "B";

                imageBox2.Image = img;
                imageBox6.Image = img;
            }
        }

        Image<Hls, byte> Hls = null;
        Image<Hsv, byte> Hsv = null;
        Image<Lab, byte> Lab = null;
        Image<Luv, byte> Luv = null;
        Image<Xyz, byte> Xyz = null;
        Image<Ycc, byte> Ycc = null;
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)//設定HLS = true
            {
                checkBox1.Checked = false;  //設定RGB = false
                checkBox3.Checked = false;  //設定HSV = false
                checkBox4.Checked = false;  //設定Lab = false
                checkBox5.Checked = false;  //設定Luv = false
                checkBox6.Checked = false;  //設定Xyz = false
                checkBox7.Checked = false;  //設定Ycc = false

                //RGB 轉 Hls
                Hls = img.Convert<Hls, byte>();
                //儲存在資料夾
                CvInvoke.Imwrite(@"D:\C#_test\LayerSepartion_2023_12_22\hlsImage.bmp", Hls);
                //無法用ImageBox直接顯示，RGB色彩模型之變數名稱hls，複製(CopyTo) Hls
                Image<Bgr, byte> hls = new Image<Bgr, byte>(Hls.Size);
                Hls.CopyTo(hls); 

                imageBox2.Image = hls;  //顯示Hls色彩模型影像
                imageBox6.Image = hls;  //顯示Hls色彩模型影像

                //灰階直與文字名稱更改
                Image<Gray, byte> HlsImgH = new Image<Gray, byte>(Hls[0].ToBitmap());
                Image<Gray, byte> HlsImgL = new Image<Gray, byte>(Hls[1].ToBitmap());
                Image<Gray, byte> HlsImgS = new Image<Gray, byte>(Hls[2].ToBitmap());
                imageBox3.Image = HlsImgH;
                label2.Text = "H";
                imageBox4.Image = HlsImgL;
                label3.Text = "L";
                imageBox5.Image = HlsImgS;
                label4.Text = "S";
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)//設定HSV = true
            {
                checkBox1.Checked = false;  //設定RGB = false
                checkBox2.Checked = false;  //設定HLS = false
                checkBox4.Checked = false;  //設定Lab = false
                checkBox5.Checked = false;  //設定Luv = false
                checkBox6.Checked = false;  //設定Xyz = false
                checkBox7.Checked = false;  //設定Ycc = false

                //RGB 轉 Hsv
                Hsv = img.Convert<Hsv, byte>();
                //儲存在資料夾
                CvInvoke.Imwrite(@"D:\C#_test\LayerSepartion_2023_12_22\hsvImage.bmp", Hsv);
                //無法用ImageBox直接顯示，RGB色彩模型之變數名稱hsv，複製(CopyTo) Hsv
                Image<Bgr, byte> hsv = new Image<Bgr, byte>(Hsv.Size);
                Hsv.CopyTo(hsv);

                imageBox2.Image = hsv;  //顯示Hsv色彩模型影像
                imageBox6.Image = hsv;  //顯示Hsv色彩模型影像

                //灰階直與文字名稱更改
                Image<Gray, byte> HsvImgH = new Image<Gray, byte>(Hsv[0].ToBitmap());
                Image<Gray, byte> HsvImgS = new Image<Gray, byte>(Hsv[1].ToBitmap());
                Image<Gray, byte> HsvImgV = new Image<Gray, byte>(Hsv[2].ToBitmap());
                imageBox3.Image = HsvImgH;
                label2.Text = "H";
                imageBox4.Image = HsvImgS;
                label3.Text = "S";
                imageBox5.Image = HsvImgV;
                label4.Text = "V";
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)//設定Lab = true
            {
                checkBox1.Checked = false;  //設定RGB = false
                checkBox2.Checked = false;  //設定HLS = false
                checkBox3.Checked = false;  //設定HSV = false
                checkBox5.Checked = false;  //設定Luv = false
                checkBox6.Checked = false;  //設定Xyz = false
                checkBox7.Checked = false;  //設定Ycc = false

                //RGB 轉 Lab
                Lab = img.Convert<Lab, byte>();
                //儲存在資料夾
                CvInvoke.Imwrite(@"D:\C#_test\LayerSepartion_2023_12_22\labImage.bmp", Lab);
                //無法用ImageBox直接顯示，RGB色彩模型之變數名稱lab，複製(CopyTo) Lab
                Image<Bgr, byte> lab = new Image<Bgr, byte>(Lab.Size);
                Lab.CopyTo(lab);

                imageBox2.Image = lab;  //顯示Hsv色彩模型影像
                imageBox6.Image = lab;  //顯示Hsv色彩模型影像

                //灰階直與文字名稱更改
                Image<Gray, byte> LabImgL = new Image<Gray, byte>(Lab[0].ToBitmap());
                Image<Gray, byte> LabImgA = new Image<Gray, byte>(Lab[1].ToBitmap());
                Image<Gray, byte> LabImgB = new Image<Gray, byte>(Lab[2].ToBitmap());
                imageBox3.Image = LabImgL;
                label2.Text = "L";
                imageBox4.Image = LabImgA;
                label3.Text = "A";
                imageBox5.Image = LabImgB;
                label4.Text = "B";
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)//設定Luv = true
            {
                checkBox1.Checked = false;  //設定RGB = false
                checkBox2.Checked = false;  //設定HLS = false
                checkBox3.Checked = false;  //設定HSV = false
                checkBox4.Checked = false;  //設定Lab = false
                checkBox6.Checked = false;  //設定Xyz = false
                checkBox7.Checked = false;  //設定Ycc = false

                //RGB 轉 Luv
                Luv = img.Convert<Luv, byte>();
                //儲存在資料夾
                CvInvoke.Imwrite(@"D:\C#_test\LayerSepartion_2023_12_22\luvImage.bmp", Luv);
                //無法用ImageBox直接顯示，RGB色彩模型之變數名稱luv，複製(CopyTo) Luv
                Image<Bgr, byte> luv = new Image<Bgr, byte>(Luv.Size);
                Luv.CopyTo(luv);

                imageBox2.Image = luv;  //顯示Hsv色彩模型影像
                imageBox6.Image = luv;  //顯示Hsv色彩模型影像

                //灰階直與文字名稱更改
                Image<Gray, byte> LuvImgL = new Image<Gray, byte>(Luv[0].ToBitmap());
                Image<Gray, byte> LuvImgU = new Image<Gray, byte>(Luv[1].ToBitmap());
                Image<Gray, byte> LuvImgV = new Image<Gray, byte>(Luv[2].ToBitmap());
                imageBox3.Image = LuvImgL;
                label2.Text = "L";
                imageBox4.Image = LuvImgU;
                label3.Text = "U";
                imageBox5.Image = LuvImgV;
                label4.Text = "V";
            }
        }
        

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)//設定Xyz = true
            {
                checkBox1.Checked = false;  //設定RGB = false
                checkBox2.Checked = false;  //設定HLS = false
                checkBox3.Checked = false;  //設定HSV = false
                checkBox4.Checked = false;  //設定Lab = false
                checkBox5.Checked = false;  //設定Luv = false
                checkBox7.Checked = false;  //設定Ycc = false

                //RGB 轉 Xyz
                Xyz = img.Convert<Xyz, byte>();
                //儲存在資料夾
                CvInvoke.Imwrite(@"D:\C#_test\LayerSepartion_2023_12_22\xyzImage.bmp", Xyz);
                //無法用ImageBox直接顯示，RGB色彩模型之變數名稱xyz，複製(CopyTo) Xyz
                Image<Bgr, byte> xyz = new Image<Bgr, byte>(Xyz.Size);
                Xyz.CopyTo(xyz);

                imageBox2.Image = xyz;  //顯示Hsv色彩模型影像
                imageBox6.Image = xyz;  //顯示Hsv色彩模型影像

                //灰階直與文字名稱更改
                Image<Gray, byte> XyzImgX = new Image<Gray, byte>(Xyz[0].ToBitmap());
                Image<Gray, byte> XyzImgY = new Image<Gray, byte>(Xyz[1].ToBitmap());
                Image<Gray, byte> XyzImgZ = new Image<Gray, byte>(Xyz[2].ToBitmap());
                imageBox3.Image = XyzImgX;
                label2.Text = "X";
                imageBox4.Image = XyzImgY;
                label3.Text = "Y";
                imageBox5.Image = XyzImgZ;
                label4.Text = "Z";
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)//設定Ycc = true
            {
                checkBox1.Checked = false;  //設定RGB = false
                checkBox2.Checked = false;  //設定HLS = false
                checkBox3.Checked = false;  //設定HSV = false
                checkBox4.Checked = false;  //設定Lab = false
                checkBox5.Checked = false;  //設定Luv = false
                checkBox6.Checked = false;  //設定Xyz = false

                //RGB 轉 Ycc
                Ycc = img.Convert<Ycc, byte>();
                //儲存在資料夾
                CvInvoke.Imwrite(@"D:\C#_test\LayerSepartion_2023_12_22\yccImage.bmp", Ycc);
                //無法用ImageBox直接顯示，RGB色彩模型之變數名稱ycc，複製(CopyTo) Ycc
                Image<Bgr, byte> ycc = new Image<Bgr, byte>(Ycc.Size);
                Ycc.CopyTo(ycc);

                imageBox2.Image = ycc;  //顯示Hsv色彩模型影像
                imageBox6.Image = ycc;  //顯示Hsv色彩模型影像

                //灰階直與文字名稱更改
                Image<Gray, byte> YccImgY = new Image<Gray, byte>(Ycc[0].ToBitmap());
                Image<Gray, byte> YccImgC1 = new Image<Gray, byte>(Ycc[1].ToBitmap());
                Image<Gray, byte> YccImgC2 = new Image<Gray, byte>(Ycc[2].ToBitmap());
                imageBox3.Image = YccImgY;
                label2.Text = "Y";
                imageBox4.Image = YccImgC1;
                label3.Text = "C1";
                imageBox5.Image = YccImgC2;
                label4.Text = "C2";
            }
        }

        
    }
}
