using ShoppingEcommerce.Core.Paging;
using ShoppingEcommerce.DataAccess;
using ShoppingEcommerce.Repository;
using ShoppingEcommerce.Services.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ShoppingEcommerce.Services.DTOs;

namespace ShoppingEcommerce.Services.Services
{
    public class ProductService : IProductService
    {
        #region Attributes
        private readonly IProductUnitOfWork _productUnitOfWork;
        private readonly IRepository<Products> _repository;
        private readonly ILoggingService _loggingService;

        public string CurrentCulture { get; set; }
        #endregion

        #region Constructors

        public ProductService(IProductUnitOfWork unitOfWork, ILoggingService loggingService)
        {
            this._productUnitOfWork = unitOfWork;
            this._loggingService = loggingService;
            this._repository = new RepositoryBase<Products>(this._productUnitOfWork.GetContext<DbContext>());
        }



        #endregion

        #region Insert

        /// <summary>
        /// Thêm mới san pham
        /// </summary>
        /// <param name="item"></param>
        public void Add(Products item)
        {
            this._repository.Add(item);
            if (item.ProductID != 0)
            {
                // thêm file đính kèm

                // thêm vào sổ Sản phẩm

                // thêm dữ liệu phân loại Sản phẩm

                // thêm mới liên kết Sản phẩm

                // thêm mới log

                // thêm mới lược sử Sản phẩm

            }
        }


        #endregion

        #region Update

        /// <summary>
        /// Cập nhật Sản phẩm
        /// </summary>
        /// <param name="item"></param>
        public void Update(Products item)
        {
            this._repository.Update(item);

        }

        public void UpdateBussiness(Products item)
        {
            this._productUnitOfWork.BeginTransaction();
            this._repository.Update(item);

            // cập nhật file đính kèm

            // cập nhật sổ Sản phẩm

            // cập nhật phân loại Sản phẩm

            // cập nhật liên kết Sản phẩm

            // cập nhật log

            // cập nhật lược sử Sản phẩm


            this._productUnitOfWork.Commit();
        }


        /// <summary>
        /// Cập nhật tình trạng Sản phẩm
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateStatus(Guid id, bool status)
        {
            bool result = false;
            this._productUnitOfWork.BeginTransaction();
            var item = this._repository.GetByID(id);
            if (item != null)
            {
                item.Status = status;
                this._repository.Update(item);
                result = true;
            }
            else
            {
                result = false;
            }
            this._productUnitOfWork.Commit();
            return result;
        }


        #endregion

        #region Delete
        /// <summary>
        /// Xóa Sản phẩm
        /// </summary>
        /// <param name="item"></param>

        public void Delete(Products item)
        {
            this._productUnitOfWork.BeginTransaction();
            item.Status = false;
            this._repository.Update(item);
            // clear link Product
            this._productUnitOfWork.Commit();
        }

        /// <summary>
        /// Xóa Sản phẩm object
        /// </summary>
        /// <param name="id"></param>
        public void Delete(object id)
        {
            var item = this.GetByID(id);
            if (item != null)
            {
                this.Delete(item);
            }
        }

        /// <summary>
        /// Xóa danh sách nhiều sản phẩm
        /// </summary>
        /// <param name="lstDocID"></param>
        public void DeleteMulti(List<Guid> lstProductID)
        {
            if (lstProductID != null && lstProductID.Count > 0)
            {
                foreach (var productID in lstProductID)
                {
                    this.Delete(productID);
                }
            }
        }

        #endregion

        #region Select


        public int Count()
        {
            return 0;
        }

        /// <summary>
        /// Lấy tất cả Sản phẩm disabled
        /// </summary>
        /// <returns></returns>
        public IQueryable<Products> GetAll()
        {
            return _repository.GetQueryable();
        }

