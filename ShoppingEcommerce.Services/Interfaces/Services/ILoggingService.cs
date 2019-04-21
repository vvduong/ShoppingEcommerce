/*
 * Created: ntChien
 * Date: 07/10/2016
 */
using ShoppingEcommerce.Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace  ShoppingEcommerce.Services
{
    public partial interface ILoggingService
    {
        void Error(string message);
        void Error(Exception ex);
        void Initialise(int maxLogSize);
        IList<LogEntry> ListLogFile();
        void Recycle();
        void ClearLogFiles();
    }
}