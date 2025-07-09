namespace DbOperationsWithEfCoreApp.Data
{
    public class Currency
    {
        public int Id { get; set; }
        public string Currenc { get; set; }
        public string Description { get; set; }


        public ICollection<BookPrice> Price { get; set; }
    }
}
