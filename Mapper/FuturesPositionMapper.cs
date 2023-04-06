using AutoMapper;
using CryptoFutures.API.Models;

namespace CryptoFutures.API.Mapper;

public class FuturesPositionMapper : Profile
{
    public FuturesPositionMapper()
    {
        CreateMap<Entities.FuturesPosition, FuturesPositionResponseDto>();
        CreateMap<FuturesPositionRequestDto, Entities.FuturesPosition>();
    }
}