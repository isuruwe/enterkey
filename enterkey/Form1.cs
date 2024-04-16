using System.Runtime.InteropServices;

namespace enterkey
{
    public partial class Form1 : Form
    {
        private bool clicking = false;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        private const int KEYEVENTF_EXTENDEDKEY = 0x0001;
        private const int KEYEVENTF_KEYUP = 0x0002;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                clicking = true;

                // Start a new thread to perform the clicking
                Thread clickThread = new Thread(() =>
                {
                    while (clicking)
                    {
                        // Simulate an Enter key press using SendInput
                        SimulateKeyPress(Keys.Enter);

                        // Sleep for a short duration (e.g., 500 milliseconds) before the next click
                        Thread.Sleep(500);
                    }
                });

                clickThread.Start();
            }
            catch (Exception ex)
            {


            }
        }
        private void SimulateKeyPress(Keys key)
        {
            keybd_event((byte)key, 0, KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);
            keybd_event((byte)key, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            clicking = false;
        }
    }
}