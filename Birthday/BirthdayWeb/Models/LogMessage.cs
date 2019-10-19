using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirthdayWeb.Constants;

namespace BirthdayWeb.Models
{
    public class LogMessage
    {
        public int Id { get; set; }        
        public string Source { get; set; }
        public string Status { get; set; }
        public string Action { get; set; }        
        public string Message { get; set; }
        public string Level { get; set; } = LogLevel.INFO;
        public DateTime? Date { get; set; } = DateTime.Now;
    }

    public class Login : LogMessage
    {
        public Login()
        {
            Source = "Account";
            Status = LogStatus.SUCCESS;
            Action = LogAction.LOGIN;            
        }
    }

    public class Logout : LogMessage
    {
        public Logout()
        {
            Source = "Account";
            Status = LogStatus.SUCCESS;
            Action = LogAction.LOGOUT;            
        }
    }

    public class LoginSuccess    : Login { public LoginSuccess() { }}
    public class LoginFailed     : Login { public LoginFailed()  { Status = LogStatus.FAILED; }}
    public class LoginFailedUserLocked      : LoginFailed  { public LoginFailedUserLocked()      : base() { Status = LogStatus.LOCKED; }}
    public class LoginFailedUserWaitApprove : LoginFailed  { public LoginFailedUserWaitApprove() : base() { Status = LogStatus.WAITAPPROVE; }}
    public class LoginCreateUserSuccess     : LoginSuccess { public LoginCreateUserSuccess()     : base() { Action = LogAction.CREATE; }}
    public class LoginCreateUserFailed      : LoginFailed  { public LoginCreateUserFailed()      : base() { Action = LogAction.CREATE; }}

    public class AdminMessage : LogMessage
    {
        public AdminMessage() 
        {
            Source = "Admin";
            Status = LogStatus.SUCCESS;
        }
    }

    public class AdminSuccess : AdminMessage { public AdminSuccess() { }}
    public class AdminFailed  : AdminMessage { public AdminFailed()  { Status = LogStatus.FAILED; }}

    public class RoleAdminMessage : LogMessage
    {
        public RoleAdminMessage() 
        {
            Source = "RoleAdmin";
            Status = LogStatus.SUCCESS;
        }
    }

    public class RoleAdminSuccess : RoleAdminMessage { public RoleAdminSuccess() { } }
    public class RoleAdminFailed  : RoleAdminMessage { public RoleAdminFailed()  { Status = LogStatus.FAILED; } }

    public class RequestCreateSuccess : LogMessage
    {
        public RequestCreateSuccess()
        {
            Source = "Request";
            Action = LogAction.REQUEST;
            Status = LogStatus.SUCCESS;
        }
    }

    public class RequestApproveSuccess : LogMessage
    {
        public RequestApproveSuccess()
        {
            Source = "Request";
            Action = LogAction.APPROVE;
            Status = LogStatus.SUCCESS;
        }
    }

    public class RequestApproveFailed : LogMessage
    {
        public RequestApproveFailed()
        {
            Source = "Request";
            Action = LogAction.APPROVE;
            Status = LogStatus.FAILED;
        }
    }
}
