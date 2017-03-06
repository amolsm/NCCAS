using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Entity
{
    public class BookAllocation:libBookentry
    {
        public List<tbl_lib_BookIssue> _BookIssueList { get; set; }

        public List<tbl_BookStock> _BookStockList { get; set; }

        public List<sp_GetBookDetailsbyBookidorBookname_Result> _bookslist { get; set; }
    }
}
