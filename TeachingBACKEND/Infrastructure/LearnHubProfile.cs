using AutoMapper;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Infrastructure
{
    public class LearnHubProfile : Profile
    {
        public LearnHubProfile() 
        {
            CreateMap<LearnHub, PaginationLearnHubDTO>();
        }
    }
}
