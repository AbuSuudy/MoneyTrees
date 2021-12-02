using MoneyTrees.DAL;
using MoneyTrees.Interfaces;
using MoneyTrees.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Globalization;

namespace MoneyTrees.Services
{

    public class MonzoMapper : IMonzoMapper
    {

        public IMapper Mapper()
        {

            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<Token, AccessTokenModel>()
                  .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => src.Token1));

                cfg.CreateMap<AccessTokenModel, Token>()
                  .ForMember(dest => dest.Token1, opt => opt.MapFrom(src => src.AccessToken));


              
                cfg.CreateMap<TransactionModel, Transaction>()
                  .ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.Merchant.Name))
                  .ForMember(dto => dto.Logo, opt => opt.MapFrom(src => src.Merchant.Logo))
                  .ForMember(dto => dto.Longitude, opt => opt.MapFrom(src => src.Merchant.Address.Longitude))
                  .ForMember(dto => dto.Latitude, opt => opt.MapFrom(src => src.Merchant.Address.Latitude))
                  .ForMember(dto => dto.Amount, opt => opt.MapFrom(src => src.Amount))
                  .ForMember(dest => dest.Id, opt => opt.Ignore());


                cfg.CreateMap<Transaction, TransactionModel>()
                  .ForPath(dto => dto.Merchant.Name, opt => opt.MapFrom(src => src.Name))
                  .ForPath(dto => dto.Merchant.Logo, opt => opt.MapFrom(src => src.Logo))
                  .ForPath(dto => dto.LocalAmount, opt => opt.MapFrom(src => src.Amount))
                  .ForPath(dto => dto.Merchant.Emoji, opt => opt.MapFrom(src => src.Emoji))
                  .ForPath(dto => dto.Merchant.Address.Longitude, opt => opt.MapFrom(src => src.Longitude))
                  .ForPath(dto => dto.Merchant.Address.Latitude, opt => opt.MapFrom(src => src.Latitude))
                  .ForMember(dest => dest.Id, opt => opt.Ignore());



                cfg.CreateMap<TransactionModel, TrasactionViewModel>()
                                      
                    .ForMember(dto => dto.Emoji, opt => opt.MapFrom(src => src.Merchant.Emoji))
                    .ForMember(dto => dto.CounterPartName, opt => opt.MapFrom(src => src.Counterparty.Name))
                    .ForMember(dto => dto.Emoji, opt => opt.NullSubstitute("🤔❓"))
                    .ForMember(dto => dto.Logo, opt => opt.MapFrom(src => src.Merchant.Logo))
                    .ForMember(dto => dto.LocalAmount, opt => opt.ConvertUsing(new CurrencyFormatter()))                   
                    .ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.Merchant.Name));




        });

            return configuration.CreateMapper();
        }
    }
}

public class CurrencyFormatter : IValueConverter<double, string>
{
    public string Convert(double source, ResolutionContext context)
    {
        double result = source;

        return result.ToString("C2", CultureInfo.CurrentCulture);
    }
        
}

