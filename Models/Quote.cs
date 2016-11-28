using System;
using System.ComponentModel.DataAnnotations;

namespace aspQuoting.Models
{
    public abstract class BaseEntity {}

    public class Quote : BaseEntity
        {
            public int id;
            [Required]
            [MinLength(2)]
            public string user { get; set; }
            [Required]
            [MinLength(2)]
            public string quote { get; set; }
            public int likes;
            public DateTime created_at;
            public DateTime updated_at;
        }
}