using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterBot.Core.Models
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime PostAt { get; set; }
        public string PageName { get; set; }
        public int NewsId { get; set; }
        public News News { get; set; }
        public string PublisherId { get; set; }
        public ApplicationUser Publisher { get; set; }
        public IEnumerable<Image> ImagesOfPost { get; set; }
    }
}
