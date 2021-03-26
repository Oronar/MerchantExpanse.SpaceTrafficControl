﻿using MerchantExpanse.SpaceTraders.Contracts;
using MerchantExpanse.SpaceTraders.Models;
using MerchantExpanse.SpaceTrafficControl.Api.Extensions;
using MerchantExpanse.SpaceTrafficControl.Api.Services.Contracts;
using MerchantExpanse.SpaceTrafficControl.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerchantExpanse.SpaceTrafficControl.Api.Services
{
	public class SpaceTrafficService : ISpaceTrafficService
	{
		private readonly IClient Client;

		public SpaceTrafficService(IClient client)
		{
			Client = client;
		}

		public async Task<SpaceTrafficViewModel> GetSpaceTrafficAsync(string locationSymbol)
		{
			var locationDetail = await Client.GetLocationAsync(locationSymbol);
			var flightPlans = await Client.GetFlightPlansAsync(locationDetail.SystemSymbol());
			var arrivals = flightPlans.Where(fp => fp.To.Equals(locationDetail.Symbol)).OrderBy(fp => fp.ArrivesAt);
			var departures = flightPlans.Where(fp => fp.From.Equals(locationDetail.Symbol)).OrderBy(fp => fp.ArrivesAt);

			var spaceTraffic = new SpaceTrafficViewModel()
			{
				Arrivals = arrivals,
				Departures = departures,
				Location = locationDetail,
				TopShips = CalculateTopShipTypes(arrivals.Union(departures)),
				TopUsers = CalculateTopUsers(arrivals.Union(departures))
			};

			return spaceTraffic;
		}

		private static IEnumerable<UserShipCount> CalculateTopUsers(IEnumerable<PublicFlightPlan> flightPlans)
		{
			return flightPlans.GroupBy(flightPlan => flightPlan.Username)
				.Select(flightPlan => new UserShipCount()
				{
					Username = flightPlan.First().Username,
					ShipCount = flightPlan.Count()
				})
				.OrderByDescending(x => x.ShipCount);
		}

		private static IEnumerable<ShipTypeCount> CalculateTopShipTypes(IEnumerable<PublicFlightPlan> flightPlans)
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