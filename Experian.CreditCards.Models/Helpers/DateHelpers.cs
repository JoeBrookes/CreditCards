namespace Experian.CreditCards.Core.Helpers
{
    internal static class DateHelpers
    {
        public static int Age(this DateTime birthday)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - birthday.Year;
            if (now < birthday.Date.AddYears(age))
                age--;

            return age;
        }
    }
}
