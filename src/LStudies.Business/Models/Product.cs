﻿using System;

namespace LStudies.Business.Models
{
    public class Product : Entity
    {
        public Guid ProviderId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; }

        /*EF Relation*/
        public Provider Provider { get; set; }
    }
}