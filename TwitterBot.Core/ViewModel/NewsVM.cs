namespace TwitterBot.Web.Areas.NewsReviewer.ViewModel
{
    public class NewsVM
    {
        public int Id { get; set; }
        public bool IsApproved { get; set; }
        public string DecisionById { get; set; }
        public string OriganalNews { get; set; }
        public string ParaphrasdNews { get; set; }
        public string Comment { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public DateTime? PublishedAt { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime DecisionTime { get; set; }
    }
}
