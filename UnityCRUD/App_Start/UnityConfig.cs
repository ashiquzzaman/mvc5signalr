using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using SignalRMVCUnityCRUD.Controllers;
using SignalRMVCUnityCRUD.Managers;
using SignalRMVCUnityCRUD.Models;
using SignalRMVCUnityCRUD.Repositories;
using Unity.Mvc5;

namespace SignalRMVCUnityCRUD
{
	public static class UnityConfig
	{
		public static void RegisterComponents()
		{
			var container = new UnityContainer();
			container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());
			container.RegisterType(typeof(UserManager<>), new InjectionConstructor(typeof(IUserStore<>)));
			container.RegisterType<IUser>(new InjectionFactory(c => c.Resolve<IUser>()));
			container.RegisterType(typeof(IUserStore<>), typeof(UserStore<>));
			container.RegisterType<IdentityUser, ApplicationUser>(new ContainerControlledLifetimeManager());
			container.RegisterType<UserManager<ApplicationUser>>(new HierarchicalLifetimeManager());
			container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager());

			container.RegisterType<IDepertmentRepository, DepertmentRepository>();
			container.RegisterType<IPersonRepository, PersonRepository>();
			container.RegisterType<IDepertmentManager, DepertmentManager>();
			container.RegisterType<IPersonManager, PersonManager>();


			container.RegisterType<AccountController>(new InjectionConstructor());
			container.RegisterType<ManageController>(new InjectionConstructor());
			DependencyResolver.SetResolver(new UnityDependencyResolver(container));
		}
	}
}