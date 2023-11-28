namespace TokenTest.Data.Entity
{
    public class FavBook
    {
        public int ID { get; set; }
        //Nav Props
        public Book Book { get; set; }
        public int BookID { get; set; }

        public AppUser AppUser { get; set; }
        public string AppUserID { get; set; }
    }
}
