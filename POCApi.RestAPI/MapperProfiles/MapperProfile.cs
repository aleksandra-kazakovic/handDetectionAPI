using AutoMapper;
using POCApi.Core.Entities;
using POCApi.RestAPI.Requests;

namespace POCApi.RestAPI.MapperProfiles
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<CompartmentPick, ExamineCompartmentPickingRequest>().ReverseMap();
        }
    }
}
