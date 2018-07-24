using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AirportTests.Fakes
{
    class FakeUnitOfWork : IUnitOfWork
    {
        public async Task DropDB()
        {
            await Task.Run(() => DoNothing());
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (typeof(T) == typeof(Flight))
            {
                var fake = A.Fake<IRepository<Flight>>();
                A.CallTo(() => fake.Create(A<Flight>._)).Invokes(() => DoNothing());
                A.CallTo(() => fake.Update(A<int>._, A<Flight>._)).Invokes(() => DoNothing());
                return (IRepository<T>)A.Fake<IRepository<Flight>>();
            }
            else if (typeof(T) == typeof(Crew))
            {
                var fake = A.Fake<IRepository<Crew>>();
                A.CallTo(() => fake.Create(A<Crew>._)).Invokes(() => DoNothing());
                A.CallTo(() => fake.Update(A<int>._, A<Crew>._)).Invokes(() => DoNothing());
                return (IRepository<T>)A.Fake<IRepository<Crew>>();
            }
            else if (typeof(T) == typeof(Departure))
            {
                var fake = A.Fake<IRepository<Departure>>();
                A.CallTo(() => fake.Create(A<Departure>._)).Invokes(() => DoNothing());
                A.CallTo(() => fake.Update(A<int>._, A<Departure>._)).Invokes(() => DoNothing());
                return (IRepository<T>)A.Fake<IRepository<Departure>>();
            }
            else if (typeof(T) == typeof(Pilot))
            {
                var fake = A.Fake<IRepository<Pilot>>();
                A.CallTo(() => fake.Create(A<Pilot>._)).Invokes(() => DoNothing());
                A.CallTo(() => fake.Update(A<int>._, A<Pilot>._)).Invokes(() => DoNothing());
                return (IRepository<T>)A.Fake<IRepository<Pilot>>();
            }
            else if (typeof(T) == typeof(Plane))
            {
                var fake = A.Fake<IRepository<Plane>>();
                A.CallTo(() => fake.Create(A<Plane>._)).Invokes(() => DoNothing());
                A.CallTo(() => fake.Update(A<int>._, A<Plane>._)).Invokes(() => DoNothing());
                return (IRepository<T>)A.Fake<IRepository<Plane>>();
            }
            else if (typeof(T) == typeof(PlaneType))
            {
                var fake = A.Fake<IRepository<PlaneType>>();
                A.CallTo(() => fake.Create(A<PlaneType>._)).Invokes(() => DoNothing());
                A.CallTo(() => fake.Update(A<int>._, A<PlaneType>._)).Invokes(() => DoNothing());
                return (IRepository<T>)A.Fake<IRepository<PlaneType>>();
            }
            else if (typeof(T) == typeof(Stewardess))
            {
                var fake = A.Fake<IRepository<Stewardess>>();
                A.CallTo(() => fake.Create(A<Stewardess>._)).Invokes(() => DoNothing());
                A.CallTo(() => fake.Update(A<int>._, A<Stewardess>._)).Invokes(() => DoNothing());
                return (IRepository<T>)A.Fake<IRepository<Stewardess>>();
            }
            else if (typeof(T) == typeof(Ticket))
            {
                var fake = A.Fake<IRepository<Ticket>>();
                A.CallTo(() => fake.Create(A<Ticket>._)).Invokes(() => DoNothing());
                A.CallTo(() => fake.Update(A<int>._, A<Ticket>._)).Invokes(() => DoNothing());
                return (IRepository<T>)A.Fake<IRepository<Ticket>>();
            }
            else
            {
                throw new TypeAccessException("Wrong type of repo");
            }
        }


        public async Task SaveChanges()
        {
            await Task.Run(() => DoNothing());
        }

        public async Task SeedDB()
        {
           await Task.Run(() => DoNothing());
        }

        public void DoNothing()
        {

        }
    }
}
