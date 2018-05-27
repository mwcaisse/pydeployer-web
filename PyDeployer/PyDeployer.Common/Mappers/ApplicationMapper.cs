using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PyDeployer.Common.Entities;
using PyDeployer.Common.ViewModels;

namespace PyDeployer.Common.Mappers
{
    public static class ApplicationMapper
    {

        public static ApplicationViewModel ToViewModel(this Application application)
        {
            var vm = new ApplicationViewModel()
            {
                ApplicationId = application.ApplicationId,
                ApplicationUuid = application.ApplicationUuid,
                Name = application.Name
            };

            return vm;
        }

        public static IEnumerable<ApplicationViewModel> ToViewModel(this IEnumerable<Application> applications)
        {
            return applications.Select(a => a.ToViewModel());
        }

    }
}
