using MerchantExpanse.SpaceTraders.Contracts;
using MerchantExpanse.SpaceTrafficControl.Api.Extensions;
using MerchantExpanse.SpaceTrafficControl.Api.Services.Contracts;
using MerchantExpanse.SpaceTrafficControl.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MerchantExpanse.SpaceTrafficControl.Api.Services
{
	public class SpaceTrafficService : ISpaceTrafficService
	{
		private readonly IClient Client;
		private readonly IMemoryCache MemoryCache;

		public SpaceTrafficService(IClient client, IMemoryCache memoryCache)
		{
			Client = client;
			MemoryCache = memoryCache;
		}

		public async Task<SpaceTrafficViewModel> GetSpaceTrafficAsync(string locationSymbol)
		{
			var locationDetail = await MemoryCache.GetOrCreateAsync($"LocationDetail-{locationSymbol}", async cacheEntry =>
			{
				cacheEntry.SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
				return await Client.GetLocationAsync(locationSymbol);
			});

			var flightPlans = await MemoryCache.GetOrCreateAsync($"FlightPlans-{locationSymbol}", async cacheEntry =>
			{
				cacheEntry.SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
				return await Client.GetFlightPlansAsync(locationDetail.SystemSymbol());
			});

			var arrivals = flightPlans.Where(fp => fp.To.Equals(locationDetail.Symbol)).OrderBy(fp => fp.ArrivesAt);
			var departures = flightPlans.Where(fp => fp.From.Equals(locationDetail.Symbol)).OrderBy(fp => fp.ArrivesAt);

			var spaceTraffic = new SpaceTrafficViewModel()
			{
				Arrivals = arrivals,
				Departures = departures,
				Location = locationDetail,
				TopShips = arrivals.Union(departures).TopShipTypes(),
				TopUsers = arrivals.Union(departures).TopUsers()
			};

			return spaceTraffic;
		}
	}
}