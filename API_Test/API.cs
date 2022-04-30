using System.Collections.Generic;
namespace API_Test
{
    public class Profil
    {
        public string company_name { get; set; }
        public string GSTNo { get; set; }
        public string Official_Company_Number { get; set; }
        public string Address { get; set; }
        public string Phone_Number { get; set; }
        public string Fax_Number { get; set; }
        public string email { get; set; }
        public string domicile { get; set; }
        public string curr_default { get; set; }
    }

    public class CutomerDetails
    {
        public string _0 { get; set; }
        public string debtor_no { get; set; }
        public string _1 { get; set; }
        public string name { get; set; }
        public string _2 { get; set; }
        public string address { get; set; }
        public string _3 { get; set; }
        public string tax_id { get; set; }
        public string _4 { get; set; }
        public string curr_code { get; set; }
        public string _5 { get; set; }
        public string sales_type { get; set; }
        public string _6 { get; set; }
        public string dimension_id { get; set; }
        public string _7 { get; set; }
        public string dimension2_id { get; set; }
        public string _8 { get; set; }
        public string credit_status { get; set; }
        public string _9 { get; set; }
        public string payment_terms { get; set; }
        public string _10 { get; set; }
        public decimal discount { get; set; }
        public string _11 { get; set; }
        public decimal pymt_discount { get; set; }
        public string _12 { get; set; }
        public decimal credit_limit { get; set; }
        public string _13 { get; set; }
        public string notes { get; set; }
        public string _14 { get; set; }
        public string inactive { get; set; }
        public string _15 { get; set; }
        public string debtor_ref { get; set; }
    }

    public class Area
    {
        public string _0 { get; set; }
        public string area_code { get; set; }
        public string _1 { get; set; }
        public string description { get; set; }
        public string _2 { get; set; }
        public string inactive { get; set; }
    }

    public class GroupNo
    {
        public string _0 { get; set; }
        public string id { get; set; }
        public string _1 { get; set; }
        public string description { get; set; }
        public string _2 { get; set; }
        public string inactive { get; set; }
    }

    public class BranchData
    {
        public string _0 { get; set; }
        public string branch_code { get; set; }
        public string _1 { get; set; }
        public string debtor_no { get; set; }
        public string _2 { get; set; }
        public string br_name { get; set; }
        public string _3 { get; set; }
        public string br_address { get; set; }
        public string _4 { get; set; }
        public Area area { get; set; }
        public string _5 { get; set; }
        public string salesman { get; set; }
        public string _6 { get; set; }
        public string default_location { get; set; }
        public string _7 { get; set; }
        public string tax_group_id { get; set; }
        public string _8 { get; set; }
        public object sales_account { get; set; }
        public string _9 { get; set; }
        public decimal sales_discount_account { get; set; }
        public string _10 { get; set; }
        public decimal receivables_account { get; set; }
        public string _11 { get; set; }
        public decimal payment_discount_account { get; set; }
        public string _12 { get; set; }
        public string default_ship_via { get; set; }
        public string _13 { get; set; }
        public string br_post_address { get; set; }
        public string _14 { get; set; }
        public GroupNo group_no { get; set; }
        public string _15 { get; set; }
        public string notes { get; set; }
        public object _16 { get; set; }
        public object bank_account { get; set; }
        public string _17 { get; set; }
        public string inactive { get; set; }
        public string _18 { get; set; }
        public string branch_ref { get; set; }
        public string _19 { get; set; }
        public string salesman_name { get; set; }
    }

    public class TaxType
    {
        public string tax_type_name { get; set; }
        public string tax_type_id { get; set; }
        public decimal rate { get; set; }
        public int tax_shipping { get; set; }
    }

    public class TaxGroup
    {
        public string tax_group_id { get; set; }
        public string tax_group_name { get; set; }
        public List<TaxType> taxType { get; set; }
    }

