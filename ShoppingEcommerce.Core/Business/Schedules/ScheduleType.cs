using System.ComponentModel.DataAnnotations;

namespace ShoppingEcommerce.Core.Business.Schedules
{
    public enum ScheduleType
    {
        [Display(Name = "Hằng ngày")] Daily,
        [Display(Name = "Hằng tuần")] Weekly,
        [Display(Name = "Hằng tháng")] Monthly,
        [Display(Name = "Hằng năm")] Yearly
    }
}