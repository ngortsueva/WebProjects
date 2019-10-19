using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BirthdayWeb.Constants
{
    public class LogStatus
    {
        public static readonly string SUCCESS = "Success";
        public static readonly string FAILED = "Failed";
        public static readonly string LOCKED = "Locked";
        public static readonly string WAITAPPROVE = "WaitApprove";
    }

    public class LogAction
    {
        public static readonly string LOGIN  = "Login";
        public static readonly string LOGOUT = "Logout";
        public static readonly string CREATE = "Create";
        public static readonly string READ   = "Read";
        public static readonly string UPDATE = "Update";        
        public static readonly string DELETE = "Delete";
        public static readonly string REQUEST = "Request";
        public static readonly string APPROVE = "Aprove";

    }

    public class LogResult
    {
        public static readonly string SUCCESS = "Success";
        public static readonly string FAILED = "Failed";
    }

    public class LogLevel
    {
        public static readonly string INFO = "Info";
        public static readonly string WARNING = "Warning";
        public static readonly string ERROR = "Error";
        public static readonly string DEBUG = "Debug";
    }

    public class LogSource
    {
        public const string Account = "Account";
        public const string Admin = "Admin";
        public const string RoleAdmin = "RoleAdmin";
        public const string Request = "Request";
    }

    public static class ObjectType
    {
        public const string User = "user";
    }
}
