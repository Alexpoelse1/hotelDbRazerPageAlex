using hotelDbRazerPage.Interfaces;
using hotelDbRazerPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace hotelDbRazerPage.Pages.Hotels
{
    public class DeleteHotelModel : PageModel
    {
        private IHotelServiceAsync _hotelService;
        private List<Hotel> Hotels = new List<Hotel>();
        #region Properties
        public Hotel Hotel { get; set; }
        public int HotelNr { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        #endregion
        //        private Task<List<Hotel>> hots;
        #region constructor
        public DeleteHotelModel(IHotelServiceAsync hotelService) // dependency injection
        {
            _hotelService = hotelService; // parameter overført 
            /*HotelNr= 0;
            HotelName = "";
            HotelAddress = ""*/
        }
        #endregion
        public List<Hotel> sHotels
        {
            get { return _hotelService.GetAllHotelAsync().Result; }
        }
        public void setvar(int hotNum, string hotNam, string hotAdr)
        {
            /*HotelNr = hotNum;
            HotelName = hotNam;
            HotelAddress = hotAdr;*/
            
            Console.WriteLine($"HotelNr : {HotelNr}, HotelName : {HotelName} , HotelAddress : {HotelAddress}" );
        }

        public async Task OnGet(int deleteHotelNr)
        {
            Hotels = await _hotelService.GetAllHotelAsync();
            Hotel = await _hotelService.GetHotelFromIdAsync(deleteHotelNr);
            HotelNr = deleteHotelNr;
            HotelName = Hotel.Name;
            HotelAddress = Hotel.Address;
            setvar(deleteHotelNr, Hotel.Name, Hotel.Address);
        }
        public async Task<IActionResult> OnPost(int Nr)
        {
            Hotel = await _hotelService.GetHotelFromIdAsync(Nr);
            HotelNr = Nr;
            HotelName = Hotel.Name;
            HotelAddress = Hotel.Address;
            Console.WriteLine($"HotelNr : {Hotel.Hotel_No} \n HotelName : {Hotel.Name} \n HotelAddress : {Hotel.Address}");
            //Hotel hotel = new Hotel(HotelNr, HotelName, HotelAddress);
            await _hotelService.DeleteHotelAsync(HotelNr);

            return RedirectToPage("ShowHotels");
        }

    }
}
