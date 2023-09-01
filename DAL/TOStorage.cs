using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices.ComTypes;
//using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using db_TO_web.Models.TO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace db_TO_web.DAL
{
    internal class AutoParkDBStorage //помог интернал
    {
        private readonly TOContext _context;
        public AutoParkDBStorage(TOContext context)
        {
            _context = context;
        }
        public void DeleteCar(int id)
        {
            _context.CarListDb.Remove(_context.CarListDb.Find(id));
            _context.SaveChanges();
        }
        public void UpdateCar(int id, CarList car)
        {
            var elem = _context.CarListDb.Find(id);
            elem.AutoType = car.AutoType;
            elem.CurrentTraveled = car.CurrentTraveled;
            elem.DateOfDeregistration = car.DateOfDeregistration;
            elem.DateOfRegistration = car.DateOfRegistration;
            elem.LastToDate = car.LastToDate;
            elem.ModelId = car.ModelId;
            elem.VIN = car.VIN;
            _context.SaveChanges();
        }
        public IEnumerable<SelectListItem> GetAllStates()
        {
            IEnumerable<SelectListItem> list = _context.ModelDb.Select(s => new SelectListItem
            {
                Selected = false,
                Text = s.ModelName,
                Value = s.ModelId.ToString()
            });

            return list;
        }
        public SelectList GetModelsId()
        {
            List<int> ListId = new List<int>();

            foreach (var elem in _context.ModelDb)
                ListId.Add(elem.ModelId);

            var list = new SelectList(ListId);
            return list;
        }
        public IQueryable<CarList> GetAllCars(int key)
        {
            if (key == -1)
                return GetAllCars();
            else
                return _context.CarListDb.Where(p => p.ModelId == key);
        }

        private IQueryable<CarList> GetAllCars()
        {
            return _context.CarListDb;
        }

        public CarList GetCarWithId(int carId)
        {
            return _context.CarListDb.Find(carId);
        }
        public string GetModelNameForCar(CarList car)
        {
            return _context.ModelDb.Find(car.ModelId).ModelName;
        }
        public string GetMarkNameForCar(CarList car)
        {
            return _context.MarkDb.Find(_context.ModelDb.Find(car.ModelId).MarkId).MarkName;
        }
        public void AddCar(CarList car)
        {
            _context.CarListDb.Add(car);
            _context.SaveChanges();
        }
        public IQueryable<Model> GetAllModels()
        {
            return _context.ModelDb;
        }

        public string GetMarkNameForMarkId(int markId)
        {
            return _context.MarkDb.Where(c => c.MarkId == markId).FirstOrDefault().MarkName;
        }

        public  Model GetModelWithId(int modelId)
        {
            return _context.ModelDb.Find(modelId);
        }

        public DateTime? GetLastToDateForCarWithId(int carId)
        {
            return _context.CarListDb.Find(carId).LastToDate;
        }
        public void SetNewLastToDateForCarWithId(int carId, DateTime newDate)
        {
            var car = _context.CarListDb.Where(c => c.CarListId == carId).FirstOrDefault();
            car.LastToDate = newDate;
            _context.SaveChanges();
        }

        public int GetTraveledForCarWithId(int carId)
        {
            return _context.CarListDb.Find(carId).CurrentTraveled;
        }

        public void SetNewTraveledForCarWithId(int carId, int newTraveled)
        {
            var car = _context.CarListDb.Where(c => c.CarListId == carId).FirstOrDefault();
            car.CurrentTraveled = newTraveled;
            _context.SaveChanges();
        }

        public bool ModelIdIsNotExsist(int modelId)
        {
            return _context.ModelDb.Find(modelId) == null;
        }

        public bool CarIdIsNotExsist(int carId)
        {
            return _context.CarListDb.Find(carId) == null;
        }
    }
}
