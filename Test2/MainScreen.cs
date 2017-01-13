using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework.Properties;
using System.Reflection;
using MetroFramework;

namespace Test2
{
    public partial class MainScreen : MetroForm
    {
        public MainScreen()
        {
            InitializeComponent();
            this.StyleManager = msMgr;
        }
        private float _ogrenmeKatsayisi = 0.9f;
        private float _momentum = 0.5f;

        private float _noronx1, _noronx2, _noronx3;
        private float _noron1, _noron2;
        private float _delta1, _delta2;

        private float _noron3;
        private float _delta3;

        private float _dendrit11, _dendrit21, _dendrit31, _dendritB1;
        private float _degisim11, _degisim21, _degisim_31, _degisimB1;

        private float _dendrit12, _dendrit22, _dendrit32, _dendritB2;
        private float _degisim12, _degisim22, _degisim32, _degisimB2;


        private float _dendrit13, _dendrit23, _dendritB3;
        private float _degisim13, _degisim23, _degisimB3;



        private void btn_training_all_Click(object sender, EventArgs e)
        {
            StartAnimate();
            btn_UpdateWeight.Enabled = false;


            tb_numberOfSteps.Enabled = false;
            btn_training_all.Enabled = false;
            tb_numberOfSteps.Minimum = 0;
            int kac = (int)tb_numberOfSteps.Value;
            for (int i = 0; i < kac; i++)
            {
                tb_numberOfSteps.Value--;
                Egit(0, 0, 0);
                lblKO1.Text = KarelerOrtalamasi(0).ToString();
                Egit(0, 1, 1);
                lblKO2.Text = KarelerOrtalamasi(1).ToString();
                Egit(1, 0, 1);
                lblKO3.Text = KarelerOrtalamasi(1).ToString();
                Egit(1, 1, 0);
                lblKO4.Text = KarelerOrtalamasi(0).ToString();

         

            }
            tb_numberOfSteps.Value = kac;
            tb_numberOfSteps.Minimum = 10;
            tb_numberOfSteps.Enabled = true;
            btn_training_all.Enabled = true;

            TumCikislarIcinHatalar();

            btn_UpdateWeight.Enabled = true;
            StopAnimate();





            grdResult.Rows.Clear();
            lbl_TableTitle.Text = $@"The results of {tb_numberOfSteps.Value} Epoch : ";
            GrideEkle(0, 0);
            GrideEkle(0, 1);
            GrideEkle(1, 0);
            GrideEkle(1, 1);


        }




        private void Form1_Load(object sender, EventArgs e)
        {

            grdResult.Rows.Add("NCalc", "NCalc", "NCalc", "NCalc", "NCalc", "NCalc", "NCalc", "NCalc", "NCalc", "NCalc", "NCalc");

            _dendrit11 = Rastgele();
            lbl_w13.Text = _dendrit11.ToString();
            _dendrit21 = Rastgele();
            lbl_w23.Text = _dendrit21.ToString();
            _dendritB1 = Rastgele();
            lbl_wb13.Text = _dendritB1.ToString();
            _dendrit12 = Rastgele();
            lbl_w14.Text = _dendrit12.ToString();
            _dendrit22 = Rastgele();
            lbl_w24.Text = _dendrit22.ToString();
            _dendritB2 = Rastgele();
            lbl_w14.Text = _dendritB2.ToString();
            _degisim11 = 0;
            _degisim21 = 0;
            _degisimB1 = 0;
            _degisim12 = 0;
            _degisim22 = 0;
            _degisimB2 = 0;

            _dendrit13 = Rastgele();
            lbl_w35.Text = _dendrit13.ToString();
            _dendrit23 = Rastgele();
            lbl_w45.Text = _dendrit23.ToString();
            _dendritB3 = Rastgele();
            lbl_wb25.Text = _dendritB3.ToString();
            _degisim13 = 0;
            _degisim23 = 0;
            _degisimB3 = 0;

        }

        private void btn_UpdateWeight_Click(object sender, EventArgs e)
        {

            int g1 = 0, g2 = 0, c = 0;
            g1 = tg_IN01.Checked ? 1 : 0;
            g2 = tg_IN02.Checked ? 1 : 0;
            c = tg_OUT01.Checked ? 1 : 0;



            Egit(g1, g2, c);
            if (g1 == 0 && g2 == 0) lblKO1.Text = KarelerOrtalamasi(c).ToString();
            else if (g1 == 0) lblKO2.Text = KarelerOrtalamasi(c).ToString();
            else if (g2 == 0) lblKO3.Text = KarelerOrtalamasi(c).ToString();
            else lblKO4.Text = KarelerOrtalamasi(c).ToString();




            grdResult.Rows.Clear();
            lbl_TableTitle.Text = @" The results of " + tb_numberOfSteps.Value + " Epoch :";
            GrideEkle(0, 0);
            GrideEkle(0, 1);
            GrideEkle(1, 0);
            GrideEkle(1, 1);



            this.Height = 695;

        }

