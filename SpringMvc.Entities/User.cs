using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpringMvc.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string  Id { get; set; }

        [BsonElement("UserName")]
        [Required]
        public virtual string UserName { get; set; }

        [BsonElement("Password")]
        [Required]
        public virtual string Password { get; set; }

        [BsonElement("ConfirmPassword")]
        [Required]
        public virtual string ConfirmPassword { get; set; }

        [BsonElement("Email")]
        [Display(Name = "Email")]
        [DisplayFormat(DataFormatString = "{0:#,0}")]
        public virtual string Email { get; set; }

        //[BsonElement("ImageUrl")]
        //[Display(Name = "Photo")]
        //[DataType(DataType.ImageUrl)]
        [Required]
        public virtual string Photo { get; set; }
    }
}
