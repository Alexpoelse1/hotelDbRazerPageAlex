using hotelDbRazerPage.Interfaces;
using hotelDbRazerPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace hotelDbRazerPage.Pages.Hotels
{
    public class ShowHotelsModel : PageModel
    {
        private IHotelServiceAsync _hotelService;

        [BindProperty]
        public Search Search { get; set; } = new Search();

        [BindProperty(SupportsGet = true)]
        public string SearchText { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchType { get; set; }


       

        public List<Hotel> Hotels { get; set; }




        public List<string> types = [ "ID", "NAME", "ADDRESS" ];
        
        public ShowHotelsModel(IHotelServiceAsync hotelService)
        {
            _hotelService = hotelService;
        }

        #region Methods
        public IActionResult OnPost(int Id)
        {
            return RedirectToPage("UpdateHotel", new { Id });
        }
        public IActionResult OnPostDelete(int deleteHotelNr)
        {
            return RedirectToPage("DeleteHotel", new  { deleteHotelNr});
        }
        public IActionResult OnPostSearch(Search Search)
        {
            Console.WriteLine($"Searching for {Search.SearchText} by {Search.SearchType}");

            return RedirectToPage("SearchHotel", new { search = Search.SearchText, searchType = Search.SearchType });
        }


        public IActionResult OnPostCreate()
        {
            return RedirectToPage("CreateHotel");
        }

        public void OnPostSelectType(string type)
        {
            SearchType = type;
            
        }
       
        public async Task OnGetAsync()
        {
            if (Hotels == null)
            {
                Hotels = await _hotelService.GetAllHotelAsync();
                Thread.Sleep(1000);
            }
            Console.WriteLine( "OnGetAsync is done");
        }
        #endregion
    }
}
