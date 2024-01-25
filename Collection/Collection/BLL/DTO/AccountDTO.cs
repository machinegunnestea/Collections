namespace Collection.BLL.DTO
{
    public class AccountDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public IList<string> Roles { get; set; }
    }
}