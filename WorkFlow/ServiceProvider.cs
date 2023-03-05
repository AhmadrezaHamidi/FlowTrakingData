using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RepositoryEfCore.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow
{
    public static class ServiceProvider
    {
        public static void AddWorkFlowContext(this IServiceCollection services, string connectionString)
        {
            services
            .AddDbContext<WorkFlowContext>(opt => opt.UseSqlServer(connectionString))
            .AddUnitOfWork<WorkFlowContext>();
        }
    }
}
