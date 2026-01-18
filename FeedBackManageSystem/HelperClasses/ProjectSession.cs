using FeedBackManageSystem.Enum;

namespace FeedBackManageSystem.HelperClasses
{
    public static class ProjectSession
    {
        private static IHttpContextAccessor _contextAccessor;
        public static void Configure(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        private static ISession Session => _contextAccessor.HttpContext.Session;
        public static long? UserId
        {
            get { var value = Session.GetString("UserId"); return long.TryParse(value,out var id) ? id:null; }
            set { if(value== null)
                    Session.Remove("UserId");
                else
                    Session.SetString("UserId", value.ToString());
            }
        }
        public static UserType? UserType
        {
            get { var value = Session.GetString("UserId"); if (string.IsNullOrEmpty(value))
                    return null;

                return System.Enum.TryParse<UserType>(value, out var role)
                    ? role
                    : null;
            }
            set
            {
                if (value == null)
                    Session.Remove("UserType");
                else
                    Session.SetString("UserType", value.ToString());
            }
        }
    }
}
