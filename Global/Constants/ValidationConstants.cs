namespace Enterprise_E_Commerce_Management_System.Global.Constants
{
    public static class ValidationConstants
    {
        public const int CartCookieDays = 14;
        public const int CustomerCookieMonths = 2;

        public const int EgyptId = 29;

        public const int DollarCurrencyId = 30;

        public const int MinQuantity = 0;
        public const int MaxQuantity = int.MaxValue;

        public const string MinPrice = "0.001";
        public const string MaxPrice = "999999999999";

        public const int MinEmailLength = 5;
        public const int MaxEmailLength = 50;

        public const int MinPhoneLength = 10;
        public const int MaxPhoneLength = 15;

        public const int MoneyPrecision = 18;
        public const int MoneyScale = 6;

        public const int MinRate = 1;
        public const int MaxRate = 5;

        public const int CommentMinLength = 1;

        public const int DefaultPage = 1;
        public const int DefaultCartPageSize = 10;
        public const int DefaultManagePageSize = 20;

        public const int OrderReturnMaxDays = 14;
    }
}
