using MerchantExpanse.SpaceTraders.Models;
using MerchantExpanse.SpaceTrafficControl.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MerchantExpanse.SpaceTrafficControl.Api.Extensions
{
	public static class PublicFlightPlansExtensions
	{
		public static IEnumerable<UserShipCount> TopUsers(this IEnumerable<PublicFlightPlan> flightPlans)
		{
			return flightPlans.GroupBy(flightPlan => flightPlan.Username)
				.Select(flightPlan => new UserShipCount()
				{
					Username = flightPlan.First().Username,
					Count = flightPlan.Count()
				})
				.OrderByDescending(x => x.Count);
		}

		public static IEnumerable<ShipTypeCount> TopShipTypes(this IEnumerable<PublicFlightPlan> flightPlans)
		{
			return flightPlans.GroupBy(flightPlan => flightPlan.ShipType)
				.Select(flightPlan => new ShipTypeCount()
				{
					ShipType = flightPlan.First().ShipType,
					Count = flightPlan.Count()
				})
				.OrderByDescending(x => x.Count);
		}
	}
}