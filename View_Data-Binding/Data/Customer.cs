using Microsoft.AspNetCore.Components;

namespace View_Data_Binding.Data
{
    public class Customer : ComponentBase
    {
        public string Name { get; } = "Koushik";
        public string UserInput { get; set; }
        public int Number { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        // Private Methods
        private int _Natural() => (Number * (Number + 1)) / 2;
        private int _NaturalEvenOdd(int mulValue) => (Number * (2 * mulValue + (Number - 1) * 2)) / 2;
        private int _Factorial()
        {
            int Sum = 1;
            while (Number > 0) Sum *= (Number--);

            return Sum;
        }
        private int _Fibonacci()
        {
            int first = 0, second = 1, sum = 0;
            for (int i = 2; i < Number; i++)
            {
                sum = first + second;
                first = second;
                second = sum;
            }
            return sum;
        }
        private bool _Palindrome()
        {
            int size = UserInput.Length;
            for (int i = 0; i < size / 2; i++)
            {
                if (UserInput[i] != UserInput[size - i - 1])
                { return false; }
            }
            return true;
        }
        private bool _LeapYear()
        {
            if (Number % 4 == 0)
            {
                if ((Number % 100) != 0 || (Number / 100) % 4 == 0)
                    return true;
            }
            return false;
        }

        // Public Methods
        public int? SumOfN => _Natural();
        public int? SumOfNEven => _NaturalEvenOdd(2);
        public int? SumOfNOdd => _NaturalEvenOdd(1);
        public int? Factorial => _Factorial();
        public int? Fibonacci => _Fibonacci();
        public bool Palindrome => _Palindrome();
        public bool LeapYear => _LeapYear();
    }
}
