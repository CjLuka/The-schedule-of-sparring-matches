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
            // Domy�lnie ustawiamy na bie��cy miesi�c
            PierwszyDzienMiesiaca = DateTime.Now;
            UpdateMonthAndYear();
        }

        public IActionResult OnPost(string action)
        {
            //PierwszyDzienMiesiaca = DateTime.Now;
            // Obs�uga przycisk�w "Previous Month" i "Next Month"
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

            // Ustalamy dat� pocz�tkow� na pierwszy dzie� bie��cego miesi�ca
            StartDate = new DateTime(PierwszyDzienMiesiaca.Year, PierwszyDzienMiesiaca.Month, 1);

            // Okre�lamy r�nic� w dniach mi�dzy rzeczywistym pierwszym dniem tygodnia a oczekiwanym pierwszym dniem tygodnia
            int daysUntilFirstDay = ((int)format.FirstDayOfWeek - (int)StartDate.DayOfWeek + 7) % 7;

            // Przesuwamy dat� pocz�tkow� o odpowiedni� liczb� dni wstecz
            StartDate = StartDate.AddDays(-daysUntilFirstDay);

            // Sprawdzamy, czy po dodaniu/odejmowaniu miesi�ca nie przesun�li�my si� o jeden miesi�c za daleko
            if (PierwszyDzienMiesiaca.Month != StartDate.Month)
            {
                PierwszyDzienMiesiaca = StartDate;
            }
        }
    }
}


