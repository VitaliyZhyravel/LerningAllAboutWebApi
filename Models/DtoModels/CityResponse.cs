﻿using LearningWebApi.Models.DomainModels;
using System.ComponentModel.DataAnnotations;

namespace LearningWebApi.Models.DtoModels
{
    public class CityResponse
    {
        public Guid Id { get; set; }
        [MaxLength(30, ErrorMessage = "Max length 30 characters")]
        public string Name { get; set; } = string.Empty;
        public Guid? CountryId { get; set; }
        public Country? Country { get; set; }
    }
}
