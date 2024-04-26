namespace BTL_LTWNC.Models.EF
{
    public class CartItem
    {

        public int iProductID { get; set; }

        public string? sProductName { get; set; }

        public string? sImageUrl { get; set; }

        public double fPrice { get; set; }

        public int iQuantity { get; set; }

        public double fTotalPrice => fPrice * iQuantity;
    }
}
