using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Entity
{
    public class Dashboardviewmodel
    {
        public class menumodel
        {
            public int Menuid { get; set; }
            public string MenuName { get; set; }
            public string MenuLink { get; set; }
            public bool Status { get; set; }
        }

        public class submenumodel
        {
            public int Submenuid { get; set; }
            public string SubmenuName { get; set; }
            public string Submenulink { get; set; }
            public int Menuid { get; set; }
            public bool Status { get; set; }
        }

            [DisplayName("News ID ")]
            public int Newsid { get; set; }
            [DisplayName("News Date ")]
            public DateTime NewsDate { get; set; }
            [DisplayName("News Title ")]
            public string NewsTitle { get; set; }
            [DisplayName("News Desc ")]
            public string NewsDesc { get; set; }
            [DisplayName("News Status ")]
            public bool Status { get; set; }

            public string Course { get; set; }
            [DisplayName("Name")]
            public string Studnm { get; set; }
            [DisplayName("Birth Day")]
            public DateTime Birthday { get; set; }
            public byte[] StudPic { get; set; }
       
        //public List<sp_menudatacollection_Result> Menudatacollection { get; set; }
        public List<tbl_submenu> SubMenudatacollection { get; set; }
        public List<tbl_news> Newsdatacollection { get; set; }
        public List<sp_GetBirthdayListDetails_Result> BirthdaylistDetails { get; set; }
        public List<sp_getNewslist_Result> _Newslist { get; set; }
        public List<DateofBirth_Result> _BirthDayList { get; set; }

        public class MyProfileView
        {
            public int UserId { get; set; }
            public string UserName { get; set; }

           
        } 
    }

}
