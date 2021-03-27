using MerchantExpanse.SpaceTraders.Models;
using System.Collections.Generic;

namespace MerchantExpanse.SpaceTrafficControl.ViewModels
{
	public class SpaceTrafficViewModel
	{
		public IEnumerable<PublicFlightPlan> Arrivals { get; set; }

		public IEnumerable<PublicFlightPlan> Departures { get; set; }

		public LocationDetail Location { get; set; }

		public IEnumerable<UserShipCount> TopUsers { get; set; }

		public IEnumerable<ShipTypeCount> TopShips { get; set; }
	}

	public class UserShipCount
	{
		public string Username { get; set; }

		public int ShipCount { get; set; }
	}

	public class ShipTypeCount
	{
		public string ShipType { get; set; }

		public int Count { get; set; }
	}
}