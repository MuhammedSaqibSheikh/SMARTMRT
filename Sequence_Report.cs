using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;

namespace SMARTMRT
{
    public partial class Sequence_Report : Form
    {
        public Sequence_Report()
        {
            InitializeComponent();
        }

        Database_Connection dc = new Database_Connection();    //connection class

        private void Sequence_Report_Load_1(object sender, EventArgs e)
        {
            try
            {
                RadMessageBox.SetThemeName("FluentDark");   //set messagebox theme
                String mono = "";
                DataRow row1;
                DataView view, view1;
                dc.OpenConnection();

                //get last selected mo
                SqlDataAdapter da1 = new SqlDataAdapter("SELECT V_MO_NO from LAST_SELECT_MO", dc.con);
                DataTable dt = new DataTable();
                da1.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    mono = dt.Rows[0]["V_MO_NO"].ToString();
                }

                DataSet SET = new DataSet("SEQ");
                DataTable data = new DataTable();
                DataTable data1 = new DataTable();

                data1.Columns.Add("V_MO_NO");
                data1.Columns.Add("V_COLOR_ID");
                data1.Columns.Add("V_SIZE_ID");
                data1.Columns.Add("V_ARTICLE_ID");
                data1.Columns.Add("V_MO_LINE");

                data1.Columns.Add("V_USER_DEF1");
                data1.Columns.Add("V_USER_DEF2");
                data1.Columns.Add("V_USER_DEF3");
                data1.Columns.Add("V_USER_DEF4");
                data1.Columns.Add("V_USER_DEF5");
                data1.Columns.Add("V_USER_DEF6");
                data1.Columns.Add("V_USER_DEF7");
                data1.Columns.Add("V_USER_DEF8");
                data1.Columns.Add("V_USER_DEF9");
                data1.Columns.Add("V_USER_DEF10");
                data1.Columns.Add("D_SHIPMENT_DATE");
                data1.Columns.Add("V_SHIPPING_MODE");
                data1.Columns.Add("V_PROD_LINE");
                data1.Columns.Add("V_CUSTOMER_NAME");
                data1.Columns.Add("V_SHIPPING_DEST");
                data1.Columns.Add("V_PURCHASE_ORDER");
                data1.Columns.Add("I_ORDER_QTY");
                data1.Columns.Add("V_SALES_ORDER");


                data.Columns.Add("V_MO_NO");
                data.Columns.Add("V_COLOR_ID");
                data.Columns.Add("V_COLOR_DESC");
                data.Columns.Add("V_SIZE_ID");
                data.Columns.Add("V_ARTICLE_ID");
                data.Columns.Add("V_MO_LINE");

                data.Columns.Add("V_USER_DEF1");
                data.Columns.Add("V_USER_DEF2");
                data.Columns.Add("V_USER_DEF3");
                data.Columns.Add("V_USER_DEF4");
                data.Columns.Add("V_USER_DEF5");
                data.Columns.Add("V_USER_DEF6");
                data.Columns.Add("V_USER_DEF7");
                data.Columns.Add("V_USER_DEF8");
                data.Columns.Add("V_USER_DEF9");
                data.Columns.Add("V_USER_DEF10");
                data.Columns.Add("I_ORDER_QTY");
                data.Columns.Add("V_OPERATION_CODE");
                data.Columns.Add("V_OPERATION_DESC");
                data.Columns.Add("D_PIECERATE");
                data.Columns.Add("D_SAM");
                data.Columns.Add("I_SEQUENCE_NO");
                data.Columns.Add("D_STATION_NO");
                data.Columns.Add("I_LINE_NO");
                data.Columns.Add("SEQUENCE1");
                data.Columns.Add("SEQUENCE2");
                data.Columns.Add("SEQUENCE3");
                data.Columns.Add("SEQUENCE4");
                data.Columns.Add("SEQUENCE5");
                data.Columns.Add("SEQUENCE6");
                data.Columns.Add("SEQUENCE7");
                data.Columns.Add("SEQUENCE8");
                data.Columns.Add("SEQUENCE9");
                data.Columns.Add("SEQUENCE10");
                data.Columns.Add("SEQUENCE11");
                data.Columns.Add("SEQUENCE12");
                data.Columns.Add("SEQUENCE13");
                data.Columns.Add("SEQUENCE14");

                data.Columns.Add("user1");
                data.Columns.Add("user2");
                data.Columns.Add("user3");
                data.Columns.Add("user4");
                data.Columns.Add("user5");
                data.Columns.Add("user6");
                data.Columns.Add("user7");
                data.Columns.Add("user8");
                data.Columns.Add("user9");
                data.Columns.Add("user10");
                data.Columns.Add("Version");

                SET.Tables.Add(data);
                SET.Tables.Add(data1);

                //get mo details
                SqlDataAdapter da9 = new SqlDataAdapter("SELECT LAST_SELECT_MO.V_MO_NO, MO_DETAILS.V_COLOR_ID, MO_DETAILS.V_SIZE_ID, MO_DETAILS.V_ARTICLE_ID, MO_DETAILS.V_MO_LINE, MO_DETAILS.V_USER_DEF1, MO_DETAILS.V_USER_DEF2, MO_DETAILS.V_USER_DEF3, MO_DETAILS.V_USER_DEF4, MO_DETAILS.V_USER_DEF5, MO_DETAILS.V_USER_DEF6, MO_DETAILS.V_USER_DEF7, MO_DETAILS.V_USER_DEF8, MO_DETAILS.V_USER_DEF9, MO_DETAILS.V_USER_DEF10, MO_DETAILS.D_SHIPMENT_DATE, MO_DETAILS.V_SHIPPING_MODE, MO_DETAILS.V_PROD_LINE, CUSTOMER_DB.V_CUSTOMER_NAME, MO_DETAILS.V_SHIPPING_DEST, MO_DETAILS.V_PURCHASE_ORDER, MO_DETAILS.I_ORDER_QTY, MO_DETAILS.V_SALES_ORDER FROM MRT_GLOBALDB.dbo.CUSTOMER_DB CUSTOMER_DB, MRT_GLOBALDB.dbo.LAST_SELECT_MO LAST_SELECT_MO, MRT_GLOBALDB.dbo.MO MO, MRT_GLOBALDB.dbo.MO_DETAILS MO_DETAILS WHERE LAST_SELECT_MO.V_MO_NO = MO.V_MO_NO AND MO.V_MO_NO = MO_DETAILS.V_MO_NO AND CUSTOMER_DB.V_CUSTOMER_ID = MO.V_CUSTOMER_ID AND(MO.V_MO_NO = '" + mono + "')  ", dc.con);
                DataTable dt9 = new DataTable();
                da9.Fill(dt9);
                for (int i = 0; i < dt9.Rows.Count; i++)
                {
                    row1 = data1.NewRow();
                    row1["V_MO_NO"] = dt9.Rows[i]["V_MO_NO"].ToString();
                    row1["V_COLOR_ID"] = dt9.Rows[i]["V_COLOR_ID"].ToString();
                    row1["V_SIZE_ID"] = dt9.Rows[i]["V_SIZE_ID"].ToString();
                    row1["V_ARTICLE_ID"] = dt9.Rows[i]["V_ARTICLE_ID"].ToString();
                    row1["V_MO_LINE"] = dt9.Rows[i]["V_MO_LINE"].ToString();
                    row1["V_USER_DEF1"] = dt9.Rows[i]["V_USER_DEF1"].ToString();
                    row1["V_USER_DEF2"] = dt9.Rows[i]["V_USER_DEF2"].ToString();
                    row1["V_USER_DEF3"] = dt9.Rows[i]["V_USER_DEF3"].ToString();
                    row1["V_USER_DEF4"] = dt9.Rows[i]["V_USER_DEF4"].ToString();
                    row1["V_USER_DEF5"] = dt9.Rows[i]["V_USER_DEF5"].ToString();
                    row1["V_USER_DEF6"] = dt9.Rows[i]["V_USER_DEF6"].ToString();
                    row1["V_USER_DEF7"] = dt9.Rows[i]["V_USER_DEF7"].ToString();
                    row1["V_USER_DEF8"] = dt9.Rows[i]["V_USER_DEF8"].ToString();
                    row1["V_USER_DEF9"] = dt9.Rows[i]["V_USER_DEF9"].ToString();
                    row1["V_USER_DEF10"] = dt9.Rows[i]["V_USER_DEF10"].ToString();
                    row1["D_SHIPMENT_DATE"] = dt9.Rows[i]["D_SHIPMENT_DATE"].ToString();
                    row1["V_SHIPPING_MODE"] = dt9.Rows[i]["V_SHIPPING_MODE"].ToString();
                    row1["V_PROD_LINE"] = dt9.Rows[i]["V_PROD_LINE"].ToString();
                    row1["V_CUSTOMER_NAME"] = dt9.Rows[i]["V_CUSTOMER_NAME"].ToString();
                    row1["V_SHIPPING_DEST"] = dt9.Rows[i]["V_SHIPPING_DEST"].ToString();
                    row1["V_PURCHASE_ORDER"] = dt9.Rows[i]["V_PURCHASE_ORDER"].ToString();
                    row1["I_ORDER_QTY"] = dt9.Rows[i]["I_ORDER_QTY"].ToString();
                    row1["V_SALES_ORDER"] = dt9.Rows[i]["V_SALES_ORDER"].ToString();
                    data1.Rows.Add(row1);
                }

                //special fields
                String USER1 = "";
                String USER2 = "";
                String USER3 = "";
                String USER4 = "";
                String USER5 = "";
                String USER6 = "";
                String USER7 = "";
                String USER8 = "";
                String USER9 = "";
                String USER10 = "";

                //get special field name
                SqlDataAdapter da = new SqlDataAdapter("select V_USER FROM USER_COLUMN_NAMES WHERE V_ENABLED='TRUE'and V_MRT='USER_DEF1'", dc.con);
                DataTable dt1 = new DataTable();
                da.Fill(dt1);
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    USER1 = dt1.Rows[i][0].ToString() + " :";
                }

