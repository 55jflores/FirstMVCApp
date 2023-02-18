using FirstMVCApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstMVCApp.Controllers
{
    public class HelloWorldController : Controller
    {
        // List that holds all the tasks
        private static List<TaskViewModel> tasks = new List<TaskViewModel>();

        // Variables to hold the time string and for 24 -> 12 hr conversion 
        private string time = string.Empty;
        private int hr_part = 0;
        private string displayed_hour = string.Empty;
        private string displayed_ampm = string.Empty;
        private List<string> time_arr = new List<string>();

        public IActionResult Index()
        {            
            return View(tasks);
        }

        public IActionResult Create()
        {
            var taskVm = new TaskViewModel();
            return View(taskVm);
        }

        public IActionResult CreateTask(TaskViewModel taskViewModel)
        {
            // Grabbing time string and extracting the hour from it
            time = taskViewModel.Time;
            time_arr = time.Split(':').OfType<string>().ToList();
            hr_part = Int32.Parse(time_arr[0]);

            // Conditional statement to convert to 12 hr time, as well as determining AM or PM
            if (hr_part == 0)
            {
                displayed_hour = "12";
                displayed_ampm = "AM";
            }
            else if (hr_part == 12)
            {
                displayed_hour = "12";
                displayed_ampm = "PM";
            }
            else if (hr_part < 12)
            {
                displayed_hour = hr_part.ToString();
                displayed_ampm = "AM";
            }
            else {
                hr_part = hr_part - 12;
                displayed_hour = hr_part.ToString();
                displayed_ampm = "PM";

            }

            // String that will be displayed to the user
            taskViewModel.Time = displayed_hour + ":" + time_arr[1] + displayed_ampm ;
            tasks.Add(taskViewModel);
            return RedirectToAction(nameof(Index));
        }
        public string Hello()
        {
            return "Who is there?";
        }
    }
}
