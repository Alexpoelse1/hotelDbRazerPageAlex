using hotelDbRazerPage.Interfaces;
using hotelDbRazerPage.Models;
using hotelDbRazerPage.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace hotelDbRazerPage.Pages.Hotels
{
    public class SearchHotelModel : PageModel
    {
        private IHotelServiceAsync _hotelService;
        public List<Hotel> Hotels;
        public List<Hotel> ResultHotels;
        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchType { get; set; }




        public SearchHotelModel(IHotelServiceAsync hotelService)
        {
            _hotelService = hotelService;
        }
        public IActionResult OnPost(int Id)
        {
            return RedirectToPage("UpdateHotel", new { Id });
        }
        public IActionResult OnPostDelete(int deleteHotelNr)
        {
            return RedirectToPage("DeleteHotel", new { deleteHotelNr });
        }

        public async Task OnGetAsync(string SEARCHText, string SEARCHType)
        {
            Console.WriteLine($"Received Search: {Search}, Type: {SearchType}");

            Search search=new Search();
            search.SearchText = Search;
            search.SearchType = SEARCHType;
            Hotels = await _hotelService.GetAllHotelAsync();
            ResultHotels = await _hotelService.Search(search);
        }
    }
}
