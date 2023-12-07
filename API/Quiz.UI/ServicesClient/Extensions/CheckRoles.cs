namespace Quiz.UI.ServicesClient.Extensions
{
    public static class CheckRoles
    {
        public static bool CheckAdmin(HttpContext ctx)
        {
            var listRoles = ctx.Session.GetString("UserRoles");
            string[] roles = listRoles.Split(';');
            foreach (var role in roles)
            {
                if (role == "Admin")
                {
                    return true;
                }
            }
            return false;
        }
        public static bool CheckTeacher(HttpContext ctx)
        {
            var listRoles = ctx.Session.GetString("UserRoles");
            string[] roles = listRoles.Split(';');
            foreach (var role in roles)
            {
                if (role == "Teacher")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
