using System;
using System.Windows.Forms;
using System.Drawing;
using System.Net.Mail;
using System.Net;
using System.IO.Ports;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Emit;

namespace smarthome
{
    public partial class Form1 : Form
    {
        bool sensor_Flag = true;


        private string data;
        public Form1()
        {
            InitializeComponent();
            System.ComponentModel.IContainer components = new System.ComponentModel.Container();

        }



        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            data = serialPort1.ReadLine();
            this.Invoke(new EventHandler(displayData_event));


        }

        private void displayData_event(object sender, EventArgs e)
        {
            string[] value = data.Split('/');

            if (Convert.ToInt16(value[0]) == 1)
            {
                label6.Text = "Motion Detected!";


                if (sensor_Flag == true)
                {
                    SmtpClient SmtpServer = new SmtpClient();
                    SmtpServer.Credentials = new NetworkCredential("****", "*****");  //Buraya kendi gmail adresinizi ve şifrenizi girin
                    SmtpServer.Port = 587;                              //Port Numarası
                    SmtpServer.Host = "smtp.gmail.com";                 //Sunucu adresi
                    SmtpServer.EnableSsl = true;                        //SSL ayarı
                    MailMessage mail = new MailMessage();
                    mail.To.Add("*****");            //Gönderilecek adres
                    mail.From = new MailAddress("******", "Arduino - PIR Sensörü");  //Mailin gönderildiği adres ve isim tanımlaması
                    mail.Subject = "Hareket Algılandı!";     //Mail konusu
                    mail.Body = "Ortamda hareket algılandı!";//Mailin body kısmındaki metin
                    SmtpServer.Send(mail);  //Maili gönder */
                                            //MessageBox.Show("Hareket Algılandı", "Güvenlik Durumu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    sensor_Flag = false;
                }




            }
            else
            {
                label6.Text = " ";

            }
            label4.Text = value[1];
            if (Convert.ToInt16(value[1]) <= 20)
            {
                label4.Text = "It was night";
                // MessageBox.Show("Akşam oldu", "Işık Durumu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                label4.Text = "It was daytime";

            }

            if (Convert.ToInt16(value[4]) <= 6)
            {
                distance.Text = "There is a car at the garage door";
                // MessageBox.Show("Kapıda araba var","Garaj Durumu",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else if (Convert.ToInt16(value[4]) > 6)
            {
                distance.Text = "There is not car at the garage door";

            }
            else
            {
                distance.Text = " ";

            }
            label3.Text = value[2];
            label2.Text = value[3];
            if (Convert.ToInt16(value[3]) > 26)
            {
                label2.Text = "Temperature rose";

            }


        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
                comboBox1.Items.Add(port);
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
        }

        private void button1_Click(object sender, EventArgs e)
        {


            try
            {

                /* Seri Port Ayarları */
                serialPort1.PortName = comboBox1.Text;
                serialPort1.BaudRate = 9600;
                serialPort1.Parity = Parity.None;
                serialPort1.DataBits = 8;
                serialPort1.StopBits = StopBits.One;
                serialPort1.Open(); //Seri portu aç

                /* ------------------ */
                button2.Enabled = true;                          //"Kes" butonunu tıklanabilir yap
                button1.Enabled = false;                      //"Bağlan" butonunu tıklanamaz yap 
                label20.Text = "Connected";
                label20.ForeColor = System.Drawing.Color.Green;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Connecting Error");  //Hata mesajı
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Close();              //Seri portu kapa
                button2.Enabled = false;              //"Kes" butonunu tıklanamaz yap
                button1.Enabled = true;            //"Bağlan" butonunu tıklanabilir yap
                label20.Text = "Disconnected";
                label20.ForeColor = System.Drawing.Color.Red;
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message); //Hata mesajı
            }
           

        }
      

        private void button3_Click(object sender, EventArgs e)
        {
            serialPort1.Write("8");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            serialPort1.Write("9");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            serialPort1.Write("5");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            serialPort1.Write("6");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            serialPort1.Write("3");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            serialPort1.Write("7");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            serialPort1.Write("4");
        }

        private void button14_Click(object sender, EventArgs e)
        {

            serialPort1.Write("A");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            serialPort1.Write("B");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            serialPort1.Write("1");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            serialPort1.Write("2");
        }




    }
}
