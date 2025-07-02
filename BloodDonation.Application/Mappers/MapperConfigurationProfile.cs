using AutoMapper;
using BloodDonation.Application.Models.Blogs;
using BloodDonation.Application.Models.BloodChecks;
using BloodDonation.Application.Models.BloodComponents;
using BloodDonation.Application.Models.BloodDonationRequests;
using BloodDonation.Application.Models.BloodDonations;
using BloodDonation.Application.Models.BloodGroups;
using BloodDonation.Application.Models.BloodStorage;
using BloodDonation.Application.Models.EmergencyBloodRequests;
using BloodDonation.Application.Models.HealthCheckForms;
using BloodDonation.Application.Models.Roles;
using BloodDonation.Application.Models.Users;
using BloodDonation.Domain.Entities;

namespace BloodDonation.Application.Mappers;

public class MapperConfigurationProfile : Profile
{
    public MapperConfigurationProfile()
    {
        #region Customer
        //CreateMap<Admin, AdminCreateModel>().ReverseMap();
        //CreateMap<Customer, CustomerViewModel>().ReverseMap();
        //CreateMap<CustomerCreateModel, Customer>().ReverseMap();
        //CreateMap<CustomerUpdateModel, Customer>().ReverseMap();
        CreateMap<User, UserViewModel>()
            .ForMember(x => x.RoleName, cfg => cfg.MapFrom(x => x.Role.Name))
            .ForMember(x => x.BloodGroupType, cfg => cfg.MapFrom(x => $"{x.BloodGroup!.Type ?? string.Empty}{x.BloodGroup!.RhFactor ?? string.Empty}"))
            .ReverseMap();
        CreateMap<User, UserUpdateModel>().ReverseMap();


        CreateMap<User, UserCreateModel>().ReverseMap();
        #endregion

        #region BloodUnit
        //CreateMap<BloodUnitCreateModel, BloodUnit>().ReverseMap();
        //CreateMap<BloodUnitUpdateModel, BloodUnit>().ReverseMap();
        #endregion

        #region BloodDonationRequest
        CreateMap<BloodDonationRequest, BloodDonationRequestViewModel>()
            .ForMember(dest => dest.IdentityId, opt => opt.MapFrom(src => src.User.IdentityId))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src =>
                src.User.DateOfBirth.HasValue
                    ? DateTime.Now.Year - src.User.DateOfBirth.Value.Year -
                      (DateTime.Now.Date < src.User.DateOfBirth.Value.AddYears(DateTime.Now.Year - src.User.DateOfBirth.Value.Year) ? 1 : 0)
                    : 0))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
            .ForMember(dest => dest.PhoneNo, opt => opt.MapFrom(src => src.User.PhoneNo))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.Addresss, opt => opt.MapFrom(src => src.User.Addresss))
            .ForMember(dest => dest.FrontUrlIdentity, opt => opt.MapFrom(src => src.User.FrontUrlIdentity))
            .ForMember(dest => dest.BackUrlIdentity, opt => opt.MapFrom(src => src.User.BackUrlIdentity))
            .ForMember(dest => dest.HealthCheckForm, opt => opt.MapFrom(src => src.HealthCheckForm));

        CreateMap<BloodDonationRequestCreateModel, BloodDonationRequest>()
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ReverseMap();
        #endregion

        #region HealthCheckForm
        CreateMap<HealthCheckForm, HealthCheckFormViewModel>()
            //.ForMember(dest => dest.BloodDonationRequest, opt => opt.MapFrom(src => src.BloodDonationRequest))
            .ForMember(dest => dest.BloodDonateRequestId, opt => opt.MapFrom(src => src.BloodDonationRequest.Id));
        CreateMap<HealthCheckFormCreateModel, HealthCheckForm>();
        CreateMap<HealthCheckFormUpdateModel, HealthCheckForm>();
        #endregion

        #region BloodDonation
        CreateMap<BloodDonation.Domain.Entities.BloodDonation, BloodDonationViewModel>()
            .ForMember(dest => dest.BloodDonationRequest, opt => opt.MapFrom(src => src.BloodDonationRequest));
        #endregion
        #region BloodStorage
        CreateMap<BloodStorage, BloodStorageViewModel>()
            .ForMember(dest => dest.BloodDonate, opt => opt.MapFrom(src => src.BloodDonate))
            .ForMember(dest => dest.BloodComponent, opt => opt.MapFrom(src => src.BloodComponent))
            .ForMember(dest => dest.BloodGroup, opt => opt.MapFrom(src => src.BloodGroup!.Type + src.BloodGroup!.RhFactor));
        CreateMap<BloodStorage, BloodStorageAvailableViewModel>()
          .ForMember(dest => dest.BloodComponentName, opt => opt.MapFrom(src => src.BloodComponent.Name))
          .ForMember(dest => dest.BloodGroup, opt => opt.MapFrom(src => src.BloodGroup!.Type + src.BloodGroup!.RhFactor));

        #endregion
        #region BloodComponent
        CreateMap<BloodComponent, BloodComponentViewModel>();
        #endregion
        #region BloodGroup
        CreateMap<BloodGroup, BloodGroupViewModel>();

        #endregion

        #region BloodCheck
        CreateMap<BloodCheck, BloodCheckViewModel>()
            .ForMember(dest => dest.BloodGroup, opt => opt.MapFrom(src => src.BloodGroup))
            .ForMember(dest => dest.BloodDonation, opt => opt.MapFrom(src => src.BloodDonation));
        #endregion
        #region Role
        CreateMap<Role, RoleViewModel>();
        #endregion

        CreateMap<EmergencyBloodRequest, EmergencyBloodCreateModel>().ReverseMap();
        CreateMap<EmergencyBloodRequest, EmergencyBloodViewModel>().ReverseMap();
        CreateMap<EmergencyBloodRequest, EmergencyBloodUpdateModel>().ReverseMap();
        #region Blogs 
        CreateMap<Blog, BlogCreateModel>().ReverseMap();
        #endregion
    }
}
