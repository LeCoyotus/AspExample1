namespace AppDbContext.Entity.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Book
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Synopsis { get; set; }

        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }
    }
}
