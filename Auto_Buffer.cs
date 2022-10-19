using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace SMARTMRT
{
    public partial class Auto_Buffer : Telerik.WinControls.UI.RadForm
    {
        public Auto_Buffer()
        {
            InitializeComponent();
        }
        Database_Connection dc = new Database_Connection();  //connection class
        String controller_name = "";
        String bufferstation = "";
        String bufferstationid = "";
        String theme = "";

        private void Auto_Buffer_Load(object sender, EventArgs e)
        {
            dgvmo.MasterTemplate.SelectLastAddedRow = false;
            dgvautobuffer.MasterTemplate.SelectLastAddedRow = false;
            RadMessageBox.SetThemeName("FluentDark"); //Message Box theme
            dgvmo.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvautobuffer.MasterView.TableSearchRow.ShowCloseButton = false;

            this.CenterToScreen(); //Centered to screen
            select_controller(); //Get the selected Controller

            //Get the all the buffer groups
            SqlDataAdapter sda = new SqlDataAdapter("Select V_BUFFER_GROUP_DESC from BUFFER_GROUP", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                cmbbuffergroup.Items.Add(dt.Rows[j][0].ToString());
                cmbbuffergroup.SelectedIndex = 0;
            }
            sda.Dispose();
        }

        public void Buffer_Station()
        {
            //clear the grids
            dgvmo.Rows.Clear();
            dgvautobuffer.Rows.Clear();
            String buffergroup = "";

            //get the selected Buffer group ID
            SqlCommand cmd = new SqlCommand("select V_BUFFER_GROUP_ID from BUFFER_GROUP where V_BUFFER_GROUP_DESC='" + cmbbuffergroup.Text + "'", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                buffergroup = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            bufferstation = "";
            bufferstationid = "";

            //get the Stations for that buffer group
            SqlDataAdapter sda = new SqlDataAdapter("Select V_BUFFER_STATION_ID from BUFFER_STATION where V_BUFFER_GROUP_ID='" + buffergroup + "'", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                bufferstation += " STN_ID='" + dt.Rows[j][0].ToString() + "' or";
                bufferstationid += dt.Rows[j][0].ToString() + ",";
            }
            sda.Dispose();

            //check if the buffer group has any station assigned
            if (bufferstation.Length > 0)
            {
                bufferstation = bufferstation.Remove(bufferstation.Length - 1, 1);
                bufferstation = bufferstation.Remove(bufferstation.Length - 1, 1);
                bufferstationid = bufferstationid.Remove(bufferstationid.Length - 1, 1);
            }
            else
            {
                radLabel1.Text = "Create the Buffer Group and Assign Stations to that Group";
                return;
            }

            //get the MO from buffer
            MySqlDataAdapter sda1 = new MySqlDataAdapter("Select distinct MO_NO,MO_LINE from bufferhangers where " + bufferstation, dc.conn);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            sda1.Dispose();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                String MO = dt1.Rows[i][0].ToString();
                String MOLINE = dt1.Rows[i][1].ToString();

                int count1 = 0;
                int count = 0;

                //get the count of the hanger for the buffer group
                sda = new SqlDataAdapter("Select V_BUFFER_STATION_ID from BUFFER_STATION where V_BUFFER_GROUP_ID='" + buffergroup + "'", dc.con);
                dt = new DataTable();
                sda.Fill(dt);
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    //get the hanger count for that station in bufferhangers
                    MySqlCommand cmd2 = new MySqlCommand("Select count(*) from bufferhangers where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "' and STN_ID='" + dt.Rows[j][0].ToString() + "'", dc.conn);
                    count += int.Parse(cmd2.ExecuteScalar() + "");

                    //get the hanger count for that station in buffercallout
                    cmd2 = new MySqlCommand("Select sum(COUNT) from buffercallout where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "' and STN_ID='" + dt.Rows[j][0].ToString() + "'", dc.conn);
                    String temp = cmd2.ExecuteScalar() + "";
                    if (temp != "")
                    {
                        count1 += int.Parse(temp);
                    }                    
                }
                sda.Dispose();

                //hangers in bufferhangers - hangers in buffercallout
                count = count - count1;
                if (count < 0)
                {
                    count = 0;
                }

                //check if the MO has Auto buffer Enabled
                String autobuffer = "FALSE";
                MySqlCommand cmd1 = new MySqlCommand("Select COUNT(*) from autobufferlink where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "'", dc.conn);
                int temp1 = int.Parse(cmd1.ExecuteScalar() + "");
                if (temp1 > 0)
                {
                    autobuffer = "TRUE";
                }

                dgvmo.Rows.Add(MO, MOLINE, count, autobuffer);
            }
        }

        public void select_controller()
        {
            dc.OpenConnection();  //Open Connection
            String ipaddress = "";
            String controller = "";

            //get the ip address and port number of the selected controller
            SqlCommand cmd = new SqlCommand("select V_CONTROLLER from Setup", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                controller = sdr.GetValue(0).ToString();
                controller_name = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get the IP address of the selected Controller
            cmd = new SqlCommand("select V_CLUSTER_IP_ADDRESS from CLUSTER_DB where V_CLUSTER_ID='" + controller + "'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                ipaddress = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //return if there is no ip address
            if (ipaddress == "")
            {
                controller_name = "--SELECT--";
                return;
            }

            dc.Close_Connection(); //close the connection if Open
            dc.OpenMYSQLConnection(ipaddress); //Open connection
        }

        private void Auto_Buffer_Initialized(object sender, EventArgs e)
        {
            dc.OpenConnection(); //Open connection

            //get language and theme
            String Lang = "";
            SqlCommand cmd = new SqlCommand("SELECT Language,ThemeName FROM Setup", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Lang = sdr.GetValue(0).ToString();
                theme = sdr.GetValue(1).ToString();
            }
            sdr.Close();

            //change the grid theme
            GridTheme(theme);
        }

        //set the grid theme
        public void GridTheme(String theme)
        {
            dgvmo.ThemeName = theme;
            dgvautobuffer.ThemeName = theme;
        }

        private void Auto_Buffer_FormClosed(object sender, FormClosedEventArgs e)
        {
            dc.Close_Connection(); //close the connection on form close
        }        

        private void cmbbuffergroup_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //check if the controller is selected
            if (controller_name == "--SELECT--" || controller_name == "")
            {
                return;
            }
            Buffer_Station(); //get the buffer details
        }
        
        private void btnsave_Click(object sender, EventArgs e)
        {
            //check if the controller is selected
            if (controller_name == "" || controller_name == "--SELECT--")
            {
                return;
            }

            //check if the buffer group has station assigned to it 
            if (bufferstationid.Length <= 0)
            {
                radLabel1.Text = "Create the Buffer Group and Assign Stations to that Group";
                return;
            }

            try
            {
                //get the selected MO
                if (dgvmo.SelectedRows.Count > 0)
                {
                    String MO = dgvmo.SelectedRows[0].Cells[0].Value.ToString();
                    String MOLINE = dgvmo.SelectedRows[0].Cells[1].Value.ToString();

                    String[] temp = bufferstationid.Split(',');
                    for (int j = 0; j < temp.Length; j++)
                    {
                        for (int i = 0; i < dgvautobuffer.Rows.Count; i++)
                        {
                            String stnid = dgvautobuffer.Rows[i].Cells[0].Value.ToString();
                            String count = dgvautobuffer.Rows[i].Cells[2].Value.ToString();

                            //delete if the MO is already in auto buffer
                            MySqlCommand cmd = new MySqlCommand("delete from autobufferlink where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "' and LINK_STN_ID='" + stnid + "' and BUFF_STN_ID='" + temp[j] + "'", dc.conn);
                            cmd.ExecuteNonQuery();

                            //insert into auto buffer
                            cmd = new MySqlCommand("insert into autobufferlink (MO_NO,MO_LINE,LINK_STN_ID,LINK_LIMIT,BUFF_STN_ID) values('" + MO + "','" + MOLINE + "','" + stnid + "','" + count + "','" + temp[j] + "')", dc.conn);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    radLabel1.Text = "Auto Buffer Configured for MO : " + MO + " MOLINE :" + MOLINE;
                }
            }
            catch (Exception ex)
            {
                radLabel1.Text = ex.Message;
            }
        }

        private void radLabel1_TextChanged(object sender, EventArgs e)
        {
            MyTimer.Interval = 5000; //5 Sec
            panel1.Visible = true;
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            MyTimer.Start();
        }

        Timer MyTimer = new Timer();

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            radLabel1.Text = "";
            panel1.Visible = false;
            MyTimer.Stop();
        }
                
        private void dgvmo_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            //check if the buffer group has station assigned to it
            if (bufferstationid.Length <= 0)
            {
                radLabel1.Text = "Create the Buffer Group and Assign Stations to that Group";
                return;
            }

            try
            {
                String MO = dgvmo.Rows[e.RowIndex].Cells[0].Value.ToString();
                String MOLINE = dgvmo.Rows[e.RowIndex].Cells[1].Value.ToString();
                int seq1 = 0;
                panel3.Visible = true;
                String prev_seq = "0";
                String cur_seq = "0";

                dgvautobuffer.Rows.Clear();
                //get the next station id of the buffer
                MySqlDataAdapter cmd3 = new MySqlDataAdapter("SELECT s.STN_ID,s.SEQ_NO from sequencestations s,stationdata sd WHERE s.MO_NO='" + MO + "' AND s.MO_LINE='" + MOLINE + "' AND sd.STATIONTYPE='3' AND s.STN_ID=sd.STN_ID", dc.conn);
                DataTable d1 = new DataTable();
                cmd3.Fill(d1);
                cmd3.Dispose();
                for (int i1 = 0; i1 < d1.Rows.Count; i1++)
                {
                    //check if the buffer group contains the station 
                    if (!bufferstation.Contains(d1.Rows[i1][0].ToString()))
                    {
                        continue;
                    }

                    //check if the previos sequence and current sequence same
                    prev_seq = cur_seq;
                    cur_seq = d1.Rows[i1][1].ToString();

                    if (prev_seq == cur_seq)
                    {
                        continue;
                    }

                    //get the next sequence
                    int nextseq = int.Parse(d1.Rows[i1][1].ToString());
                    nextseq += 1;

                    //get the next stations after the buffer
                    MySqlDataAdapter sda3 = new MySqlDataAdapter("select STN_ID from sequencestations where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "' and SEQ_NO='" + nextseq + "'", dc.conn);
                    DataTable dt4 = new DataTable();
                    sda3.Fill(dt4);
                    sda3.Dispose();
                    for (int k = 0; k < dt4.Rows.Count; k++)
                    {
                        //get the next station's station type
                        SqlDataAdapter sequence = new SqlDataAdapter("select I_STATION_TYPE from  STATION_DATA where I_STN_ID='" + dt4.Rows[k][0].ToString() + "'", dc.con);
                        DataTable dtseq = new DataTable();
                        sequence.Fill(dtseq);
                        sequence.Dispose();
                        for (int q = 0; q < dtseq.Rows.Count; q++)
                        {                 
                            //check if the next sequence is a Sorting station
                            if (dtseq.Rows[q][0].ToString() == "7")
                            {
                                seq1 = nextseq + 1;

                                //get the stations after the Sorting stations
                                sda3 = new MySqlDataAdapter("select STN_ID from sequencestations where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "' and SEQ_NO='" + seq1 + "'", dc.conn);
                                DataTable dt5 = new DataTable();
                                sda3.Fill(dt5);
                                sda3.Dispose();
                                for (int m = 0; m < dt5.Rows.Count; m++)
                                {
                                    //get the minimum hanger form the auto buffer
                                    String minhanger = "10";
                                    MySqlCommand cmd = new MySqlCommand("select LINK_LIMIT from autobufferlink where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "' and LINK_STN_ID='" + dt5.Rows[m][0].ToString() + "'", dc.conn);
                                    if (cmd.ExecuteScalar() + "" != "")
                                    {
                                        minhanger = cmd.ExecuteScalar() + "";
                                    }

                                    //get the station no for that station
                                    sequence = new SqlDataAdapter("select I_INFEED_LINE_NO,I_STN_NO_INFEED from  STATION_DATA where I_STN_ID='" + dt5.Rows[m][0].ToString() + "'", dc.con);
                                    DataTable dtseq1 = new DataTable();
                                    sequence.Fill(dtseq1);
                                    sequence.Dispose();
                                    for (int n = 0; n < dtseq1.Rows.Count; n++)
                                    {
                                        dgvautobuffer.Rows.Add(dt5.Rows[m][0].ToString(), dtseq1.Rows[n][0].ToString() + "." + dtseq1.Rows[n][1].ToString(), minhanger);
                                    }
                                }
                            }
                            else
                            {
                                seq1 = nextseq;

                                //get the minimum hanger form the auto buffer
                                String minhanger = "10";
                                MySqlCommand cmd = new MySqlCommand("select LINK_LIMIT from autobufferlink where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "' and LINK_STN_ID='" + dt4.Rows[k][0].ToString() + "'", dc.conn);
                                if (cmd.ExecuteScalar() + "" != "")
                                {
                                    minhanger = cmd.ExecuteScalar() + "";
                                }

                                //get the station no for that stations
                                sequence = new SqlDataAdapter("select I_INFEED_LINE_NO,I_STN_NO_INFEED from  STATION_DATA where I_STN_ID='" + dt4.Rows[k][0].ToString() + "'", dc.con);
                                DataTable dtseq1 = new DataTable();
                                sequence.Fill(dtseq1);
                                sequence.Dispose();
                                for (int n = 0; n < dtseq1.Rows.Count; n++)
                                {
                                    dgvautobuffer.Rows.Add(dt4.Rows[k][0].ToString(), dtseq1.Rows[n][0].ToString() + "." + dtseq1.Rows[n][1].ToString(), minhanger);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                radLabel1.Text = ex.Message;
            }
        }

        private void dgvautobuffer_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                {
                    return;
                }

                //Check if the minimum hanger is a integer
                Regex r = new Regex("^[0-9]*$");
                if (!r.IsMatch(dgvautobuffer.Rows[e.RowIndex].Cells[2].Value.ToString()))
                {
                    radLabel1.Text = "Invalid Minimum Hangers value. Example : 15";
                    dgvautobuffer.SelectedCells[0].Value = "10";
                    return;
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex + "", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        private void dgvmo_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change the fore color if the selected theme is these
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvmo.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvmo.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvmo.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvmo.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvautobuffer_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change the fore color if the selected theme is these
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvautobuffer.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvautobuffer.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvautobuffer.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvautobuffer.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void btndisable_Click(object sender, EventArgs e)
        {
            //check if the controller is selected
            if (controller_name == "" || controller_name == "--SELECT--")
            {
                return;
            }

            //check if the station are assigned for that buffer group
            if (bufferstationid.Length <= 0)
            {
                radLabel1.Text = "Create the Buffer Group and Assign Stations to that Group";
                return;
            }

            try
            {
                //get the selected mo 
                if (dgvmo.SelectedRows.Count > 0)
                {
                    String MO = dgvmo.SelectedRows[0].Cells[0].Value.ToString();
                    String MOLINE = dgvmo.SelectedRows[0].Cells[1].Value.ToString();

                    //delete from the auto buffer
                    MySqlCommand cmd = new MySqlCommand("delete from autobufferlink where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "'", dc.conn);
                    cmd.ExecuteNonQuery();

                    radLabel1.Text = "Auto Buffer Disabled for MO : " + MO + " MOLINE :" + MOLINE;
                }
            }
            catch (Exception ex)
            {
                radLabel1.Text = ex.Message;
            }
        }
    }
}
