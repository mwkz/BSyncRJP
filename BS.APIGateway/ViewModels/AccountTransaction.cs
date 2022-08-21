namespace BS.APIGateway.ViewModels
{
    public class AccountTransaction
    {
        public long Id { get; set; }

        public int AccountId { get; set; }

        public decimal Credit { get; set; }

        public decimal Debit { get; set; }

        public int CreatedByUserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UpdatedByUserId { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
