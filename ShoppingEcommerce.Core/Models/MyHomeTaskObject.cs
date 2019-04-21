using System;
using System.Collections.Generic;

namespace ShoppingEcommerce.Core.Models
{
    /// <summary>
    ///     Đối tượng dữ liệu 1 task
    /// </summary>
    public class MyHomeTaskObject
    {
        public MyHomeTaskObject()
        {
            Files = null;
        }

        /// <summary>
        ///     Họ và tên người tạo/người giao/người gửi
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        ///     Tiêu đề task
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Nội dung mô tả
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        ///     Chuỗi Tình trạng của item
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        ///     Màu nền hiển thị tình trạng
        ///     Do server trả về dạng
        ///     #xxxxxx
        /// </summary>
        public string BackgroundColorStatus { get; set; }

        /// <summary>
        ///     Màu chữ hiển thị tình trạng
        ///     Do server trả về dạng
        ///     #xxxxxx
        /// </summary>
        public string TextColorStatus { get; set; }

        /// <summary>
        ///     Thời gian của task
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        ///     Địa chỉ truy cập trên web
        /// </summary>
        public string WebUrl { get; set; }

        /// <summary>
        ///     ID dẫn trên mobile
        /// </summary>
        public string MobileObjectID { get; set; }

        /// <summary>
        ///     Chuỗi Json string data mở rộng cho module
        /// </summary>
        public string ExtensionData { get; set; }

        /// <summary>
        ///     Chuỗi chưa ngày tháng theo dạng
        ///     dd/MM/yyyy
        ///     Tùy theo module mà dữ liệu khác nhau:
        ///     ví dụ: VB : Số hiêu: 12/LV-HPS
        ///     CV:         dd/MM/yyyy - dd/MM/yyyy
        /// </summary>
        public string TimeString { get; set; }

        /// <summary>
        ///     Danh sách file đính kèm
        /// </summary>
        public List<MyFileObject> Files { get; set; }
    }
}