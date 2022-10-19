using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace SMARTMRT
{
    public partial class Design_Sequence : Telerik.WinControls.UI.RadForm
    {
        public Design_Sequence()
        {
            InitializeComponent();
        }
        Database_Connection dc = new Database_Connection();  //connection class
        String theme = "";

        private void Design_Sequence_Load(object sender, EventArgs e)
        {
            
            dgvdesignoperation.MasterTemplate.SelectLastAddedRow = false;
            dgvdesignsequence.MasterTemplate.SelectLastAddedRow = false;
            this.CenterToParent();       //keep form centered to screen      
            dc.OpenConnection();         //open connection
            radPanel2.Visible = false;

            //get all the operations details
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_OPERATION_CODE,V_OPERATION_DESC,D_PIECERATE,D_SAM FROM OPERATION_DB", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "OPERATION_DB");

            dgvdesignoperation.DataSource = ds.Tables["OPERATION_DB"].DefaultView;
            dgvdesignoperation.Columns["V_OPERATION_CODE"].HeaderText = "Operation Code ";
            dgvdesignoperation.Columns["V_OPERATION_DESC"].HeaderText = "Operation Decsription";
            dgvdesignoperation.Columns["D_PIECERATE"].HeaderText = "Piece Rate";
            dgvdesignoperation.Columns["D_SAM"].HeaderText = "SAM";
            dgvdesignoperation.Visible = false;

            if (dgvdesignoperation.Rows.Count > 0)
            {
                dgvdesignoperation.Visible = true;
            }
            dgvdesignsequence.Visible = false;
            da.Dispose();
            dgvdesignsequence.Columns[4].IsVisible = false;
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            RowSelected();   //add operation to design sequence
        }

        public void RowSelected()
        {
            //check if user selected any operation
            if (dgvdesignoperation.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                dgvdesignsequence.Visible = true;

                String opcode = dgvdesignoperation.SelectedRows[0].Cells[0].Value + string.Empty;
                String opdesc = dgvdesignoperation.SelectedRows[0].Cells[1].Value + string.Empty;

                //check if operation already exists
                for (int i = 0; i < dgvdesignsequence.Rows.Count; i++)
                {
                    if (dgvdesignsequence.Rows[i].Cells[2].Value.ToString().Equals(opcode))// && dataGridView1.Rows[i].Cells[1].Value.ToString().Equals(cmbarticle.Text) && dataGridView1.Rows[i].Cells[2].Value.ToString().Equals(cmbsize.Text) && dataGridView1.Rows[i].Cells[3].Value.ToString().Equals(user1) && dataGridView1.Rows[i].Cells[4].Value.ToString().Equals(user2) && dataGridView1.Rows[i].Cells[5].Value.ToString().Equals(user3) && dataGridView1.Rows[i].Cells[6].Value.ToString().Equals(user4) && dataGridView1.Rows[i].Cells[7].Value.ToString().Equals(user5) && dataGridView1.Rows[i].Cells[8].Value.ToString().Equals(user6) && dataGridView1.Rows[i].Cells[9].Value.ToString().Equals(user7) && dataGridView1.Rows[i].Cells[10].Value.ToString().Equals(user8) && dataGridView1.Rows[i].Cells[11].Value.ToString().Equals(user9) && dataGridView1.Rows[i].Cells[12].Value.ToString().Equals(user10))
                    {
                        dgvdesignsequence.Rows[i].IsSelected = true;
                        dgvdesignsequence.Rows[i].IsCurrent = true;
                        radLabel4.Text = "Row Already Exists";
                        return;
                    }
                }

                //add to operation
                int k = dgvdesignsequence.Rows.Count + 1;
                dgvdesignsequence.Rows.Add(k, k, opcode, opdesc, 'Y');
                btnsavesequence.ForeColor = Color.Red;
            }
        }

        private void radLabel4_TextChanged(object sender, EventArgs e)
        {
            MyTimer.Interval = 5000; //5 Sec
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            radPanel2.Visible = true;
            MyTimer.Start();
        }

        Timer MyTimer = new Timer();

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            radLabel4.Text = "";
            radPanel2.Visible = false;
            MyTimer.Stop();
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            //delete the operation from design sequence
            if (dgvdesignsequence.SelectedRows.Count > 0)
            {
                dgvdesignsequence.Rows.RemoveAt(dgvdesignsequence.SelectedRows[0].Index);
                btnsavesequence.ForeColor = Color.Red;
            }
            //reset sequence
            UpdateSeqno();
        }

        public void UpdateSeqno()
        {
            //reset sequence
            for (int i = 0; i < dgvdesignsequence.Rows.Count; i++)
            {
                dgvdesignsequence.Rows[i].Cells[0].Value = i + 1;
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateSeqno();   //reste sequence
                dgvdesignsequence.Rows[0].Cells[1].Value = "1";
                String sequence_no = "1";
                int sequence_no1 = 1;

                //check if any operation selected
                if (dgvdesignsequence.Rows.Count == 0)
                {
                    return;
                }

                //get all the selected operation
                for (int i = 0; i < dgvdesignsequence.Rows.Count; i++)
                {
                    if (sequence_no != "")
                    {
                        sequence_no1 = Int32.Parse(sequence_no);
                    }

                    //check seuqnece if integer and starts with 1
                    sequence_no = dgvdesignsequence.Rows[i].Cells[1].Value + string.Empty;
                    Regex r = new Regex("^[0-9]*$");
                    if (!(r.IsMatch(sequence_no)) || sequence_no == "" || sequence_no == "0")
                    {
                        sequence_no = "1";
                    }   
                    
                    //check if sequence order is correct
                    int n = sequence_no1 + 1;
                    if (sequence_no != sequence_no1.ToString() && sequence_no != n.ToString())
                    {
                        DialogResult result = MessageBox.Show("Sequence is not in Order. Do you want to make it in Order", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if (result.Equals(DialogResult.OK))
                        {
                            SequenceOrdering();  //re-order the sequence
                        }
                    }
                }

                //check if the article is already used in production
                SqlCommand cmd = new SqlCommand("select count(*) from STATION_ASSIGN where V_ARTICLE_ID='" + txtarticleid.Text + "'", dc.con);
                int count = int.Parse(cmd.ExecuteScalar() + "");
                if (count != 0)
                {
                    DialogResult result = MessageBox.Show("Changes to this Article will Affect all Station Assigns?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (result.Equals(DialogResult.Cancel))
                    {
                        return;
                    }
                } 
                
                //delete design sequence
                cmd = new SqlCommand("delete from DESIGN_SEQUENCE where V_ARTICLE_ID='" + txtarticleid.Text + "'", dc.con);
                cmd.ExecuteNonQuery();

                //get all the operation
                for (int i = 0; i < dgvdesignsequence.Rows.Count; i++)
                {
                    String opcode = dgvdesignsequence.Rows[i].Cells[2].Value.ToString();
                    sequence_no = dgvdesignsequence.Rows[i].Cells[1].Value.ToString();
                    String articleId = txtarticleid.Text;
                    String op_seq = dgvdesignsequence.Rows[i].Cells[0].Value.ToString();

                    //insert into design sequence
                    cmd = new SqlCommand("insert into DESIGN_SEQUENCE values('" + opcode + "','" + articleId + "','" + op_seq + "','" + sequence_no + "')", dc.con);
                    cmd.ExecuteNonQuery();

                    //DebugLog("Design_Sequence.cs(radButton1_Click), SQL - insert into DESIGN_SEQUENCE values('" + opcode + "','" + articleId + "','" + op_seq + "','" + sequence_no + "')");
                }

                //delete sequence in station assign which is not present in design sequence for that article
                cmd = new SqlCommand("DELETE FROM STATION_ASSIGN WHERE V_ARTICLE_ID='" + txtarticleid.Text + "' and I_SEQUENCE_NO NOT IN(SELECT D.I_SEQUENCE_NO FROM DESIGN_SEQUENCE D WHERE V_ARTICLE_ID='" + txtarticleid.Text + "')", dc.con);
                cmd.ExecuteNonQuery();

                //get all the operation
                for(int i = 0; i < dgvdesignsequence.Rows.Count; i++)
                {
                    String edit= dgvdesignsequence.Rows[i].Cells[4].Value.ToString();
                    if (edit == "Y")
                    {
                        //reset the sequence in station assign
                        SqlDataAdapter sda = new SqlDataAdapter("select I_SEQUENCE_NO from STATION_ASSIGN where I_SEQUENCE_NO>='" + i + "' and V_ARTICLE_ID='" + txtarticleid.Text + "' order by I_SEQUENCE_NO desc", dc.con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        sda.Dispose();
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            int seq = int.Parse(dt.Rows[j][0].ToString());
                            int seq1 = seq + 1;

                            //update sequence in station assign
                            cmd = new SqlCommand("update STATION_ASSIGN set I_SEQUENCE_NO='" + seq1 + "' where I_SEQUENCE_NO='" + seq + "'", dc.con);
                            cmd.ExecuteNonQuery();                            
                        }

                        String strStnAsgn = txtStnAsgn.Text;
                        //add new sequence into the station assign
                        sda = new SqlDataAdapter("select distinct V_MO_NO,V_MO_LINE,I_ROW_NO from STATION_ASSIGN where  V_ARTICLE_ID='" + txtarticleid.Text + "' ", dc.con);
                        dt = new DataTable();
                        sda.Fill(dt);
                        sda.Dispose();
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            String MO = dt.Rows[j][0].ToString();
                            String MOLINE = dt.Rows[j][1].ToString();
                            String ROWNO = dt.Rows[j][2].ToString();

                            cmd = new SqlCommand("insert into STATION_ASSIGN values('" + MO + "','" + MOLINE + "','" + i + "','0','" + ROWNO + "','0','" + txtarticleid.Text + "','0','" + strStnAsgn + "')", dc.con);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                radLabel4.Text = "Records Saved";
                btnsavesequence.ForeColor = Color.Lime;

            }
            catch(Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        //re-order the sequence
        public void SequenceOrdering()
        {
            String sequence_no = "1";
            int n = 1;
            dgvdesignsequence.Rows[0].Cells[1].Value = "1";

            for (int i = 0; i < dgvdesignsequence.Rows.Count; i++)
            {
                sequence_no = dgvdesignsequence.Rows[i].Cells[1].Value + string.Empty;
                if (sequence_no == n.ToString())
                {
                    continue;
                }

                n = n + 1;
                dgvdesignsequence.Rows[i].Cells[1].Value = n;
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            RowSelected();   //add the selected operation
        }

        private void Design_Sequence_Shown(object sender, EventArgs e)
        {
            //get the all the operation for the article
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_OPERATION_CODE,I_SEQUENCE_NO,I_OPERATION_SEQUENCE_NO FROM DESIGN_SEQUENCE WHERE V_ARTICLE_ID='" + txtarticleid.Text + "' ORDER BY I_SEQUENCE_NO", dc.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String opcode = dt.Rows[i][0].ToString();
                String opdesc = "";
                String seqno = dt.Rows[i][1].ToString();
                String op_seq_no = dt.Rows[i][2].ToString();

                //get operation description
                da = new SqlDataAdapter("SELECT V_OPERATION_DESC FROM OPERATION_DB WHERE V_OPERATION_CODE='" + opcode + "'", dc.con);
                DataTable dt1 = new DataTable();
                da.Fill(dt1);
                da.Dispose();
                for (int k = 0; k < dt1.Rows.Count; k++)
                {
                    opdesc = dt1.Rows[k][0].ToString();
                }

                //add to grid
                dgvdesignsequence.Rows.Add(op_seq_no, seqno, opcode, opdesc, 'N');
                dgvdesignsequence.Visible = true;
            }
        }

        private void dgvsequence_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            //UpdateSeqno();
        }

        private void Design_Sequence_Initialized(object sender, EventArgs e)
        {
            dc.OpenConnection();   //open connection

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

            //change grid theme
            GridTheme(theme);
        }

        //set grid theme
        public void GridTheme(String theme)
        {
            dgvdesignoperation.ThemeName = theme;
            dgvdesignsequence.ThemeName = theme;
        }

        private void dgvdesignoperation_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme is selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvdesignoperation.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvdesignoperation.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvdesignoperation.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvdesignoperation.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvdesignsequence_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme is selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvdesignsequence.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvdesignsequence.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvdesignsequence.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvdesignsequence.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        //public void DebugLog(string Message)
        //{
        //    try
        //    {
        //        //string path = "C:\\SMARTMRT\\SmartMRT MGIS\\Debug\\" + DateTime.Now.ToString("MMMM yyyy");
        //        string path = Application.StartupPath + "\\Debug\\" + DateTime.Now.ToString("MMMM yyyy");
        //        if (!Directory.Exists(path))
        //        {
        //            Directory.CreateDirectory(path);
        //        }
        //        string filepath = path + "\\DebugLogs_" + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".txt";
        //        if (!File.Exists(filepath))
        //        {
        //            using (StreamWriter sw = File.CreateText(filepath))
        //            {
        //                sw.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " : " + Message);
        //            }
        //        }
        //        else
        //        {
        //            using (StreamWriter sw = File.AppendText(filepath))
        //            {
        //                sw.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " : " + Message);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //WriteToExFile("Debug Logfile is in Use : " + ex.Message + " : " + ex);
        //    }
        //}
    }
}
