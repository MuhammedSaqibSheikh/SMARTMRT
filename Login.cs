using Rockey4NDControl;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace SMARTMRT
{
    public partial class Login : RadForm
    {
        public Login()
        {
            InitializeComponent();
        }

        Database_Connection dc = new Database_Connection();     //connection class
        String super_username = "super";   //super user username
        String super_password = "w1dHAqrf0sI="; //super user password
        String PMSCLIENT = "0";    
        String LINES = "0";
        String dongleflag = "0";

        private void Login_Load(object sender, EventArgs e)
        {
            RadMessageBox.SetThemeName("FluentDark");    //set message box theme

            //check if the pms server is connected
            if (dc.OpenConnection() != "Connected to Database")    
            {
                Environment.Exit(0);
            }

           // Dongle2();    //dongle setup

            tmrdongle.Enabled = true;
            panel1.Visible = false;
            this.CenterToScreen();   //keep the form centered to screen
        }


        //encrypt password
        public static string EncryptPassword(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            AppSettingsReader settingsReader = new AppSettingsReader();
            string key = "WETHEPEOPLEOFINDIAHAVING";
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();

            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }        

        private void radLabel4_TextChanged(object sender, EventArgs e)
        {
            MyTimer.Interval = 5000; //5 Sec
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            radPanel2.Visible = true;
            MyTimer.Start();
        }

        System.Windows.Forms.Timer MyTimer = new System.Windows.Forms.Timer();

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            radLabel4.Text = "";
            radPanel2.Visible = false;
            MyTimer.Stop();
        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {
            
            //encrypt password
            String password = EncryptPassword(txtpass.Text, false);
           
            //check if super user username
            if (txtuser.Text == super_username)
            {
                //encrypt password
                String pass = EncryptPassword(txtpass.Text, false);

                //check if the super user password
                if (pass == super_password)
                {
                    //open splash screen
                    Splash_Screen ss = new Splash_Screen();
                    Database_Connection.SET_USER = "Super User";
                    radLabel4.Text = "";

                    this.Hide();
                    ss.ShowDialog();
                    this.Close();
                }
                else
                {
                    radLabel4.Text = "Wrong Username or Password";
                }

                return;
            }

            //get the user group
            SqlCommand cmd = new SqlCommand("Select V_USER_GROUP from USER_LOGIN where V_USERNAME='" + txtuser.Text + "' and V_PASSWORD='" + password + "'", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                //open splash screen
                Splash_Screen ss = new Splash_Screen();
                Database_Connection.SET_USER = sdr.GetValue(0).ToString();
                radLabel4.Text = "";

                this.Hide();
                ss.ShowDialog();
                this.Close();
            }
            else
            {
                radLabel4.Text = "Wrong Username or Password";
            }
            sdr.Close();
        }

        private void radButton2_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);   //exit application
        }

        private void txtuser_Enter(object sender, EventArgs e)
        {
            //enter key press event
            if (txtuser.Text == "Username")
            {
                txtuser.Text = "";
            }

            if (txtpass.Text == "")
            {
                txtpass.isPassword = false;
                txtpass.Text = "Password";
            }
        }

        private void txtpass_Enter(object sender, EventArgs e)
        {
            //enter key press event
            if (txtpass.Text == "Password")
            {
                txtpass.Text = "";
            }

            txtpass.isPassword = true;

            if (txtuser.Text == "")
            {
                txtuser.Text = "Username";
            }
        }

        private void btnlogin_Click_1(object sender, EventArgs e)
        {
            radButton1.PerformClick();   //press login button
        }

        private void btncancel_Click_1(object sender, EventArgs e)
        {
            radButton2.PerformClick();   //press close button
        }

        private void txtpass_KeyDown_1(object sender, KeyEventArgs e)
        {
            //enter key press event
            if (e.KeyCode == Keys.Enter)
            {
                radButton1.PerformClick();
            }
        }

        private void txtuser_KeyDown(object sender, KeyEventArgs e)
        {
            //enter key press event
            if (e.KeyCode == Keys.Enter)
            {
                radButton1.PerformClick();
            }
        }


        public void Dongle2()
        {
            //check if the connection is closed
            if (dc.con.State == ConnectionState.Closed)
            {
                dc.OpenConnection();
            }

            //get path to the User data folder
            String activationKey = "";
            String path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SmartMRT PMS\\");
            if (Directory.Exists(path))
            {
                //get the file
                string filepath = path + "\\globalSetting.mrt";
                String line;
                //chekc if the file exists
                if (File.Exists(filepath))
                {
                    //open file
                    StreamReader file = new StreamReader(filepath);

                    while ((line = file.ReadLine()) != null)
                    {
                        if (line.Trim().Equals(""))
                        {

                        }
                        else
                        {
                            StringTokenizer token = new StringTokenizer(line, "=");
                            String GlobalVarName = "";
                            String GlobalVarValue = "";

                            GlobalVarName = token.NextToken();
                            GlobalVarName = GlobalVarName.Trim();

                            //get the activation key
                            if (GlobalVarName.Equals("ActivationKey"))
                            {
                                GlobalVarValue = token.NextToken();
                                GlobalVarValue = GlobalVarValue.Trim();
                                activationKey = GlobalVarValue;
                            }
                        }
                    }
                    file.Close();
                }
                else
                {
                    //if the file does not exists then create the file and type the trial version key
                    using (StreamWriter sw = File.CreateText(filepath))
                    {
                        sw.WriteLine("ActivationKey=FDkr/Q6UDfxm+OJ23DLTgmwy3BbQ6/bI");
                    }

                    //restart the application
                    Application.Restart();
                }
            }
            else
            {
                //create the path and file
                Directory.CreateDirectory(path);

                String filepath = path + "\\globalSetting.mrt";
                using (StreamWriter sw = new StreamWriter(filepath))
                {
                    sw.WriteLine("ActivationKey=FDkr/Q6UDfxm+OJ23DLTgmwy3BbQ6/bI");
                }

                Application.Restart();
            }

            //get the key details
            if (activationKey != "")
            {
                //DebugLog("Login.cs(Dongle2), activationKey - " + activationKey);
                ValidateDongle2(Decrypt(activationKey, false)); //temp
            }
            else
            {
                RadMessageBox.Show("No Activation Key", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
                Environment.Exit(0);
            }
        }

        //decrypt the password
        public static string Decrypt(string cipherString, bool useHashing)
        {
            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(cipherString);

                string key = "WETHEPEOPLEOFINDIAHAVING";
                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    hashmd5.Clear();
                }
                else
                {
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);
                }

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();

                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                RadMessageBox.Show("Wrong Activation Key", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
                Environment.Exit(0);

                Console.WriteLine(ex);
                return "";
            }
        }

        public void ValidateDongle2(String Serial)
        {
            try
            {
                //DebugLog("Login.cs(ValidateDongle2), Track 1");
                byte[] buffer = new byte[1024];
                ushort handle = 0;
                ushort function = 0;
                ushort p1 = 0;
                ushort p2 = 0;
                ushort p3 = 0;
                ushort p4 = 0;
                uint lp1 = 0;
                uint lp2 = 0;

                int iMaxRockey = 0;
                uint[] uiarrRy4ID = new uint[32];
                string strRet = "";
                String HID = Serial.Substring(8, Serial.Length - 8);

                String time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                //get the count of records id hanger history
                int count1 = 0;
                SqlDataAdapter sdadongle = new SqlDataAdapter("SELECT COUNT(*) FROM HANGER_HISTORY", dc.con);
                DataTable dtdongle = new DataTable();
                sdadongle.Fill(dtdongle);
                sdadongle.Dispose();
                for (int i = 0; i < dtdongle.Rows.Count; i++)
                {
                    count1 = int.Parse(dtdongle.Rows[i][0].ToString());
                }

                if (count1 > 0)
                {
                    
                    //get the last hanger time
                    sdadongle = new SqlDataAdapter("SELECT TOP(1) CONVERT(VARCHAR(10), TIME, 111) +' '+ CONVERT(VARCHAR(10),TIME, 108) from HANGER_HISTORY order by time desc", dc.con);
                    dtdongle = new DataTable();
                    sdadongle.Fill(dtdongle);
                    sdadongle.Dispose();
                    for (int i = 0; i < dtdongle.Rows.Count; i++)
                    {
                        if (dtdongle.Rows[i][0].ToString() != "")
                        {
                            time = dtdongle.Rows[i][0].ToString();
                        }
                    }
                }
                
                Rockey4ND R4nd = new Rockey4ND();
                R4nd.Rockey(function, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, buffer);

                ushort ret = 0;

                //find the dongle
                for (int j = 0; j < 7; j++)
                {
                    //p1: b839 p2: 74bb p3: 8431  p4: 8788
                    p1 = 0xb839; p2 = 0x74bb; p3 = 0x8431; p4 = 0x8788;
                    ret = R4nd.Rockey((ushort)Ry4Cmd.RY_FIND, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, buffer);
                    if (0 == ret)
                    {
                        uiarrRy4ID[iMaxRockey] = lp1;
                        strRet = "1 Found  Rockey4ND(s)";
                        strRet = string.Format("{0:x8}", uiarrRy4ID[iMaxRockey]);
                        //check if the key is for the trial version
                        if (HID == "00000000")
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                p1 = 0xb839; p2 = 0x74bb; p3 = 0x8431; p4 = 0x8788;

                                //open the dongle
                                ret = R4nd.Rockey((ushort)Ry4Cmd.RY_OPEN, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, buffer);
                                if (0 == ret)
                                {
                                    uiarrRy4ID[iMaxRockey] = lp1;
                                    iMaxRockey++;
                                    break;
                                }
                                else
                                {
                                    Thread.Sleep(2000);
                                    if (i >= 5)
                                    {
                                        RadMessageBox.Show("Dongle Open Failed : " + ret, "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
                                        Environment.Exit(0);
                                    }
                                }
                            }

                            for (int i = 0; i < 6; i++)
                            {
                                p1 = 3;
                                p2 = 3;
                                buffer[0] = 0;

                                //read the trial version date
                                ret = R4nd.Rockey((ushort)Ry4Cmd.RY_READ, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, buffer);
                                if (0 == ret)
                                {
                                    uiarrRy4ID[iMaxRockey] = lp1;
                                    iMaxRockey++;
                                    String date = buffer[0].ToString();
                                    String month = buffer[1].ToString();
                                    String year = buffer[2].ToString();

                                    if (buffer[0].ToString().Length == 1)
                                    {
                                        date = "0" + buffer[0];
                                    }

                                    if (buffer[1].ToString().Length == 1)
                                    {
                                        month = "0" + buffer[1];
                                    }

                                    if (buffer[2].ToString().Length == 2)
                                    {
                                        year = "20" + buffer[2];
                                    }

                                    String temp1 = date + "-" + month + "-" + year;
                                    temp1 = temp1 + " 23:59:59";

                                    //get the current date time
                                    DateTime date1 = DateTime.ParseExact(time, "yyyy/MM/dd HH:mm:ss", null);

                                    //check if the last hanger time less than current date
                                    if (date1 < DateTime.Now)
                                    {
                                        date1 = DateTime.ParseExact(DateTime.Now.ToString("yyyy/MM/dd") + " 23:59:59", "yyyy/MM/dd HH:mm:ss", null);
                                    }

                                    DateTime date2 = DateTime.ParseExact(temp1, "dd-MM-yyyy HH:mm:ss", null);

                                    //check if the trial version is less than current date
                                    if (date1 > date2)
                                    {
                                        RadMessageBox.Show("Trial Version Expired", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
                                        Environment.Exit(0);  //close the appication
                                    }

                                    //flag to show the message box to only once on loading
                                    if (dongleflag == "0")
                                    {
                                        dongleflag = "1";
                                        RadMessageBox.Show("Trial Version Valid Till " + date2.ToString("yyyy-MM-dd"), "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Info);
                                    }

                                    break;
                                }
                                else
                                {
                                    Thread.Sleep(2000);
                                    if (i >= 5)
                                    {
                                        RadMessageBox.Show("Dongle Read Failed : " + ret, "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
                                        Environment.Exit(0);
                                    }
                                }
                            }

                            //close the dongle
                            for (int i = 0; i < 6; i++)
                            {
                                ret = R4nd.Rockey((ushort)Ry4Cmd.RY_CLOSE, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, buffer);

                                if (0 == ret)
                                {
                                    uiarrRy4ID[iMaxRockey] = lp1;
                                    iMaxRockey++;
                                    break;
                                }
                                else
                                {
                                    Thread.Sleep(2000);
                                    if (i >= 5)
                                    {
                                        RadMessageBox.Show("Dongle Close Failed : " + ret, "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
                                        Environment.Exit(0);
                                    }
                                }
                            }
                        }
                        //check if the dongle key and activation are matching
                        else if (HID != strRet)
                        {
                            RadMessageBox.Show("Wrong Activation Key", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
                            Environment.Exit(0);
                        }

                        iMaxRockey++;
                        break;
                    }
                    //check if there is any dingle
                    else
                    {
                        Thread.Sleep(10000);
                        if (j >= 6)
                        {
                            RadMessageBox.Show("No Dongle", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
                            Environment.Exit(0);
                        }
                    }
                }

                //check if there is more than one dongle
                ret = R4nd.Rockey((ushort)Ry4Cmd.RY_FIND_NEXT, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, buffer);
                if (0 == ret)
                {
                    uiarrRy4ID[iMaxRockey] = lp1;
                    strRet = "1 Found  Rockey4ND(s)";
                    strRet = string.Format("{0:x8}", uiarrRy4ID[iMaxRockey]);

                    if (HID != strRet)
                    {
                        tmrdongle.Enabled = false;
                        RadMessageBox.Show("More then One Dongle(s)", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
                        Environment.Exit(0);
                    }

                    iMaxRockey++;
                }

                //open dongle
                for (int i = 0; i < 6; i++)
                {
                    p1 = 0xb839; p2 = 0x74bb; p3 = 0x8431; p4 = 0x8788;
                    ret = R4nd.Rockey((ushort)Ry4Cmd.RY_OPEN, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, buffer);
                    
                    if (0 == ret)
                    {
                        uiarrRy4ID[iMaxRockey] = lp1;
                        iMaxRockey++;
                        break;
                    }
                    else
                    {
                        Thread.Sleep(2000);
                        if (i >= 5)
                        {
                            RadMessageBox.Show("Dongle Open Failed : " + ret, "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
                            Environment.Exit(0);
                        }
                    }
                }

                //read no of lines 
                for (int i = 0; i < 6; i++)
                {
                   // DebugLog("Login.cs(ValidateDongle2), Track 2");
                    //////////////////////////////////////Number of Lines////////////////////////////////////////
                    p1 = 1;
                    p2 = 1;
                    buffer[0] = 0;

                    ret = R4nd.Rockey((ushort)Ry4Cmd.RY_READ, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, buffer);
                    if (0 == ret)
                    {
                       // DebugLog("Login.cs(ValidateDongle2), Track 3");
                        uiarrRy4ID[iMaxRockey] = lp1;
                        iMaxRockey++;
                        LINES = buffer[0].ToString();
                       // DebugLog("Login.cs(ValidateDongle2), LINES - " + LINES + ", Track 4");

                        //delete the prod lines if exceeds the allowed lines
                        string strSql = "DELETE FROM PROD_LINE_DB WHERE I_ID NOT IN (SELECT TOP " + LINES + " I_ID FROM PROD_LINE_DB)";
                        DebugLog("Login.cs(ValidateDongle2), strSql - " + strSql + ", Track 5");
                        SqlCommand cmdd = new SqlCommand(strSql, dc.con);
                        cmdd.ExecuteNonQuery();

                        break;
                    }
                    else
                    {
                        //DebugLog("Login.cs(ValidateDongle2), Track 6");
                        Thread.Sleep(2000);
                        if (i >= 5)
                        {
                            RadMessageBox.Show("Dongle Read Failed : " + ret, "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
                            Environment.Exit(0);
                        }
                    }
                }

                //read products enabled
                for (int i = 0; i < 6; i++)
                {
                    ////////////////////////////////////Products Enabled////////////////////////////////////////
                    p1 = 6;
                    p2 = 1;
                    buffer[0] = 0;

                    ret = R4nd.Rockey((ushort)Ry4Cmd.RY_READ, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, buffer);
                    if (0 == ret)
                    {
                        uiarrRy4ID[iMaxRockey] = lp1;
                        iMaxRockey++;
                        PMSCLIENT = buffer[0].ToString();
                        break;
                    }
                    else
                    {
                        Thread.Sleep(2000);
                        if (i >= 5)
                        {
                            RadMessageBox.Show("Dongle Read Failed : " + ret, "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
                            Environment.Exit(0);
                        }
                    }
                }

                //close the dongle
                for (int i = 0; i < 6; i++)
                {
                    ret = R4nd.Rockey((ushort)Ry4Cmd.RY_CLOSE, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, buffer);
                    if (0 == ret)
                    {
                        uiarrRy4ID[iMaxRockey] = lp1;
                        iMaxRockey++;
                        break;
                    }
                    else
                    {
                        Thread.Sleep(2000);
                        if (i >= 5)
                        {
                            RadMessageBox.Show("Dongle Close Failed : " + ret, "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
                            Environment.Exit(0);
                        }
                    }
                }

                PMS_Version();   //check the pms version
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex + "", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        enum Ry4Cmd : ushort
        {
            RY_FIND = 1,
            RY_FIND_NEXT,
            RY_OPEN,
            RY_CLOSE,
            RY_READ,
            RY_WRITE,
            RY_RANDOM,
            RY_SEED,
            RY_WRITE_USERID,
            RY_READ_USERID,
            RY_SET_MOUDLE,
            RY_CHECK_MOUDLE,
            RY_WRITE_ARITHMETIC,
            RY_CALCULATE1,
            RY_CALCULATE2,
            RY_CALCULATE3,
            RY_DECREASE
        };

        private void timer2_Tick(object sender, EventArgs e)
        {          
            try
            {
                //thread to check the dongle
                Thread thr_Controller = new Thread(Dongle2);
                thr_Controller.Start();
                //Dongle2();

                ////delete the prod lines if exceeds the allowed lines
                //DebugLog("Login.cs(timer2_Tick), LINES - " + LINES);
                //SqlCommand cmdd = new SqlCommand("DELETE FROM PROD_LINE_DB WHERE I_ID NOT IN (SELECT TOP " + LINES + " I_ID FROM PROD_LINE_DB)", dc.con);
                //cmdd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                RadMessageBox.Show(ex + "");
            }
        }

        public void PMS_Version()
        {
            //set the pms version
            Database_Connection.SET_PMSCIENT = PMSCLIENT;
            if (PMSCLIENT == "1")
            {
                //update the user access previlages
                SqlCommand cmd = new SqlCommand("UPDATE USER_LOGIN_DETAILS SET BUFFER_STATION = 'N',QC_MAIN = 'N',QC_SUB = 'N', MB_MAIN = 'N',MB_SUB = 'N',PRODUCTION_PLANNING = 'N',SKILLS = 'N',EMPLOYEE_SKILL = 'N',OPERATION_SKILL = 'N',EMPLOYEE_QC_REPORT =  'N',OPERATION_QC_REPORT = 'N',PAYROLL_REPORT = 'N',QC_MO_REPORT = 'N',QC_STATION_REPORT = 'N',MACHINE_REPORT = 'N',MACHINE_ASSIGN_REPORT = 'N',MACHINE_REPAIR_REPORT = 'N',TOP_DEFECTS = 'N',STATION_WIP = 'N',LINE_BALANCING = 'N',PERFORMANCE_REPORT = 'N',SPARE_REPORT = 'N',SPARE_INVENTORY_REPORT = 'N',SPARE_MAIN = 'N',SPARE_SUB = 'N'", dc.con);
                cmd.ExecuteNonQuery();
            }
            else if (PMSCLIENT == "2")
            {

            }
            else
            {
                //close the application if pms client not enabled for the key
                RadMessageBox.Show("PMS CLIENT Not Enabled for this Key", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
                Environment.Exit(0);
            }
        }

        public void DebugLog(string Message)
        {
            try
            {
                //string path = "C:\\SMARTMRT\\SmartMRT MGIS\\Debug\\" + DateTime.Now.ToString("MMMM yyyy");
                string path = Application.StartupPath + "\\Debug\\" + DateTime.Now.ToString("MMMM yyyy");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filepath = path + "\\DebugLogs_" + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".txt";
                if (!File.Exists(filepath))
                {
                    using (StreamWriter sw = File.CreateText(filepath))
                    {
                        sw.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " : " + Message);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(filepath))
                    {
                        sw.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " : " + Message);
                    }
                }
            }
            catch (Exception ex)
            {
                //WriteToExFile("Debug Logfile is in Use : " + ex.Message + " : " + ex);
            }
        }
    }
}
