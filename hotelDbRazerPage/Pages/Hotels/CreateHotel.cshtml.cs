using hotelDbRazerPage.Interfaces;
using hotelDbRazerPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace hotelDbRazerPage.Pages.Hotels
{
    public class CreateHotelModel : PageModel
    {
        private IHotelServiceAsync _hotelService;
        private List<Hotel> Hotels = new List<Hotel>();
        #region Properties
        [BindProperty] // Two way binding
        public Hotel Hotel { get; set; }
        [BindProperty]
        public int HotelNr { get; set; }
        [BindProperty]
        public string HotelName { get; set; }
        [BindProperty]
        public string HotelAddress { get; set; }
        #endregion
        //        private Task<List<Hotel>> hots;
        #region constructor
        public CreateHotelModel(IHotelServiceAsync hotelService) // dependency injection
        {
            _hotelService = hotelService; // parameter overført 
        }
        #endregion
        /*public async List<Hotel> sHotels
        {
            get { return await _hotelService.GetAllHotelAsync(); }
        }*/

        public IActionResult OnGet(/*int Id*/)
        {/*
            Hotels = sHotels;

            Hotel = _hotelService.GetHotelFromIdAsync(Id).Result;

            HotelNr = Hotel.HotelNr;
            HotelName = Hotel.Navn;
            HotelAddress = Hotel.Adresse;*/
            return Page();
        }
        public IActionResult OnPost(int id)
        {
            Hotel hotel = new Hotel(HotelNr, HotelName, HotelAddress);
            _hotelService.CreateHotelAsync(hotel);
            return RedirectToPage("ShowHotels");
        }
        public IActionResult OnPostCancel()
        {
            return RedirectToPage("ShowHotels");
        }
    }
}
