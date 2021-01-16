﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChessMeters.Core.Entities
{
    public class TreeMove
    {
        public long Id { get; set; }

        [MaxLength(5)]
        public string Move { get; set; }

        public bool Color
        {
            get
            {
                return (!string.IsNullOrEmpty(FullPath) ? FullPath.Split(' ').Length : 0) / 2 != 0;
            }
            private set { }
        }

        public long? ParentTreeMoveId { get; set; }

        [ForeignKey(nameof(ParentTreeMoveId))]
        public virtual TreeMove ParentTreeMove { get; set; }

        public string FullPath { get; set; }
    }
}