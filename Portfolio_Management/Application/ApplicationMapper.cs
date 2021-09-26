using AutoMapper;
using Portfolio_Management.Dto;
using Portfolio_Management.Entities;
using Portfolio_Management.ViewModel;

namespace Portfolio_Management.Application
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Stock, StockDto>().ReverseMap();
            CreateMap<StockTransaction, StockTransactionDto>().ReverseMap();
            CreateMap<StockTransaction, StockTransactionVm>().ReverseMap();
            CreateMap<Stock, StockVm>().ReverseMap();
            CreateMap<StockDto, StockVm>().ReverseMap();
        }
    }
}