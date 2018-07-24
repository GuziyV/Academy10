using Business_Layer.MyMapperConfiguration;
using Business_Layer.Services;
using Data_Access_Layer;
using Data_Access_Layer.Contexts;
using Data_Access_Layer.DbInitializer;
using Data_Access_Layer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ninject.Modules;
using Presentation_Layer.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTests.Modules
{
    class AirPortServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<AirportService>().ToSelf();
            Bind<IUnitOfWork>().To<AirportUnitOfWork>();

            string conStr = @"Server=.\SQLEXPRESS;Database=AirportDB;Trusted_Connection=True;";
            var opt = new DbContextOptionsBuilder<AirportContext>()
                .UseSqlServer(conStr, b => b.MigrationsAssembly("Presentation Layer")).Options;

            Bind<AirportContext>().ToSelf().WithConstructorArgument("options", opt);
            Bind<FlightsController>().ToSelf();
            Bind<CrewsController>().ToSelf();
            

        }
    }
}