                //get special field name
                da = new SqlDataAdapter("select V_USER FROM USER_COLUMN_NAMES WHERE V_ENABLED='TRUE'and V_MRT='USER_DEF2'", dc.con);
                dt1 = new DataTable();
                da.Fill(dt1);
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    USER2 = dt1.Rows[i][0].ToString() + " :";
                }

                //get special field name
                da = new SqlDataAdapter("select V_USER FROM USER_COLUMN_NAMES WHERE V_ENABLED='TRUE'and V_MRT='USER_DEF3'", dc.con);
                dt1 = new DataTable();
                da.Fill(dt1);
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    USER3 = dt1.Rows[i][0].ToString() + " :";
                }

                //get special field name
                da = new SqlDataAdapter("select V_USER FROM USER_COLUMN_NAMES WHERE V_ENABLED='TRUE'and V_MRT='USER_DEF4'", dc.con);
                dt1 = new DataTable();
                da.Fill(dt1);
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    USER4 = dt1.Rows[i][0].ToString() + " :";
                }

                //get special field name
                da = new SqlDataAdapter("select V_USER FROM USER_COLUMN_NAMES WHERE V_ENABLED='TRUE'and V_MRT='USER_DEF5'", dc.con);
                dt1 = new DataTable();
                da.Fill(dt1);
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    USER5 = dt1.Rows[i][0].ToString() + " :";
                }

                //get special field name
                da = new SqlDataAdapter("select V_USER FROM USER_COLUMN_NAMES WHERE V_ENABLED='TRUE'and V_MRT='USER_DEF6'", dc.con);
                dt1 = new DataTable();
                da.Fill(dt1);
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    USER6 = dt1.Rows[i][0].ToString() + " :";
                }

                //get special field name
                da = new SqlDataAdapter("select V_USER FROM USER_COLUMN_NAMES WHERE V_ENABLED='TRUE'and V_MRT='USER_DEF7'", dc.con);
                dt1 = new DataTable();
                da.Fill(dt1);
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    USER7 = dt1.Rows[i][0].ToString() + " :";
                }

                //get special field name
                da = new SqlDataAdapter("select V_USER FROM USER_COLUMN_NAMES WHERE V_ENABLED='TRUE'and V_MRT='USER_DEF8'", dc.con);
                dt1 = new DataTable();
                da.Fill(dt1);
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    USER8 = dt1.Rows[i][0].ToString() + " :";
                }

                //get special field name
                da = new SqlDataAdapter("select V_USER FROM USER_COLUMN_NAMES WHERE V_ENABLED='TRUE'and V_MRT='USER_DEF9'", dc.con);
                dt1 = new DataTable();
                da.Fill(dt1);
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    USER9 = dt1.Rows[i][0].ToString() + " :";
                }

                //get special field name
                da = new SqlDataAdapter("select V_USER FROM USER_COLUMN_NAMES WHERE V_ENABLED='TRUE'and V_MRT='USER_DEF10'", dc.con);
                dt1 = new DataTable();
                da.Fill(dt1);
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    USER10 = dt1.Rows[i][0].ToString() + " :";
                }

                //get mo details
                SqlDataAdapter sda = new SqlDataAdapter("select V_MO_LINE,MO.V_COLOR_ID,V_ARTICLE_ID,V_SIZE_ID,V_USER_DEF1,V_USER_DEF2,V_USER_DEF3,V_USER_DEF4, V_USER_DEF5,V_USER_DEF6,V_USER_DEF7,V_USER_DEF8,V_USER_DEF9,V_USER_DEF10,I_ORDER_QTY,V_COLOR_DESC,V_COLOR_DESC from  MO_DETAILS MO,LAST_SELECT_MO LT,COLOR_DB CB where MO.V_MO_NO=LT.V_MO_NO AND MO.V_COLOR_ID=CB.V_COLOR_ID", dc.con);
                dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    String moline = dt.Rows[i][0].ToString();
                    String color = dt.Rows[i][1].ToString();
                    String article = dt.Rows[i][2].ToString();
                    String size = dt.Rows[i][3].ToString();
                    String user1 = dt.Rows[i][4].ToString();
                    String user2 = dt.Rows[i][5].ToString();
                    String user3 = dt.Rows[i][6].ToString();
                    String user4 = dt.Rows[i][7].ToString();
                    String user5 = dt.Rows[i][8].ToString();
                    String user6 = dt.Rows[i][9].ToString();
                    String user7 = dt.Rows[i][10].ToString();
                    String user8 = dt.Rows[i][11].ToString();
                    String user9 = dt.Rows[i][12].ToString();
                    String user10 = dt.Rows[i][13].ToString();
                    String qty = dt.Rows[i][14].ToString();
                    String colorDESC = dt.Rows[i][15].ToString();

                    //get desc for master
                    String article_desc = "";
                    SqlCommand cmd1 = new SqlCommand("select V_ARTICLE_DESC from ARTICLE_DB where V_ARTICLE_ID='" + article + "'", dc.con);
                    SqlDataReader sdr = cmd1.ExecuteReader();
                    if (sdr.Read())
                    {
                        article_desc = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get desc for master
                    cmd1 = new SqlCommand("select V_SIZE_DESC from SIZE_DB where V_SIZE_ID='" + size + "'", dc.con);
                    sdr = cmd1.ExecuteReader();
                    if (sdr.Read())
                    {
                        size = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get desc for master
                    cmd1 = new SqlCommand("select V_DESC from USER_DEF1_DB where V_USER_ID='" + user1 + "'", dc.con);
                    sdr = cmd1.ExecuteReader();
                    if (sdr.Read())
                    {
                        user1 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get desc for master
                    cmd1 = new SqlCommand("select V_DESC from USER_DEF2_DB where V_USER_ID='" + user2 + "'", dc.con);
                    sdr = cmd1.ExecuteReader();
                    if (sdr.Read())
                    {
                        user2 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get desc for master
                    cmd1 = new SqlCommand("select V_DESC from USER_DEF3_DB where V_USER_ID='" + user3 + "'", dc.con);
                    sdr = cmd1.ExecuteReader();
                    if (sdr.Read())
                    {
                        user3 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get desc for master
                    cmd1 = new SqlCommand("select V_DESC from USER_DEF4_DB where V_USER_ID='" + user4 + "'", dc.con);
                    sdr = cmd1.ExecuteReader();
                    if (sdr.Read())
                    {
                        user4 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get desc for master
                    cmd1 = new SqlCommand("select V_DESC from USER_DEF5_DB where V_USER_ID='" + user5 + "'", dc.con);
                    sdr = cmd1.ExecuteReader();
                    if (sdr.Read())
                    {
                        user5 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get desc for master
                    cmd1 = new SqlCommand("select V_DESC from USER_DEF6_DB where V_USER_ID='" + user6 + "'", dc.con);
                    sdr = cmd1.ExecuteReader();
                    if (sdr.Read())
                    {
                        user6 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get desc for master
                    cmd1 = new SqlCommand("select V_DESC from USER_DEF7_DB where V_USER_ID='" + user7 + "'", dc.con);
                    sdr = cmd1.ExecuteReader();
                    if (sdr.Read())
                    {
                        user7 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get desc for master
                    cmd1 = new SqlCommand("select V_DESC from USER_DEF8_DB where V_USER_ID='" + user8 + "'", dc.con);
                    sdr = cmd1.ExecuteReader();
                    if (sdr.Read())
                    {
                        user8 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get desc for master
                    cmd1 = new SqlCommand("select V_DESC from USER_DEF9_DB where V_USER_ID='" + user9 + "'", dc.con);
                    sdr = cmd1.ExecuteReader();
                    if (sdr.Read())
                    {
                        user9 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get desc for master
                    cmd1 = new SqlCommand("select V_DESC from USER_DEF10_DB where V_USER_ID='" + user10 + "'", dc.con);
                    sdr = cmd1.ExecuteReader();
                    if (sdr.Read())
                    {
                        user10 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    String opcode = "";
                    String opdesc = "";
                    String piecerate = "";
                    String sam = "";
                    String seqno = "";
                    String prevseqno = "0";
                    String curseqno = "0";

                    //get all the operations for the article
                    sda = new SqlDataAdapter("select DS.V_OPERATION_CODE,OP.V_OPERATION_DESC,OP.D_PIECERATE,OP.D_SAM,ds.I_SEQUENCE_NO from OPERATION_DB OP,DESIGN_SEQUENCE DS where DS.V_ARTICLE_ID='" + article + "' and DS.V_OPERATION_CODE=OP.V_OPERATION_CODE order by DS.I_SEQUENCE_NO", dc.con);
                    dt1 = new DataTable();
                    sda.Fill(dt1);
                    sda.Dispose();
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        prevseqno = curseqno;
                        opcode = dt1.Rows[j][0].ToString();
                        opdesc = dt1.Rows[j][1].ToString();
                        piecerate = dt1.Rows[j][2].ToString();
                        sam = dt1.Rows[j][3].ToString();
                        seqno = dt1.Rows[j][4].ToString();
                        curseqno = seqno;

                        //get all station version
                        sda = new SqlDataAdapter("select distinct V_ASSIGN_TYPE from STATION_ASSIGN where V_MO_NO='" + mono + "' and V_MO_LINE='" + moline + "'", dc.con);
                        DataTable dt3 = new DataTable();
                        sda.Fill(dt3);
                        sda.Dispose();
                        for (int p = 0; p < dt3.Rows.Count; p++)
                        {
                            //get the station number
                            String version = dt3.Rows[p][0].ToString();
                            sda = new SqlDataAdapter("select I_LINE_NO,D_STATION_NO,I_ROW_NO from STATION_ASSIGN where V_ARTICLE_ID='" + article + "' and V_MO_NO='" + mono + "' and V_MO_LINE='" + moline + "' and I_STATION_ID!=0 and I_SEQUENCE_NO='" + seqno + "' and V_ASSIGN_TYPE='" + version + "' order by I_ROW_NO", dc.con);
                            DataTable dt2 = new DataTable();
                            sda.Fill(dt2);
                            sda.Dispose();

                            //check if datatabl has srows
                            int max = dt2.Rows.Count;
                            if (dt2.Rows.Count > 0)
                            {
                                String r1 = "";
                                String r2 = "";
                                String r3 = "";
                                String r4 = "";
                                String r5 = "";
                                String r6 = "";
                                String r7 = "";
                                String r8 = "";
                                String r9 = "";
                                String r10 = "";
                                String r11 = "";
                                String r12 = "";
                                String r13 = "";
                                String r14 = "";

                                //add the station assign to each column of datatable
                                int k = 0;
                                r1 = dt2.Rows[k][0].ToString() + "." + dt2.Rows[k][1].ToString();
                                k = k + 1;
                                if (k < max)
                                {
                                    r2 = dt2.Rows[k][0].ToString() + "." + dt2.Rows[k][1].ToString();
                                }

                                k = k + 1;
                                if (k < max)
                                {
                                    r3 = dt2.Rows[k][0].ToString() + "." + dt2.Rows[k][1].ToString();
                                }

                                k = k + 1;
                                if (k < max)
                                {
                                    r4 = dt2.Rows[k][0].ToString() + "." + dt2.Rows[k][1].ToString();
                                }

                                k = k + 1;
                                if (k < max)
                                {
                                    r5 = dt2.Rows[k][0].ToString() + "." + dt2.Rows[k][1].ToString();
                                }

                                k = k + 1;
                                if (k < max)
                                {
                                    r6 = dt2.Rows[k][0].ToString() + "." + dt2.Rows[k][1].ToString();
                                }

                                k = k + 1;
                                if (k < max)
                                {
                                    r7 = dt2.Rows[k][0].ToString() + "." + dt2.Rows[k][1].ToString();
                                }

                                k = k + 1;
                                if (k < max)
                                {
                                    r8 = dt2.Rows[k][0].ToString() + "." + dt2.Rows[k][1].ToString();
                                }

                                k = k + 1;
                                if (k < max)
                                {
                                    r9 = dt2.Rows[k][0].ToString() + "." + dt2.Rows[k][1].ToString();
                                }

                                k = k + 1;
                                if (k < max)
                                {
                                    r10 = dt2.Rows[k][0].ToString() + "." + dt2.Rows[k][1].ToString();
                                }

                                k = k + 1;
                                if (k < max)
                                {
                                    r11 = dt2.Rows[k][0].ToString() + "." + dt2.Rows[k][1].ToString();
                                }

                                k = k + 1;
                                if (k < max)
                                {
                                    r12 = dt2.Rows[k][0].ToString() + "." + dt2.Rows[k][1].ToString();
                                }

                                k = k + 1;
                                if (k < max)
                                {
                                    r13 = dt2.Rows[k][0].ToString() + "." + dt2.Rows[k][1].ToString();
                                }

                                k = k + 1;
                                if (k < max)
                                {
                                    r14 = dt2.Rows[k][0].ToString() + "." + dt2.Rows[k][1].ToString();
                                }

                                if (prevseqno == curseqno)
                                {
                                    r1 = "";
                                    r2 = "";
                                    r3 = "";
                                    r4 = "";
                                    r5 = "";
                                    r6 = "";
                                    r7 = "";
                                    r8 = "";
                                    r9 = "";
                                    r10 = "";
                                    r11 = "";
                                    r12 = "";
                                    r13 = "";
                                    r14 = "";
                                } 
                                
                                //add to datatable
                                data.Rows.Add(mono, color, colorDESC, size, article_desc, moline, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, qty, opcode, opdesc, piecerate, sam, seqno, dt2.Rows[0][0].ToString(), dt2.Rows[0][0].ToString(), r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12, r13, r14, USER1, USER2, USER3, USER4, USER5, USER6, USER7, USER8, USER9, USER10, version);
                            }
                        }
                    }
                }

                view = new DataView(data);
                view1 = new DataView(data1);

                //get logo
                DataTable dt_image = new DataTable();
                dt_image.Columns.Add("image", typeof(byte[]));
                dt_image.Rows.Add(dc.GetImage());
                DataView dv_image = new DataView(dt_image);

                reportViewer2.LocalReport.ReportEmbeddedResource = "SMARTMRT.Sequence_Report.rdlc";
                reportViewer2.LocalReport.DataSources.Clear();

                //add views to dataset
                reportViewer2.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
                reportViewer2.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", view1));
                reportViewer2.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", dv_image));
                reportViewer2.RefreshReport();
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex.ToString(), "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }
    }
}
