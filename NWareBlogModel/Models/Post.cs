using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NWareBlogModels
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Pub Date")]
        public DateTime PublicationDate { get; set; } = DateTime.Now.Date;

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public int CategoryId { get; set; }

    }
}