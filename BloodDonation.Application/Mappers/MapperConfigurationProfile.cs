using System.Security.AccessControl;
using System.Transactions;
using AutoMapper;
using BloodDonation.Application.Models.BloodDonationRequests;
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
        #endregion

        #region BloodUnit
        //CreateMap<BloodUnitCreateModel, BloodUnit>().ReverseMap();
        //CreateMap<BloodUnitUpdateModel, BloodUnit>().ReverseMap();
        #endregion

        #region BloodDonationRequest
        CreateMap<BloodDonationRequest, BloodDonationRequestViewModel>().ReverseMap();
        #endregion
    }
}
