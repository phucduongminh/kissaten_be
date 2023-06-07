using ItssProject.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ItssProject.Interfaces;
using ItssProject.Models;
using System;

namespace ItssProject.Services
{
    public class GetDataService : IGetDataService
    {
        private readonly ApplicationContext _applicationContext;
        //private readonly IConfiguration _configuration;
        public GetDataService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            //_configuration = configuration;
        }
        public Boolean CheckUserInformation(string userName, string password)
        {
            var user = (from User in _applicationContext.Users.AsNoTracking()
                        where (User.Name == userName && User.Password == password)
                        select User).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            return true;
        }
        public List<CoffeeShop> GetCoffeeShopByRequest(string Description, string Name, double Rank, string Address, bool Service)
        {
            try
            {
                var listCoffeeShop = new List<CoffeeShop>();
                listCoffeeShop = (from shop in _applicationContext.CoffeeShops.AsNoTracking()
                                  where (shop.Service == Service && shop.Description == Description && shop.Name == Name && shop.AverageRating == Rank && shop.Address == Address)
                                  select new CoffeeShop
                                  {
                                      Id = shop.Id,
                                      Name = shop.Name,
                                      Address = shop.Address,
                                      Gmail = shop.Gmail,
                                      ContactNumber = shop.ContactNumber,
                                      ImageCover = shop.ImageCover,
                                      AverageRating = shop.AverageRating,
                                      OpenHour = shop.OpenHour,
                                      CloseHour = shop.CloseHour,
                                      Description = shop.Description,
                                      Status = shop.Status,
                                      PostedByUser = shop.PostedByUser,
                                      Approved = shop.Approved
                                  }
                                  ).ToList();
                return listCoffeeShop;
            }
            catch
            {
                throw new Exception();
            }
        }
        public CoffeeShop GetCoffeeShopById(int coffeeId)
        {
            try
            {
                var coffeeShop = (from shop in _applicationContext.CoffeeShops.AsNoTracking()
                                  where (shop.Id == coffeeId)
                                  select new CoffeeShop
                                  {
                                      Id = shop.Id,
                                      Name = shop.Name,
                                      Address = shop.Address,
                                      Gmail = shop.Gmail,
                                      ContactNumber = shop.ContactNumber,
                                      ImageCover = shop.ImageCover,
                                      AverageRating = shop.AverageRating,
                                      OpenHour = shop.OpenHour,
                                      CloseHour = shop.CloseHour,
                                      Description = shop.Description,
                                      Status = shop.Status,
                                      PostedByUser = shop.PostedByUser,
                                      Approved = shop.Approved
                                  }
                                     ).FirstOrDefault();
                return coffeeShop;
            }
            catch
            {
                throw new Exception();
            }
        }
        public void AddInformationOfCoffeeShop(CoffeeShop Model)
        {
            try
            {
                _applicationContext.Add(Model);
                _applicationContext.SaveChanges();
            }
            catch
            {
                throw new Exception();
            }
        }
        private List<CoffeeShop> GetCoffeeShop()
        {
            try
            {
                //var listCoffeeShop = _dBRepository.Query(_applicationContext => _applicationContext.Set<CoffeeShop>()
                //.Select(i => new CoffeeShop
                //{
                //    Id = i.Id,
                //    Name = i.Name,
                //    Address = i.Address,
                //    Gmail = i.Gmail,
                //    ContactNumber = i.ContactNumber,
                //    ImageCover = i.ImageCover,
                //    AverageRating = i.AverageRating,
                //    OpenHour = i.OpenHour,
                //    CloseHour = i.CloseHour,
                //    Description = i.Description,
                //    Status = i.Status,
                //    PostedByUser = i.PostedByUser,
                //    Approved = i.Approved
                //})).ToList();
                var listCoffeeShops = (from shop in _applicationContext.CoffeeShops.AsNoTracking()
                                       select new CoffeeShop
                                       {
                                           Id = shop.Id,
                                           Name = shop.Name,
                                           Address = shop.Address,
                                           Gmail = shop.Gmail,
                                           ContactNumber = shop.ContactNumber,
                                           ImageCover = shop.ImageCover,
                                           AverageRating = shop.AverageRating,
                                           OpenHour = shop.OpenHour,
                                           CloseHour = shop.CloseHour,
                                           Description = shop.Description,
                                           Status = shop.Status,
                                           PostedByUser = shop.PostedByUser,
                                           Approved = shop.Approved
                                       }
                                     ).ToList();


                return listCoffeeShops;
            }
            catch
            {
                throw new Exception();
            }
        }
        public List<CoffeeShop> GetCoffeeShopSort(string pullDown)
        {
            try
            {
                var listCoffeeShops = new List<CoffeeShop>();
                if (pullDown == null)
                {
                    return GetCoffeeShop();
                }
                if (pullDown == "Number of comment (count)")
                {
                    var listCoffeeIdWithManyComments = GetIdCoffeeShopHadMostComment();
                    foreach (int i in listCoffeeIdWithManyComments)
                    {
                        var item = GetCoffeeShopById(i);
                        listCoffeeShops.Add(item);
                    }
                    return listCoffeeShops;
                }
                if (pullDown == "Rating")
                {
                    listCoffeeShops = GetCoffeeShop();
                    IQueryable<CoffeeShop> queryableCoffeeShops = listCoffeeShops.AsQueryable();
                    queryableCoffeeShops.OrderByDescending(i => i.AverageRating);
                    return listCoffeeShops;
                }
                if (pullDown == "Address")
                {
                    listCoffeeShops = GetCoffeeShop();
                    IQueryable<CoffeeShop> queryableCoffeeShops = listCoffeeShops.AsQueryable();
                    queryableCoffeeShops.OrderByDescending(i => i.Address);
                    return listCoffeeShops;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw new Exception();
            }
        }
        private List<int> GetIdCoffeeShopHadMostComment()
        {
            try
            {
                var listReview = (from item in _applicationContext.Reviews.AsNoTracking()
                                  select new
                                  {
                                      ReviewId = item.Id,
                                      CoffeeShopId = item.CoffeeId
                                  }).ToList();
                var listCoffeeIdWithManyComments = listReview
            .GroupBy(c => c.CoffeeShopId)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .ToList();
                return listCoffeeIdWithManyComments;
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}



