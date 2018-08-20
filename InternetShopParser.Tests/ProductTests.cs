using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using InternetShopParser.Model.Database.Services;
using InternetShopParser.Model.Services;
using Xunit;

namespace InternetShopParser.Tests
{
    public class ProductTests : BaseTest
    {
        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -10)]
        [InlineData(-1, -1)]
        [InlineData(0, 0)]
        public void ShouldResultBeEmptyProductsGetList(int skip, int take)
        {
            InMemoryTest(context =>
            {
                AddProducts(context, 3);
                IProductService productService = new ProductService(context, _htmlParserService, _dateTimeProvider, _mapper);
                var result = productService.GetList(skip, take);
                result.IsSuccess.Should().BeTrue();
                result.Result.TotalCount.Should().Be(3);
                result.Result.Products.Should().BeEmpty();
            });
        }

        [Theory]
        [InlineData(-10, 10)]
        [InlineData(0, 1)]
        public void ShouldResultNotBeEmptyProductsGetList(int skip, int take)
        {
            InMemoryTest(context =>
            {
                AddProducts(context, 1);
                var product = context.Products.First();

                IProductService productService = new ProductService(context, _htmlParserService, _dateTimeProvider, _mapper);
                var result = productService.GetList(skip, take);
                result.IsSuccess.Should().BeTrue();
                result.Result.TotalCount.Should().Be(1);
                result.Result.Products.Should().NotBeEmpty();

                result.Result.Products.First().Id.Should().Be(product.Id);
                result.Result.Products.First().Name.Should().Be(product.Name);
                result.Result.Products.First().Price.Should().Be(product.Price);
                result.Result.Products.First().Currency.Should().Be(product.Currency);
            });
        }

        [Theory]
        [InlineData(-2)]
        [InlineData(-1)]
        [InlineData(0)]
        public void ShouldFailedProductGetInfoWhenIdIsNotCorrect(int id)
        {
            InMemoryTest(context =>
            {
                AddProducts(context, 1);
                IProductService productService = new ProductService(context, _htmlParserService, _dateTimeProvider, _mapper);
                var result = productService.GetInfo(id);
                result.IsSuccess.Should().BeFalse();
                result.Result.Should().BeNull();
            });
        }

        [Fact]
        public void ShouldSuccessProductGetInfo()
        {
            InMemoryTest(context =>
            {
                AddProducts(context, 1);
                var product = context.Products.First();
                IProductService productService = new ProductService(context, _htmlParserService, _dateTimeProvider, _mapper);
                var result = productService.GetInfo(product.Id);
                result.IsSuccess.Should().BeTrue();
                result.Result.Should().NotBeNull();

                result.Result.Id.Should().Be(product.Id);
                result.Result.Name.Should().Be(product.Name);
                result.Result.Price.Should().Be(product.Price);
                result.Result.Currency.Should().Be(product.Currency);
                result.Result.Descriptions.Should().Be(product.Descriptions);
                result.Result.ImageSource.Should().Be(product.ImageSource);
            });
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -10)]
        [InlineData(-1, -1)]
        [InlineData(0, 0)]
        public void ShouldResultBeEmptyProductUpdatePrisesGetList(int skip, int take)
        {
            InMemoryTest(context =>
            {
                AddProducts(context, 1);
                var product = context.Products.First();
                IProductService productService = new ProductService(context, _htmlParserService, _dateTimeProvider, _mapper);
                var result = productService.GetUpdatePricesList(product.Id, skip, take);
                result.IsSuccess.Should().BeTrue();
                result.Result.TotalCount.Should().Be(1);
                result.Result.ProductUpdetePrices.Should().BeEmpty();
            });
        }

        [Theory]
        [InlineData(-2)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(2)]
        public void ShouldResultBeEmptyProductUpdatePrisesGetListWhenIdIsNotCorrect(int id)
        {
            InMemoryTest(context =>
            {
                AddProducts(context, 1);
                var product = context.Products.First();
                IProductService productService = new ProductService(context, _htmlParserService, _dateTimeProvider, _mapper);
                var result = productService.GetUpdatePricesList(id, 0, 10);
                result.IsSuccess.Should().BeTrue();
                result.Result.ProductUpdetePrices.Should().BeEmpty();
            });
        }

        [Fact]
        public void ShouldSuccessProductUpdatePrisesGetListWhenParametrIsCorrect()
        {
            InMemoryTest(context =>
            {
                AddProducts(context, 1);
                var productUpdatePrice = context.ProductUpdetePrices.First();
                IProductService productService = new ProductService(context, _htmlParserService, _dateTimeProvider, _mapper);
                var result = productService.GetUpdatePricesList(productUpdatePrice.Id, 0, 10);
                result.IsSuccess.Should().BeTrue();
                result.Result.TotalCount.Should().Be(1);
                result.Result.ProductUpdetePrices.Should().NotBeEmpty();

                result.Result.ProductUpdetePrices.First().Id.Should().Be(productUpdatePrice.Id);
                result.Result.ProductUpdetePrices.First().PriceUpdate.Should().Be(productUpdatePrice.PriceUpdate);
                result.Result.ProductUpdetePrices.First().DateUpdate.Should().Be(productUpdatePrice.DateUpdate);
                result.Result.ProductUpdetePrices.First().DateUpdateStr.Should().Be(productUpdatePrice.DateUpdate.ToString("dd.MM.yy HH:mm"));
            });
        }
    }
}
