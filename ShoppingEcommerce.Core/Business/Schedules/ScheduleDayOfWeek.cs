using System.ComponentModel.DataAnnotations;

namespace ShoppingEcommerce.Core.Business.Schedules
{
    public enum ScheduleDayOfWeek
    {
        [Display(Name = "Chủ nhật")] Sunday = 1,
        [Display(Name = "Thứ 2")] Monday = 2,
        [Display(Name = "Thứ 3")] Tuesday = 3,
        [Display(Name = "Thứ 4")] Wednesday = 4,
        [Display(Name = "Thứ 5")] Thursday = 5,
        [Display(Name = "Thứ 6")] Friday = 6,
        [Display(Name = "Thứ 7")] Saturday = 7
    }
}