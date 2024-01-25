﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Collection.Data;

namespace Collection.DAL.Entities
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public int Likes { get; set; } = 0;
        public int? FirstInteger { get; set; }
        public int? SecondInteger { get; set; }
        public int? ThirdInteger { get; set; }
        public string FirstString { get; set; }
        public string SecondString { get; set; }
        public string ThirdString { get; set; }
        public string FirstText { get; set; }
        public string SecondText { get; set; }
        public string ThirdText { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FirstDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? SecondDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ThirdDate { get; set; }

        public bool? FirstBoolean { get; set; }
        public bool? SecondBoolean { get; set; }
        public bool? ThirdBoolean { get; set; }
        public ICollection<ApplicationUser> UsersLiked { get; set; } = new List<ApplicationUser>();
        public int CollectionId { get; set; }
        public CollectionModel Collection { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}