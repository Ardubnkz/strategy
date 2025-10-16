namespace esercizio_2
{
    #region INTERFACE
    interface IPiatto
    {
        string Descrizione();
        string prepara();
    }
    interface IpreprazioneStrategia
    {
        string Prepara(string descrizione);
    }
    #endregion
    #region CLASSI CONCRETE
    class Pizza : IPiatto
    {
        public string Descrizione()
        {
            return "Pizza";
        }
        public string prepara()
        {
            return "come da strategia";
        }
    }
    class Hamburger : IPiatto
    {
        public string Descrizione()
        {
            return "Hamburger";
        }
        public string prepara()
        {
            return "come da strategia";
        }
    }
    class Insalata : IPiatto
    {
        public string Descrizione()
        {
            return "Insalata";
        }
        public string prepara()
        {
            return "come da strategia";
        }
    }
    #endregion
    #region DECORATOR
    abstract class  IngredienteExtra : IPiatto
    {
        protected IPiatto piatto;
        public IngredienteExtra(IPiatto piatto)
        {
            this.piatto = piatto;
        }
        public virtual string Descrizione()
        {
            return piatto.Descrizione();
        }
        public virtual string prepara()
        {
            return piatto.prepara();
        }

    }
    class ConBacon : IngredienteExtra
    {
        public ConBacon(IPiatto piatto) : base(piatto) { }
        public override string Descrizione()
        {
            return base.Descrizione() +" con bacon";
        }
    }
    class ConFormaggio : IngredienteExtra
    {
        public ConFormaggio(IPiatto piatto) : base(piatto) { }
        public override string Descrizione()
        {
            return base.Descrizione() + " con formaggio";
        }
    }
    class ConSalsa : IngredienteExtra
    {
        public ConSalsa(IPiatto piatto) : base(piatto) { }
        public override string Descrizione()
        {
            return base.Descrizione() + " con salsa";
        }
    }
    #endregion
    #region FACTORY
    static class PiattoFactory
    {
        public static IPiatto CreaPiatto(string tipoPiatto)
        {
            switch (tipoPiatto.ToLower())
            {
                case "pizza":
                    return new Pizza();
                case "hamburger":
                    return new Hamburger();
                case "insalata":
                    return new Insalata();
                default:
                    throw new ArgumentException("Tipo di piatto non riconosciuto");
            }
        }


    }
    #endregion
    #region STRATEGIE CONCRETE
    class  Fritto : IpreprazioneStrategia
    {
        public string Prepara(string descrizione)
        {
            return $"Il piatto {descrizione} è stato preparato fritto.";
        }

    }
    class AlForno : IpreprazioneStrategia
    {
        public string Prepara(string descrizione)
        {
            return $"Il piatto {descrizione} è stato preparato al forno.";
        }
    }
    class AllaGriglia : IpreprazioneStrategia
    {
        public string Prepara(string descrizione)
        {
            return $"Il piatto {descrizione} è stato preparato alla griglia.";
        }
    }
    #endregion
    #region CHEF
    class Chef
    {
        private IpreprazioneStrategia strategia;
        public void SetStrategia(IpreprazioneStrategia strategia)
        {
            this.strategia = strategia;
        }
        public string PreparaPiatto(IPiatto piatto)
        {
            if (strategia == null)
            {
                throw new InvalidOperationException("Strategia di preparazione non impostata.");
            }
            return strategia.Prepara(piatto.Descrizione());
        }
    }
    #endregion
    #region PROGRAMMA
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("scegli Il tipo di piatto(pizza, hamburger, insalata): ");
            string tipoPiatto = Console.ReadLine();
            IPiatto piatto = PiattoFactory.CreaPiatto(tipoPiatto);
            if (piatto == null)
            {
                Console.WriteLine("Tipo di piatto non riconosciuto.");
                return;
            }
            Console.WriteLine("Vuoi aggiungere ingredienti extra? (s/n): ");
            string risposta = Console.ReadLine();
            if (risposta == "s")
            {
                Console.WriteLine("scegli Ingredienti extra(formaggio, salsa, bacon): ");
                foreach (var ingrediente in Console.ReadLine().Split(','))
                {
                    switch (ingrediente.Trim().ToLower())
                    {
                        case "formaggio":
                            piatto = new ConFormaggio(piatto);
                            break;
                        case "salsa":
                            piatto = new ConSalsa(piatto);
                            break;
                        case "bacon":
                            piatto = new ConBacon(piatto);
                            break;
                        default:
                            Console.WriteLine($"Ingrediente {ingrediente} non riconosciuto.");
                            break;
                    }
                }
            }
            Chef chef = new Chef();
            Console.WriteLine("scegli Il metodo di preparazione(fritto, al forno, alla griglia): ");
            string metodoPreparazione = Console.ReadLine();
            switch(metodoPreparazione.ToLower())
            {
                case "fritto":
                    chef.SetStrategia(new Fritto());
                    break;
                case "al forno":
                    chef.SetStrategia(new AlForno());
                    break;
                case "alla griglia":
                    chef.SetStrategia(new AllaGriglia());
                    break;
                default:
                    Console.WriteLine("Metodo di preparazione non riconosciuto.");
                    return;
            }
            string PiattoFinale = chef.PreparaPiatto(piatto);
            Console.WriteLine("Piato finale: " + PiattoFinale);
        }
    }
    #endregion
}
