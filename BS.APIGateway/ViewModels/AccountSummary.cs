namespace BS.APIGateway.ViewModels
{
    public class AccountSummary
    {
        public int Id { get; set; }

        public string? AccountNo { get; set; }

        public string? Name { get; set; }

        public decimal Balance { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CustomerId { get; set; }

        public IEnumerable<AccountTransaction>? Transactions { get; set; }
    }
}