    public class TaxType2
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal rate { get; set; }
    }

    public class ItemTaxType
    {
        public int item_tax_type_id { get; set; }
        public string item_tax_type_name { get; set; }
        public string item_tax_type_exempt { get; set; }
        public List<TaxType2> tax_type { get; set; }
    }

    public class ItemCode
    {
        public string item_code { get; set; }
        public decimal quantity { get; set; }
        public string units { get; set; }
        public string description { get; set; }
        public string stock_id { get; set; }
        public string category_id { get; set; }
        public string category_name { get; set; }
        public int is_foreign { get; set; }
        public int inactive { get; set; }
        public int id { get; set; }
    }

    public class LineItem
    {
        public string _0 { get; set; }
        public string id { get; set; }
        public string _1 { get; set; }
        public string debtor_trans_no { get; set; }
        public string _2 { get; set; }
        public string debtor_trans_type { get; set; }
        public string _3 { get; set; }
        public string stock_id { get; set; }
        public string _4 { get; set; }
        public string description { get; set; }
        public string _5 { get; set; }
        public decimal unit_price { get; set; }
        public string _6 { get; set; }
        public decimal unit_tax { get; set; }
        public string _7 { get; set; }
        public decimal quantity { get; set; }
        public string _8 { get; set; }
        public decimal discount_percent { get; set; }
        public string _9 { get; set; }
        public decimal standard_cost { get; set; }
        public string _10 { get; set; }
        public decimal qty_done { get; set; }
        public string _11 { get; set; }
        public string src_id { get; set; }
        public string _12 { get; set; }
        public decimal FullUnitPrice { get; set; }
        public string _13 { get; set; }
        public string StockDescription { get; set; }
        public string _14 { get; set; }
        public string units { get; set; }
        public string _15 { get; set; }
        public string mb_flag { get; set; }
        public string category_id { get; set; }
        public decimal sales_account { get; set; }
        public decimal cogs_account { get; set; }
        public decimal inventory_account { get; set; }
        public decimal adjustment_account { get; set; }
        public string wip_account { get; set; }
        public decimal purchase_cost { get; set; }
        public decimal material_cost { get; set; }
        public decimal labour_cost { get; set; }
        public decimal overhead_cost { get; set; }
        public ItemTaxType item_tax_type { get; set; }
        public List<ItemCode> item_codes { get; set; }
    }

    public class SalesFilterInvoice
    {
        public string _0 { get; set; }
        public int type { get; set; }
        public string _1 { get; set; }
        public int trans_no { get; set; }
        public string _2 { get; set; }
        public int order_ { get; set; }
        public string _3 { get; set; }
        public string reference { get; set; }
        public string _4 { get; set; }
        public string tran_date { get; set; }
        public string _5 { get; set; }
        public string due_date { get; set; }
        public string _6 { get; set; }
        public string name { get; set; }
        public string _7 { get; set; }
        public string br_name { get; set; }
        public string _8 { get; set; }
        public string curr_code { get; set; }
        public string _9 { get; set; }
        public string debtor_no { get; set; }
        public decimal _10 { get; set; }
        public decimal TotalAmount { get; set; }
        public string _11 { get; set; }
        public decimal Allocated { get; set; }
        public string _12 { get; set; }
        public string OverDue { get; set; }
        public string _13 { get; set; }
        public string Outstanding { get; set; }
        public string _14 { get; set; }
        public string HasChild { get; set; }
        public CutomerDetails cutomer_details { get; set; }
        public BranchData branch_data { get; set; }
        public decimal ov_freight_tax { get; set; }
        public decimal ov_amount { get; set; }
        public decimal ov_gst { get; set; }
        public decimal rate { get; set; }
        public object related_invoice { get; set; }
        public object related_delivery_note { get; set; }
        public decimal Total { get; set; }
        public TaxGroup tax_group { get; set; }
        public List<LineItem> line_items { get; set; }
    }

    public class RelatedInvoice
    {
        public string trans_no { get; set; }
        public string type { get; set; }
        public string reference { get; set; }
        public string tran_date { get; set; }
        public int this_total { get; set; }
    }

    public class RelatedDeliveryNote
    {
        public string trans_no { get; set; }
        public int this_total { get; set; }
        public string type { get; set; }
        public string reference { get; set; }
        public string tran_date { get; set; }
    }

    public class SalesFilterCredit
    {
        public string _0 { get; set; }
        public string type { get; set; }
        public string _1 { get; set; }
        public string trans_no { get; set; }
        public string _2 { get; set; }
        public string order_ { get; set; }
        public string _3 { get; set; }
        public string reference { get; set; }
        public string _4 { get; set; }
        public string tran_date { get; set; }
        public string _5 { get; set; }
        public string due_date { get; set; }
        public string _6 { get; set; }
        public string name { get; set; }
        public string _7 { get; set; }
        public string br_name { get; set; }
        public string _8 { get; set; }
        public string curr_code { get; set; }
        public string _9 { get; set; }
        public string debtor_no { get; set; }
        public string _10 { get; set; }
        public decimal TotalAmount { get; set; }
        public string _11 { get; set; }
        public string Allocated { get; set; }
        public string _12 { get; set; }
        public string OverDue { get; set; }
        public string _13 { get; set; }
        public string Outstanding { get; set; }
        public string _14 { get; set; }
        public string HasChild { get; set; }
        public CutomerDetails cutomer_details { get; set; }
        public BranchData branch_data { get; set; }
        public decimal ov_freight_tax { get; set; }
        public decimal ov_amount { get; set; }
        public decimal ov_gst { get; set; }
        public decimal rate { get; set; }
        public List<RelatedInvoice> related_invoice { get; set; }
        public List<RelatedDeliveryNote> related_delivery_note { get; set; }
        public decimal Total { get; set; }
        public TaxGroup tax_group { get; set; }
        public List<LineItem> line_items { get; set; }
    }

    public class LoginInfo
    {
        public string state { get; set; }
        public string companyNumber { get; set; }
        public string user { get; set; }
    }


}
