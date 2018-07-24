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


        public Task<IEnumerable<Flight>> GetFlightWithTimer()
        {
            var timer = new Timer(5000);
            var tcs = new TaskCompletionSource<IEnumerable<Flight>>();
            timer.Elapsed += async (e, o) =>
            {
                var res = await GetAll<Flight>();
                tcs.SetResult(res);
            };

            timer.Start();

            return tcs.Task;
        }

        public async Task<string> DownloadTenCrews()
        {
            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync("http://5b128555d50a5c0014ef1204.mockapi.io/crew");
            var crewsJson = GetCrewsFromJson(json, 10);
            List<Crew> crews = ParseCrewsJsonToCrews(crewsJson.ToList()).ToList();

            var task1 = Task.Run(() => AddCrews(crews));
            var task2 = Task.Run(() => WriteCrewsToFile(crews,
                string.Format(@"../Data Access Layer/Files/Crews" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".csv")));



            await Task.WhenAll(task1, task2);
            return "Tasks completed in " + DateTime.Now;

        }

        private async Task AddCrews(IEnumerable<Crew> crews)
        {
            foreach (var crew in crews)
            {
                await Post(crew);
            }
            await SaveChanges();
        }

        private async Task WriteCrewsToFile(IEnumerable<Crew> crews, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
            {
                foreach (var crew in crews)
                {
                    await sw.WriteLineAsync("Crew: ");
                    await sw.WriteLineAsync(string.Format("pilot: {0}, {1}, exp: {2}", crew.Pilot.Name, crew.Pilot.Surname, crew.Pilot.Experience));
                    foreach (var stewardess in crew.Stewardesses)
                    {
                        await sw.WriteLineAsync(string.Format("stewardess: {0}, {1}, dateOfBirth: {2}", stewardess.Name, stewardess.Surname, stewardess.DateOfBirth));
                    }
                }
            }

        }

        private IEnumerable<CrewJson> GetCrewsFromJson(string json, int numberOfItems)
        {
            List<CrewJson> crewsJson = JsonConvert.DeserializeObject<List<CrewJson>>(json);

            return crewsJson.Take(numberOfItems);
        }

        private IEnumerable<Crew> ParseCrewsJsonToCrews(List<CrewJson> crewsJson)
        {
            List<Crew> crews = new List<Crew>();
            foreach (var crewJson in crewsJson)
            {
                List<StewardessJson> stewardessJsons = crewJson.Stewardess;
                List<Stewardess> stewardesses = new List<Stewardess>();
                foreach (var stewardessJson in stewardessJsons)
                {
                    stewardesses.Add(new Stewardess()
                    {
                        DateOfBirth = stewardessJson.BirthDate,
                        Surname = stewardessJson.LastName,
                        Name = stewardessJson.FirstName
                    });
                }
                crews.Add(new Crew()
                {
                    Pilot = new Pilot()
                    {
                        Name = crewJson.Pilot[0].FirstName,
                        Surname = crewJson.Pilot[0].LastName,
                        Experience = crewJson.Pilot[0].Exp
                    },
                    Stewardesses = stewardesses
                });
            }
            return crews;
        }
    }
        
}

