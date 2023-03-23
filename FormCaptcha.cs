using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolovovStroy
{
    public partial class FormCaptcha : Form
    {
        FormAuth auth;
        string result = "";
        string str = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";

        public FormCaptcha(FormAuth form)
        {
            auth = form;
            InitializeComponent();
        }

        private string SetStringCaptcha()
        {
            string res = "";
            char[] chars = str.ToCharArray();
            Random random = new Random();

            for (int i = 1; i <= 4; i++)
            {
                res += chars[random.Next(0, chars.Length)];
            }
            
            return res;
        }

        private void SetCaptcha()
        {
            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bitmap);

            result = SetStringCaptcha();

            using (Font myFont = new Font("Times New Roman", 20, FontStyle.Bold))
            {
                g.Clear(Color.Black);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                g.DrawString(result, myFont, Brushes.Green, new Point(0, pictureBox1.Height / 4));
                g.DrawLine(new Pen(Color.Red, (float)1.5), new Point(0, 15), new Point(300, 15));
                g.DrawLine(new Pen(Color.Red, (float)1.5), new Point(0, 30), new Point(300, 30));
                g.DrawLine(new Pen(Color.Red, (float)1.5), new Point(0, 45), new Point(300, 45));

                pictureBox1.Image = bitmap;
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string popit = textBox1.Text;
            string lower = result.ToLower();
            
            if (popit == result || popit == lower)
            {
                auth.Enabled = true;
                auth.count = 0;
                this.Close();
            }

            else
            {
                MessageBox.Show("Попробуйте еще раз ввести капчу");
                SetCaptcha();
                textBox1.Text = "";
            }
            
        }

        private void FormCaptcha_Load(object sender, EventArgs e)
        {
            SetCaptcha();
        }
    }
}
