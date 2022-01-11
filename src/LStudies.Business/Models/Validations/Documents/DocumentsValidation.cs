using System.Collections.Generic;
using System.Linq;

namespace LStudies.Business.Models.Validations.Documents
{
    public class CpfValidation
    {
        public const int CpfSize = 11;

        public static bool Validate(string cpf)
        {
            var cpfNumbers = Utils.JustDigits(cpf);

            if (!ValidSize(cpfNumbers)) return false;

            return !HasRepeatedDigits(cpfNumbers) && HasValidDigits(cpfNumbers);
        }

        private static bool ValidSize(string valor)
        {
            return valor.Length == CpfSize;
        }

        private static bool HasRepeatedDigits(string valor)
        {
            string[] invalidNumbers =
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };

            return invalidNumbers.Contains(valor);
        }

        private static bool HasValidDigits(string value)
        {
            var number = value.Substring(0, CpfSize - 2);
            var digitoVerificador = new DigitoVerifier(number)
                .ComMultiplicadoresDeAte(2, 11)
                .Substituindo("0", 10, 11);
            var firstDigit = digitoVerificador.CalculateDigit();
            digitoVerificador.AddDigit(firstDigit);
            var secondDigit = digitoVerificador.CalculateDigit();

            return string.Concat(firstDigit, secondDigit) == value.Substring(CpfSize - 2, 2);
        }
    }

    public class CnpjValidation
    {
        public const int CnpjSize = 14;

        public static bool Validate(string cpnj)
        {
            var cnpjNumbers = Utils.JustDigits(cpnj);

            if (!HasValidSize(cnpjNumbers)) return false;
            return !HasRepeatedDigits(cnpjNumbers) && HasValidDigits(cnpjNumbers);
        }

        private static bool HasValidSize(string valor)
        {
            return valor.Length == CnpjSize;
        }

        private static bool HasRepeatedDigits(string valor)
        {
            string[] invalidNumbers =
            {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };
            return invalidNumbers.Contains(valor);
        }

        private static bool HasValidDigits(string valor)
        {
            var number = valor.Substring(0, CnpjSize - 2);

            var digitVerifier = new DigitoVerifier(number)
                .ComMultiplicadoresDeAte(2, 9)
                .Substituindo("0", 10, 11);
            var firstDigit = digitVerifier.CalculateDigit();
            digitVerifier.AddDigit(firstDigit);
            var secondDigit = digitVerifier.CalculateDigit();

            return string.Concat(firstDigit, secondDigit) == valor.Substring(CnpjSize - 2, 2);
        }
    }

    public class DigitoVerifier
    {
        private string _number;
        private const int Module = 11;
        private readonly List<int> _multipliers = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9 };
        private readonly IDictionary<int, string> _subs = new Dictionary<int, string>();
        private bool _moduleComplement = true;

        public DigitoVerifier(string number)
        {
            _number = number;
        }

        public DigitoVerifier ComMultiplicadoresDeAte(int firstMultiplier, int lastMultiplier)
        {
            _multipliers.Clear();
            for (var i = firstMultiplier; i <= lastMultiplier; i++)
                _multipliers.Add(i);

            return this;
        }

        public DigitoVerifier Substituindo(string subs, params int[] digits)
        {
            foreach (var i in digits)
            {
                _subs[i] = subs;
            }
            return this;
        }

        public void AddDigit(string digit)
        {
            _number = string.Concat(_number, digit);
        }

        public string CalculateDigit()
        {
            return !(_number.Length > 0) ? "" : GetDigitSum();
        }

        private string GetDigitSum()
        {
            var sum = 0;
            for (int i = _number.Length - 1, m = 0; i >= 0; i--)
            {
                var product = (int)char.GetNumericValue(_number[i]) * _multipliers[m];
                sum += product;

                if (++m >= _multipliers.Count) m = 0;
            }

            var mod = (sum % Module);
            var result = _moduleComplement ? Module - mod : mod;

            return _subs.ContainsKey(result) ? _subs[result] : result.ToString();
        }
    }

    public class Utils
    {
        public static string JustDigits(string value)
        {
            var onlyNumber = "";
            foreach (var s in value)
            {
                if (char.IsDigit(s))
                {
                    onlyNumber += s;
                }
            }
            return onlyNumber.Trim();
        }
    }
}