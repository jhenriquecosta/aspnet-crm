using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolidCRM.Data;
using Microsoft.EntityFrameworkCore;
using SolidCRM.Repo;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using SolidCRM.Service; 
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SolidCRM.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        { 
            //services.AddMvc();

            services.AddTransient<SetViewDataFilter>();
            services.AddMvc(options =>
            {
                options.Filters.AddService<SetViewDataFilter>();
            });

            //this service is used for mysql data base.
            services.AddDbContext<ApplicationContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),  mysqlOptions => {  mysqlOptions.ServerVersion(new Version(5, 7, 14), ServerType.MySql);    }));

            //if you want to use MS Sql server database than comment above servies and uncomment below.
            //services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IAuthLoginService, AuthLoginService>();
            services.AddTransient<IMenuBarService, MenuBarService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IRoleUserService, RoleUserService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<IMenuPermissionService, MenuPermissionService>();
            services.AddTransient<IProjectMileStoneService, ProjectMileStoneService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IBlogService, BlogService>();
            services.AddTransient<IBlogCategoryService, BlogCategoryService>();
            services.AddTransient<INewsMediaService, NewsMediaService>();
            services.AddTransient<ISourceService, SourceService>();
            services.AddTransient<ILeadStatusService, LeadStatusService>();
            services.AddTransient<ILeadService, LeadService>();
            services.AddTransient<IContractStatusService, ContractStatusService>();
            services.AddTransient<IContractTypeService, ContractTypeService>();
            services.AddTransient<IContractService, ContractService>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<ITicketTaskService, TicketTaskService>();
            services.AddTransient<IPaymentModeService, PaymentModeService>();
            services.AddTransient<IQuantityUnitService, QuantityUnitService>();
            services.AddTransient<IInvoiceService, InvoiceService>();
            services.AddTransient<IInvoiceItemService, InvoiceItemService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<ILedgerAccountService, LedgerAccountService>();
            services.AddTransient<ICurrencyService, CurrencyService>();
            services.AddTransient<IProjectMemberService, ProjectMemberService>();
            services.AddTransient<IAddressTypeService, AddressTypeService>();
            services.AddTransient<IClientAddressService, ClientAddressService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<ICompanyClientService, CompanyClientService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IProjectTaskService, ProjectTaskService>();
            services.AddTransient<IFileManagerService, FileManagerService>();
            services.AddTransient<IProjectFileService, ProjectFileService>();
            services.AddTransient<IStatusService, StatusService>();
            services.AddTransient<IPriorityService, PriorityService>();
            services.AddTransient<ITodoService, TodoService>();
            services.AddTransient<INotesService, NotesService>();
            services.AddTransient<IDirectionService, DirectionService>();
            services.AddTransient<ILanguageService, LanguageService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAppSettingService, AppSettingService>();
            services.AddTransient<IGeneralSettingService, GeneralSettingService>();
 

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(options =>
                {
                    options.LoginPath = "/auth/login";
                    options.LogoutPath = "/auth/logout";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

