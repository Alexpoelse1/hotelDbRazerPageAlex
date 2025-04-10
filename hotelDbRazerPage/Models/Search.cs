namespace hotelDbRazerPage.Models
{
    public class Search
    {
        
        public String SearchText { get; set; }
        public String SearchType { get; set; }

        public Search()
        {
        }

        public Search( string searchText, string searchType)
        {
            
            SearchText = searchText;
            SearchType = searchType;
        }

        public override string ToString()
        {
            return $" {nameof(SearchText)}: {SearchText}, {nameof(SearchType)}: {SearchType}";
        }
    }
}
