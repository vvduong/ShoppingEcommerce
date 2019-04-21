using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingEcommerce.Core.DomainModel.Enums
{
    /// <summary>
    /// Danh sách các phân hệ thuộc hệ thống
    /// </summary>
    public enum ModuleEnums
    {
        /// <summary>
        /// Quản lý tài khoản
        /// </summary>
        Account,
        /// <summary>
        /// Quản lý công tác
        /// </summary>
        BusinessTripRequest,
        /// <summary>
        /// Quy trình ISO, VB trình ký
        /// </summary>
        BusinessWorkflow,
        /// <summary>
        /// Danh bạ
        /// </summary>
        Contact,
        /// <summary>
        /// Quản trị hệ thống
        /// </summary>
        ControlPanel,
        /// <summary>
        /// Quy trình số
        /// </summary>
        DigitalProcedure,
        /// <summary>
        /// Văn bản
        /// </summary>
        Document,
        /// <summary>
        /// Thư viện tài liệu
        /// </summary>
        DocumentLibrary,
        /// <summary>
        /// Help desk
        /// </summary>
        HelpDesk,
        /// <summary>
        /// Đăng ký phòng họp
        /// </summary>
        MeetingRoom,
        /// <summary>
        /// Tin tức
        /// </summary>
        New,
        /// <summary>
        /// Đề nghị Thanh toán
        /// </summary>
        Payment,
        /// <summary>
        /// Báo cáo
        /// </summary>
        Report,
        /// <summary>
        /// Lịch làm việc
        /// </summary>
        Schedule,
        /// <summary>
        /// Văn phòng phẩm
        /// </summary>
        Stationery,
        /// <summary>
        /// Thống kê
        /// </summary>
        Statistic,
        /// <summary>
        /// Công việc
        /// </summary>
        Task,
        /// <summary>
        /// Nghỉ phép
        /// </summary>
        TimeSheet,
        /// <summary>
        /// Đăng ký xe
        /// </summary>
        Vehicle
    }
}
