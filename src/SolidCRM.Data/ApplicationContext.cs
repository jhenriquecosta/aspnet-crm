using Microsoft.EntityFrameworkCore;
using SolidCRM.Models;

namespace SolidCRM.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {  
            new RoleMap(modelBuilder.Entity<Role>());
            new RoleUserMap(modelBuilder.Entity<RoleUser>());
            new MenuMap(modelBuilder.Entity<Menu>());
            new MenuPermissionMap(modelBuilder.Entity<MenuPermission>());
            new ProjectMileStoneMap(modelBuilder.Entity<ProjectMileStone>());
            new CategoryMap(modelBuilder.Entity<Category>());
            new BlogMap(modelBuilder.Entity<Blog>());
            new BlogCategoryMap(modelBuilder.Entity<BlogCategory>());
            new NewsMediaMap(modelBuilder.Entity<NewsMedia>());
            new SourceMap(modelBuilder.Entity<Source>());
            new LeadStatusMap(modelBuilder.Entity<LeadStatus>());
            new LeadMap(modelBuilder.Entity<Lead>());
            new ContractStatusMap(modelBuilder.Entity<ContractStatus>());
            new ContractTypeMap(modelBuilder.Entity<ContractType>());
            new ContractMap(modelBuilder.Entity<Contract>());
            new TicketMap(modelBuilder.Entity<Ticket>());
            new TicketTaskMap(modelBuilder.Entity<TicketTask>());
            new PaymentModeMap(modelBuilder.Entity<PaymentMode>());
            new QuantityUnitMap(modelBuilder.Entity<QuantityUnit>());
            new InvoiceMap(modelBuilder.Entity<Invoice>());
            new InvoiceItemMap(modelBuilder.Entity<InvoiceItem>());
            new TransactionMap(modelBuilder.Entity<Transaction>());
            new LedgerAccountMap(modelBuilder.Entity<LedgerAccount>());
            new CurrencyMap(modelBuilder.Entity<Currency>());
            new ProjectMemberMap(modelBuilder.Entity<ProjectMember>());
            new AddressTypeMap(modelBuilder.Entity<AddressType>());
            new ClientAddressMap(modelBuilder.Entity<ClientAddress>());
            new CountryMap(modelBuilder.Entity<Country>());
            new CompanyMap(modelBuilder.Entity<Company>());
            new CompanyClientMap(modelBuilder.Entity<CompanyClient>());
            new ProjectMap(modelBuilder.Entity<Project>());
            new ProjectTaskMap(modelBuilder.Entity<ProjectTask>());
            new FileManagerMap(modelBuilder.Entity<FileManager>());
            new ProjectFileMap(modelBuilder.Entity<ProjectFile>());
            new StatusMap(modelBuilder.Entity<Status>());
            new PriorityMap(modelBuilder.Entity<Priority>());
            new TodoMap(modelBuilder.Entity<Todo>());
            new NotesMap(modelBuilder.Entity<Notes>());
            new DirectionMap(modelBuilder.Entity<Direction>());
            new LanguageMap(modelBuilder.Entity<Language>());
            new UserMap(modelBuilder.Entity<User>());
            new AppSettingMap(modelBuilder.Entity<AppSetting>());
            new GeneralSettingMap(modelBuilder.Entity<GeneralSetting>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
