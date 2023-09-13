namespace E_Commerce_App.Models
{
    public class Image
    {
    }
    public class BingImageSearchResult
    {
        public List<BingImageResult> value { get; set; }
    }

    public class BingImageResult
    {
        public string contentUrl { get; set; }
        // Add other properties you need
    }
}
