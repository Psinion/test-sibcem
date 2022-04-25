namespace Test.WPF.UI.Data.Models
{
    public class User
    {
        public virtual int Id { get; set; }

        public virtual string Login { get; set; }

        public virtual string LastName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
    }
}
