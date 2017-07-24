namespace TransactionApi.Models
{
    public static class TransactionValidator
    {
        public static bool IsValid(this Transaction transaction)
        {
            return transaction.Description != null && 
                    transaction.TransactionAmount != null &&
                    transaction.TransactionDate != null;
        }

    }
}