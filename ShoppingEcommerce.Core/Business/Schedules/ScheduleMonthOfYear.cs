using System.ComponentModel.DataAnnotations;

namespace ShoppingEcommerce.Core.Business.Schedules
{
    public enum ScheduleMonthOfYear
    {
        [Display(Name = "Tháng 1")] January = 1,
        [Display(Name = "Tháng 2")] February = 2,
        [Display(Name = "Tháng 3")] March = 3,
        [Display(Name = "Tháng 4")] April = 4,
        [Display(Name = "Tháng 5")] May = 5,
        [Display(Name = "Tháng 6")] June = 6,
        [Display(Name = "Tháng 7")] July = 7,
        [Display(Name = "Tháng 8")] August = 8,
        [Display(Name = "Tháng 9")] September = 9,
        [Display(Name = "Tháng 10")] October = 10,
        [Display(Name = "Tháng 11")] November = 11,
        [Display(Name = "Tháng 12")] December = 12
    }
}