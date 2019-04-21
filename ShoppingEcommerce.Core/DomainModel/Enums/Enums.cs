using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingEcommerce.Core.Enums
{
    /// <summary>
    /// Status values returned when creating a user
    /// </summary>
    public enum MembershipCreateStatus
    {
        Success,
        DuplicateUserName,
        DuplicateEmail,
        DuplicateMobilePhone,
        InvalidPassword,
        InvalidEmail,
        InvalidAnswer,
        InvalidQuestion,
        InvalidUserName,
        ProviderError,
        UserRejected
    }
    public enum LoginAttemptStatus
    {
        LoginSuccessful,
        UserNotFound,
        PasswordIncorrect,
        PasswordAttemptsExceeded,
        UserLockedOut,
        UserNotApproved,
        Banned
    }
    public enum SurePortalRole
    {
        Administrator=1,
        User=2
    }

    public enum LeaveTime
    {
        AllDay,
        Morning,
        Afternoon

    }
    public enum NavNodeElementType
    {
        Link,
        Heading
    }
}