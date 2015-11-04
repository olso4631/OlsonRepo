using CarDealership.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Repositories
{
    public interface ICarRepository
    {
        List<Car> GetAllCars();
        Car GetCarById(int id);
        void AddCar(Car car);
        void EditCar(Car car);
        void DeleteCar(int carId);
        Car GetCarByModel(string name);
        User LoginUser(string username, string password);
        Car GetCarByMMY(string year, string make, string model);
    }
}
