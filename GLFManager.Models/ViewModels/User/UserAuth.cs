namespace GLFManager.Models.ViewModels.User
{
    public class UserAuth
    {
        public UserAuth(bool authBool, string authMessage)
        {
            this.IsAuth = authBool;
            this.Message = authMessage;
        }
        public bool IsAuth { get; set; }
        public string Message { get; set; }
    }
}