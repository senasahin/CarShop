﻿using Autofac;
using Autofac.Integration.Mvc;
using CarShop.Data;
using CarShop.Model;
using CarShop.Service;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CarShop.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var builder = new ContainerBuilder();

            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();


            //db context 'i  mi scoped( yani request bazlı) olarak register et
            builder.RegisterType<ApplicationDbContext>().InstancePerRequest();

            // generic repository geçici instance olarak register et
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerDependency();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();
            //servisleri register et
            builder.RegisterType(typeof(ProductService)).As(typeof(IProductService)).InstancePerDependency();
            builder.RegisterType(typeof(CategoryService)).As(typeof(ICategoryService)).InstancePerDependency();
            builder.RegisterType(typeof(PhotoService)).As(typeof(IPhotoService)).InstancePerDependency();
            builder.RegisterType(typeof(AboutService)).As(typeof(IAboutService)).InstancePerDependency();
            builder.RegisterType(typeof(ContactPageService)).As(typeof(IContactPageService)).InstancePerDependency();
            builder.RegisterType(typeof(MainPageService)).As(typeof(IMainPageService)).InstancePerDependency();
            builder.RegisterType(typeof(CartService)).As(typeof(ICartService)).InstancePerDependency();
            builder.RegisterType(typeof(CountryService)).As(typeof(ICountryService)).InstancePerDependency();
            builder.RegisterType(typeof(CityService)).As(typeof(ICityService)).InstancePerDependency();
            builder.RegisterType(typeof(DistrictService)).As(typeof(IDistrictService)).InstancePerDependency();
            builder.RegisterType(typeof(OrderService)).As(typeof(IOrderService)).InstancePerDependency();
            builder.RegisterType(typeof(OrderProductsService)).As(typeof(IOrderProductsService)).InstancePerDependency();
            builder.RegisterType(typeof(LocationService)).As(typeof(ILocationService)).InstancePerDependency();


            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.Register(c => new UserStore<ApplicationUser>(c.Resolve<ApplicationDbContext>())).AsImplementedInterfaces().InstancePerRequest();
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();
            builder.Register(c => new IdentityFactoryOptions<ApplicationUserManager>
            {
                DataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("Application​")
            });

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
