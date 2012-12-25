using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LibModels
{
    public class Item
    {
        public Int64 ID { get; set; }
        [Required]
        [Display(Name = "Titel")]
        [StringLength(255, ErrorMessage = "Het {0} moet tenminste {2} karakters lang zijn.", MinimumLength = 6)]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Omschrijving")]
        [StringLength(500, ErrorMessage = "Het {0} moet tenminste {2} karakters lang zijn.", MinimumLength = 6)]
        public string Description { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }
        public Nullable<DateTime> PublishedDate { get; set; }
        public Nullable<DateTime> ApprovedDate { get; set; }
    }
}
