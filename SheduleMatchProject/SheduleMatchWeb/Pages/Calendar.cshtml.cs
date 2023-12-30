using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace SheduleMatchWeb.Pages
{
    public class CalendarModel : PageModel
    {
        public DateTime PierwszyDzienMiesiaca { get; set; } 
        public DateTime StartDate { get; set; }
        public string NazwaMiesiaca { get; set; }
        public int Rok { get; set; }
        public static DateTime CurrentDate { get; set; }

        public void OnGet()
        {
            // Domyœlnie ustawiamy na bie¿¹cy miesi¹c
            PierwszyDzienMiesiaca = DateTime.Now;
            UpdateMonthAndYear();
        }

        public IActionResult OnPost(string action)
        {
            //PierwszyDzienMiesiaca = DateTime.Now;
            // Obs³uga przycisków "Previous Month" i "Next Month"
            if (action == "PreviousMonth")
            {
                PierwszyDzienMiesiaca = PierwszyDzienMiesiaca.AddMonths(-1);
            }
            else if (action == "NextMonth")
            {
                PierwszyDzienMiesiaca = PierwszyDzienMiesiaca.AddMonths(1);
            }

            UpdateMonthAndYear();
            return Page();
        }

        private void UpdateMonthAndYear()
        {
            CultureInfo userCulture = CultureInfo.CurrentCulture;
            DateTimeFormatInfo format = userCulture.DateTimeFormat;

            NazwaMiesiaca = PierwszyDzienMiesiaca.ToString("MMMM", userCulture);
            Rok = PierwszyDzienMiesiaca.Year;

            // Ustalamy datê pocz¹tkow¹ na pierwszy dzieñ bie¿¹cego miesi¹ca
            StartDate = new DateTime(PierwszyDzienMiesiaca.Year, PierwszyDzienMiesiaca.Month, 1);

            // Okreœlamy ró¿nicê w dniach miêdzy rzeczywistym pierwszym dniem tygodnia a oczekiwanym pierwszym dniem tygodnia
            int daysUntilFirstDay = ((int)format.FirstDayOfWeek - (int)StartDate.DayOfWeek + 7) % 7;

            // Przesuwamy datê pocz¹tkow¹ o odpowiedni¹ liczbê dni wstecz
            StartDate = StartDate.AddDays(-daysUntilFirstDay);

            // Sprawdzamy, czy po dodaniu/odejmowaniu miesi¹ca nie przesunêliœmy siê o jeden miesi¹c za daleko
            if (PierwszyDzienMiesiaca.Month != StartDate.Month)
            {
                PierwszyDzienMiesiaca = StartDate;
            }
        }
    }
}


