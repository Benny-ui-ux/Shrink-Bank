using System;
using System.ComponentModel.DataAnnotations;

namespace Shrink_Bank.Models 
{
    public class InventoryViewModel
    {
        public int InventoryID { get; set; }

        [Display(Name = "Item Name")]
        public string InventoryName { get; set; } = string.Empty;

        public string? Department { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }

        public string? Quantity { get; set; }

        [Display(Name = "Expiration Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpirationDate { get; set; }

        [Display(Name = "Status")]
        public string ExpirationStatus { get; set; } = string.Empty;

        public int DaysUntilExpiration { get; set; }

    
        [Display(Name = "Last Ever Checked By")]
        public string? AbsoluteLastCheckedBy { get; set; }

        [Display(Name = "Last Ever Checked Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? AbsoluteLastCheckedDate { get; set; }

        [Display(Name = "Checked This Period By")]
        public string? CheckedThisPeriodBy { get; set; }

        [Display(Name = "Checked This Period Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? CheckedThisPeriodDate { get; set; }

        public bool IsCheckedThisPeriod { get; set; }
        public bool CanCurrentUserCheck { get; set; } 

        public string RowCssClass { get; set; } = string.Empty;
    }
}