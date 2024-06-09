namespace MutationBankTransfer;

public class InsufficientFundsException(string message) : Exception(message);