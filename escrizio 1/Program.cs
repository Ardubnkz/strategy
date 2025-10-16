namespace escrizio_1
{
    interface IStrategiaOperazione
    {
        double Calcola(double a, double b);
    }
    class SommaStrategia : IStrategiaOperazione
    {
        public double Calcola(double a, double b)
        {
            return a + b;
        }
    }
    class SottrazioneStrategia : IStrategiaOperazione
    {
        public double Calcola(double a, double b)
        {
            return a - b;
        }
    }
    class MoltiplicazioneStrategia : IStrategiaOperazione
    {
        public double Calcola(double a, double b)
        {
            return a * b;
        }
    }
    class DivisioneStrategia : IStrategiaOperazione
    {
        public double Calcola(double a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Divisione per zero non consentita.");
            }
            return a / b;
        }
    }
    class Calcolatrice
    {
        private IStrategiaOperazione strategia;
        public void SetStrategia(IStrategiaOperazione NewStrategy)
        {
            strategia = NewStrategy;

        }
        public double EseguiOperazione(double a, double b)
        {
            return strategia.Calcola(a, b);
        }
    }
    //singleton ustente
    class Utente
    {
        private static Utente instance;
        public string Nome { get; private set; }
        private Utente(string nome)
        {
            Nome = nome;
        }
        public static Utente GetInstance(string nome)
        {
            if (instance == null)
            {
                instance = new Utente(nome);
            }
            return instance;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            /*Calcolatrice C= new Calcolatrice();
            C.SetStrategia(new SommaStrategia());
            Console.WriteLine("somma: "+ C.EseguiOperazione(14,3));
            C.SetStrategia(new SottrazioneStrategia());
            Console.WriteLine("sottrazione: " + C.EseguiOperazione(14, 3));
            C.SetStrategia(new MoltiplicazioneStrategia());
            Console.WriteLine("moltiplicazione: " + C.EseguiOperazione(14, 3));
            C.SetStrategia(new DivisioneStrategia());
            Console.WriteLine("divisione: " + C.EseguiOperazione(14, 3));
            Utente u1 = Utente.GetInstance("Mario");
            Console.WriteLine("Utente :"+ u1.Nome );*/
            Console.WriteLine("inserisci nome: ");
            Utente u1 = Utente.GetInstance(Console.ReadLine());
            Console.WriteLine("Utente :" + u1.Nome);
            Calcolatrice calcolatrice = new Calcolatrice();

            Console.Write(u1.Nome + " Inserisci il primo numero: ");
            double num1 = Convert.ToDouble(Console.ReadLine());

            Console.Write("Inserisci il secondo numero: ");
            double num2 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Scegli l'operazione: somma, sottrazione, moltiplicazione, divisione");
            string operazione = Console.ReadLine();

            switch (operazione.ToLower())
            {
                case "somma":
                    calcolatrice.SetStrategia(new SommaStrategia());
                    break;
                case "sottrazione":
                    calcolatrice.SetStrategia(new SottrazioneStrategia());
                    break;
                case "moltiplicazione":
                    calcolatrice.SetStrategia(new MoltiplicazioneStrategia());
                    break;
                case "divisione":
                    calcolatrice.SetStrategia(new DivisioneStrategia());
                    break;
                default:
                    Console.WriteLine("Operazione non valida.");
                    return;
            }
            double risultato =calcolatrice.EseguiOperazione(num1, num2);
            Console.WriteLine("risultato per " + u1.Nome + " " + risultato);

        }
    }
}
