using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Service
    {
        public int Id { get; set; }
        public User User { get; set; }
        [DisplayName("Наименование")]
        public string Title { get; set; }
        [DisplayName("Категория")]
        public int CategoryId { get; set; }
        [DisplayName("Стоимость")]
        [DisplayFormat(DataFormatString = "{0:c0}")]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
