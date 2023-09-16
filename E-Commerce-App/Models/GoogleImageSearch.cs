namespace E_Commerce_App.Models
{
    public class GoogleImageSearchResult
    {
        public List<Item> items { get; set; }

        public class Item
        {
            public string link { get; set; }
            // Add other properties as needed
        }
    }
}
