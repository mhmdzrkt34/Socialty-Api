using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Socialty.Models
{
    public class Post
    {

        [Key]
        public int Id { get; set; }

    
        
        public String Post_Url { get; set; }

        [JsonIgnore]
       
        public virtual ApplicationUser User { get; set; }


    }
}
