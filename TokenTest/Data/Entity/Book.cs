namespace TokenTest.Data.Entity
{
    public class Book
    {
        public int ID { get; set; }
        public string BookName { get; set; }
        public string AuthoerName { get; set; }
        public string Category { get; set; }
        public int PageCount { get; set; }
        public string Colour { get; set; }

        //Navigation Props
        public List<FavBook> FavBooks{ get; set; }
    }
}
