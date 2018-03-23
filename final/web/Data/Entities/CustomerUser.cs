namespace web.Data.Entities
{
    public class CustomerUser
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int RewardPoints { get; set; }
        public string CreditCardHash { get; set; }
    }
}