        public IList<ProductDTO> GetAllDTO()
        {
            return _repository.GetAll().Select(x => new ProductDTO
            {
                ProductID = x.ProductID,
                ProductCode = x.ProductCode,
                ProductName = x.ProductName,
                MetaTitle = x.MetaTitle,
                Description = x.Description,
                ProductImage = x.ProductImage,
                MoreImages = x.MoreImages,
                Price = x.Price,
                Status = x.Status
            }).ToList();
        }

       

        /// <summary>
        /// Lấy thông tin Sản phẩm theo mã ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Products GetByID(object id)
        {
            return _repository.GetByID(id);
        }

       

        public ProductDTO GetDTOByID(int ID)
        {
            return _repository.GetQueryable().Where(x => x.ProductID == ID).Select(x => new ProductDTO
            {
                ProductID = x.ProductID,
                ProductCode = x.ProductCode,
                ProductName = x.ProductName,
                MetaTitle = x.MetaTitle,
                Description = x.Description,
                ProductImage = x.ProductImage,
                MoreImages = x.MoreImages,
                Price = x.Price,
                Status = x.Status
            }).FirstOrDefault();
        }

      
        /// <summary>
        /// Hàm cung cấp dữ liệu filter 
        /// </summary>
        /// <param name="keyWord">nội dung tìm kiếm</param>
        /// <param name="page">số trang cần lấy</param>
        /// <param name="paramValues">danh sách các đối số và giá trị tương ứng ví dụ: @DocStatus;1,2,3</param>
        /// <param name="orderBy">Chỉ định sort theo tiêu chí cột nào, tăng dần hay giảm dần</param>
        /// <returns></returns>
        public Paging<ProductDTO> GetProductWithPaging(int categoryID,string keyWord, int? page, List<string> paramValues, string orderBy = "Created DESC", Guid currentUserID = default(Guid), bool isCount = false)
        {
            
            Paging<ProductDTO> dataResult = new Paging<ProductDTO>();
            dataResult.Page = new Page(0, 1, 1, 1);
            dataResult.Items = null;
            dataResult.AdditionalInformation = null;

            int pageIndex = page ?? 1;
            int pageSize = 15;
            // tmquan - edited 
            // add more param and query from data 
            // get parameter from paramValues
            #region Get param values
            DateTime currentDate = DateTime.Now;
            
            int linkProduct = 0;
            orderBy = "Created DESC";
            
            #endregion
            try
            {
                // LoggingService.Write(string.Format("{0}\t{1}", "GetMainProductWithPaging", "begin call store"));
                ShoppingCartEntities db = this._productUnitOfWork.GetContext<DbContext>() as ShoppingCartEntities;
                var dataStore = db.SP_Select_Products_MultiFilters(categoryID,keyWord,page,pageSize,orderBy);
                var array = dataStore.ToArray();
                // LoggingService.Write(string.Format("{0}\t{1}", "GetMainProductWithPaging", "end call store"));
                if (array != null && array.Count() > 0)
                {
                    List<ProductDTO> result = new List<ProductDTO>();
                    List<string> additionalInformation = new List<string>();
                    var totalCount = 0;
                    

                    dataResult.Page = new Page(totalCount, pageIndex, pageSize, 1);
                    dataResult.Items = result;
                    dataResult.AdditionalInformation = additionalInformation;
                }
                else
                {
                    dataResult.Page = new Page(0, pageIndex, pageSize, 1);
                    dataResult.Items = null;
                    dataResult.AdditionalInformation = null;
                }
            }
            catch (Exception ex)
            {
                LoggingService.Write(ex);
            }
            return dataResult;
        }


