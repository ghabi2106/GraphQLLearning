using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLHotChocolate.DAL.Entities
{
    public class School
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SchoolId { get; set; }

        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
