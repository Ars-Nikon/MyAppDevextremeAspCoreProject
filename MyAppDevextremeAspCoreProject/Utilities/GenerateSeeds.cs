using Bogus;
using MyAppDevextremeAspCoreProject.Models;

namespace MyAppDevextremeAspCoreProject.Utilities
{
    public static class GenerateSeeds
    {

        public static IEnumerable<Organization> GetOrganizations()
        {
            var typeOrg = new string[] { "ООО", "ИП", "ОАО", "ЗАО" };

            var organizations = new Faker<Organization>()
                .StrictMode(true)
                .RuleFor(x => x.Id, y => y.Database.Random.Uuid())
                .RuleFor(x => x.Name, y => $"{y.PickRandom(typeOrg)} {y.Company.CompanyName()}")
                .RuleFor(x => x.INN, y => y.Random.Long(1000000000, 9999999999).ToString())
                .RuleFor(x => x.Phone, y => y.Phone.PhoneNumber(@"79#########").ToString())
                .RuleFor(x => x.FullNameOwner, y => y.Name.FullName())
                .RuleFor(x => x.Address, y => y.Address.FullAddress())
                .Generate(5000)
                .ToList();

            return organizations;
        }
    }
}
