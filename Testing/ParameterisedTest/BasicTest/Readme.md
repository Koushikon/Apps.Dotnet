# Parameterised Unit Test with xUnit

[Learn Source](https://andrewlock.net/creating-parameterised-tests-in-xunit-with-inlinedata-classdata-and-memberdata/)

- Did Basic Test with `[Fact]` attribute with `Calculator` class `CalculateAdd_ReturnAdd` method.
- Did Parameterised Test with `[Theory]` and `[InlineData]` attribute with `Calculator` class `CalculateAddWithTheoryAndInlineData_ReturnAddValue` method.
- Did Parameterised Test with `[Theory]` and `[ClassData]` attribute with `Calculator` class `CalculateAddWithTheoryAndClassData_ReturnAddValue` method.
- Did Parameterised Test with `[Theory]` and `[MemberData]` attribute with `Calculator` class `CalculateAddWithTheoryAndMemberData_ReturnAddValue` method.
- Did Parameterised Test with `[Theory]` and `[MemberData]` attribute Method Data with `Calculator` class `CalculateAddWithTheoryAndMethodData_ReturnAddValue` method.
- Did Parameterised Test with `[Theory]` and `[MemberData]` attribute for Method or Class Data with `Calculator` class `CalculateAddWithTheoryAndMethodOrClassData_ReturnAddValue` method.


## Run the Test:
- Inside project directory run `dotnet test`.