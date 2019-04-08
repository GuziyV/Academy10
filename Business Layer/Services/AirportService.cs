using Business_Layer.Interfaces;
using Data_Access_Layer;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models;
using Newtonsoft.Json;
using Shared.JsonObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;

namespace Business_Layer.Services
{
    public class AirportService : IService
    {

        private readonly IUnitOfWork _unitOfWork;
        public AirportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        #region General

        public async Task Seed()
        {
            await _unitOfWork.SeedDB();
        }

        public async Task Drop()
        {
            await _unitOfWork.DropDB();
        }

        public async Task<T> GetById<T>(int id) where T : class
        {
            return await _unitOfWork.GetRepository<T>().Get(id);
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : class
        {
            return await _unitOfWork.GetRepository<T>().GetAll();
        }

        public async Task Post<T>(T item) where T : class
        {
            await _unitOfWork.GetRepository<T>().Create(item);
        }

        public async Task Update<T>(int id, T item) where T : class
        {
            await _unitOfWork.GetRepository<T>().Update(id, item);
        }

        public async Task Delete<T>(int number) where T : class
        {
            await _unitOfWork.GetRepository<T>().Delete(number);
        }
        #endregion

        public async Task SaveChanges()
        {
            await _unitOfWork.SaveChanges();
        }
    } 
}

