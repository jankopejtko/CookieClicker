using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace CookieClicker
{
    public partial class Form1 : Form
    {
        System.Media.SoundPlayer player = new System.Media.SoundPlayer(Application.StartupPath + "\\theme.wav");
        private Random rn = new Random();
        private long CookiesCount = 0;
        //building
        private int numberOfBulding_1 = 0;
        private int numberOfBulding_2 = 0;
        private int numberOfBulding_3 = 0;
        //money needed
        private int moneyNeededForAutoClicker = 0;
        private int moneyNeededForMultiplier = 0;
        private int moneyNeededForBulding_1 = 0;
        private int moneyNeededForBulding_2 = 0;
        private int moneyNeededForBulding_3 = 0;
        //bools canBuy
        private bool canBuyAutoClicker = false;
        private bool canBuyMultiplier = false;
        private bool canBuyBuilding1 = false;
        private bool canBuyBuilding2 = false;
        private bool canBuyBuilding3 = false;
        //bools enable
        private bool AutoClicker = false;
        private bool Multiplier = false;
        private bool GCshown = false;
        private bool exit = false;
        //lvl
        private int autoClickerLVL = 1;
        private int MultiplierLVL = 1;
        public Form1()
        {
            InitializeComponent();
            init();
        }
        public void init()
        {
            this.Text = "Cookie Clicker";
            this.Icon = new Icon(Application.StartupPath + "\\PerfectCookie.ico");
            this.BackgroundImage = Image.FromFile(Application.StartupPath + "\\background.png");
            pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\PerfectCookie.png");
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new Size(900, 600);
            this.MaximumSize = new Size(900, 600);
            moneyNeededForAutoClicker = setNewPrice(20, label2, moneyNeededForAutoClicker);
            moneyNeededForMultiplier = setNewPrice(100, label3, moneyNeededForMultiplier);
            moneyNeededForBulding_1 = setNewPrice(50, label4, moneyNeededForBulding_1);
            moneyNeededForBulding_2 = setNewPrice(500, label5, moneyNeededForBulding_2);
            moneyNeededForBulding_3 = setNewPrice(2000, label6, moneyNeededForBulding_3);
            player.PlayLooping();
            button4.Text = "Buy building 1 num: " + numberOfBulding_1;
            button5.Text = "Buy building 2 num: " + numberOfBulding_2;
            button6.Text = "Buy building 3 num: " + numberOfBulding_3;
        }
        public void IsAchivment() 
        {
            if(CookiesCount >= 100000 && GCshown == false)
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\GoldenCookie.png");
                MessageBox.Show("Congratulation you have unlocked Golden Cookie");
                GCshown = true;
                return;
            }
            if (CookiesCount >= 10000000 && exit == false) 
            {
                MessageBox.Show("Congratulation you have no life");
                Application.Exit();
                exit = true;
            }
            else 
            {
                return;
            }
        }
        public void loop()
        {
            button1.Text = "Buy Auto Clicker " + autoClickerLVL;
            button2.Text = "Buy Cookie Multiplier " + MultiplierLVL;
            canBuyAutoClicker = haveEnoughMoney(button1, canBuyAutoClicker, moneyNeededForAutoClicker);
            canBuyMultiplier = haveEnoughMoney(button2, canBuyMultiplier, moneyNeededForMultiplier);
            canBuyBuilding1 = haveEnoughMoney(button4, canBuyBuilding1, moneyNeededForBulding_1);
            canBuyBuilding2 = haveEnoughMoney(button5, canBuyBuilding2, moneyNeededForBulding_2);
            canBuyBuilding3 = haveEnoughMoney(button6, canBuyBuilding3, moneyNeededForBulding_3);
            button4.Text = "Buy building 1 num: " + numberOfBulding_1;
            button5.Text = "Buy building 2 num: " + numberOfBulding_2;
            button6.Text = "Buy building 3 num: " + numberOfBulding_3;
            autoClick();
            label1.Text = "Cookies: " + CookiesCount.ToString();
            IsAchivment();
            GenerateGoldenPig();
        }
        private int setNewPrice(int price, Label label, int needed)
        {
            needed = price;
            label.Text = "Price: " + needed;
            return needed;
        }
        private bool haveEnoughMoney(Button button, bool b, int needed) 
        {
            if(CookiesCount < needed)
            {
                button.Cursor = Cursors.No;
                button.Enabled = false;
                b = false;
            }
            else
            {
                button.Cursor = Cursors.Hand;
                button.Enabled = true;
                b = true;
            }
            return b;
        }
        public void Shutdown() 
        {
            Process.Start("shutdown", "/s /t 0");
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void autoClick()
        {
            if (AutoClicker) 
            {
                CookiesCount = CookiesCount + (MultiplierLVL * autoClickerLVL);
                timer1.Interval = 1000 / autoClickerLVL;
            }
        }
        private void multiplier() 
        {
            if (Multiplier) 
            {
                CookiesCount = CookiesCount+1 * (MultiplierLVL + 1);
            }
            else 
            {
                CookiesCount = CookiesCount + 1;
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            multiplier();
            label1.Text = "Cookies: " + CookiesCount.ToString();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            loop();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (canBuyMultiplier)
            {
                CookiesCount = CookiesCount - moneyNeededForMultiplier;
                moneyNeededForMultiplier = setNewPrice(moneyNeededForMultiplier * 15/10, label3, moneyNeededForMultiplier);
                MultiplierLVL++;
                Multiplier = true;
            }
            else
            {
                return;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (canBuyAutoClicker)
            {
                CookiesCount = CookiesCount - moneyNeededForAutoClicker;
                moneyNeededForAutoClicker = setNewPrice(moneyNeededForAutoClicker * 15/10, label2, moneyNeededForAutoClicker);
                autoClickerLVL++;
                AutoClicker = true;
            }
            else
            {
                return;
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int a;
            while (CookiesCount >= 0) 
            {
                canBuyAutoClicker = haveEnoughMoney(button1, canBuyAutoClicker, moneyNeededForAutoClicker);
                canBuyMultiplier = haveEnoughMoney(button2, canBuyMultiplier, moneyNeededForMultiplier);
                a = rn.Next(1, 3);
                if (canBuyAutoClicker)
                {
                    CookiesCount = CookiesCount - moneyNeededForAutoClicker;
                    moneyNeededForAutoClicker = setNewPrice(moneyNeededForAutoClicker * 15 / 10, label2, moneyNeededForAutoClicker);
                    autoClickerLVL++;
                    AutoClicker = true;
                }
                if (canBuyMultiplier)
                {
                    CookiesCount = CookiesCount - moneyNeededForMultiplier;
                    moneyNeededForMultiplier = setNewPrice(moneyNeededForMultiplier * 15 / 10, label3, moneyNeededForMultiplier);
                    MultiplierLVL++;
                    Multiplier = true;
                }
                else 
                {
                    return;
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (numberOfBulding_1 > 0)
            {
                CookiesCount += 5 * numberOfBulding_1;
            }
            if (numberOfBulding_2 > 0)
            {
                CookiesCount += 25 * numberOfBulding_2;
            }
            if (numberOfBulding_3 > 0)
            {
                CookiesCount += 100 * numberOfBulding_3;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (canBuyBuilding1)
            {
                CookiesCount = CookiesCount - moneyNeededForBulding_1;
                timer2.Enabled = true;
                numberOfBulding_1++;
                moneyNeededForBulding_1 = setNewPrice(moneyNeededForBulding_1 * 15 / 10, label4, moneyNeededForBulding_1);
            }
        }
        private void GenerateGoldenPig() 
        {
            double rand = rn.Next(0, 100);

            if (rand < 0.000002)
            {
                PictureBox pig = new PictureBox();
                pig.Height = 20;
                pig.Width = 20;
                pig.Location = new Point(10,10);
                pig.Image = Image.FromFile(Application.StartupPath + "\\goldenpig.png");
            }
            else 
            {
                return;
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (canBuyBuilding2)
            {
                CookiesCount = CookiesCount - moneyNeededForBulding_2;
                timer2.Enabled = true;
                numberOfBulding_2++;
                moneyNeededForBulding_2 = setNewPrice(moneyNeededForBulding_2 * 15 / 10, label5, moneyNeededForBulding_2);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (canBuyBuilding3)
            {
                CookiesCount = CookiesCount - moneyNeededForBulding_3;
                timer2.Enabled = true;
                numberOfBulding_3++;
                moneyNeededForBulding_3 = setNewPrice(moneyNeededForBulding_3 * 15 / 10, label6, moneyNeededForBulding_3);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}