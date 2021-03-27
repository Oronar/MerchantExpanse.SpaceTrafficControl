using MerchantExpanse.SpaceTrafficControl.Api.Services.Contracts;
using MerchantExpanse.SpaceTrafficControl.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MerchantExpanse.SpaceTrafficControl.Api.Controllers
{
	[Route("space-traffic")]
	[ApiController]
	public class SpaceTrafficController : Controller
	{
		private readonly ISpaceTrafficService spaceTrafficService;

		public SpaceTrafficController(ISpaceTrafficService spaceTrafficService)
		{
			this.spaceTrafficService = spaceTrafficService;
		}

		[HttpGet("{symbol}")]
		public async Task<SpaceTrafficViewModel> GetSpaceTraffic(string symbol)
		{
			return await spaceTrafficService.GetSpaceTrafficAsync(symbol);
		}
	}
}