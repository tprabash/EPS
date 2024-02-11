namespace API.Entities
{
    public class UserMenuList
    {
        public int MenuId { get; set; }       
        public string MenuName { get; set; }
        public string MenuDescription { get; set; }
        public string mType { get; set; }
        public string GroupName { get; set; }
        public int IsPermit { get; set; }
    }
}