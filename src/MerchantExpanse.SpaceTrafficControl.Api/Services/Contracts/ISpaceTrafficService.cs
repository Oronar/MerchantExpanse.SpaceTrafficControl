using MerchantExpanse.SpaceTrafficControl.ViewModels;
using System.Threading.Tasks;

namespace MerchantExpanse.SpaceTrafficControl.Api.Services.Contracts
{
	public interface ISpaceTrafficService
	{
		Task<SpaceTrafficViewModel> GetSpaceTrafficAsync(string locationSymbol);
	}
}