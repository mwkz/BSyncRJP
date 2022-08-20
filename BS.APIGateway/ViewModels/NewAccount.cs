namespace BS.APIGateway.ViewModels
{
    public class NewAccount
    {
        public string? AccountNo { get; set; }

        public string? Name { get; set; }

        public decimal InitialBalance { get; set; }

        public int CustomerId { get; set; }
    }
}
