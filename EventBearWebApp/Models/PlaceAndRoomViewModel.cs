using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventBearWebApp.Models
{
    public class PlaceAndRoomViewModel
    {
        public PlaceModel PlaceModel { get; set; }
        public RoomAndZoneModel RoomAndZoneModel { get; set; }
        public List<RoomAndZoneModel> ListRoomAndZoneModel { get; set; }
    }
}