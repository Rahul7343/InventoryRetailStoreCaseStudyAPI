namespace BusinessLogic.Models
{
    public class SupplierModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string MobileNo { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }

    }
}
