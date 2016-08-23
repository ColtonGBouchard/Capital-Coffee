using CapitalCoffee.Data.Models;

namespace CapitalCoffee.Data.Access
{
    public class HoursOfOperationDao
    {
        private readonly CapitalCoffeeContext context;

        public HoursOfOperationDao(CapitalCoffeeContext context)
        {
            this.context = context;
        }

        public void AddHours(HoursOfOperation hours)
        {
            context.HoursOfOperation.Add(hours);
            context.SaveChanges();
        }
    }
}
