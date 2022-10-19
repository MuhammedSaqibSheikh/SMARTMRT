using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace SMARTMRT
{
    class Database_Connection
    {
        public SqlConnection con;  //sql conncetion object
        public MySqlConnection conn;    //mysql connection object
        String a = "Connected to Database";
        String password = "1234";
        String IPAddress = "127.0.0.1";
        String UserName = "sa";

        public static String PMSCLIENT = "0"; 
        public static String user = "Super User";
        public static String IP = "127.0.0.1";

        public static String SET_PMSCIENT
        {
            get { return PMSCLIENT; }
            set { PMSCLIENT = value; }
        }

        public static String SET_USER
        {
            get { return user; }
            set { user = value; }
        }
        public static String GET_SERVER_IP
        {
            get { return IP; }
            set { IP = value; }
        }

        public String OpenConnection()
        {
            try
            {
                getPassword();  //get pms server ipaddress and login credential
                password = DecryptPassword(password, false);   //decrypt password
                con = new SqlConnection("Data Source=" + IPAddress + ",1433;Network Library=DBMSSOCN;Initial Catalog=MRT_GLOBALDB;User ID=" + UserName + ";Password=" + password + ";Connection Timeout=5;MultipleActiveResultSets=true");
                con.Open();  //connect to pms server
                return (a);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to Connect to Database "+ex);
                Console.WriteLine(ex);
                return ("UNABLE");
            }            
        }

        public String OpenMYSQLConnection(String server)
        {
            try
            {
                conn = new MySqlConnection("SERVER=" + server + ";DATABASE=mrt_local;UID=GUI;PASSWORD=octorite!;Connection Timeout=5;");
                conn.Open();  //connect to controller
                return (a);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                OpenConnection();   //open coonection

                //update controller
                SqlCommand cmd = new SqlCommand("update Setup set V_CONTROLLER = '--SELECT--'", con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Unable to Connect to Cluster : " + server + ". Please Check the Internet Connections.");
                return "UNABLE";
            }
        }

        //convert image to byte[]
        public byte[] GetImage()
        {
            byte[] image = null;
            try
            {
                OpenConnection();  //open connection

                //get logo
                SqlCommand cmd = new SqlCommand("SELECT COMPANY_LOGO FROM SETUP", con);
                if (cmd.ExecuteScalar().ToString() != "")
                {
                    image = (byte[])cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
            }
            return image;
        }

        //close connection to controller
        public void Close_Connection()
        {
            try
            {
                //check if connection is open
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        //triple DES Algorithm Decrypt
        public static string DecryptPassword(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            AppSettingsReader settingsReader = new AppSettingsReader();
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

        //get pms server
        public void getPassword()
        {            
            //file path
            String path = "C:\\Program Files (x86)\\SmartMRT";
            //check if file path exists
            if (Directory.Exists(path))
            {
                //file name
                string filepath = path + "\\SmartMRT_Connection.txt";
                String line;
                //check if file exists
                if (File.Exists(filepath))
                {
                    //open the file
                    StreamReader file = new StreamReader(filepath);
                    while ((line = file.ReadLine()) != null)
                    {
                        //check if file empty
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
                            //get password
                            if (GlobalVarName.Equals("Password"))
                            {
                                GlobalVarValue = token.NextToken();
                                GlobalVarValue = GlobalVarValue.Trim();
                                password = GlobalVarValue;
                                password = password + "=";
                            }

                            //get ipaddress
                            if (GlobalVarName.Equals("IPAddress"))
                            {
                                GlobalVarValue = token.NextToken();
                                GlobalVarValue = GlobalVarValue.Trim();
                                IPAddress = GlobalVarValue;
                                GET_SERVER_IP = IPAddress;
                            }

                            //get username
                            if (GlobalVarName.Equals("UserName"))
                            {
                                GlobalVarValue = token.NextToken();
                                GlobalVarValue = GlobalVarValue.Trim();
                                UserName = GlobalVarValue;
                            }
                        }
                    }
                    file.Close();
                }
            }            
        }
    }
}
