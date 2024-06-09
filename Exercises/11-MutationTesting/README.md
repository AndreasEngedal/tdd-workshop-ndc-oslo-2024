# Let's practice: Mutation Testing with Stryker.NET

The objective of this exercise is to perform mutation testing on the `MutationBankTransfer` project using [Stryker.NET](https://stryker-mutator.io/). 
Mutation testing helps ensure the robustness of your unit tests by introducing small changes (mutants) to your code and checking if your tests can catch these changes. 
The goal is to identify and kill all mutants, thus improving your test suite.

## Important

Uncomment the tests first.

Go to the `BankTransferTests.cs` and remove the `#if false` and `#endif`.

## Step-by-Step example:

<details>
  <summary><i>Possible Solution (Step-by-Step)</i></summary>

### Step 1: Install Stryker.NET

First, install the Stryker.NET tool globally using the following command:

```bash
dotnet tool install -g dotnet-stryker
```


### Step 3: Run Mutation Testing

Run Stryker.NET using the following command in the test project directory:

```bash
dotnet stryker
```

### Step 4: Analyze the Report

After the mutation tests have run, open the generated `mutation-report.html` file. This report will show you the mutants that were created and whether they were killed or survived.

### Step 5: Identify and Kill Surviving Mutants

Identify any surviving mutants by examining the report. For each surviving mutant, improve or add tests to ensure they are caught. 

Here are some typical steps you might take:

1. **Check for Missing Assertions**: Ensure that your tests are asserting all necessary conditions.
2. **Edge Cases**: Add tests for edge cases that you might have missed.
3. **Exception Handling**: Ensure your tests cover scenarios where exceptions should be thrown.


### Step 6: Re-run Mutation Testing

After improving your tests, re-run the mutation testing to verify that all mutants are now killed:

```bash
dotnet stryker
```

Repeat the process until all mutants are killed, indicating a robust test suite.


</details>



## Code Coverage

You can measure code coverage with many tools, some are part of your IDE.

You can also do it with a package.

The first package we need in our test project is coverlet.collector. You can install it either by your IDE's Nuget client or using the command:

```shell
dotnet add package coverlet.collector
```


Now let's install the coverlet command-line tool.

```shell
dotnet tool install -g coverlet.console
```

This is a global command that you can access from anywhere on your computer. To run it against a test project, open the terminal in the project repository and run:
```shell
dotnet build
coverlet .\bin\Debug\net8.0\[PROJECT_NAME].dll --target "dotnet" --targetargs "test --no-build"
```
