# Let's practice: Test List

Write a list of test cases to implement a Stack.

## What is a Stack?

A stack is a data structure that follows the Last In, First Out (LIFO) principle. This means that the last element added to the stack will be the first one to be removed. Think of it like a stack of plates: you add plates to the top and also remove the top plate first.

### Key operations:

- **Pop:** Remove and return the top element from the stack.
- **Push:** Add an element to the top of the stack.
- **IsEmpty:** Check if the stack is empty.

### Test List

<details>
    <summary>A possible solution</summary>

```md
    - Stack Is Empty for a new Stack
    - Stack Is Empty for with Pushed items
    - Push an item & pop it
    - Push 2 items & pop them in the right order
    - Popping from an empty stack.
```
</details>

### Implementation

<details>
    <summary>Possible specification decisions</summary>

- Stack is an object
- Name is “Stack”
- There is an operation to add an element
- It is called “push”
- It takes a parameter
- The type of the parameter is the same as the type of the stack
- Stack has a type parameter
- There is an operation to remove elements
- Its name is “pop”
- Its return value is the same as the type of the stack
- Elements are ordered LIFO

</details>

<details>
    <summary>Possible implementation decisions</summary>

- Store the elements in a List
- Type of the list is the same as the type of the stack
- Implementation type is ArrayList
- Add/remove elements at the beginning of the list

</details>