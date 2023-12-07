namespace Quiz.UI.ServicesClient
{
    public interface IRolesService
    {
        bool CheckAdmin(HttpContext ctx);

        bool CheckTeacher(HttpContext ctx);
    }
}
