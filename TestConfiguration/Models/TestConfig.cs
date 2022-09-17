using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestConfiguration.Models
{
    public class TestConfig
    {
        public int Id { get; set; }
        public int AdmissionReferenceId { get; set; }
        public int BatchId { get; set; }
        public int SesionId { get; set; }
        public decimal MinimumGPA { get; set; }
        public decimal PassMark { get; set; }
        public string ProfileUrl { get; set; }
        public string SignatureUrl { get; set; }
        public virtual List<TestConfigDetail> TestConfigDetails { get; set; } = new List<TestConfigDetail>();
        [NotMapped]
        public IFormFile  ProfileImage { get; set; }
        [NotMapped]
        public IFormFile  SignatureImage { get; set; }
    }
}
