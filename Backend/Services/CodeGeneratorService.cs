namespace Services
{
    public class CodeGeneratorService
    {
        private static readonly Random _random = new Random();

        public static string GenerateTipoMedicamentoCode(string nombreGenerico)
        {
            string prefijo = GetCleanChars(nombreGenerico, 3);
            string aleatorio = GenerateRandomAlphanumeric(4);
            return $"TMED-{prefijo}-{aleatorio}".ToUpper();
        }

        public static string GenerateMedicamentoCode(string nombreComercial, string nombreForma)
        {
            string prefijoMed = GetCleanChars(nombreComercial, 3);
            string prefijoForma = GetCleanChars(nombreForma, 3);
            string aleatorio = GenerateRandomAlphanumeric(4);
            return $"MED-{prefijoMed}-{prefijoForma}-{aleatorio}".ToUpper();
        }

        public static string GenerateUbicacionCode(string nombreUbicacion)
        {
            string prefijo = GetCleanChars(nombreUbicacion, 3);
            string aleatorio = GenerateRandomAlphanumeric(4);
            return $"UBIC-{prefijo}-{aleatorio}".ToUpper();
        }

        public static string GenerateLoteCode(DateOnly fechaVencimiento)
        {
            string fechaStr = fechaVencimiento.ToString("yyMMdd");
            string aleatorio = GenerateRandomAlphanumeric(4);
            return $"LOT-{fechaStr}-{aleatorio}".ToUpper();
        }

        public static string GenerateMovimientoCode()
        {
            string timestamp = DateTime.Now.ToString("yyMMdd");
            string aleatorio = GenerateRandomAlphanumeric(4);
            return $"MOV-{timestamp}-{aleatorio}".ToUpper();
        }

        public static string GenerateRecepcionCode()
        {
            string timestamp = DateTime.Now.ToString("yyMMdd");
            string aleatorio = GenerateRandomAlphanumeric(4);
            return $"RCP-{timestamp}-{aleatorio}".ToUpper();
        }

        public static string GenerateRecetaCode()
        {
            string timestamp = DateTime.Now.ToString("yyMMdd");
            string aleatorio = GenerateRandomAlphanumeric(4);
            return $"RCT-{timestamp}-{aleatorio}".ToUpper();
        }

        public static string GeneratePosologiaCode()
        {
            string aleatorio = GenerateRandomAlphanumeric(6);
            return $"POS-{aleatorio}".ToUpper();
        }

        public static string GenerateDispensacionCode()
        {
            string timestamp = DateTime.Now.ToString("yyMMdd");
            string aleatorio = GenerateRandomAlphanumeric(4);
            return $"DSP-{timestamp}-{aleatorio}".ToUpper();
        }

        private static string GetCleanChars(string text, int length)
        {
            if (string.IsNullOrEmpty(text))
                return "XXX";
            var clean = new string(text.Where(char.IsLetter).ToArray());
            return clean.Length >= length
                ? clean.Substring(0, length)
                : clean.PadRight(length, 'X');
        }

        private static string GenerateRandomAlphanumeric(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(
                Enumerable.Repeat(chars, length).Select(s => s[_random.Next(s.Length)]).ToArray()
            );
        }
    }
}
