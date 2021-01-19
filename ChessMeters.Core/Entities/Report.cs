using System;
using System.Collections.Generic;
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

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [Required]
        public string PGN { get; set; }

        public DateTime? LastUpdated { get; set; }

        public string LastUpdateUserId { get; set; }

        [ForeignKey(nameof(LastUpdateUserId))]
        public virtual User LastUpdateUser { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
