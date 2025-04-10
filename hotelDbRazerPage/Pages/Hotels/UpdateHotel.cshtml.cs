using hotelDbRazerPage.Interfaces;
using hotelDbRazerPage.Models;
using hotelDbRazerPage.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace hotelDbRazerPage.Pages.Hotels
{
    public class UpdateHotelModel : PageModel
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
        public string HotelAddress {get; set;}
        #endregion
        //        private Task<List<Hotel>> hots;
#region constructor
        public UpdateHotelModel(IHotelServiceAsync hotelService) // dependency injection
        {
            _hotelService = hotelService; // parameter overført 
        }
        #endregion
        public List<Hotel> sHotels
        {
            get { return _hotelService.GetAllHotelAsync().Result; }
        }

        public async Task<IActionResult> OnGet(int Id)
        {
            Hotels = sHotels;

            Hotel = await _hotelService.GetHotelFromIdAsync(Id);

            HotelNr = Hotel.Hotel_No;
            HotelName = Hotel.Name;
            HotelAddress = Hotel.Address;
            return Page();
        }
        public IActionResult OnPost(int id)
        {
            Hotel hotel = new Hotel(HotelNr,HotelName,HotelAddress);
            _hotelService.UpdateHotelAsync(hotel, id);
            return RedirectToPage("ShowHotels");
        }
        public IActionResult OnPostDelete(int DeleteHotelNr)
        {

            return RedirectToPage("DeleteHotel", new { DeleteHotelNr });
        }
    }
}
