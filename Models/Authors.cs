namespace Medrea_Diana_Lab2.Models
{
    public class Authors
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public ICollection<Book>? Books { get; set; }
    }
}
