using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingEcommerce.Web
{
    public class SystemConstants
    {
        public static ChatHub CurrentChatHub = null;

        //    public const string LanguageIdCookieName = "LanguageCulture";
        //    public const string LanguageDefault = "en-GB";
        //    // Cache names
        //    //TODO - Move to cache keys
        //    public const string LocalisationCacheName = "Localization-";
        //    public static string LanguageStrings = string.Concat(LocalisationCacheName, "LangStrings-");
        //    //Mobile Check Name
        //    public const string IsMobileDevice = "IsMobileDevice";
        //    // Paging options
        //    public const string PagingUrlFormat = "{0}?p={1}";

        //    // View Bag / Temp Data Constants
        public const string MessageViewBagName = "Message";
        //    public const string GlobalClass = "GlobalClass";
        //    public const string CurrentAction = "CurrentAction";
        //    public const string CurrentController = "CurrentController";
        //    public const string MemberRegisterModel = "MemberViewModel";
        //    public const int PagingSize = 10;
        //    public const int PagingSizeLanguage = 10; //Controller Language
        //    public const string DefaultLanguageID = "C2A5B88D-0ABB-4052-AD6E-A68800FFB614"; //Controller Language                                                                             //public const string userLogin = "sharepoint\\system";
        //    public const string DDMMYYYY = "dd/MM/yyyy hh:mm tt";
        //    //public const string DDMMYYYYhhmmtt = "dd/MM/yyyy hh:mm tt";
        #region Button, Title
        public const string KeyButtonLanguage = "Button.";
        public const string Save = "Save";
        public const string Close = "Close";
        public const string Layout = "Layout";
        public const string Title = "Title";
        public const string Header = "Header";
        public const string View = "View";
        #endregion
        public const string NavigationLink = "Link";
        public const string NavigationHeading = "Heading";
    }
    public class ActionConstants
    {
        public const string Index = "Index";
        public const string View = "View";
        public const string Create = "Create";
        public const string Edit = "Edit";
        public const string Delete = "Delete";
        public const string Details = "Details";
        public const string List = "List";
        #region Manage Language
        public const string ManageLanguageResourceValues = "ManageLanguageResourceValues";
        public const string ManageLanguageResourceKeys = "ManageLanguageResourceKeys";
        public const string ManageResourceKeys = "ManageResourceKeys";
        public const string EditAll = "EditAll";
        public const string GetLanguages = "GetLanguages";
        public const string DeleteLanguageConfirmation = "DeleteLanguageConfirmation";
        public const string DeleteResource = "DeleteResource";
        public const string DeleteLanguage = "DeleteLanguage";
        public const string CreateLanguage = "CreateLanguage";
        public const string AddResourceKey = "AddResourceKey";
        public const string DeleteResourceConfirmation = "DeleteResourceConfirmation";
        public const string Languages = "Languages";
        #endregion
        #region Language
        public const string ChangeLanguage = "ChangeLanguage";
        #endregion
        #region Navigation
        public const string NavigationLeft = "NavigationLeft";
        public const string SubNavigation = "SubNavigation";
        #endregion
        #region Account
        public const string AccessDenied = "AccessDenied";
        public const string NotAuthorized = "NotAuthorized";
        public const string Login = "Login";
        public const string Logout = "Logout";
        public const string Redirect = "Redirect";
        public const string ForgotPassword = "ForgotPassword";
        public const string ChangeForgotPass = "ChangeForgotPass";
        public const string GetNewPass = "GetNewPass";
        public const string SuccessfulChangePass = "SuccessfulChangePass";
        public const string ChangePassword = "ChangePassword";
        #endregion
        #region Document
        public const string NavigationLeftFilter = "NavigationLeftFilter";
        public const string NavigationTopFilter = "NavigationTopFilter";
        public const string MainDocuments = "MainDocuments";
        public const string MainDocumentDetails = "MainDocumentDetails";
        public const string MainDocumentDetailsAction = "MainDocumentDetailsAction";
        public const string GetDocumentDetailsAction = "GetDocumentDetailsAction";
        public const string MainDocumentDetailsFile = "MainDocumentDetailsFile";
        public const string MainDocumentDetailsProcessing = "MainDocumentDetailsProcessing";
        public const string MainDocumentDetailsReceivePlace = "MainDocumentDetailsReceivePlace";
        public const string MainDocumentDetailsChat = "MainDocumentDetailsChat";
        public const string MainDocumentDetailsChatAddMessage = "MainDocumentDetailsChatAddMessage";
        public const string MainDocumentDetailsChatAddMessageWithFile = "MainDocumentDetailsChatAddMessageWithFile";
        public const string MainDocumentDetailsAttachment = "MainDocumentDetailsAttachment";
        public const string DownloadFileDocument = "DownloadFileDocument";
        public const string DownloadFileComment = "DownloadFileComment";
        public const string UserProcessModal = "UserProcessModal";
        public const string UserProcessModalBody = "UserProcessModalBody";
        public const string UploadAssignTaskAttachment = "UploadAssignTaskAttachment";
        public const string AssignTaskToTrackingDocument = "AssignTaskToTrackingDocument";
        public const string AssignTask = "AssignTask";
        public const string ApproveDocument = "ApproveDocument";
        public const string BookDocumentJson = "BookDocumentJson";
        public const string ExecuteStep = "ExecuteStep";
        public const string GetNextDocNumber = "GetNextDocNumber";
        public const string WorkFlowDocumentJson = "WorkFlowDocumentJson";
        public const string WorkFlowStepDocumentJson = "WorkFlowStepDocumentJson";
        public const string UploadDocumentFromAttachment = "UploadDocumentFromAttachment";
        public const string PreviewFile = "PreviewFile";
        public const string CreateDocumentFrom = "CreateDocumentFrom";
        public const string UploadDocumentToAttachment = "UploadDocumentToAttachment";
        public const string DocumentFrom = "DocumentFrom";
        public const string DocumentTo = "DocumentTo";
        public const string CreateDocumentTo = "CreateDocumentTo";
        public const string GetNextSerialNumber = "GetNextSerialNumber";
        public const string ProcessTask = "ProcessTask";
        public const string AdvancedSearch = "AdvancedSearch";
        public const string GetBookDocumentsByDepartment = "GetBookDocumentsByDepartment";
        public const string AdvancedSearchAction = "AdvancedSearchAction";
        public const string TrackingDocumentJson = "TrackingDocumentJson";
        public const string ProcessTaskToTrackingDocument = "ProcessTaskToTrackingDocument";
        public const string FinishTask = "FinishTask";
        public const string AppraiseTask = "AppraiseTask";
        public const string AppraiseTaskToTrackingDocument = "AppraiseTaskToTrackingDocument";
        public const string WithdrawDocument = "WithdrawDocument";
        public const string TransferDocument = "TransferDocument";
        public const string PostWithdrawDocument = "PostWithdrawDocument";
        public const string UpdateReadStatus = "UpdateReadStatus";
        public const string Report = "Report";
        public const string PostEditDocumentFrom = "PostEditDocumentFrom";
        public const string LoadBookDocByBookDocType = "LoadBookDocByBookDocType";
        public const string LoadDocTypesByBookDocumentID = "LoadDocTypesByBookDocumentID";
        public const string PostReport = "PostReport";
        public const string ExportReportToExcel = "ExportReportToExcel";
        public const string NotReceiveLinkDocument = "NotReceiveLinkDocument";
        public const string PostEditDocumentTo = "PostEditDocumentTo";
        public const string Download = "Download";
        public const string MoveToPrivateFolder = "MoveToPrivateFolder";
        public const string ReceiveLinkDocumentModal = "ReceiveLinkDocumentModal";
        public const string ReceiveLinkDocument = "ReceiveLinkDocument";
        public const string Notification = "Notification";
        public const string UpdateNotificationReadStatus = "UpdateNotificationReadStatus";
        public const string DeleteNotification = "DeleteNotification";
        public const string DeleteAllNotification = "DeleteAllNotification";
        public const string EditTrackingDocumentName = "EditTrackingDocumentName";
        public const string TrackingWorkflowDocument = "TrackingWorkflowDocument";
        public const string DocumentDetailModal = "DocumentDetailModal";
        #endregion
        #region Contact
        public const string DepartmentTree = "DepartmentTree";
        public const string UserDepartment = "UserDepartment";
        public const string UserDepartmentWell = "UserDepartmentWell";
        public const string UserDepartmentJson = "UserDepartmentJson";
        public const string UserDelegationJson = "UserDelegationJson";
        public const string DepartmentJson = "DepartmentJson";
        public const string DocumentTypeJson = "DocumentTypeJson";
        public const string BookDocumentTable = "BookDocumentTable";
        public const string BookDocumentModal = "BookDocumentModal";
        public const string BookDocumentViewModal = "BookDocumentViewModal";
        public const string BookDocumentTypeFormatModal = "BookDocumentTypeFormatModal";
        public const string CreateBookDocument = "CreateBookDocument";
        public const string EditBookDocument = "EditBookDocument";
        public const string ImportExcel = "ImportExcel";
        public const string ExportDepartmentExcel = "ExportDepartmentExcel";
        public const string ExportUserExcel = "ExportUserExcel";
        public const string UserDetails = "UserDetails";
        public const string EditUser = "EditUser";
        public const string CreateUser = "CreateUser";
        public const string DeleteUser = "DeleteUser";
        public const string DepartmentDetails = "DepartmentDetails";
        public const string EditDepartment = "EditDepartment";
        public const string CreateDepartment = "CreateDepartment";
        public const string DeleteDepartment = "DeleteDepartment";
        public const string Paging = "Paging";
        public const string LoadProfileTopNavigation = "LoadProfileTopNavigation";
        public const string DepartmentModal = "DepartmentModal";
        #endregion
        #region WorkingCalendar
        public const string Events = "Events";
        public const string CalendarEvent = "CalendarEvent";
        #endregion
        #region DigitalProcess
        public const string Category = "Category";
        public const string CreateCategory = "CreateCategory";
        public const string EditCategory = "EditCategory";
        public const string DeleteCategory = "DeleteCategory";
        public const string ProcessTemplate = "ProcessTemplate";
        public const string CreateProcessTemplate = "CreateProcessTemplate";
        public const string EditProcessTemplate = "EditProcessTemplate";
        public const string DeleteProcessTemplate = "DeleteProcessTemplate";
        public const string ProcessType = "ProcessType";
        public const string CreateProcessType = "CreateProcessType";
        public const string EditProcessType = "EditProcessType";
        public const string DeleteProcessType = "DeleteProcessType";
        public const string Signature = "Signature";
        public const string CreateSignature = "CreateSignature";
        public const string EditSignature = "EditSignature";
        public const string DeleteSignature = "DeleteSignature";
        public const string WorkflowTemplate = "WorkflowTemplate";
        public const string CreateWorkflowTemplate = "CreateWorkflowTemplate";
        public const string EditWorkflowTemplate = "EditWorkflowTemplate";
        public const string DeleteWorkflowTemplate = "DeleteWorkflowTemplate";
        public const string CreateProcess = "CreateProcess";
        public const string ProcessSearchFilterPartial = "ProcessSearchFilterPartial";
        public const string ProcessListPartial = "ProcessListPartial";
        public const string ProcessDetailPartial = "ProcessDetailPartial";
        public const string ProcessHistoryPartial = "ProcessHistoryPartial";
        public const string ProcessChatPartial = "ProcessChatPartial";
        public const string ProcessWorkflowPartial = "ProcessWorkflowPartial";
        public const string ProcessHistoryActionPartial = "ProcessHistoryActionPartial";
        public const string ProcessActionPartial = "ProcessActionPartial";
        public const string CommentDialog = "CommentDialog";
        public const string ServerSignDialog = "ServerSignDialog";
        public const string RejectDialog = "RejectDialog";
        public const string TransferDialog = "TransferDialog";
        public const string ReturnDialog = "ReturnDialog";
        public const string AssignDialog = "AssignDialog";
        public const string ExportProcessExcel = "ExportProcessExcel";
        #endregion
        #region NavNode
        public const string NavNodeTree = "NavNodeTree";
        #endregion
        #region ControlPanel
        public const string WorkflowTree = "WorkflowTree";
        public const string DocumentWorkflow = "DocumentWorkflow";
        public const string WorkflowStep = "WorkflowStep";
        public const string AddWorkflowModal = "AddWorkflowModal";
        public const string InsertWorkflow = "InsertWorkflow";
        public const string DeleteWorkflow = "DeleteWorkflow";
        public const string EditWorkflow = "EditWorkflow";
        public const string WorkflowStepModal = "WorkflowStepModal";
        public const string AddUpdateStepModal = "AddUpdateStepModal";
        public const string LoadWorkflowStepType = "LoadWorkflowStepType";
        public const string LoadJobTitle = "LoadJobTitle";
        public const string LoadUser = "LoadUser";
        public const string LoadStep = "LoadStep";
        public const string InsertStep = "InsertStep";
        public const string DeleteStep = "DeleteStep";
        public const string OrgChart = "OrgChart";
        public const string Permission = "Permission";

        #region Permission
        public const string PermissionMain = "Index";
        #endregion
        #endregion
    }
    public class ControllerConstants
    {
        public const string Navigation = "Navigation";
        public const string Account = "Account";
        public const string Home = "Home";
        public const string Security = "Security";
        public const string Main = "Main";
        public const string Admin = "Admin";
        public const string Shared = "Shared";
        public const string NavNode = "NavNode";
        public const string AdminNavNode = "AdminNavNode";

        #region ControlPanel

        public const string Permission = "Permission";
        #endregion
    }
    public class AreaContants
    {
        public const string Administrator = "Administrator";
        public const string LeaveRegistration = "LeaveRegistration";
        public const string Document = "Document";
        public const string Contact = "Contact";
        public const string DigitalProcess = "DigitalProcess";
        public const string WorkingCalendar = "WorkingCalendar";
        public const string ControlPanel = "ControlPanel";
    }
    public class RolesContants
    {
        public const string Administrator = "Administrator";
        public const string User = "User";
    }
    public class PermissionsContants
    {
        public const string FullControl = "FullControl";
        public const string Create = "Create";
        public const string Edit = "Edit";
        public const string Delete = "Delete";
        public const string View = "View";
    }
    public class TrackingTypesColorContants
    {
        public const string PrimaryColor = "primary-color";
        public const string SupportColor = "support-color";
        public const string ReadOnlyColor = "readonly-color";
    }
    public class ResourceExtensionsContants
    {
        public const string NullOrEmpty = "ResourceExtensions.NullOrEmpty";
        public const string MissingSelectData = "ResourceExtensions.MissingSelectData";
        public const string WrongSelectDataType = "ResourceExtensions.WrongSelectDataType";
        public const string SelectExpressionNotEnumerable = "ResourceExtensions.SelectExpressionNotEnumerable";
        public const string SelectExtensions_InvalidExpressionParameterNoMetadata = "ResourceExtensions.SelectExtensions_InvalidExpressionParameterNoMetadata";
        

    }
}