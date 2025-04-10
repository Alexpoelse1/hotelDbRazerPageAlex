using hotelDbRazerPage.Models;

namespace hotelDbRazerPage.Interfaces
{
        
    public interface IHotelServiceAsync
    {
        #region GetAllHotelAsync
        /// <summary>
        /// henter alle hoteller fra databasen
        /// </summary>
        /// <returns>Liste af hoteller</returns>
        #endregion
        public Task<List<Hotel>> GetAllHotelAsync();
        #region GetHotelsFromIdAsync
        // <summary>
        // Henter et specifik hotel fra database 
        // </summary>
        // <param name="hotelNr">Udpeger det hotel der ønskes fra databasen</param>
        // <returns>Det fundne hotel eller null hvis hotellet ikke findes</returns>
        #endregion
        public Task<Hotel> GetHotelFromIdAsync(int hotelNr);
        #region CreateHotelAsync
        // <summary>
        // Indsætter et nyt hotel i databasen
        // </summary>
        // <param name="hotel">hotellet der skal indsættes</param>
        // <returns>Sand hvis der er gået godt ellers falsk</returns>
        #endregion
        public Task<bool> CreateHotelAsync(Hotel hotel);
        #region UpdateHotelAsync
        /// <summary>
        /// Opdaterer en hotel i databasen
        /// </summary>
        // <param name="hotel">De nye værdier til hotellet</param>
        // <param name="hotelNr">Nummer på den hotel der skal opdateres</param>
        /// <returns>Sand hvis der er gået godt ellers falsk</returns>
        #endregion
        public Task<bool> UpdateHotelAsync(Hotel hotel, int hotelNr);
        #region DeleteHotelAsync
        /// <summary>
        /// Sletter et hotel fra databasen
        /// </summary>
        // <param name="hotelNr">Nummer på det hotel der skal slettes</param>
        /// <returns>Det hotel der er slettet fra databasen, returnere null hvis hotellet ikke findes</returns>
        #endregion
        public Task<Hotel> DeleteHotelAsync(int hotelNr);
        #region GetHotelsByNameAsync
        /// <summary>
        /// henter alle hoteller fra databasen
        /// </summary>
        // <param name="name">Angiver navn på hotel der hentes fra databasen</param>
        /// <returns></returns>
        #endregion
        public Task<List<Hotel>> GetHotelsByNameAsync(string name);

        public Task<List<Hotel>> Search(Search SearchI);
    }
}
