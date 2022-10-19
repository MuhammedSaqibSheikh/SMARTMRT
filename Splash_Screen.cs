using System;

namespace SMARTMRT
{
    public partial class Splash_Screen : Telerik.WinControls.UI.RadForm
    {
        public Splash_Screen()
        {
            InitializeComponent();
            this.CenterToScreen();
        }        

        private void Splash_Screen_Load(object sender, EventArgs e)
        {
            timer1.Start();
            radProgressBar1.Visible = true;
            radLabel1.Text = Database_Connection.SET_USER;   //get user
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //check if the value is 100
                if (radProgressBar1.Value2 == 100)
                {
                    //open home page
                    radProgressBar1.Text = "100 %";
                    timer1.Stop();

                    this.Hide();
                    Home hm = new Home();
                    hm.ShowDialog();
                    this.Close();
                }
                else
                {
                    radProgressBar1.Value2 = radProgressBar1.Value2 + 1;
                    radProgressBar1.Text = radProgressBar1.Value2.ToString() + " %";
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
