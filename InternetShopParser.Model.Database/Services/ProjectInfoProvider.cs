using System;
using InternetShopParser.Model.Database.Options;
using InternetShopParser.Model.Services;
using Microsoft.Extensions.Options;

namespace InternetShopParser.Model.Database.Services
{
    public class ProjectInfoProvider : IProjectInfoProvider
    {
        private readonly ProjectOption _projectOptions;

        public ProjectInfoProvider(IOptions<ProjectOption> projectOptions)
        {
            _projectOptions = projectOptions.Value;
        }

        public string GetProjectUrl()
        {
            return _projectOptions.ProjectUrl;
        }
    }
}
