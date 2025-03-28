﻿using System.ComponentModel.DataAnnotations;

namespace LearningWebApi.Models.DomainModels
{
    public class Country
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        public int? NumberOfPeople { get; set; }
        [MaxLength(50)]
        public string? Faith { get; set; } = string.Empty;
    }
}
