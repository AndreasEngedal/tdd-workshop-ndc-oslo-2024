namespace RefactoringBankTransfer.Tests;

internal class InsufficientFundsException(string message) : Exception(message);