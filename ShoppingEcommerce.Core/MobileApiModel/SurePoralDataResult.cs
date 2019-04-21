using System;
using System.Collections.Generic;
using System.Configuration;
using Newtonsoft.Json;

namespace ShoppingEcommerce.Core.MobileApiModel
{
    /// <summary>
    /// Cấu trúc dữ liệu data trao đổi qua lại
    /// </summary>
    public class SurePortalDataResult
    {
        public SurePortalDataResult()
        {

        }

        /// <summary>
        /// Constructor đối số
        /// </summary>
        /// <param name="status"></param>
        /// <param name="msg"></param>
        /// <param name="dataReturn"></param>
        public SurePortalDataResult(SurePoralStatusCode status, string msg, object dataReturn)
        {
            this.Status = status;
            this.Message = msg;
            this.Data = dataReturn;
        }

        /// <summary>
        /// Mã tình trạng lỗi
        /// </summary>
        public SurePoralStatusCode Status { get; set; }

        /// <summary>
        /// Thông tin chuỗi
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Dữ liệu
        /// </summary>
        public Object Data { get; set; }

        /// <summary>
        /// Convert hàm chuyển về dạng jsonstring
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ConfigurationManager.AppSettings["IsEncryptData"] == "true" ? ToEncryptString() : JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ToEncryptString()
        {
            return SurePortalEncrypting.EncryptAES(JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
          //  return Sureportal_Encrypting.Encrypt(JsonConvert.SerializeObject(this), SurePortalGlobal.EncryptSalt);
            //return Sureportal_Encrypting.Encrypt(JSONParser.Serialize<SurePoralDataResult>(this), SurePortalGlobal.EncryptSalt);
        }
    }

    /// <summary>
    /// Cấu trúc dữ liệu data param truyền lên 
    /// </summary>
    public class SurePortalDataParam
    {
        /// <summary>
        /// Tên đối số
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Dữ liệu đối số
        /// </summary>
        public string Value { get; set; }
        
        public SurePortalDataParam()
        {

        }

        /// <summary>
        /// Parse lấy danh sách dữ liệu từ chuỗi param
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        public static Dictionary<string, SurePortalDataParam> GetListParams(string jsonData, bool isEncrypted = false)
        {
            if (isEncrypted)
            {
                jsonData = SurePortalEncrypting.DecryptAES(jsonData);
            }
            var dicParams = new Dictionary<string, SurePortalDataParam>();
            var dataParams = JsonConvert.DeserializeObject<SurePortalDataParam[]>(jsonData);
            //JSONParser.Deserialize<SurePortalDataParam[]>(jsonData);
            if (dataParams != null)
            {
                foreach (SurePortalDataParam pr in dataParams)
                {
                    dicParams.Add(pr.Name.ToLower(), pr);
                }
            }
            return dicParams;
        }
    }

    /// <summary>
    /// Mã lỗi dùng trao đổi
    /// </summary>
    public enum SurePoralStatusCode
    {
        UnAuthorized = 0,
        Success = 1,
        Error = 2,
        DataNotExisted = 3
    }
}