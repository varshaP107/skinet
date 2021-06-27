using AutoMapper;
using API.Dtos;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string> //<source, destination, destination Property>
    {
        private readonly IConfiguration _config;
        public ProductUrlResolver(IConfiguration config) //while selecting from quick fix. be carefull here becoz AutoMapper and MicrosoftExtentions both have configurations, we need MicrosoftExtensions Config. here
        {
            _config = config;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl; //property accessor []
            }
            return null;
        }
    }
}