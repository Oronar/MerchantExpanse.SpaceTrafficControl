using MerchantExpanse.SpaceTraders.Models;

namespace MerchantExpanse.SpaceTrafficControl.Api.Extensions
{
	public static class LocationDetailExtensions
	{
		public static string SystemSymbol(this LocationDetail location)
		{
			return location.Symbol.Substring(0, location.Symbol.IndexOf("-"));
		}
	}
}