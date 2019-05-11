using ShoppingEcommerce.Core.Paging;
using ShoppingEcommerce.Repository;
using ShoppingEcommerce.DataAccess;
using ShoppingEcommerce.Services.DTOs;
using System;
using System.Collections.Generic;

namespace ShoppingEcommerce.Services.Interfaces.Services
{
    public interface IProductService : IService<Products>
    {
        #region Insert
        #endregion

        #region Update

        void UpdateBussiness(Products item);




        #endregion

        #region Delete
        /// <summary>
        /// Xóa danh sách nhiều sản phẩm
        /// </summary>
        /// <param name="lstProductID"></param>
        void DeleteMulti(List<Guid> lstProductID);

        #endregion

        #region Select



        /// <summary>
        /// Hàm cung cấp dữ liệu filter 
        /// </summary>
        /// <param name="keyWord">nội dung tìm kiếm</param>
        /// <param name="page">số trang cần lấy</param>
        /// <param name="paramValues">danh sách các đối số và giá trị tương ứng ví dụ: @DocStatus;1,2,3</param>
        /// <param name="orderBy">Chỉ định sort theo tiêu chí cột nào, tăng dần hay giảm dần</param>
        /// <returns></returns>
        Paging<ProductDTO> GetProductWithPaging(int categoryID,string keyWord, int? page, List<string> paramValues, string orderBy = "Created DESC", Guid currentUserID = default(Guid), bool isCount = false);


        /// <summary>
        /// Hàm tìm kiếm dữ liệu filter 
        /// </summary>
        /// <param name="keyWord">nội dung tìm kiếm</param>
        /// <param name="page">số trang cần lấy</param>
        /// <param name="paramValues">danh sách các đối số và giá trị tương ứng ví dụ: @DocStatus;1,2,3</param>
        /// <param name="orderBy">Chỉ định sort theo tiêu chí cột nào, tăng dần hay giảm dần</param>
        /// <returns></returns>
        Paging<ProductDTO> AdvancedSearchProductWithPaging(string keyWord, int? page, int? pageSize, List<string> paramValues, Guid currentUserID, bool isOld, string orderBy = "Created DESC");

        /// <summary>
        /// Lấy chi tiết văn bản 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Products GetProductDetails(int Id);

        
        #endregion
    }
}