        private void tg_IN01_CheckedChanged(object sender, EventArgs e)
        {
            metroLabel3.Text = tg_IN01.Checked ? "1" : "0";
        }

        private void tg_IN02_CheckedChanged(object sender, EventArgs e)
        {
            metroLabel4.Text = tg_IN02.Checked ? "1" : "0";
        }

        private void tg_OUT01_CheckedChanged(object sender, EventArgs e)
        {
            metroLabel5.Text = tg_OUT01.Checked ? "1" : "0";
        }

        private void btn_calculate_Click(object sender, EventArgs e)
        {

            int g1 = 0, g2 = 0, c = 0;
            g1 = tg_IN01.Checked ? 1 : 0;
            g2 = tg_IN02.Checked ? 1 : 0;
            c = tg_OUT01.Checked ? 1 : 0;




            CikisHesapla(g1, g2);
         

            if (Math.Round(Convert.ToDouble(lbl_Input5.Text)).ToString() == "1")
            {
                tg_OUT01.Checked = true;
            }
            else
            {
                tg_OUT01.Checked = false;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                msMgr.Theme = msMgr.Theme == MetroThemeStyle.Light ? MetroThemeStyle.Dark : MetroThemeStyle.Light;
                this.Theme = msMgr.Theme;
                this.Refresh();
            }
            e.Handled = false;
        }

        private void btn_tbBarPlus_Click(object sender, EventArgs e)
        {
            if ((tb_numberOfSteps.Value) < (tb_numberOfSteps.Maximum - 100))
                tb_numberOfSteps.Value += 100;
            else
                tb_numberOfSteps.Value = tb_numberOfSteps.Maximum;

        }

        private void btn_tbBarMinus_Click(object sender, EventArgs e)
        {
            if (tb_numberOfSteps.Value > 100)
                tb_numberOfSteps.Value -= 100;

        }

        private void Egit(int giris1, int giris2, int cikis) 
        {
            CikisHesapla(giris1, giris2);
            Application.DoEvents(); 

            _delta3 = (cikis - _noron3);
            _delta3 *= _noron3 * (1 - _noron3); // δ3
            _delta1 = _dendrit13 * _delta3;
            _delta1 *= _noron1 * (1 - _noron1);// δ1
            _delta2 = _dendrit23 * _delta3;
            _delta2 *= _noron2 * (1 - _noron2);// δ2

            _degisim11 = _ogrenmeKatsayisi * _delta1 * _noronx1 + _momentum * _degisim11;
            _dendrit11 += _degisim11;
            lbl_w13.Text = _dendrit11.ToString();

            _degisim21 = _ogrenmeKatsayisi * _delta1 * _noronx2 + _momentum * _degisim21;
            _dendrit21 += _degisim21;
            lbl_w23.Text = _dendrit21.ToString();

            _degisim_31 = _ogrenmeKatsayisi * _delta1 * _noronx3 + _momentum * _degisim_31;
            _dendrit31 += _degisim_31;


            _degisimB1 = _ogrenmeKatsayisi * _delta1 * 1 + _momentum * _degisimB1;
            _dendritB1 += _degisimB1;
            lbl_wb13.Text = _dendritB1.ToString();

            _degisim12 = _ogrenmeKatsayisi * _delta2 * _noronx1 + _momentum * _degisim12;
            _dendrit12 += _degisim12;
            lbl_w14.Text = _dendrit12.ToString();

            _degisim22 = _ogrenmeKatsayisi * _delta2 * _noronx2 + _momentum * _degisim22;
            _dendrit22 += _degisim22;
            lbl_w24.Text = _dendrit22.ToString();



            _degisim32 = _ogrenmeKatsayisi * _delta2 * _noronx3 + _momentum * _degisim32;
            _dendrit32 += _degisim32;


            _degisimB2 = _ogrenmeKatsayisi * _delta2 * 1 + _momentum * _degisimB2;
            _dendritB2 += _degisimB2;
            lbl_wb14.Text = _dendritB2.ToString();


            _degisim13 = _ogrenmeKatsayisi * _delta3 * _noron1 + _momentum * _degisim13;
            _dendrit13 += _degisim13;
            lbl_w35.Text = _dendrit13.ToString();
            _degisim23 = _ogrenmeKatsayisi * _delta3 * _noron2 + _momentum * _degisim23;
            _dendrit23 += _degisim23;
            lbl_w45.Text = _dendrit23.ToString();
            _degisimB3 = _ogrenmeKatsayisi * _delta3 * 1 + _momentum * _degisimB3;
            _dendritB3 += _degisimB3;
            lbl_wb25.Text = _dendritB3.ToString();

        }
        private float CikisHesapla(int giris1, int giris2)
        {
            _noronx1 = (float)giris1;
            _noronx2 = (float)giris2;

            lbl_Input1.Text = _noronx1.ToString();
            lbl_Input2.Text = _noronx2.ToString();

            _noron1 = Aktivasyon(_noronx1 * _dendrit11 + _noronx2 * _dendrit21 + _noronx3 * _dendrit31 + 1 * _dendritB1);
            _noron2 = Aktivasyon(_noronx1 * _dendrit12 + _noronx2 * _dendrit22 + _noronx3 * _dendrit32 + 1 * _dendritB2);
            lbl_Input3.Text = _noron1.ToString();
            lbl_Input4.Text = _noron2.ToString();

            _noron3 = Aktivasyon(_noron1 * _dendrit13 + _noron2 * _dendrit23 + 1 * _dendritB3);
            lbl_Input5.Text = _noron3.ToString();

            return _noron3;
        }


