using System;
using System.Linq.Expressions;
using ShoppingEcommerce.DataAccess;

namespace ShoppingEcommerce.Core.Specifications.TrackingDocuments
{
    //public sealed class TrackingDocumentOutOfDateSpecification : Specification<TrackingDocument>
    //{
    //    public override Expression<Func<TrackingDocument, bool>> ToExpression()
    //    {
    //        return trackingDocument => trackingDocument.FinishedDate.HasValue
    //            ? trackingDocument.FinishedDate > trackingDocument.ToDate
    //            : DateTime.Now > trackingDocument.ToDate;
    //    }
    //}
}