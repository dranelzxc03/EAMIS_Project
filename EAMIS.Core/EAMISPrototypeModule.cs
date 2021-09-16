using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using EAMIS.Core.Domain;

namespace EAMIS.Core
{
    public class EAMISPrototypeModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EAMISEntities>().AsSelf();

            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("BL"))
                .As(t => t.GetInterfaces()[0]);
        }
    }
}