        public void GrideEkle(int giris1, int giris2)
        {
            _noronx1 = (float)giris1;
            _noronx2 = (float)giris2;
            _noron1 = Aktivasyon(_noronx1 * _dendrit11 + _noronx2 * _dendrit21 + 1 * _dendritB1);
            _noron2 = Aktivasyon(_noronx1 * _dendrit12 + _noronx2 * _dendrit22 + 1 * _dendritB2);
            _noron3 = Aktivasyon(_noron1 * _dendrit13 + _noron2 * _dendrit23 + 1 * _dendritB3);


            grdResult.Rows.Add(_noronx1, _noronx2, _noron1, _noron2,
                lbl_w13.Text,
                lbl_w14.Text,
                lbl_w23.Text,
                lbl_w24.Text,
                lbl_w35.Text,
                lbl_w45.Text,
                Math.Round(Convert.ToDouble(_noron3)).ToString());

        }

         

        private float Aktivasyon(float x) // Sigmoid fonk.
        {
            return (float)(1.0f / (1.0f + Math.Exp(-x)));
        }

        private float KarelerOrtalamasi(int hedef)
        {
            return (float)Math.Pow(hedef - _noron3, 2) / 2.0f; 
        }

       

        private void TumCikislarIcinHatalar()
        {
            CikisHesapla(0, 0);
            lblKO1.Text = KarelerOrtalamasi(0).ToString();
            CikisHesapla(0, 1);
            lblKO2.Text = KarelerOrtalamasi(1).ToString();
            CikisHesapla(1, 0);
            lblKO3.Text = KarelerOrtalamasi(1).ToString();
            CikisHesapla(1, 1);
            lblKO4.Text = KarelerOrtalamasi(0).ToString();
        }

        private readonly Random _rastgeleUretec = new Random();
        private float Rastgele()
        {
            return (float)_rastgeleUretec.Next(1, 999) / 1000.0f; 
        }



        private static void Animate(PictureBox box, bool enable)
        {
            var anim = box.GetType().GetMethod("Animate",
                BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(bool) }, null);
            anim.Invoke(box, new object[] { enable });
        }

        private void StartAnimate()
        {
            pb1.Paint -= StopAnimate_OnLoad;
            pb2.Paint -= StopAnimate_OnLoad;
            pb3.Paint -= StopAnimate_OnLoad;
            pb4.Paint -= StopAnimate_OnLoad;
            pb5.Paint -= StopAnimate_OnLoad;
            pbb1.Paint -= StopAnimate_OnLoad;
            pbb2.Paint -= StopAnimate_OnLoad;

            Animate(pb1, true);
            Animate(pb2, true);
            Animate(pb3, true);
            Animate(pb4, true);
            Animate(pb5, true);
            Animate(pbb1, true);
            Animate(pbb2, true);
        }
        private void StopAnimate()
        {
            pb1.Paint += StopAnimate_OnLoad;
            pb2.Paint += StopAnimate_OnLoad;
            pb3.Paint += StopAnimate_OnLoad;
            pb4.Paint += StopAnimate_OnLoad;
            pb5.Paint += StopAnimate_OnLoad;
            pbb1.Paint += StopAnimate_OnLoad;
            pbb2.Paint += StopAnimate_OnLoad;
        }
        private void StopAnimate_OnLoad(object sender, PaintEventArgs e)
        {
            Animate(pb1, false);
            Animate(pb2, false);
            Animate(pb3, false);
            Animate(pb4, false);
            Animate(pb5, false);
            Animate(pbb1, false);
            Animate(pbb2, false);
        }

        private void metroTrackBar1_ValueChanged(object sender, EventArgs e)
        {
            lblEpochNumber.Text = tb_numberOfSteps.Value.ToString();
        }
    }
}

