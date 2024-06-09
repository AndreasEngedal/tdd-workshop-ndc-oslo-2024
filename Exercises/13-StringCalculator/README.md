# TDD Kata: String Calculator

The objective of this exercise is to implement a simple string calculator that can add numbers passed as a string input. 

You will use Test-Driven Development (TDD) to guide your implementation, writing tests before writing the actual implementation code.

The string calculator should be able to handle the following:

1. The input string can contain 0, 1, or 2 numbers separated by commas (",").
2. An empty string should return 0.
3. The method should return the sum of the numbers in the input string.

## Requirements

1. **Add Method**:
    - Implement a method named `Add` that takes a string containing numbers separated by commas and returns their sum.
    - The method signature should be `public int Add(string numbers)`.

2. **Empty String**:
    - If the input string is empty or null, the method should return 0.

3. **Single Number**:
    - If the input string contains a single number, the method should return that number.

4. **Two Numbers**:
    - If the input string contains two numbers separated by a comma, the method should return their sum.

## Instructions

1. **Set Up Your Environment**:
    - Create a new project in your preferred language.
    - Set up a testing framework (e.g., xUnit).

2. **Write Tests**:
    - Start by writing tests for the simplest cases based on the requirements.
    - Gradually add more tests covering different scenarios, including edge cases.

3. **Implement the Functionality**:
    - Implement the code to make the tests pass.
    - Refactor your code to improve its structure and readability while keeping the tests green.

## References

- https://osherove.com/tdd-kata-1