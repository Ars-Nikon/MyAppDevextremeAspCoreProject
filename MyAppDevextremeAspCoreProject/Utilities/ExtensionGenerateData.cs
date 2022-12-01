using Bogus;
using Microsoft.EntityFrameworkCore;
using MyAppDevextremeAspCoreProject.Models;
using System;

namespace MyAppDevextremeAspCoreProject.Utilities
{
    public static class ExtensionGenerateData
    {
        private static readonly string[] femalePatronymics = new string[] {"Александровна",
"Алексеевна",
"Анатольевна",
"Андреевна",
"Антоновна",
"Аркадьевна",
"Артемовна",
"Богдановна",
"Борисовна",
"Валентиновна",
"Валерьевна",
"Васильевна",
"Викторовна",
"Виталиевна",
"Владимировна",
"Владиславовна",
"Вячеславовна",
"Геннадиевна",
"Георгиевна",
"Григорьевна",
"Даниловна",
"Денисовна",
"Дмитриевна",
"Евгеньевна",
"Егоровна",
"Ефимовна",
"Ивановна",
"Игоревна",
"Ильинична",
"Иосифовна",
"Кирилловна",
"Константиновна",
"Леонидовна",
"Львовна",
"Максимовна",
"Матвеевна",
"Михайловна",
"Николаевна",
"Олеговна",
"Павловна",
"Петровна",
"Платоновна",
"Робертовна",
"Романовна",
"Семеновна",
"Сергеевна",
"Станиславовна",
"Степановна",
"Тарасовна",
"Тимофеевна",
"Федоровна",
"Феликсовна",
"Филипповна",
"Эдуардовна",
"Юрьевна",
"Яковлевна",
"Ярославовна"};
        private static readonly string[] malePatronymics = new string[] {"Александрович",
"Алексеевич",
"Анатольевич",
"Андреевич",
"Антонович",
"Аркадьевич",
"Артемович",
"Бедросович",
"Богданович",
"Борисович",
"Валентинович",
"Валерьевич",
"Васильевич",
"Викторович",
"Витальевич",
"Владимирович",
"Владиславович",
"Вольфович",
"Вячеславович",
"Геннадиевич",
"Георгиевич",
"Григорьевич",
"Данилович",
"Денисович",
"Дмитриевич",
"Евгеньевич",
"Егорович",
"Ефимович",
"Иванович",
"Иваныч",
"Игнатьевич",
"Игоревич",
"Ильич",
"Иосифович",
"Исаакович",
"Кириллович",
"Константинович",
"Леонидович",
"Львович",
"Максимович",
"Матвеевич",
"Михайлович",
"Николаевич",
"Олегович",
"Павлович",
"Палыч",
"Петрович",
"Платонович",
"Робертович",
"Романович",
"Саныч",
"Северинович",
"Семенович",
"Сергеевич",
"Станиславович",
"Степанович",
"Тарасович",
"Тимофеевич",
"Федорович",
"Феликсович",
"Филиппович",
"Эдуардович",
"Юрьевич",
"Яковлевич",
"Ярославович" };
        public static void GenerateData(this ModelBuilder modelBuilder)
        {
            var organizations = GetOrganizations();
            var filials = GetFilials(organizations);
            var services = GetServices();
            var employees = GetEmployees(filials, services);
            var employeeServices = employees.SelectMany(x => x.EmployeeServices).ToList();
            var employeeFilials = employees.SelectMany(x => x.EmployeeFilials).ToList();

            foreach (var employee in employees)
            {
                employee.EmployeeFilials = new();
                employee.EmployeeServices = new();
            }

            modelBuilder.Entity<Organization>().HasData(organizations);
            modelBuilder.Entity<Filial>().HasData(filials);
            modelBuilder.Entity<Service>().HasData(services);
            modelBuilder.Entity<Employee>().HasData(employees);
            modelBuilder.Entity<EmployeeService>().HasData(employeeServices);
            modelBuilder.Entity<EmployeeFilial>().HasData(employeeFilials);
        }

