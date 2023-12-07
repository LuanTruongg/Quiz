namespace Quiz.UI.ServicesClient.Implements
{
    public class RolesService : IRolesService
    {
        public bool CheckAdmin(HttpContext ctx)
        {
            var listRoles = ctx.Session.GetString("UserRoles");
            if (!string.IsNullOrEmpty(listRoles))
            {
                string[] roles = listRoles.Split(';');
                foreach (var role in roles)
                {
                    if (role == "Admin")
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool CheckTeacher(HttpContext ctx)
        {
            var listRoles = ctx.Session.GetString("UserRoles");
            if (!string.IsNullOrEmpty(listRoles))
            {
                string[] roles = listRoles.Split(';');
                foreach (var role in roles)
                {
                    if (role == "Teacher")
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
