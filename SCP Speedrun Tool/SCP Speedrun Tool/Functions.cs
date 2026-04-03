namespace SCP_Speedrun_Tool
{
    public class Functions
    {
        public int FunctionP(int x)
        {
            double xd = (double)x;

            double b = 10000;

            double t = 0;

            int final = new int();

            double a = ((((xd / 10000) * 3) - Math.Round(((xd / 10000) * 3))) * 10000) + b;

            if (a > 9999)
            {
                t = 1;

                a = a - (10000 * t);

                if (a <= 1000)
                {
                    a = a + (1000 * t);
                }
            }

            final = (int)Math.Round(a);

            return final;
        }

        public int FunctionM(int x)
        {
            double xd = (double)x;

            int final = new int();

            double a = 0;

            double m = (((xd / 10000) + a) / 3) * 10000;

            while (!int.TryParse(m.ToString(), out final))
            {
                a++;
                m = (((xd / 10000) + a) / 3) * 10000;

                if (a >= 10)
                    break;
            }

            if (final > 9999)
            {
                final = final % 10000;
            }

            if (final < 1000)
            {
                xd = xd - 1000;

                a = 0;

                m = (((xd / 10000) + a) / 3) * 10000;

                while (!int.TryParse(m.ToString(), out final))
                {
                    a++;
                    m = (((xd / 10000) + a) / 3) * 10000;

                    if (a >= 10)
                        break;
                }

                if (final > 9999)
                {
                    final = final % 10000;
                }
            }



            //final = (int)Math.Round(m);

            return final;
        }
    }
}
