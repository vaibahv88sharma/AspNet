using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CourseApplication.Data
{
    public class AttendanceResulting
    {
        [Display(Name = "login")]
        public string Login { get; set; }

        [Display(Name = "bannerId")]
        public string BannerId { get; set; }

    }
}
