using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroserviceAuthor.Models
{
    public class Degree
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DegreeID { get; set; }
        public string Name { get; set; }
        public string Institution { get; set; }
        public DateTime? DegreeDate { get; set; }
        public string DegreeGuid { get; set; }
        public int AuthorID { get; set; }
        public virtual Author Author { get; set; }
    }
}