        private static IEnumerable<Organization> GetOrganizations()
        {

            var faker = new Faker<Organization>("ru");

            return faker.StrictMode(false)
                 .RuleFor(x => x.Id, y => y.Database.Random.Uuid())
                 .RuleFor(x => x.Name, y => $"{y.Company.CompanySuffix()} {y.Company.CompanyName()}")
                 .RuleFor(x => x.INN, y => y.Random.Long(1000000000, 9999999999).ToString())
                 .RuleFor(x => x.Phone, y => y.Phone.PhoneNumber(@"79#########").ToString())
                 .RuleFor(x => x.FullNameOwner, y => $"{y.Person.FullName} {(y.Person.Gender == Bogus.DataSets.Name.Gender.Male ? y.PickRandom(malePatronymics) : y.PickRandom(femalePatronymics))}")
                 .RuleFor(x => x.Address, y => y.Address.FullAddress())
                 .Generate(5000)
                 .ToList();
        }
        private static IEnumerable<Filial> GetFilials(IEnumerable<Organization> organizations)
        {
            if (organizations == null)
            {
                throw new ArgumentNullException(nameof(organizations));
            }
            if (organizations.Count() == 0)
            {
                throw new Exception($"{nameof(organizations)} is empty");
            }

            var beginTimes = new DateTime[]
            {
                new DateTime(1, 1, 1, 08, 00, 00),
                new DateTime(1, 1, 1, 09, 00, 00),
                new DateTime(1, 1, 1, 07, 00, 00),
                new DateTime(1, 1, 1, 07,30,00),
                new DateTime(1, 1, 1, 09,30,00)
            };

            var endTimes = new DateTime[]
            {
                new DateTime(1, 1, 1, 20, 00, 00),
                new DateTime(1, 1, 1, 21, 00, 00),
                new DateTime(1, 1, 1, 22, 00, 00),
                new DateTime(1, 1, 1, 21,30,00),
                new DateTime(1, 1, 1, 22,30,00)
            };


            var faker = new Faker<Filial>("ru");

            return faker.StrictMode(false)
                .RuleFor(x => x.Id, y => y.Database.Random.Uuid())
                .RuleFor(x => x.Name, y => y.Company.CompanyName())
                .RuleFor(x => x.PhoneAdmin, y => y.Phone.PhoneNumber(@"79#########").ToString())
                .RuleFor(x => x.BeginningOfWork, y => y.PickRandom(beginTimes))
                .RuleFor(x => x.EndOfWork, y => y.PickRandom(endTimes))
                .RuleFor(x => x.Address, y => y.Address.FullAddress())
                .RuleFor(x => x.IdOrganization, y => y.PickRandom(organizations).Id)
                .Generate(5000)
                .ToList();

        }
        private static IEnumerable<Service> GetServices()
        {
            var services = new string[] { "Карбоновый пилинг",
"Фотоомоложение IPL лицо",
"Фотоомоложение IPL шея",
"Фотоомоложение IPL декольте",
"Фотоомоложение IPL кисти рук",
"Лазерное лечение акне лицо полностью",
"Лазерное лечение пигментации лицо 1 зона",
"Лазерное лечение пигментации лицо полностью",
"Лазерное лечение сосудистой сеточки лицо 1 зона",
"Лазерное лечение сосудистой сеточки лицо полностью",
"Классический массаж лица",
"Миофасциальный массаж / Хиромассаж / Пластический массаж лица",
"MLD Моделирующий лифтинг-дренаж массаж лица",
"Буккальный эстетический массаж лица",
"Буккальный эстетический массаж лица, дополнительно к процедуре",
"Массаж Жаке (доп.услуга)",
"Консультация",
"Уход «Лифтинг и питание»",
"Омолаживающий уход с энзимным пилингом",
"Сезонный витаминизирующий уход",
"Увлажняющий уход для обезвоженной кожи",
"Уход-очищение для выравнивания цвета кожи",
"Массаж лица Sothys",
"Ультразвуковая чистка лица с кремовой маской",
"Ультразвуковая чистка лица с альгинатной маской",
"Мануальная / Комбинированная чистка лица",
"Глубокая комбинированная чистка лица (Sothys)",
"Массаж детский до 1 года",
"Массаж детский от 1 года до 10 лет",
"Массаж детский от 10 до 16 лет",
"Пилинг",
"Обертывание (проблемные зоны)",
"Прокол ушек",
"Плетение кос (дети)",
"Простая прическа (детская)",
"Сложная прическа (детская)",
"Маникюр детский с покрытием лаком до 9 лет",
"Маникюр детский с покрытием лаком с 10 до 14 лет",
"Пилинг Джесснера ONmacabim летний (1 слой)",
"Пилинг Джесснера ONmacabim осень-зима (3 слоя)",
"Миндальный пилинг",
"Пилинг BioRePeel",
"Неинвазивная карбокситерапия",
"Доп. средство Маска Eklado",
"Доп.средство Миндальный пилинг",
"Доп.средство гиалуроновая маска ONMacabim",
"Доп.средство маска SOS Neutragen",
"Доп.средство Маска красоты ONmacabim",
"Доп.средство маска ONmacabim с витамином С",
"Стрижка детская от 3 до 5 лет",
"Стрижка детская от 5 до 10 лет",
"Доп.средство солнцезащитный крем ONmacabim",
"Доп.средство пилинг Джесснера после чистки лица",
"Доп.услуга Дарсонваль"
};
            var timeWorks = new TimeSpan[] { new TimeSpan(0, 30, 0), new TimeSpan(0, 45, 0), new TimeSpan(0, 50, 0), new TimeSpan(1, 0, 0) };
            var prices = new double[] { 2000, 2500, 3000, 3500, 4000, 4500, 5000, 5500, 6000 };
            var random = new Random();

            var results = new List<Service>(services.Length);


            foreach (var servise in services)
            {
                results.Add(new Service
                {
                    Id = Guid.NewGuid(),
                    Name = servise,
                    Description = $"Одна из самых популярных услуг это {servise}",
                    Price = prices[random.Next(0, prices.Length - 1)],
                    TimeWork = timeWorks[random.Next(0, timeWorks.Length - 1)]
                });
            }
            return results;
        }
        private static IEnumerable<Employee> GetEmployees(IEnumerable<Filial> filials, IEnumerable<Service> Services)
        {
            if (filials == null)
            {
                throw new ArgumentNullException(nameof(filials));
            }
            if (filials.Count() == 0)
            {
                throw new Exception($"{nameof(filials)} is empty");
            }
            if (Services == null)
            {
                throw new ArgumentNullException(nameof(Services));
            }
            if (Services.Count() == 0)
            {
                throw new Exception($"{nameof(Services)} is empty");
            }

            var faker = new Faker<Employee>("ru");
            var countFilialsInWork = new int[] { 1, 2, 3, 4 };
            var countServicesInWork = new int[] { 3, 4, 5, 6 };

            var employees = faker.StrictMode(false)
                .RuleFor(x => x.Id, y => y.Database.Random.Uuid())
                .RuleFor(x => x.Name, y => y.Person.FirstName)
                .RuleFor(x => x.Surname, y => y.Person.LastName)
                .RuleFor(x => x.Patronymic, y => (y.Person.Gender == Bogus.DataSets.Name.Gender.Male ? y.PickRandom(malePatronymics) : y.PickRandom(femalePatronymics)))
                .RuleFor(x => x.BirthDate, y => y.Person.DateOfBirth)
                .RuleFor(x => x.Phone, y => y.Phone.PhoneNumber(@"79#########").ToString())
                .RuleFor(x => x.Status, y => Status.Works)
                .RuleFor(x => x.EmployeeFilials, y => y.PickRandom(filials, y.PickRandom(countFilialsInWork)).Select(x => new EmployeeFilial { FilialId = x.Id }).ToList())
                .RuleFor(x => x.EmployeeServices, y => y.PickRandom(Services, y.PickRandom(countServicesInWork)).Select(x => new EmployeeService { ServiceId = x.Id }).ToList())
                .Generate(5000)
                .ToList();


            foreach (var employee in employees)
            {
                employee.EmployeeFilials.ForEach(x => x.EmployeeId = employee.Id);
                employee.EmployeeServices.ForEach(x => x.EmployeeId = employee.Id);
            }
            return employees;
        }
    }
}
