using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace PingPong
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort1;
        private int ledIndex = -1;          // active LED index (0..14)
        private bool gameRunning = false;

        public Form1()
        {
            InitializeComponent();

            serialPort1 = new SerialPort();
            serialPort1.PortName = "COM4";   // change if needed
            serialPort1.BaudRate = 9600;
            serialPort1.DataReceived += SerialPort1_DataReceived;

            // attach paint event for the single panel named "panel"
            panel.Paint += Panel_Paint;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort1.IsOpen)
                {
                    serialPort1.Open();
                    MessageBox.Show($"{serialPort1.PortName} port is open.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The port couldn't be opened: " + ex.Message);
            }
        }

        // Draw 15 LEDs inside single panel
        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            int totalLeds = 15;
            int size = 25;      // diameter
            int spacing = 10;   // horizontal spacing

            // compute total width to center if you want (optional)
            //int totalWidth = totalLeds * size + (totalLeds - 1) * spacing;
            //int startX = Math.Max(10, (panel.Width - totalWidth) / 2);

            for (int i = 0; i < totalLeds; i++)
            {
                int x = i * (size + spacing) + 10;
                int y = panel.Height / 2 - size / 2;

                Rectangle rect = new Rectangle(x, y, size, size);

                Brush brush;
                if (i == ledIndex)
                {
                    // LED color mapping: 1 & 15 -> red, 2 & 14 -> yellow, others green
                    if (i == 0 || i == 14) brush = Brushes.Red;
                    else if (i == 1 || i == 13) brush = Brushes.Yellow;
                    else brush = Brushes.Lime;
                }
                else
                {
                    brush = Brushes.Gray; // off
                }

                e.Graphics.FillEllipse(brush, rect);
                e.Graphics.DrawEllipse(Pens.Black, rect);
            }
        }

        // Start button: resets view and instructs Arduino to start
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort1.IsOpen)
                {
                    serialPort1.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open serial port: " + ex.Message);
                return;
            }

            // reset display
            ledIndex = -1;
            panel.Invalidate();

            gameRunning = true;
            // send START command to Arduino
            try { serialPort1.WriteLine("START"); }
            catch { /* ignore write errors */ }
        }

        // End button: send STOP, close port, and exit form
        private void btnEnd_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                try
                {
                    serialPort1.WriteLine("STOP");
                    serialPort1.Close();
                }
                catch { /* ignore errors */ }
            }

            gameRunning = false;
            ledIndex = -1;
            panel.Invalidate();

            this.Close(); // close the form
        }

        // Serial data handler: expects either a number 1..15 (LED position)
        // Any other incoming text is ignored here (you can extend later).
        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!gameRunning) return;

            try
            {
                string line = serialPort1.ReadLine().Trim();

                if (int.TryParse(line, out int ledNumber))
                {
                    int index = ledNumber - 1; // Arduino sends 1..15
                    if (index >= 0 && index < 15)
                    {
                        ledIndex = index;
                        this.Invoke(new Action(() => panel.Invalidate()));
                    }
                }
                // else: ignore other messages (no scores)
            }
            catch
            {
                // ignore serial read errors
            }
        }
    }
}
