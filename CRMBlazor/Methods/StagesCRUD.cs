using CRMBlazor.Data;
using CRMBlazor.Interface;
using CRMBlazor.Models;
using CRMBlazor.Models.Statistics;
using CRMBlazor.Models.Statistics.Graphic;
using Microsoft.EntityFrameworkCore;


namespace CRMBlazor.Methods
{
    public class StagesCRUD
    {
        ApplicationContext db;

        

        public StagesCRUD(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<List<Stage>> GetStages()
        {
            if (db.JobTypes.Count() == 0)
            {
                db.Add(new Stage { Name = "Заявка" });
                db.Add(new Stage { Name = "Замер" });
                db.SaveChanges();
            }
            return db.Stages.AsNoTracking().ToList();
        }
        public async Task AddStage(Stage stage)
        {
            db.Add(stage);
            await db.SaveChangesAsync();
        }

        public async Task RemoveStage(Stage stage)
        {
            db.Remove(stage);
            await db.SaveChangesAsync();
        }
        public async Task<List<JobType>> GetJobTypes()
        {
            if (db.JobTypes.Count() == 0)
            {
                db.Add(new JobType { Name = "Покраска" });
                db.Add(new JobType { Name = "Шпаклевка" });
                db.SaveChanges();
            }
            return db.JobTypes.AsNoTracking().ToList();
        }
        public async Task AddJobType(JobType jobType)
        {
            db.Add(jobType);
            await db.SaveChangesAsync();
        }
        public async Task<List<AdvertisingSource>> GetAdvertisingSources()
        {
            if (db.AdvertisingSources.Count() == 0)
            {
                db.Add(new AdvertisingSource { Name = "Авито" });
                db.Add(new AdvertisingSource { Name = "Яндекс" });
                db.SaveChanges();
            }
            return db.AdvertisingSources.AsNoTracking().ToList();
        }
        public async Task AddAdvertisingSource(AdvertisingSource advSource)
        {
            db.Add(advSource);
            await db.SaveChangesAsync();
        }
        public async Task<List<User>> GetUsers()
        {
            if (db.Users.Count() == 0)
            {
                var pass = HashPassword.HashPasword("228");
                db.Users.Add(new User { Name = "Den", UserRole = new Role { Name = "Regular" },Password=pass });
                db.SaveChanges();
            }

            return db.Users.Include(x => x.UserRole)
                .Include(x => x.UserTasks)
                .AsNoTracking()
                .ToList();
        }

        public async Task AddUser(User user)
        {
            db.Add(user);
            await db.SaveChangesAsync();
        }
        public async Task AddGeneric(ITestik user)
        {
            db.Add(user);
            await db.SaveChangesAsync();
        }
        public async Task<Order> GetOrderById(int id)
        {
            return db.Orders.Where(x => x.Id == id).Include(x => x.Advertise)
                .Include(x => x.Author)
                .Include(x => x.JobType)
                .Include(x => x.Stage)
                .AsNoTracking()
                .FirstOrDefault();
        }
        public async Task<List<Order>> GetOrders()
        {
            return db.Orders.Include(x => x.Advertise)
                .Include(x => x.Author)
                .Include(x => x.JobType)
                .Include(x => x.Stage)
                .OrderByDescending(x=>x.RegistredDate)
                .AsNoTracking()
                .ToList();
        }
        public async Task<List<Order>> GetOrdersWithTasks()
        {
            return db.Orders.Where(x=>x.UserTasks!=null).Include(x => x.Advertise)
                .Include(x => x.Author)
                .Include(x => x.JobType)
                .Include(x => x.Stage)
                .Include(x => x.UserTasks)
                .AsNoTracking()
                .ToList();
        }
        public async Task AddOrder(Order order)
        {
            await db.AddAsync(order);
            await db.SaveChangesAsync();
        }
        public async Task UpdateOrder(Order order)
        {
            
            db.Update(order);
            await db.SaveChangesAsync();
        }

        public async Task<List<UserTask>> GetAllTasks()
        {
            return await db.Tasks.ToListAsync();
        }
        public async Task AddUserTask(UserTask userTask)
        {
            db.Add(userTask);
            await db.SaveChangesAsync();
        }

        public async Task<bool> isUser(string name)
        {
            return db.Users.Any(x => x.Name == name);
        }
        public async Task<bool> isPassword(string name,string password)
        {
            return db.Users.Any(x => x.Name == name&&x.Password==password);
        }

        public async Task<User> GetUserByName(string name)
        {
            return db.Users.Where(x => x.Name == name).Include(x=>x.UserRole).First();
        }

        public async Task<List<AdvertiseStatatistics>> GetAdvertiseStatistics()
        {
            return db.AdvStats.Include(x=>x.advSource).Include(x=>x.jobType).ToList();
        }
        public async Task AddAdvertiseStatistics(AdvertiseStatatistics advStat)
        {
            db.Add(advStat);
            await db.SaveChangesAsync();
        }

        public async Task Test()
        {
            await Console.Out.WriteLineAsync("run228");

           
            var zz = (from c in db.AdvStatsByJob.Include(x => x.AdvByJobTypes).ThenInclude(x => x.jobType).Select(x => new { Date = x.Date, x.AdvByJobTypes }).OrderBy(x => x.Date)
                      select new
                      {
                          Date = c.Date.ToString("MMMM.y"),

                          Job = c.AdvByJobTypes.Select(x => new {Name=x.jobType.Name, CountJ= x.Expense}).ToList(),
                          ExpenseSum=c.AdvByJobTypes.Select(x=>x.Expense).Sum()

                          
                      }).ToList();
            var qq = from a in zz
                     group a by a.Date;

            foreach (var item in zz)
            {
                await Console.Out.WriteLineAsync(item.Date+" "+item.ExpenseSum);
                foreach (var iteml in item.Job)
                {
                    await Console.Out.WriteLineAsync(iteml.Name + " " + iteml.CountJ);
                }
            }

            foreach (var item in qq)
            {
                Console.WriteLine(item.Key);
                foreach (var itemk in item)
                {
                    Console.WriteLine(itemk.ExpenseSum);
                    foreach (var itemz in itemk.Job)
                    {
                        Console.WriteLine(itemz.Name+" "+itemz.CountJ);
                    }
                }
            }


            var aa = (from c in db.AdvStatsByJob.Include(x => x.AdvByJobTypes).ThenInclude(x => x.jobType).Select(x => new { Date = x.Date, x.AdvByJobTypes })
                      group c by c.Date into g
                      select new
                      {
                          Date = g.Key,

                          Job = g.Select(x => new { JJ = x.AdvByJobTypes.Select(x => new { Name = x.jobType.Name, Expense = x.Expense }) })
                      });

            foreach (var item in aa)
            {
                await Console.Out.WriteLineAsync(item.Date.ToString());
                foreach (var iteml in item.Job)
                {
                    foreach (var itemk in iteml.JJ)
                    {
                        await Console.Out.WriteLineAsync(itemk.Name+" "+itemk.Expense);
                    }
                }
            }

            Console.Clear();

            var advjobbydate = new List<AdvertiseStatatisticsByJobTypes>();
            int i = 0,j;
            var advList = db.AdvStatsByJob.Include(x => x.AdvByJobTypes).ThenInclude(x => x.jobType).OrderBy(x => x.Date).GroupBy(x => x.Date);

            foreach (var item in advList)
            {
                advjobbydate.Add(new AdvertiseStatatisticsByJobTypes { Date = item.Key });
                foreach (var itemk in item)
                {
                    foreach (var itemz in itemk.AdvByJobTypes)
                    {
                        j = -1;
                        j = advjobbydate[i].AdvByJobTypes.FindIndex(x => x.jobType.Name == itemz.jobType.Name);
                        if (j != -1)
                            advjobbydate[i].AdvByJobTypes[j] += itemz;
                        else
                            advjobbydate[i].AdvByJobTypes.Add(itemz);
                        
                    }
                }
                i++;
            }
            advList = null;
            
            
            foreach (var item in advjobbydate)
            {
                await Console.Out.WriteLineAsync(item.Date.ToString());
                foreach (var itemz in item.AdvByJobTypes)
                {
                    Console.WriteLine(itemz.jobType.Name + " " + itemz.Expense);
                }
            }


        }
        public async Task<List<AdvertiseStatatisticsByJobTypes>> GetAdvStatsByJob()
        {
            return db.AdvStatsByJob.Include(x => x.advSource).Include(x => x.AdvByJobTypes).ThenInclude(x=>x.jobType).OrderBy(x=>x.Date).ToList();
        }

        public async Task<List<AdvertiseStatatisticsByJobTypes>> GetAdvStatsByJobForGraphic()
        {
            var advjobbydate = new List<AdvertiseStatatisticsByJobTypes>();
            int i = 0, j;
            var advList = db.AdvStatsByJob.Include(x => x.AdvByJobTypes).ThenInclude(x => x.jobType).OrderBy(x => x.Date).GroupBy(x => x.Date);

            foreach (var item in advList)
            {
                advjobbydate.Add(new AdvertiseStatatisticsByJobTypes { Date = item.Key });
                foreach (var itemk in item)
                {
                    foreach (var itemz in itemk.AdvByJobTypes)
                    {
                        j = -1;
                        j = advjobbydate[i].AdvByJobTypes.FindIndex(x => x.jobType.Name == itemz.jobType.Name);
                        if (j != -1)
                            advjobbydate[i].AdvByJobTypes[j] += itemz;
                        else
                            advjobbydate[i].AdvByJobTypes.Add(itemz);

                    }
                }
                i++;
            }
            advList = null;
            return advjobbydate;
        }


        public async Task AddAdvStatatisticsByJobTypes(AdvertiseStatatisticsByJobTypes adv)
        {
            db.Add(adv);
            await db.SaveChangesAsync();
        }
        public async Task AddAdvByJob(AdvByJobType adv)
        {
            db.Add(adv);
            await db.SaveChangesAsync();
        }

        public async Task<List<OrdersCountByDate>> GetOrderCountByDate()
        {
            //Попробовать поменять. Непонятно зачем тут группировка по дате
            // тут просто выводим дату и считаем число заказов в день
            return (from c in db.Orders.Include(x => x.Advertise).Select(x => new { x.RegistredDate, x.Advertise })
                    group c by c.RegistredDate.Date into g
                    select new OrdersCountByDate()
                    {
                        Date = g.Key,
                        Counts = (from ad in g
                                  group ad by ad.Advertise.Name into adv
                                  select new ItemCount() { Name = adv.Key, Count = adv.Count() }).ToList(),
                        Count = g.Count()
                    }).ToList();
        }
        public async Task<List<DateAdvByJob>> GetAdvExpenseByDateJob()
        {
            return (from c in db.AdvStatsByJob.Include(x => x.AdvByJobTypes).ThenInclude(x => x.jobType).Select(x => new { x.Date, x.AdvByJobTypes }).OrderBy(x => x.Date)
                    select new DateAdvByJob()
                    {
                        Date = c.Date,
                        Job = c.AdvByJobTypes,
                        Count = c.AdvByJobTypes.Select(x => x.Expense).Sum(),
                    }).ToList();
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }


    }
}
