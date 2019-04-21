using System.Collections.Generic;

namespace ShoppingEcommerce.Core.Models
{
    /// <summary>
    ///     Dữ liệu my task của 1 module
    /// </summary>
    public class MyHomeTask
    {
        /// <summary>
        ///     Tổng số item
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        ///     Mã code module
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        ///     Danh sách item
        /// </summary>
        public List<MyHomeTaskObject> Items { get; set; }
    }
}