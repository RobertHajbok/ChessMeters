using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChessMeters.Core.Entities
{
    public class Report
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
