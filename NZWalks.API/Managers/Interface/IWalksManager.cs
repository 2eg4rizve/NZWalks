
using NZWalks.API.Entities.RequestEntity;
using NZWalks.API.Entities.ResponseEntity;

namespace NZWalks.API.Managers.Interface
{
    public interface IWalksManager
    {
        Task<CommonResponse> AddWalks(WalksRequestAddRequestEntity walksRequestAddRequestEntity);
    }
}
