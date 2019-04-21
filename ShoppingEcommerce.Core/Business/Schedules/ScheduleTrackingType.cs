using System.ComponentModel.DataAnnotations;

namespace ShoppingEcommerce.Core.Business.Schedules
{
    public enum ScheduleTrackingType
    {
        [Display(Name = "Hoàn thành")] Finish,
        [Display(Name = "Tạm dừng")] Pause,
        [Display(Name = "Đang thực hiện")] Processing,
        [Display(Name = "Hủy")] Cancer
    }
}