        /// <summary>
        /// Hàm tìm kiếm dữ liệu filter 
        /// </summary>
        /// <param name="keyWord">nội dung tìm kiếm</param>
        /// <param name="page">số trang cần lấy</param>
        /// <param name="paramValues">danh sách các đối số và giá trị tương ứng ví dụ: @DocStatus;1,2,3</param>
        /// <param name="orderBy">Chỉ định sort theo tiêu chí cột nào, tăng dần hay giảm dần</param>
        /// <returns></returns>
        public Paging<ProductDTO> AdvancedSearchProductWithPaging(string keyWord, int? page, int? pageSize, List<string> paramValues, Guid currentUserID, bool isOld, string orderBy = "Created DESC")
        {
            Paging<ProductDTO> dataResult = new Paging<ProductDTO>();
            dataResult.Page = new Page(0, 1, 1, 2);
            dataResult.Items = null;
            dataResult.AdditionalInformation = null;

            int pageIndex = page ?? 1;
            if (!pageSize.HasValue)
            {
                pageSize = 15;
            }
            // tmquan - edited 
            // add more param and query from data 
            // get parameter from paramValues
            #region Get param values
            DateTime currentDate = DateTime.Now;
            string searchMode = "None";
            string unitDeptIDs = "";
            string signedBy = "";
            string userID = currentUserID.ToString();
            string publishDeptID = "";
            string editDeptID = "";
            string bookID = "";
            string sender = "";
            string docTypeID = "";
            string receiver = "";
            string secretID = "";
            string priorityID = "";
            string docStatus = "";
            string internalReceiver = "";
            Nullable<System.DateTime> fromDocDate = null;
            Nullable<System.DateTime> toDocDate = null;
            Nullable<System.DateTime> fromCreated = null;
            Nullable<System.DateTime> toCreated = null;
            string trackStatus = "";
            Nullable<int> processType = null;
            string trackType = "";
            string docType = "";
            Nullable<int> externalType = null;
            Nullable<int> userType = null;
            Nullable<bool> isSearchFile = null;
            string category = "All";
            orderBy = "Created DESC";
            foreach (var param in paramValues)
            {
                var key = param.Split(':')[0];
                var value = param.Split(':')[1];
                switch (key)
                {
                    case "@UserID": userID = value; break;
                    case "@PublishDeptID": publishDeptID = value; break;
                    case "@EditDeptID": editDeptID = value; break;
                    case "@BookID": bookID = value; break;
                    case "@Sender": sender = value; break;
                    case "@DocTypeID": docTypeID = value; break;
                    case "@Receiver": receiver = value; break;
                    case "@SecretID": secretID = value; break;
                    case "@PriorityID": priorityID = value; break;
                    case "@SearchMode": searchMode = value; break;
                    case "@FromDocDate":
                        {
                            if (!string.IsNullOrEmpty(value))
                            {
                                fromDocDate = DateTime.Parse(value);
                            }
                            break;
                        }
                    case "@ToDocDate":
                        {
                            if (!string.IsNullOrEmpty(value))
                            {
                                toDocDate = DateTime.Parse(value);
                            }
                            break;
                        }
                    case "@FromCreated":
                        {
                            if (!string.IsNullOrEmpty(value))
                            {
                                fromCreated = DateTime.Parse(value);
                            }
                            break;
                        }
                    case "@ToCreated":
                        {
                            if (!string.IsNullOrEmpty(value))
                            {
                                toCreated = DateTime.Parse(value);
                            }
                            break;
                        }
                    case "@TrackStatus": trackStatus = value; break;
                    case "@DocStatus": docStatus = value; break;
                    case "@ProcessType": processType = int.Parse(value); break;
                    case "@TrackType": trackType = value; break;
                    case "@DocType": docType = value; break;
                    case "@ExternalType": externalType = int.Parse(value); break;
                    case "@UserType": userType = int.Parse(value); break;
                    case "@Category": category = value; break;
                    case "@SignedBy": signedBy = value; break;
                    case "@InternalReceiver": internalReceiver = value; break;
                    default: break;
                }
            }
            #endregion

           
            return dataResult;
        }


        /// <summary>
        /// Lấy chi tiết Sản phẩm 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Products GetProductDetails(int Id)
        {
            return this._repository.GetAll(p => p.ProductID == Id).SingleOrDefault();
        }

        #endregion
    }
}
