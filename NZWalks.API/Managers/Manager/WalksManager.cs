using NZWalks.API.Entities.RequestEntity;
using NZWalks.API.Entities.ResponseEntity;
using NZWalks.API.Managers.Interface;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;

namespace NZWalks.API.Managers.Manager
{
    public class WalksManager : IWalksManager
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly object walksInstance;

        public WalksManager(IWalkRepository walkRepository, IRegionRepository regionRepository)
        {
            _walkRepository = walkRepository;
            _regionRepository = regionRepository;
        }
        public async Task<CommonResponse> AddWalks(WalksRequestAddRequestEntity walksRequestAddRequestEntity)
        {
            CommonResponse commonResponse = new CommonResponse();
            // is name already exist
            var isNameUnique = await _walkRepository.IsNameExist(walksRequestAddRequestEntity.Name);
            if (isNameUnique)
            {
                commonResponse.status_code = 422;
                commonResponse.message = "Name already exist";
                return commonResponse;
            }
            if (walksInstance == null)                                                                                                                                                                                                                                                                                                                                                                                                        
            {
                commonResponse.status_code = 422;
                commonResponse.message = "Name already exist";
                return commonResponse;
            }

            //var allRegions = await _regionRepository.GetAllAsync();

            commonResponse.status_code = 200;
            commonResponse.message = "Walks Created Successfully";
            return commonResponse;
            // return common response
        }
    }
}
