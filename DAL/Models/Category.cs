using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public int? ParentId { get; set; }
    }

    public class Node
    {
        public IEnumerable<Node> Children { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}