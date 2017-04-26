using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class Categoryviewmodel
    {
        [DisplayName("Category ID ")]
        public int catid { get; set; }

        [DisplayName("Category Name ")]
        public string catname { get; set; }

        [DisplayName("Active ")]  
        public bool status { get; set; }

        [DisplayName("Product ")]
        public string Product { get; set; }

        [DisplayName("Product ID ")]
        public int Productid { get; set; }

        [DisplayName("Inventory ID ")]
        public int Inventoryid { get; set; }

        [DisplayName("Quantity ")]
        public int Quantity { get; set; }

        [DisplayName("Price ")]
        public decimal Price { get; set; }

        [DisplayName("Sub Category ID ")]
        public int subcatid { get; set; }

        [DisplayName("Sub Category Name ")]
        public string subcatname { get; set; }

        [DisplayName("Vendor ")]
        public int VendorId { get; set; }
        public List<sp_getcategory_Result> _categorylist { get; set; }

        public List<tbl_category> Categorylist { get; set; }


        public List<tbl_SubCategory> _subCategorylist { get; set; }

        public List<sp_getProduct_Result> _Productlist { get; set; }

        public List<sp_getInventory_Result> _Inventorylist { get; set; }

        public List<tbl_Product> productlist { get; set; }

        public List<sp_getSubCategory_Result> _SubCategorylist { get; set; }

        public List<tbl_InventoryVendor> _vendorlist { get; set; }
    }
    public class InventoryVendorviewmodal
    {
        [DisplayName("Vendor ID ")]
        public int V_Id { get; set; }

        [DisplayName("Vendor Name ")]
        public string VendorName { get; set; }

        [DisplayName("Address")]
        public string V_Address { get; set; }

        [DisplayName("MobileNo.")]
        public string V_MobileNo { get; set; }

        [DisplayName("PhoneNo.")]
        public string V_PhoneNo { get; set; }

        [DisplayName("Active ")]
        public bool status { get; set; }

        public List<sp_getInventoryVendorInfo_Result> _inventoryvendorlist { get; set; }
    }
}
