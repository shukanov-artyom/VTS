using System;
using Autofac;
using test.Models;

namespace test.DependencyInjection.Autofac
{
    public class DefaultModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new TodoRepository()).As<ITodoRepository>();
        }
    }
}
