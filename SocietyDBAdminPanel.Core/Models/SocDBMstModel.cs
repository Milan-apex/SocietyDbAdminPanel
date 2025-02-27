namespace SocietyDBAdminPanel.Core.Models
{
    public class SocDBMstModel
    {
        public int IntID { get; set; }
        public string ServerName { get; set; }
        public string DBName { get; set; }
        public string UserID { get; set; }
        public string PassWord { get; set; }
        public string Year { get; set; }
        public string SocietyCd { get; set; }
        public string SocName { get; set; }
        public string SocLogo { get; set; }
        public string SocAddress { get; set; }
        public string SocCity { get; set; }
        public string SocWebSite { get; set; }
        public bool IsActive { get; set; }
    }
    public class AddUpdateSocDBMstModel
    {
        public int? IntID { get; set; }
        public string ServerName { get; set; }
        public string DBName { get; set; }
        public string UserID { get; set; }
        public string PassWord { get; set; }
        public string Year { get; set; }
        public string SocietyCd { get; set; }
        public string SocName { get; set; }
        public string SocLogo { get; set; }
        public string SocAddress { get; set; }
        public string SocCity { get; set; }
        public string SocWebSite { get; set; }
    }
}
