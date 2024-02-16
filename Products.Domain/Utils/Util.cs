using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Domain.Utils
{
    public class Util
    {
        public static bool ValidaGTIN(string vGTIN)
        {
            //Tamanhos permitidos no GTIN = 8 / 12 / 13 / 14
            int[] GTINlength = { 8, 12, 13, 14 };
            int n, soma, resultado, base10;

            if (!GTINlength.Contains(vGTIN.Length))
            {
                return false;
            }

            //Checa se todos os caracteres do GTIN são números
            for (int i = 0; i <= vGTIN.Length - 1; i++)
            {
                if (!int.TryParse(vGTIN.ElementAt(i).ToString(), out n))
                {
                    return false;
                }
            }

            soma = 0;

            //Se for GTIN-13 multiplica todas as posições pares menos a última por 1 e as ímpares por 3. Nos outros tamanhos, faz o inverso
            if (vGTIN.Length == 13)
            {
                for (int i = 0; i <= vGTIN.Length - 2; i++)
                {
                    if (i % 2 == 0)
                    {
                        soma += (Convert.ToInt32(vGTIN.ElementAt(i).ToString()) * 1);
                    }
                    else
                    {
                        soma += (Convert.ToInt32(vGTIN.ElementAt(i).ToString()) * 3);
                    }
                }
            }
            else
            {
                for (int i = 0; i <= vGTIN.Length - 2; i++)
                {
                    if (i % 2 == 0)
                    {
                        soma = soma + Convert.ToInt32(vGTIN.ElementAt(i).ToString()) * 3;
                    }
                    else
                    {
                        soma = soma + Convert.ToInt32(vGTIN.ElementAt(i).ToString()) * 1;
                    }
                }
            }

            //Procura pelo número de base 10 mais próximo do total somado (arredondando sempre para cima, se necessário)
            base10 = soma;
            if (base10 % 10 != 0)
            {
                while (base10 % 10 != 0)
                {
                    base10 += 1;
                }
            }

            //Diminui o total do número de base 10. O resultado deve ser o último digito do código de barras
            resultado = base10 - soma;
            if (resultado != Convert.ToInt32(vGTIN.ElementAt(vGTIN.Length - 1).ToString()))
            {
                return false;
            }

            return true;
        }
    }
}
