using ItssProject.Models;
using System.Net;
using System.Xml.Linq;

namespace ItssProject.Interfaces
{
    public interface IGetDataService
    {
        Boolean CheckUserInformation(string userName, string password);
        CoffeeShop GetCoffeeShopById(int IdShop);
        List<CoffeeShop> GetCoffeeShopByRequest(string Description, string Name, double Rank, string Address, bool Service);
        void AddInformationOfCoffeeShop(CoffeeShop Model);
        List<CoffeeShop> GetCoffeeShopSort(string pullDown);
    }
}
