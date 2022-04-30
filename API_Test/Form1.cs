using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Data.SqlClient;
namespace API_Test
{
    public partial class Form1 : Form
    {
        private bool dateChanged1 = false;
        private bool dateChanged2 = false;
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            JsonSerializerSettings jss = new JsonSerializerSettings();
            jss.Formatting = Formatting.None;
            jss.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;

            //get From , TO Date
            DateTime TransToDate = DateTime.Now;
            DateTime TransAfterDate = DateTime.Now.Date.AddDays(-6);
            if (dateChanged1 && dateChanged2)
            {
                TransAfterDate = dateTimePicker1.Value.Date;
                TransToDate = dateTimePicker2.Value.Date;
            }
            else if (dateChanged1)
            {
                TransAfterDate = dateTimePicker1.Value.Date;
            }
            else if (dateChanged2)
            {
                TransToDate = dateTimePicker2.Value.Date;
            }

            //get filter type
            int filter;
            if (cmbtype.Text == "Invoice")
            {
                filter = 1;
            }
            else
            {
                filter = 4;
            }

            //get sales filter api
            var client = new RestClient("https://" + Login.DomainLoged + "/modules/api3/sales/filter");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("X-COMPANY", "" + Login.companyNumber + "");
            request.AddHeader("X-USER", "" + Login.UserName + "");
            request.AddHeader("X-PASSWORD", "" + Login.Password + "");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "FAb3708d6ca6c9f028f053d60aefbdffaf=j8vvg6otv23qhtuedmnhii5880");
            var body = @"{" + "\n" + @"    ""TransAfterDate"": " + @"""" + TransAfterDate.ToString("MM/dd/yyyy") + @"""," + "\n" + @"    ""TransToDate"": " + @"""" + TransToDate.ToString("MM/dd/yyyy") + @"""," + "\n" + @"    ""filterType"":" + filter + "\n" + @"}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            //profil
            var client2 = new RestClient("https://" + Login.DomainLoged + "/modules/api3/profile");
            client2.Timeout = -1;
            var request2 = new RestRequest(Method.GET);
            request2.AddHeader("X-COMPANY", "" + Login.companyNumber + "");
            request2.AddHeader("X-USER", "" + Login.UserName + "");
            request2.AddHeader("X-PASSWORD", "" + Login.Password + "");
            request2.AddHeader("Content-Type", "application/json");
            IRestResponse response2 = client2.Execute(request2);
            Profil profil = JsonConvert.DeserializeObject<Profil>(response2.Content);

            if (filter == 1)
            {
                List<SalesFilterInvoice> salesFilterInvoice = JsonConvert.DeserializeObject<List<SalesFilterInvoice>>(response.Content);

                decimal discAmount = 0, salesTot = 0, Net = 0, Total = 0;

                using (SqlConnection connection = new SqlConnection(ConnectionString.Connection()))
                using (SqlCommand command = new SqlCommand("InsertInvoiceHeader", connection))
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@HeaderID", SqlDbType.VarChar);
                    command.Parameters.Add("@UssuerID", SqlDbType.VarChar);
                    command.Parameters.Add("@Type", SqlDbType.NVarChar);
                    command.Parameters.Add("@ID", SqlDbType.NVarChar);
                    command.Parameters.Add("@Name", SqlDbType.NVarChar);
                    command.Parameters.Add("@Country", SqlDbType.NVarChar);
                    command.Parameters.Add("@Governate", SqlDbType.NVarChar);
                    command.Parameters.Add("@Region", SqlDbType.NVarChar);
                    command.Parameters.Add("@Street", SqlDbType.NVarChar);
                    command.Parameters.Add("@BuildinNO", SqlDbType.NVarChar);
                    command.Parameters.Add("@Postal", SqlDbType.NVarChar);
                    command.Parameters.Add("@floor", SqlDbType.NVarChar);
                    command.Parameters.Add("@Room", SqlDbType.NVarChar);
                    command.Parameters.Add("@LandMark", SqlDbType.NVarChar);
                    command.Parameters.Add("@AdditionalInfo", SqlDbType.NVarChar);
                    command.Parameters.Add("@DocumentTyp", SqlDbType.NVarChar);
                    command.Parameters.Add("@DocumentTypVer", SqlDbType.NVarChar);
                    command.Parameters.Add("@DateTime", SqlDbType.VarChar);
                    command.Parameters.Add("@Taxable", SqlDbType.NVarChar);
                    command.Parameters.Add("@InternalID", SqlDbType.NVarChar);
                    command.Parameters.Add("@Reverence", SqlDbType.NVarChar);
                    command.Parameters.Add("@Descrip", SqlDbType.NVarChar);
                    command.Parameters.Add("@SalesRefer", SqlDbType.NVarChar);
                    command.Parameters.Add("@SalesDesc", SqlDbType.NVarChar);
                    command.Parameters.Add("@ProInvoice", SqlDbType.NVarChar);
                    command.Parameters.Add("@BankNa", SqlDbType.NVarChar);
                    command.Parameters.Add("@BankADD", SqlDbType.NVarChar);
                    command.Parameters.Add("@BankACC", SqlDbType.NVarChar);
                    command.Parameters.Add("@BankIBAN", SqlDbType.NVarChar);
                    command.Parameters.Add("@SWIFTCODE", SqlDbType.NVarChar);
                    command.Parameters.Add("@TERMS", SqlDbType.NVarChar);
                    command.Parameters.Add("@APPROACH", SqlDbType.NVarChar);
                    command.Parameters.Add("@PACKAGING", SqlDbType.NVarChar);
                    command.Parameters.Add("@DATEVALIDITY", SqlDbType.NVarChar);
                    command.Parameters.Add("@EXPORTPORT", SqlDbType.NVarChar);
                    command.Parameters.Add("@COUNTRYOFORIGIN", SqlDbType.NVarChar);
                    command.Parameters.Add("@GROSSWEIGHT", SqlDbType.NVarChar);
                    command.Parameters.Add("@NETWEIGHT", SqlDbType.NVarChar);
                    command.Parameters.Add("@DELIVERY_TERMS", SqlDbType.NVarChar);
                    command.Parameters.Add("@TOTALDISCOUNTAMOUNT", SqlDbType.Decimal);
                    command.Parameters.Add("@TOTALSALESAMOUNT", SqlDbType.Decimal);
                    command.Parameters.Add("@NETAMOUNT", SqlDbType.Decimal);
                    command.Parameters.Add("@TOTALAMOUNT", SqlDbType.Decimal);
                    command.Parameters.Add("@EXTRADISCOUNTAMOUNT", SqlDbType.Decimal);
                    command.Parameters.Add("@TOTALITEMSDISCOUNTAMOUNT", SqlDbType.Decimal);
                    command.Parameters.Add("@REFERENCE_UUID", SqlDbType.NVarChar);
                    command.Parameters.Add("@REFERENCE_INVOICE_NUM", SqlDbType.NVarChar);
                    command.Parameters.Add("@ACTIONTYPE", SqlDbType.VarChar);
                    command.Parameters.Add("@MAINSTATUS", SqlDbType.VarChar);
                    command.Parameters.Add("@ReadyToMain", SqlDbType.Bit);
                    command.Parameters.Add("@DateTimeIssuedTemp", SqlDbType.DateTime);

                    using (SqlCommand command3 = new SqlCommand("InsertInvoiceLines", connection))
                    {
                        command3.CommandType = CommandType.StoredProcedure;

                        command3.Parameters.Add("@headerid", SqlDbType.VarChar);
                        command3.Parameters.Add("@lineid", SqlDbType.Int);
                        command3.Parameters.Add("@descr", SqlDbType.NVarChar);
                        command3.Parameters.Add("@itemType", SqlDbType.NVarChar);
                        command3.Parameters.Add("@itemCode", SqlDbType.NVarChar);
                        command3.Parameters.Add("@unitType", SqlDbType.NVarChar);
                        command3.Parameters.Add("@qantity", SqlDbType.Decimal);
                        command3.Parameters.Add("@internalCode", SqlDbType.NVarChar);
                        command3.Parameters.Add("@salesTotal", SqlDbType.Decimal);
                        command3.Parameters.Add("@total", SqlDbType.Decimal);
                        command3.Parameters.Add("@valueDifer", SqlDbType.Decimal);
                        command3.Parameters.Add("@totalTax", SqlDbType.Decimal);
                        command3.Parameters.Add("@netTotal", SqlDbType.Decimal);
                        command3.Parameters.Add("@itemDis", SqlDbType.Decimal);
                        command3.Parameters.Add("@currSold", SqlDbType.NVarChar);
                        command3.Parameters.Add("@amountEGP", SqlDbType.Decimal);
                        command3.Parameters.Add("@amountSold", SqlDbType.Decimal);
                        command3.Parameters.Add("@currExchange", SqlDbType.Decimal);
                        command3.Parameters.Add("@disRate", SqlDbType.Decimal);
                        command3.Parameters.Add("@disAmount", SqlDbType.Decimal);
                        command3.Parameters.Add("@Ready", SqlDbType.Bit);

                        for (int i = 0; i < salesFilterInvoice.Count; i++)
                        {
                            DataSet Rs1 = new DataSet();
                            DataTable TableList = new DataTable();

                            using (SqlConnection connection2 = new SqlConnection(ConnectionString.Connection()))
                            using (SqlCommand command2 = new SqlCommand("SELECT [HeaderId],[TRX_HEADER_ID]  FROM [EINVOICES_HEADERS_TEMP] where [TRX_HEADER_ID]=@trxid", connection2))
                            {
                                command2.Parameters.Add("@trxid", SqlDbType.VarChar);
                                command2.Parameters[0].Value = salesFilterInvoice[i].trans_no;
                                connection2.Open();
                                SqlDataAdapter sqlDA = new SqlDataAdapter();
                                sqlDA.TableMappings.Add("Table", "EINVOICES_HEADERS_TEMP");
                                sqlDA.SelectCommand = command2;
                                sqlDA.Fill(Rs1);
                                connection2.Close();
                            }
                            if (Rs1.Tables.Count > 0)
                            {
                                if (Rs1.Tables[0].Rows.Count != 0) //exist
                                {
                                    var query = "DELETE FROM EINVOICES_HEADERS_TEMP WHERE TRX_HEADER_ID = '" + salesFilterInvoice[i].trans_no + "'; " + "DELETE FROM EINVOICES_LINES_TEMP WHERE TRX_HEADER_ID = '" + salesFilterInvoice[i].trans_no + "' " + " DELETE FROM TAX_HEADERS_TEMP WHERE TRX_HEADER_ID = '" + salesFilterInvoice[i].trans_no + "' " + "DELETE FROM TAX_LINES_TEMP WHERE TRX_HEADER_ID = '" + salesFilterInvoice[i].trans_no + "' ";
                                    SqlCommand cmd = new SqlCommand(query, connection);
                                    if (connection.State == ConnectionState.Closed)
                                    {
                                        connection.Open();
                                    }
                                    cmd.ExecuteNonQuery();
                                }

                                //add invoice lines
                                for (int x = 0; x < salesFilterInvoice[i].line_items.Count(); x++)
                                {
                                    command3.Parameters["@headerid"].Value = salesFilterInvoice[i].line_items[x].debtor_trans_no;
                                    command3.Parameters["@lineid"].Value = x + 1;
                                    command3.Parameters["@descr"].Value = salesFilterInvoice[i].line_items[x].description;

                                    if (salesFilterInvoice[i].line_items[x].item_codes.FirstOrDefault().category_name == "Tax")
                                    {
                                        string codes = salesFilterInvoice[i].line_items[x].item_codes.FirstOrDefault().item_code;
                                        string[] itemCode = codes.Split('-');
                                        command3.Parameters["@itemType"].Value = salesFilterInvoice[i].line_items[x].item_codes.FirstOrDefault().description;
                                        command3.Parameters["@itemCode"].Value = itemCode[0];
                                    }
                                    else
                                    {
                                        command3.Parameters["@itemType"].Value = ""; command3.Parameters["@itemCode"].Value = "";
                                    }
                                    command3.Parameters["@unitType"].Value = salesFilterInvoice[i].line_items[x].units;
                                    command3.Parameters["@qantity"].Value = salesFilterInvoice[i].line_items[x].quantity;
                                    command3.Parameters["@internalCode"].Value = salesFilterInvoice[i].line_items[x].stock_id;

                                    //taxline
                                    decimal rate = 0, taxAmount = 0;
                                    using (SqlCommand cmd2 = new SqlCommand("InsertTaxLines", connection))
                                    {
                                        cmd2.CommandType = CommandType.StoredProcedure;
                                        cmd2.Parameters.Add("@headerID", SqlDbType.VarChar);
                                        cmd2.Parameters.Add("@lineID", SqlDbType.VarChar);
                                        cmd2.Parameters.Add("@TaxType", SqlDbType.VarChar);
                                        cmd2.Parameters.Add("@SubType", SqlDbType.VarChar);
                                        cmd2.Parameters.Add("@Amount", SqlDbType.Decimal);
                                        cmd2.Parameters.Add("@rate", SqlDbType.Decimal);
                                        cmd2.Parameters.Add("@ready", SqlDbType.Bit);

                                        cmd2.Parameters["@headerID"].Value = salesFilterInvoice[i].line_items[x].debtor_trans_no;
                                        cmd2.Parameters["@lineID"].Value = x + 1;

                                        string tax = salesFilterInvoice[i].line_items[x].item_tax_type.tax_type.FirstOrDefault().name;
                                        string[] taxtype = tax.Split(':');

                                        cmd2.Parameters["@TaxType"].Value = taxtype[0];

                                        if (salesFilterInvoice[i].tax_group.taxType.FirstOrDefault().rate == 0)
                                        {
                                            cmd2.Parameters["@rate"].Value = 0;
                                            rate = 0;
                                        }
                                        else
                                        {
                                            cmd2.Parameters["@rate"].Value = salesFilterInvoice[i].line_items[x].item_tax_type.tax_type.FirstOrDefault().rate;
                                            rate = salesFilterInvoice[i].line_items[x].item_tax_type.tax_type.FirstOrDefault().rate;
                                        }

                                        cmd2.Parameters["@Amount"].Value = ((rate / 100) * (salesFilterInvoice[i].line_items[x].quantity * salesFilterInvoice[i].line_items[x].unit_price * 
                                            salesFilterInvoice[i].rate)) - (salesFilterInvoice[i].line_items[x].discount_percent * salesFilterInvoice[i].line_items[x].quantity * salesFilterInvoice[i].line_items[x].unit_tax * salesFilterInvoice[i].rate);

                                        taxAmount = ((rate / 100) * (salesFilterInvoice[i].line_items[x].quantity * salesFilterInvoice[i].line_items[x].unit_price * salesFilterInvoice[i].rate)) - 
                                            (salesFilterInvoice[i].line_items[x].discount_percent * salesFilterInvoice[i].line_items[x].quantity * salesFilterInvoice[i].line_items[x].unit_tax * salesFilterInvoice[i].rate);

                                        string sub = salesFilterInvoice[i].line_items[x].item_tax_type.item_tax_type_name;
                                        string[] subtype = sub.Split(':');
                                        cmd2.Parameters["@SubType"].Value = subtype[0];
                                        cmd2.Parameters["@ready"].Value = false;

                                        if (connection.State == ConnectionState.Closed)
                                        {
                                            connection.Open();
                                        }
                                        cmd2.ExecuteNonQuery();
                                    }

                                    decimal salestotal = (salesFilterInvoice[i].line_items[x].quantity * salesFilterInvoice[i].line_items[x].unit_price * salesFilterInvoice[i].rate);
                                    command3.Parameters["@salesTotal"].Value = salestotal;

                                    decimal total = taxAmount + (salesFilterInvoice[i].line_items[x].quantity * salesFilterInvoice[i].line_items[x].unit_price * salesFilterInvoice[i].rate) - (salesFilterInvoice[i].line_items[x].discount_percent * salesFilterInvoice[i].line_items[x].quantity * salesFilterInvoice[i].line_items[x].unit_tax * salesFilterInvoice[i].rate);

                                    command3.Parameters["@total"].Value = total;
                                    command3.Parameters["@valueDifer"].Value = 0; command3.Parameters["@totalTax"].Value = 0;

                                    decimal nettotal = (salesFilterInvoice[i].line_items[x].quantity * salesFilterInvoice[i].line_items[x].unit_price * salesFilterInvoice[i].rate) - (salesFilterInvoice[i].line_items[x].discount_percent * salesFilterInvoice[i].line_items[x].quantity * salesFilterInvoice[i].line_items[x].unit_tax * salesFilterInvoice[i].rate);

                                    command3.Parameters["@netTotal"].Value = nettotal;
                                    command3.Parameters["@itemDis"].Value = 0;
                                    command3.Parameters["@currSold"].Value = salesFilterInvoice[i].curr_code;

                                    if (command3.Parameters["@currSold"].Value.ToString() == "EGP")
                                    {
                                        command3.Parameters["@amountEGP"].Value = salesFilterInvoice[i].line_items[x].unit_price;
                                        command3.Parameters["@amountSold"].Value = 0;
                                        command3.Parameters["@currExchange"].Value = 0;
                                    }
                                    else
                                    {
                                        command3.Parameters["@amountEGP"].Value = salesFilterInvoice[i].line_items[x].unit_price * salesFilterInvoice[i].rate;
                                        command3.Parameters["@amountSold"].Value = salesFilterInvoice[i].line_items[x].unit_price;
                                        command3.Parameters["@currExchange"].Value = salesFilterInvoice[i].rate;
                                    }
                                    command3.Parameters["@disRate"].Value = salesFilterInvoice[i].line_items[x].discount_percent;

                                    decimal disam = salesFilterInvoice[i].line_items[x].discount_percent * salesFilterInvoice[i].line_items[x].quantity * salesFilterInvoice[i].line_items[x].unit_price * salesFilterInvoice[i].rate;
                                    command3.Parameters["@disAmount"].Value = disam;
                                    command3.Parameters["@Ready"].Value = false;

                                    discAmount += disam;
                                    salesTot += salestotal;
                                    Net += nettotal;
                                    Total += total;

                                    if (connection.State == ConnectionState.Closed)
                                    {
                                        connection.Open();
                                    }
                                    command3.ExecuteNonQuery();
                                }

                                command.Parameters["@HeaderID"].Value = salesFilterInvoice[i].trans_no;
                                command.Parameters["@UssuerID"].Value = profil.GSTNo;
                                command.Parameters["@Type"].Value = salesFilterInvoice[i].branch_data.group_no.description;
                                command.Parameters["@ID"].Value = salesFilterInvoice[i].cutomer_details.tax_id;
                                command.Parameters["@Name"].Value = salesFilterInvoice[i].name;

                                string FullAddress = salesFilterInvoice[i].cutomer_details.address;
                                string[] address = FullAddress.Split('|');

                                command.Parameters["@Country"].Value = salesFilterInvoice[i].branch_data.area.description;
                                command.Parameters["@Governate"].Value = address[3];
                                command.Parameters["@Region"].Value = address[2];
                                command.Parameters["@Street"].Value = address[1];
                                command.Parameters["@BuildinNO"].Value = address[0];
                                command.Parameters["@Postal"].Value = ""; command.Parameters["@floor"].Value = ""; command.Parameters["@Room"].Value = "";
                                command.Parameters["@LandMark"].Value = ""; command.Parameters["@AdditionalInfo"].Value = "";
                                command.Parameters["@DocumentTyp"].Value = 'I';
                                command.Parameters["@DocumentTypVer"].Value = "1.0";
                                command.Parameters["@DateTime"].Value = salesFilterInvoice[i].tran_date + "T08:11:12Z";
                                command.Parameters["@Taxable"].Value = "";
                                command.Parameters["@InternalID"].Value = salesFilterInvoice[i].reference;
                                command.Parameters["@Reverence"].Value = ""; command.Parameters["@Descrip"].Value = ""; command.Parameters["@SalesRefer"].Value = "";
                                command.Parameters["@SalesDesc"].Value = ""; command.Parameters["@ProInvoice"].Value = ""; command.Parameters["@BankNa"].Value = "";
                                command.Parameters["@BankADD"].Value = ""; command.Parameters["@BankACC"].Value = ""; command.Parameters["@BankIBAN"].Value = "";
                                command.Parameters["@SWIFTCODE"].Value = ""; command.Parameters["@TERMS"].Value = ""; command.Parameters["@APPROACH"].Value = "";
                                command.Parameters["@PACKAGING"].Value = ""; command.Parameters["@DATEVALIDITY"].Value = ""; command.Parameters["@EXPORTPORT"].Value = "";
                                command.Parameters["@COUNTRYOFORIGIN"].Value = ""; command.Parameters["@GROSSWEIGHT"].Value = ""; command.Parameters["@NETWEIGHT"].Value = "";
                                command.Parameters["@DELIVERY_TERMS"].Value = "";
                                command.Parameters["@TOTALDISCOUNTAMOUNT"].Value = discAmount;
                                command.Parameters["@TOTALSALESAMOUNT"].Value = salesTot;
                                command.Parameters["@NETAMOUNT"].Value = Net;
                                command.Parameters["@TOTALAMOUNT"].Value = Total;
                                command.Parameters["@EXTRADISCOUNTAMOUNT"].Value = 0.0; command.Parameters["@TOTALITEMSDISCOUNTAMOUNT"].Value = 0.0;
                                command.Parameters["@REFERENCE_UUID"].Value = 0;
                                if (salesFilterInvoice[i].related_invoice == null)
                                {
                                    command.Parameters["@REFERENCE_INVOICE_NUM"].Value = 0;
                                }

                                command.Parameters["@ACTIONTYPE"].Value = null;
                                command.Parameters["@MAINSTATUS"].Value = ""; command.Parameters["@ReadyToMain"].Value = false; command.Parameters["@DateTimeIssuedTemp"].Value = DateTime.UtcNow;

                                command.ExecuteNonQuery();

                                connection.Close();
                            }

                            using (SqlConnection source = new SqlConnection(ConnectionString.Connection()))
                            using (SqlConnection destination = new SqlConnection(ConnectionString.ConnectionEgy()))
                            {

                                source.Open();
                                destination.Open();

                                SqlCommand cmd1 = new SqlCommand("SELECT * FROM EINVOICES_HEADERS_TEMP where TRX_HEADER_ID = '" + salesFilterInvoice[i].trans_no + "' ", source);
                                cmd1.CommandType = CommandType.Text;
                                cmd1.ExecuteNonQuery();

                                DataTable dt = new DataTable();
                                SqlDataAdapter adapter = new SqlDataAdapter(cmd1);
                                adapter.Fill(dt);

                                SqlBulkCopy BDInvoiceHeader = new SqlBulkCopy(destination);
                                BDInvoiceHeader.DestinationTableName = "EINVOICES_HEADERS_TEMP";
                                BDInvoiceHeader.WriteToServer(dt);
                                BDInvoiceHeader.Close();

                                for (int m = 0; m <= dt.Rows.Count - 1; m++)
                                {
                                    //invoice line
                                    SqlCommand cmd2 = new SqlCommand("SELECT * FROM EINVOICES_LINES_TEMP where TRX_HEADER_ID = '" + salesFilterInvoice[i].trans_no + "' ", source);
                                    cmd2.CommandType = CommandType.Text;
                                    cmd2.ExecuteNonQuery();

                                    DataTable dtInvoiceLine = new DataTable();
                                    SqlDataAdapter daInvoiceLine = new SqlDataAdapter(cmd2);
                                    daInvoiceLine.Fill(dtInvoiceLine);

                                    SqlBulkCopy BDInvoiceLines = new SqlBulkCopy(destination);
                                    BDInvoiceLines.DestinationTableName = "EINVOICES_LINES_TEMP";
                                    BDInvoiceLines.WriteToServer(dtInvoiceLine);
                                    BDInvoiceLines.Close();

                                    //taxheader
                                    SqlCommand cmd3 = new SqlCommand("SELECT * FROM TAX_HEADERS_TEMP where TRX_HEADER_ID = '" + salesFilterInvoice[i].trans_no + "' ", source);
                                    cmd3.CommandType = CommandType.Text;
                                    cmd3.ExecuteNonQuery();
                                    DataTable dtTaxHeader = new DataTable();
                                    SqlDataAdapter daTaxHeader = new SqlDataAdapter(cmd3);
                                    daTaxHeader.Fill(dtTaxHeader);

                                    SqlBulkCopy BDtaxHeader = new SqlBulkCopy(destination);
                                    BDtaxHeader.DestinationTableName = "TAX_HEADERS_TEMP";
                                    BDtaxHeader.WriteToServer(dtTaxHeader);
                                    BDtaxHeader.Close();


                                    //tax line
                                    SqlCommand cmd4 = new SqlCommand("SELECT * FROM TAX_LINES_TEMP where TRX_HEADER_ID = '" + salesFilterInvoice[i].trans_no + "' ", source);
                                    cmd4.CommandType = CommandType.Text;
                                    cmd4.ExecuteNonQuery();
                                    DataTable dtTaxLine = new DataTable();
                                    SqlDataAdapter daTaxLine = new SqlDataAdapter(cmd4);
                                    daTaxLine.Fill(dtTaxLine);

                                    SqlBulkCopy BDTaxLines = new SqlBulkCopy(destination);
                                    BDTaxLines.DestinationTableName = "TAX_LINES_TEMP";
                                    BDTaxLines.WriteToServer(dtTaxLine);
                                    BDTaxLines.Close();

                                }

                            }
                            discAmount = 0; Total = 0; Net = 0; salesTot = 0;
                        }
                    }
                    MessageBox.Show("Done......" + "\n" + "Total : " + salesFilterInvoice.Count());
                }

            }

            else if (filter == 4)
            {

                List<SalesFilterCredit> salesFilterCredit = JsonConvert.DeserializeObject<List<SalesFilterCredit>>(response.Content);

                decimal discAmount = 0, salesTot = 0, Net = 0, Total = 0;

                using (SqlConnection connection = new SqlConnection(ConnectionString.Connection()))
                using (SqlCommand command = new SqlCommand("InsertInvoiceHeader", connection))
                {

                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@HeaderID", SqlDbType.VarChar);
                    command.Parameters.Add("@UssuerID", SqlDbType.VarChar);
                    command.Parameters.Add("@Type", SqlDbType.NVarChar);
                    command.Parameters.Add("@ID", SqlDbType.NVarChar);
                    command.Parameters.Add("@Name", SqlDbType.NVarChar);
                    command.Parameters.Add("@Country", SqlDbType.NVarChar);
                    command.Parameters.Add("@Governate", SqlDbType.NVarChar);
                    command.Parameters.Add("@Region", SqlDbType.NVarChar);
                    command.Parameters.Add("@Street", SqlDbType.NVarChar);
                    command.Parameters.Add("@BuildinNO", SqlDbType.NVarChar);
                    command.Parameters.Add("@Postal", SqlDbType.NVarChar);
                    command.Parameters.Add("@floor", SqlDbType.NVarChar);
                    command.Parameters.Add("@Room", SqlDbType.NVarChar);
                    command.Parameters.Add("@LandMark", SqlDbType.NVarChar);
                    command.Parameters.Add("@AdditionalInfo", SqlDbType.NVarChar);
                    command.Parameters.Add("@DocumentTyp", SqlDbType.NVarChar);
                    command.Parameters.Add("@DocumentTypVer", SqlDbType.NVarChar);
                    command.Parameters.Add("@DateTime", SqlDbType.VarChar);
                    command.Parameters.Add("@Taxable", SqlDbType.NVarChar);
                    command.Parameters.Add("@InternalID", SqlDbType.NVarChar);
                    command.Parameters.Add("@Reverence", SqlDbType.NVarChar);
                    command.Parameters.Add("@Descrip", SqlDbType.NVarChar);
                    command.Parameters.Add("@SalesRefer", SqlDbType.NVarChar);
                    command.Parameters.Add("@SalesDesc", SqlDbType.NVarChar);
                    command.Parameters.Add("@ProInvoice", SqlDbType.NVarChar);
                    command.Parameters.Add("@BankNa", SqlDbType.NVarChar);
                    command.Parameters.Add("@BankADD", SqlDbType.NVarChar);
                    command.Parameters.Add("@BankACC", SqlDbType.NVarChar);
                    command.Parameters.Add("@BankIBAN", SqlDbType.NVarChar);
                    command.Parameters.Add("@SWIFTCODE", SqlDbType.NVarChar);
                    command.Parameters.Add("@TERMS", SqlDbType.NVarChar);
                    command.Parameters.Add("@APPROACH", SqlDbType.NVarChar);
                    command.Parameters.Add("@PACKAGING", SqlDbType.NVarChar);
                    command.Parameters.Add("@DATEVALIDITY", SqlDbType.NVarChar);
                    command.Parameters.Add("@EXPORTPORT", SqlDbType.NVarChar);
                    command.Parameters.Add("@COUNTRYOFORIGIN", SqlDbType.NVarChar);
                    command.Parameters.Add("@GROSSWEIGHT", SqlDbType.NVarChar);
                    command.Parameters.Add("@NETWEIGHT", SqlDbType.NVarChar);
                    command.Parameters.Add("@DELIVERY_TERMS", SqlDbType.NVarChar);
                    command.Parameters.Add("@TOTALDISCOUNTAMOUNT", SqlDbType.Decimal);
                    command.Parameters.Add("@TOTALSALESAMOUNT", SqlDbType.Decimal);
                    command.Parameters.Add("@NETAMOUNT", SqlDbType.Decimal);
                    command.Parameters.Add("@TOTALAMOUNT", SqlDbType.Decimal);
                    command.Parameters.Add("@EXTRADISCOUNTAMOUNT", SqlDbType.Decimal);
                    command.Parameters.Add("@TOTALITEMSDISCOUNTAMOUNT", SqlDbType.Decimal);
                    command.Parameters.Add("@REFERENCE_UUID", SqlDbType.NVarChar);
                    command.Parameters.Add("@REFERENCE_INVOICE_NUM", SqlDbType.NVarChar);
                    command.Parameters.Add("@ACTIONTYPE", SqlDbType.VarChar);
                    command.Parameters.Add("@MAINSTATUS", SqlDbType.VarChar);
                    command.Parameters.Add("@ReadyToMain", SqlDbType.Bit);
                    command.Parameters.Add("@DateTimeIssuedTemp", SqlDbType.DateTime);


                    using (SqlCommand command3 = new SqlCommand("InsertInvoiceLines", connection))
                    {
                        command3.CommandType = CommandType.StoredProcedure;

                        command3.Parameters.Add("@headerid", SqlDbType.VarChar);
                        command3.Parameters.Add("@lineid", SqlDbType.Int);
                        command3.Parameters.Add("@descr", SqlDbType.NVarChar);
                        command3.Parameters.Add("@itemType", SqlDbType.NVarChar);
                        command3.Parameters.Add("@itemCode", SqlDbType.NVarChar);
                        command3.Parameters.Add("@unitType", SqlDbType.NVarChar);
                        command3.Parameters.Add("@qantity", SqlDbType.Decimal);
                        command3.Parameters.Add("@internalCode", SqlDbType.NVarChar);
                        command3.Parameters.Add("@salesTotal", SqlDbType.Decimal);
                        command3.Parameters.Add("@total", SqlDbType.Decimal);
                        command3.Parameters.Add("@valueDifer", SqlDbType.Decimal);
                        command3.Parameters.Add("@totalTax", SqlDbType.Decimal);
                        command3.Parameters.Add("@netTotal", SqlDbType.Decimal);
                        command3.Parameters.Add("@itemDis", SqlDbType.Decimal);
                        command3.Parameters.Add("@currSold", SqlDbType.NVarChar);
                        command3.Parameters.Add("@amountEGP", SqlDbType.Decimal);
                        command3.Parameters.Add("@amountSold", SqlDbType.Decimal);
                        command3.Parameters.Add("@currExchange", SqlDbType.Decimal);
                        command3.Parameters.Add("@disRate", SqlDbType.Decimal);
                        command3.Parameters.Add("@disAmount", SqlDbType.Decimal);
                        command3.Parameters.Add("@Ready", SqlDbType.Bit);

                        for (int i = 0; i < salesFilterCredit.Count; i++)
                        {
                            DataSet Rs1 = new DataSet();
                            DataTable TableList = new DataTable();

                            using (SqlConnection connection2 = new SqlConnection(ConnectionString.Connection()))
                            using (SqlCommand command2 = new SqlCommand("SELECT [HeaderId],[TRX_HEADER_ID]  FROM [EINVOICES_HEADERS_TEMP] where [TRX_HEADER_ID]=@trxid", connection2))
                            {
                                command2.Parameters.Add("@trxid", SqlDbType.VarChar);
                                command2.Parameters[0].Value = salesFilterCredit[i].trans_no;
                                connection2.Open();
                                SqlDataAdapter sqlDA = new SqlDataAdapter();
                                sqlDA.TableMappings.Add("Table", "EINVOICES_HEADERS_TEMP");
                                sqlDA.SelectCommand = command2;
                                sqlDA.Fill(Rs1);
                                connection2.Close();
                            }

                            if (Rs1.Tables.Count > 0)
                            {
                                if (Rs1.Tables[0].Rows.Count != 0)
                                {
                                    //   accepted++;
                                    var query = "DELETE FROM EINVOICES_HEADERS_TEMP WHERE TRX_HEADER_ID = '" + salesFilterCredit[i].trans_no + "'; " + "DELETE FROM EINVOICES_LINES_TEMP WHERE TRX_HEADER_ID = '" + salesFilterCredit[i].trans_no + "' " + " DELETE FROM TAX_HEADERS_TEMP WHERE TRX_HEADER_ID = '" + salesFilterCredit[i].trans_no + "' " + "DELETE FROM TAX_LINES_TEMP WHERE TRX_HEADER_ID = '" + salesFilterCredit[i].trans_no + "' ";
                                    SqlCommand cmd = new SqlCommand(query, connection);
                                    if (connection.State == ConnectionState.Closed)
                                    {
                                        connection.Open();
                                    }
                                    cmd.ExecuteNonQuery();

                                }
                                for (int x = 0; x < salesFilterCredit[i].line_items.Count(); x++)
                                {
                                    //add invoice lines
                                    command3.Parameters["@headerid"].Value = salesFilterCredit[i].line_items[x].debtor_trans_no;
                                    command3.Parameters["@lineid"].Value = x + 1;
                                    command3.Parameters["@descr"].Value = salesFilterCredit[i].line_items[x].description;
                                    if (salesFilterCredit[i].line_items[x].item_codes.FirstOrDefault().category_name == "Tax")
                                    {
                                        string codes = salesFilterCredit[i].line_items[x].item_codes.FirstOrDefault().item_code;
                                        string[] itemCode = codes.Split('-');
                                        command3.Parameters["@itemType"].Value = salesFilterCredit[i].line_items[x].item_codes.FirstOrDefault().description;
                                        command3.Parameters["@itemCode"].Value = itemCode[0];
                                    }
                                    else
                                    {
                                        command3.Parameters["@itemType"].Value = ""; command3.Parameters["@itemCode"].Value = "";
                                    }
                                    command3.Parameters["@unitType"].Value = salesFilterCredit[i].line_items[x].units;
                                    command3.Parameters["@qantity"].Value = salesFilterCredit[i].line_items[x].quantity;
                                    command3.Parameters["@internalCode"].Value = salesFilterCredit[i].line_items[x].stock_id;
                                    decimal salestotal = (salesFilterCredit[i].line_items[x].quantity * salesFilterCredit[i].line_items[x].unit_price * salesFilterCredit[i].rate);
                                    command3.Parameters["@salesTotal"].Value = salestotal;

                                    decimal total = (salesFilterCredit[i].line_items[x].quantity * salesFilterCredit[i].line_items[x].unit_price * salesFilterCredit[i].rate) - (salesFilterCredit[i].line_items[x].discount_percent) * (salesFilterCredit[i].line_items[x].quantity * salesFilterCredit[i].line_items[x].unit_tax * salesFilterCredit[i].rate);
                                    command3.Parameters["@total"].Value = total;
                                    command3.Parameters["@valueDifer"].Value = 0; command3.Parameters["@totalTax"].Value = 0;

                                    decimal nettotal = (salesFilterCredit[i].line_items[x].quantity * salesFilterCredit[i].line_items[x].unit_price * salesFilterCredit[i].rate) - (salesFilterCredit[i].line_items[x].discount_percent) * (salesFilterCredit[i].line_items[x].quantity * salesFilterCredit[i].line_items[x].unit_tax * salesFilterCredit[i].rate);
                                    command3.Parameters["@netTotal"].Value = nettotal;

                                    command3.Parameters["@itemDis"].Value = 0;
                                    command3.Parameters["@currSold"].Value = salesFilterCredit[i].curr_code;

                                    if (command3.Parameters["@currSold"].Value.ToString() == "EGP")
                                    {
                                        command3.Parameters["@amountEGP"].Value = salesFilterCredit[i].line_items[x].unit_price;
                                        command3.Parameters["@amountSold"].Value = 0;
                                        command3.Parameters["@currExchange"].Value = 0;
                                    }
                                    else
                                    {
                                        command3.Parameters["@amountEGP"].Value = salesFilterCredit[i].line_items[x].unit_price * salesFilterCredit[i].rate;
                                        command3.Parameters["@amountSold"].Value = salesFilterCredit[i].line_items[x].unit_price;
                                        command3.Parameters["@currExchange"].Value = salesFilterCredit[i].rate;
                                    }

                                    command3.Parameters["@disRate"].Value = salesFilterCredit[i].line_items[x].discount_percent;
                                    decimal disam = salesFilterCredit[i].line_items[x].discount_percent * salesFilterCredit[i].line_items[x].quantity * salesFilterCredit[i].line_items[x].unit_price * salesFilterCredit[i].rate;
                                    command3.Parameters["@disAmount"].Value = disam;
                                    command3.Parameters["@Ready"].Value = false;

                                    discAmount += disam;
                                    salesTot += salestotal;
                                    Net += nettotal;
                                    Total += total;

                                    if (connection.State == ConnectionState.Closed)
                                    {
                                        connection.Open();
                                    }
                                    command3.ExecuteNonQuery();


                                    //taxline
                                    decimal rate = 0;
                                    using (SqlCommand cmd2 = new SqlCommand("InsertTaxLines", connection))
                                    {
                                        cmd2.CommandType = CommandType.StoredProcedure;
                                        cmd2.Parameters.Add("@headerID", SqlDbType.VarChar);
                                        cmd2.Parameters.Add("@lineID", SqlDbType.VarChar);
                                        cmd2.Parameters.Add("@TaxType", SqlDbType.VarChar);
                                        cmd2.Parameters.Add("@SubType", SqlDbType.VarChar);
                                        cmd2.Parameters.Add("@Amount", SqlDbType.Decimal);
                                        cmd2.Parameters.Add("@rate", SqlDbType.Decimal);
                                        cmd2.Parameters.Add("@ready", SqlDbType.Bit);

                                        cmd2.Parameters["@headerID"].Value = salesFilterCredit[i].line_items[x].debtor_trans_no;
                                        cmd2.Parameters["@lineID"].Value = x + 1;

                                        string tax = salesFilterCredit[i].line_items[x].item_tax_type.tax_type.FirstOrDefault().name;
                                        string[] taxtype = tax.Split(':');

                                        cmd2.Parameters["@TaxType"].Value = taxtype[0];

                                        if (salesFilterCredit[i].tax_group.taxType.FirstOrDefault().rate == 0)
                                        {
                                            cmd2.Parameters["@rate"].Value = 0;
                                            rate = 0;
                                        }
                                        else
                                        {
                                            cmd2.Parameters["@rate"].Value = salesFilterCredit[i].line_items[x].item_tax_type.tax_type.FirstOrDefault().rate;
                                            rate = salesFilterCredit[i].line_items[x].item_tax_type.tax_type.FirstOrDefault().rate;
                                        }
                                        cmd2.Parameters["@Amount"].Value = ((rate / 100) * (salesFilterCredit[i].line_items[x].quantity * salesFilterCredit[i].line_items[x].unit_price * salesFilterCredit[i].rate)) - (salesFilterCredit[i].line_items[x].discount_percent) * (salesFilterCredit[i].line_items[x].quantity * salesFilterCredit[i].line_items[x].unit_tax * salesFilterCredit[i].rate);
                                        string sub = salesFilterCredit[i].line_items[x].item_tax_type.item_tax_type_name;
                                        string[] subtype = sub.Split(':');
                                        cmd2.Parameters["@SubType"].Value = subtype[0];
                                        cmd2.Parameters["@ready"].Value = false;
                                        if (connection.State == ConnectionState.Closed)
                                        {
                                            connection.Open();
                                        }
                                        cmd2.ExecuteNonQuery();

                                    }

                                }

                                command.Parameters["@HeaderID"].Value = salesFilterCredit[i].trans_no;
                                command.Parameters["@UssuerID"].Value = profil.GSTNo;
                                command.Parameters["@Type"].Value = salesFilterCredit[i].branch_data.group_no.description;
                                command.Parameters["@ID"].Value = salesFilterCredit[i].cutomer_details.tax_id;
                                command.Parameters["@Name"].Value = salesFilterCredit[i].name;

                                string FullAddress = salesFilterCredit[i].cutomer_details.address;
                                string[] address = FullAddress.Split('|');

                                command.Parameters["@Country"].Value = salesFilterCredit[i].branch_data.area.description;
                                command.Parameters["@Governate"].Value = address[3];
                                command.Parameters["@Region"].Value = address[2];
                                command.Parameters["@Street"].Value = address[1];
                                command.Parameters["@BuildinNO"].Value = address[0];
                                command.Parameters["@Postal"].Value = ""; command.Parameters["@floor"].Value = ""; command.Parameters["@Room"].Value = "";
                                command.Parameters["@LandMark"].Value = ""; command.Parameters["@AdditionalInfo"].Value = "";
                                command.Parameters["@DocumentTyp"].Value = 'C';
                                command.Parameters["@DocumentTypVer"].Value = "1.0";
                                command.Parameters["@DateTime"].Value = salesFilterCredit[i].tran_date + "T08:11:12Z";
                                command.Parameters["@Taxable"].Value = "";
                                command.Parameters["@InternalID"].Value = salesFilterCredit[i].reference;
                                command.Parameters["@Reverence"].Value = ""; command.Parameters["@Descrip"].Value = ""; command.Parameters["@SalesRefer"].Value = "";
                                command.Parameters["@SalesDesc"].Value = ""; command.Parameters["@ProInvoice"].Value = ""; command.Parameters["@BankNa"].Value = "";
                                command.Parameters["@BankADD"].Value = ""; command.Parameters["@BankACC"].Value = ""; command.Parameters["@BankIBAN"].Value = "";
                                command.Parameters["@SWIFTCODE"].Value = ""; command.Parameters["@TERMS"].Value = ""; command.Parameters["@APPROACH"].Value = "";
                                command.Parameters["@PACKAGING"].Value = ""; command.Parameters["@DATEVALIDITY"].Value = ""; command.Parameters["@EXPORTPORT"].Value = "";
                                command.Parameters["@COUNTRYOFORIGIN"].Value = ""; command.Parameters["@GROSSWEIGHT"].Value = ""; command.Parameters["@NETWEIGHT"].Value = "";
                                command.Parameters["@DELIVERY_TERMS"].Value = "";
                                command.Parameters["@TOTALDISCOUNTAMOUNT"].Value = discAmount;
                                command.Parameters["@TOTALSALESAMOUNT"].Value = salesTot;
                                command.Parameters["@NETAMOUNT"].Value = Net;
                                command.Parameters["@TOTALAMOUNT"].Value = Total;
                                command.Parameters["@EXTRADISCOUNTAMOUNT"].Value = 0.0; command.Parameters["@TOTALITEMSDISCOUNTAMOUNT"].Value = 0.0;
                                command.Parameters["@REFERENCE_UUID"].Value = 0;

                                command.Parameters["@REFERENCE_INVOICE_NUM"].Value = salesFilterCredit[i].related_invoice.FirstOrDefault().trans_no;

                                command.Parameters["@ACTIONTYPE"].Value = null;
                                command.Parameters["@MAINSTATUS"].Value = ""; command.Parameters["@ReadyToMain"].Value = false; command.Parameters["@DateTimeIssuedTemp"].Value = DateTime.UtcNow;

                                command.ExecuteNonQuery();

                                connection.Close();

                            }

                            using (SqlConnection source = new SqlConnection(ConnectionString.Connection()))
                            using (SqlConnection destination = new SqlConnection(ConnectionString.ConnectionEgy()))
                            {
                                //User Id=sa;Password=cd9009xy#;

                                source.Open();
                                destination.Open();

                                SqlCommand cmd1 = new SqlCommand("SELECT * FROM EINVOICES_HEADERS_TEMP where TRX_HEADER_ID = '" + salesFilterCredit[i].trans_no + "' ", source);
                                cmd1.CommandType = CommandType.Text;
                                cmd1.ExecuteNonQuery();

                                DataTable dt = new DataTable();
                                SqlDataAdapter adapter = new SqlDataAdapter(cmd1);
                                adapter.Fill(dt);

                                SqlBulkCopy BDInvoiceHeader = new SqlBulkCopy(destination);
                                BDInvoiceHeader.DestinationTableName = "EINVOICES_HEADERS_TEMP";
                                BDInvoiceHeader.WriteToServer(dt);
                                BDInvoiceHeader.Close();

                                for (int m = 0; m <= dt.Rows.Count - 1; m++)
                                {
                                    //invoice line
                                    SqlCommand cmd2 = new SqlCommand("SELECT * FROM EINVOICES_LINES_TEMP where TRX_HEADER_ID = '" + salesFilterCredit[i].trans_no + "' ", source);
                                    cmd2.CommandType = CommandType.Text;
                                    cmd2.ExecuteNonQuery();

                                    DataTable dtInvoiceLine = new DataTable();
                                    SqlDataAdapter daInvoiceLine = new SqlDataAdapter(cmd2);
                                    daInvoiceLine.Fill(dtInvoiceLine);

                                    SqlBulkCopy BDInvoiceLines = new SqlBulkCopy(destination);
                                    BDInvoiceLines.DestinationTableName = "EINVOICES_LINES_TEMP";
                                    BDInvoiceLines.WriteToServer(dtInvoiceLine);
                                    BDInvoiceLines.Close();

                                    //taxheader
                                    SqlCommand cmd3 = new SqlCommand("SELECT * FROM TAX_HEADERS_TEMP where TRX_HEADER_ID = '" + salesFilterCredit[i].trans_no + "' ", source);
                                    cmd3.CommandType = CommandType.Text;
                                    cmd3.ExecuteNonQuery();
                                    DataTable dtTaxHeader = new DataTable();
                                    SqlDataAdapter daTaxHeader = new SqlDataAdapter(cmd3);
                                    daTaxHeader.Fill(dtTaxHeader);

                                    SqlBulkCopy BDtaxHeader = new SqlBulkCopy(destination);
                                    BDtaxHeader.DestinationTableName = "TAX_HEADERS_TEMP";
                                    BDtaxHeader.WriteToServer(dtTaxHeader);
                                    BDtaxHeader.Close();


                                    //tax line
                                    SqlCommand cmd4 = new SqlCommand("SELECT * FROM TAX_LINES_TEMP where TRX_HEADER_ID = '" + salesFilterCredit[i].trans_no + "' ", source);
                                    cmd4.CommandType = CommandType.Text;
                                    cmd4.ExecuteNonQuery();
                                    DataTable dtTaxLine = new DataTable();
                                    SqlDataAdapter daTaxLine = new SqlDataAdapter(cmd4);
                                    daTaxLine.Fill(dtTaxLine);

                                    SqlBulkCopy BDTaxLines = new SqlBulkCopy(destination);
                                    BDTaxLines.DestinationTableName = "TAX_LINES_TEMP";
                                    BDTaxLines.WriteToServer(dtTaxLine);
                                    BDTaxLines.Close();

                                }
                            }
                            discAmount = 0; Total = 0; Net = 0; salesTot = 0;
                        }


                        MessageBox.Show("Done......" + "\n" + "Total : " + salesFilterCredit.Count());
                    }

                }

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            System.Net.ServicePointManager.SecurityProtocol =
                                    System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;
            cmbtype.Select();
            button1.Enabled = false;
            dateTimePicker1.ValueChanged += new EventHandler(dateTimePicker1_ValueChanged);
            dateTimePicker2.ValueChanged += new EventHandler(dateTimePicker2_ValueChanged);
            dateTimePicker1.Value = DateTime.Now.Date.AddDays(-6);
            dateTimePicker2.Value = DateTime.Now;
        }

        private void cmbtype_TextChanged(object sender, EventArgs e)
        {
            if (cmbtype.Text == "")
            {
                cmbtype.Select();
                MessageBox.Show("Please Choose Value !!");
            }
            else
            {
                button1.Enabled = true;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateChanged1 = true;
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {

            if (dateTimePicker1.Value.Date < DateTime.Now.Date.AddDays(-6))
            {
                MessageBox.Show("Please Spiciefic rang of days (just 6 days)");
                dateTimePicker1.Focus();

            }
            else if (dateTimePicker1.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("value inserted more than current date !");
                dateTimePicker1.Focus();

            }

        }

        private void dateTimePicker2_CloseUp(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value.Date < dateTimePicker1.Value.Date)
            {
                MessageBox.Show("invalid rang!");
                dateTimePicker2.Focus();

            }
            else if (dateTimePicker2.Value.Date > DateTime.Now.Date)  //|| dateTimePicker2.Value.Date <= dateTimePicker1.Value.Date.AddDays(-6))
            {
                MessageBox.Show("value inserted more than current date ");
                dateTimePicker2.Focus();
            }

        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateChanged2 = true;
        }

        private void cmbFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFormat.Text == "dd/MM/yyyy")
            {
                dateTimePicker1.CustomFormat = "dd/MM/yyyy";
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker2.CustomFormat = "dd/MM/yyyy";
                dateTimePicker2.Format = DateTimePickerFormat.Custom;
            }
            else if (cmbFormat.Text == "MM/dd/yyyy")
            {
                dateTimePicker1.CustomFormat = "MM/dd/yyyy";
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker2.CustomFormat = "MM/dd/yyyy";
                dateTimePicker2.Format = DateTimePickerFormat.Custom;
            }

        }
    }
}
