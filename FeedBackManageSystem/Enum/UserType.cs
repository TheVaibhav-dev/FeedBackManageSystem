using System.ComponentModel;

namespace FeedBackManageSystem.Enum
{
    public enum UserType
    {
        [Description("Visitor")]
        Visitor = 1,
        [Description("Author")]
        Author = 2,
        [Description("Admin")]
        Admin = 3  
    }
}
