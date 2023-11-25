using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Concurrent;

namespace Ecosystem
{
    public class SemaphoreQueue
    {
        private SemaphoreSlim semaphore;
        private ConcurrentQueue<TaskCompletionSource<bool>> queue =
            new ConcurrentQueue<TaskCompletionSource<bool>>();
        public SemaphoreQueue(int initialCount)
        {
            semaphore = new SemaphoreSlim(initialCount);
        }
        public SemaphoreQueue(int initialCount, int maxCount)
        {
            semaphore = new SemaphoreSlim(initialCount, maxCount);
        }
        public void Wait()
        {
            WaitAsync().Wait();
        }
        public Task WaitAsync()
        {
            var tcs = new TaskCompletionSource<bool>();
            queue.Enqueue(tcs);
            semaphore.WaitAsync().ContinueWith(t =>
            {
                TaskCompletionSource<bool> popped;
                if (queue.TryDequeue(out popped))
                    popped.SetResult(true);
            });
            return tcs.Task;
        }
        public void Release()
        {
            semaphore.Release();
        }
    }

    public class SimulateUPEventArgs : EventArgs
    {
        public int rabbitLive { get; set; }
        public int wolfLive { get; set; }

        public SimulateUPEventArgs(int conigliRimasti, int lupiRimasti)
        {
            rabbitLive = conigliRimasti;
            wolfLive = lupiRimasti;
        }
    }

    public class CellsUPEventArgs
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ElementG Elements { get; set; }

        public CellsUPEventArgs(int X, int Y, ElementG Elemento)
        {
            this.X = X; this.Y = Y; this.Elements = Elemento;
        }
    }

    public class Nets
    {
        private int intervalloCarote;
        public Random rnd { get; set; }
        public CCella[,] cells { get; set; }
        public int Larghezza { get; set; }
        public int Altezza { get; set; }
        private SemaphoreQueue simulazioneFinitaMutex = new SemaphoreQueue(1, 1);
        public bool simulateFinish { get; set; }
        private SemaphoreQueue numConigliMutex = new SemaphoreQueue(1, 1);
        private SemaphoreQueue numLupiMutex = new SemaphoreQueue(1, 1);
        public int NumConigli { get; set; }
        public int NumLupi { get; set; }
        public Nets(int larghezza, int altezza)
        {
            cells = new CCella[larghezza, altezza];
            Larghezza = larghezza;
            Altezza = altezza;
            
            for(int i=0; i<larghezza; i++)
            {
                for (int j = 0; j < altezza; j++)
                    cells[i, j] = new CCella(i, j);
            }

            SimulazioneInPausa = true;
        }

        public void Init(int numConigli, int numLupi, int numCarote, int intervalloCarote)
        {
            NumConigli = numConigli;
            NumLupi = numLupi;
            simulateFinish = false;
            this.intervalloCarote = intervalloCarote*1000;
            int[,] Combinazioni = new int[Larghezza * Altezza, 2];
            int[] indiciCombinazioni = new int[Larghezza * Altezza];
            for (int x = 0; x < Larghezza; x++)
            {
                for (int y = 0; y < Altezza; y++)
                {
                    Combinazioni[y + x * Altezza, 0] = x;
                    Combinazioni[y + x * Altezza, 1] = y;
                    indiciCombinazioni[y + x * Altezza] = y + x * Altezza;
                }
            }
            rnd = new Random();
            indiciCombinazioni = indiciCombinazioni.OrderBy(x => rnd.Next()).ToArray();
            int i = 0;
            for (int j = 0; j < numConigli; j++)
            {
                cells[Combinazioni[indiciCombinazioni[i], 0], Combinazioni[indiciCombinazioni[i], 1]].Element = new Rabbit(Combinazioni[indiciCombinazioni[i], 0], Combinazioni[indiciCombinazioni[i], 1], this);
                i++;
            }
            for (int j = 0; j < numLupi; j++)
            {
                cells[Combinazioni[indiciCombinazioni[i], 0], Combinazioni[indiciCombinazioni[i], 1]].Element = new Wolf(Combinazioni[indiciCombinazioni[i], 0], Combinazioni[indiciCombinazioni[i], 1], this);
                i++;
            }
            for (int j = 0; j < numCarote; j++)
            {
                cells[Combinazioni[indiciCombinazioni[i], 0], Combinazioni[indiciCombinazioni[i], 1]].Element = new Carrot(Combinazioni[indiciCombinazioni[i], 0], Combinazioni[indiciCombinazioni[i], 1], this);
                i++;
            }

            foreach(CCella c in cells)
            {
                (c.Element as AnimalSett)?.DiminuisciVita();
                (c.Element as AnimalSett)?.Muovi();
            }

            InserisciCarote();
        }

        public async Task DiminuisciNumConigli()
        {
            await numConigliMutex.WaitAsync();

            NumConigli--;

            if(NumConigli == 0)
            {
                await simulazioneFinitaMutex.WaitAsync();
                simulateFinish = true;
                OnFineSimulazione(new SimulateUPEventArgs(NumConigli, NumLupi));
                simulazioneFinitaMutex.Release();
            }

            numConigliMutex.Release();
        }

        public async Task DiminuisciNumLupi()
        {
            await numLupiMutex.WaitAsync();

            NumLupi--;

            if (NumLupi == 0)
            {
                await simulazioneFinitaMutex.WaitAsync();
                simulateFinish = true;
                OnFineSimulazione(new SimulateUPEventArgs(NumConigli, NumLupi));
                simulazioneFinitaMutex.Release();
            }

            numLupiMutex.Release();
        }

        public async void InserisciCarote()
        {
            while (true)
            {
                if (simulateFinish)
                {
                    return;
                }

                if (!SimulazioneInPausa)
                {
                    int x, y;

                    bool cellaVuotaTrovata = false;
                    while (!cellaVuotaTrovata)
                    {
                        x = rnd.Next(0, cells.GetLength(0));
                        y = rnd.Next(0, cells.GetLength(1));

                        await cells[x, y].OttieniAccessoAsync();
                        if (cells[x, y].Element == null)
                        {
                            cellaVuotaTrovata = true;
                            cells[x, y].Element = new Carrot(x, y, this);
                            cells[x, y].AggiornaCella();
                        }
                        try
                        {
                            cells[x, y].RilasciaAccesso();
                        }
                        catch(Exception e)
                        {

                        }
                    }
                }

                await Task.Delay(intervalloCarote);
            }
        }

        public bool SimulazioneInPausa { get; set; }
        public void Play()
        {
            SimulazioneInPausa = false;
        }

        public void Pause()
        {
            SimulazioneInPausa = true;
        }

        public void Stop()
        {
            simulateFinish = true;
            OnFineSimulazione(new SimulateUPEventArgs(NumConigli, NumLupi));
        }

        private void OnFineSimulazione(SimulateUPEventArgs e)
        {
            Simulate?.Invoke(this, e);
        }

        public event EventHandler<SimulateUPEventArgs> Simulate;

    }

    public class CCella
    { 
        private SemaphoreQueue semaforo = new SemaphoreQueue(1, 1);
        public ElementG Element { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public CCella(int X, int Y)
        {
            this.X = X; this.Y = Y;
        }

        public event EventHandler<CellsUPEventArgs> CellsUP;

        protected void OnCellaAggiornata(CellsUPEventArgs e)
        {
            CellsUP?.Invoke(this, e);
        }
        public void AggiornaCella()
        {
            CellsUPEventArgs e = new CellsUPEventArgs(X, Y, Element);
            OnCellaAggiornata(e);
        }

        public async Task OttieniAccessoAsync()
        {
            await semaforo.WaitAsync();
        }

        public void RilasciaAccesso()
        {
            semaforo.Release();
        }


        public void Elimina()
        {
            Element = null;
            AggiornaCella();
        }
    }

    public abstract class ElementG
    {
        protected Nets griglia;
        public ElementG(int X, int Y, Nets griglia)
        {
            this.X = X;
            this.Y = Y;
            this.griglia = griglia;
        }
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class CInformazioni
    {
        public bool AccessoOttenuto { get; set; }
        public bool NuovaPosizioneTrovata { get; set; }
        public bool CiboTrovato { get; set; }
        public int newX { get; set; }
        public int newY { get; set; }
        public bool Eliminato { get; set; }

        public CInformazioni(bool accessoOttenuto, bool nuovaPosizioneTrovata, bool ciboTrovato,
            int x, int y, bool eliminato)
        {
            AccessoOttenuto = accessoOttenuto;
            NuovaPosizioneTrovata = nuovaPosizioneTrovata;
            CiboTrovato = ciboTrovato;
            newX = x;
            newY = y;
            Eliminato = eliminato;
        }
    }
    public abstract class AnimalSett : ElementG
    {
        public SemaphoreQueue vitaMutex = new SemaphoreQueue(1, 1);
        public int Vita { get; set; }
        public AnimalSett(int X, int Y, Nets griglia) : base(X, Y, griglia) { Vita = 10; }

        public void AnalizzaCella(int xCella, int yCella, ref int newX, ref int newY, ref bool NuovaPosizioneTrovata, ref bool CiboTrovato)
        {
            // controlla se nella cella che si sta analizzando c'è del cibo
            bool ciboPresenteNellaCella = CiboPresenteNellaCella(griglia.cells[xCella, yCella].Element);

            // se è già stata trovata una cella vicina con del cibo e la cella in analisi contiene del cibo
            // ha una possibilità del 50% di rendere la cella in analisi la nuova cella verso cui spostarsi
            if (CiboTrovato && ciboPresenteNellaCella && griglia.rnd.Next(0, 100) < 50)
            {
                // non occorre più avere l'accesso della cella precedente
                griglia.cells[newX, newY].RilasciaAccesso();

                newX = xCella;
                newY = yCella;
            }

            // se non è ancora stata trovata una cella vicina con del cibo e la cella in analisi contiene del cibo
            // rendi la cella in analisi la nuova cella verso cui spostarsi
            else if (!CiboTrovato && ciboPresenteNellaCella)
            {
                // se era già stata selezionata un'altra cella come prossima cella, rilascia l'accesso
                if (NuovaPosizioneTrovata)
                    griglia.cells[newX, newY].RilasciaAccesso();

                newX = xCella;
                newY = yCella;
                CiboTrovato = true;
                NuovaPosizioneTrovata = true;
            }

            // se non è stato trovato ancora cibo, la cella in analisi non ne contiene ed è vuota, 
            // segna la cella in analisi come prossima cella se non è stata trovata nessuna nuova posizione
            // oppure segnala con il 50% di probabilità se è già stata trovata un'altra cella verso cui muoversi
            else if (!CiboTrovato && (!NuovaPosizioneTrovata || griglia.rnd.Next(0, 100) < 50) && griglia.cells[xCella, yCella].Element == null)
            {
                // se era già stata selezionata un'altra cella come prossima cella, rilascia l'accesso
                if (NuovaPosizioneTrovata)
                    griglia.cells[newX, newY].RilasciaAccesso();

                newX = xCella;
                newY = yCella;
                NuovaPosizioneTrovata = true;
            }

            // nel caso la cella in analisi non sia stata segnata come prossima cella, il suo accesso si può rilasciare
            if (newX != xCella || newY != yCella)
                griglia.cells[xCella, yCella].RilasciaAccesso();
        }
        public async void Muovi()
        {
            while (true)
            {
                if (griglia.simulateFinish)
                    return;

                if (!griglia.SimulazioneInPausa)
                {
                    if (Y > 0)
                        await griglia.cells[X, Y - 1].OttieniAccessoAsync();
                    if (X > 0)
                        await griglia.cells[X - 1, Y].OttieniAccessoAsync();


                    await griglia.cells[X, Y].OttieniAccessoAsync();

                    var animale = griglia.cells[X, Y].Element as AnimalSett;
                    if (animale != null)
                    {
                        if(animale.Vita == 0)
                        {
                            griglia.cells[X, Y].Elimina();
                            await DiminuisciContatore();
                        }
                    }

                    if (griglia.cells[X, Y].Element != this)
                    {
                        griglia.cells[X, Y].RilasciaAccesso();
                        
                        if (Y > 0)
                            griglia.cells[X, Y - 1].RilasciaAccesso();
                        if (X > 0)
                            griglia.cells[X - 1, Y].RilasciaAccesso();

                        return;
                    }

                    if (X < griglia.cells.GetLength(0) - 1)
                        await griglia.cells[X + 1, Y].OttieniAccessoAsync();
                    if (Y < griglia.cells.GetLength(1) - 1)
                        await griglia.cells[X, Y + 1].OttieniAccessoAsync();


                    int newX = -1, newY = -1;
                    bool CiboTrovato = false, NuovaPosizioneTrovata = false;


                    if (Y > 0)
                    {
                        AnalizzaCella(X, Y - 1, ref newX, ref newY, ref NuovaPosizioneTrovata, ref CiboTrovato);
                    }

                    if (X > 0)
                    {
                        AnalizzaCella(X - 1, Y, ref newX, ref newY, ref NuovaPosizioneTrovata, ref CiboTrovato);
                    }

                    if (X < griglia.cells.GetLength(0) - 1)
                    {
                        AnalizzaCella(X + 1, Y, ref newX, ref newY, ref NuovaPosizioneTrovata, ref CiboTrovato);
                    }

                    if (Y < griglia.cells.GetLength(1) - 1)
                    {
                        AnalizzaCella(X, Y + 1, ref newX, ref newY, ref NuovaPosizioneTrovata, ref CiboTrovato);
                    }

                    

                    if (NuovaPosizioneTrovata)
                    {
                        int oldX, oldY;
                        oldX = X;
                        oldY = Y;
                        X = newX;
                        Y = newY;
                        if (CiboTrovato)
                        {
                            await vitaMutex.WaitAsync();
                            Vita = 10;
                            vitaMutex.Release();
                            await DiminuisciContatoreCibo();
                        }
                        griglia.cells[X, Y].Element = this;
                        griglia.cells[X, Y].AggiornaCella();
                        griglia.cells[oldX, oldY].Elimina();
                        griglia.cells[oldX, oldY].RilasciaAccesso();
                        griglia.cells[X, Y].RilasciaAccesso();
                    }
                    else
                    {
                        griglia.cells[X, Y].RilasciaAccesso();
                    }
                }   

                await Task.Delay(griglia.rnd.Next(500, 1000));
            }
        }

        public abstract Task DiminuisciContatoreCibo();

        public async void DiminuisciVita()
        {
            while (true)
            {
                if (griglia.simulateFinish)
                    return;

                if (!griglia.SimulazioneInPausa)
                {
                    await vitaMutex.WaitAsync();
                    Vita--;
                    vitaMutex.Release();
                    int a = X, b = Y;
                    griglia.cells[X, Y].AggiornaCella();

                    if (Vita == 0)
                    {
                        return;
                    }
                }
                
                await Task.Delay(1000);
            }
        }

        public abstract Task DiminuisciContatore();

        public abstract bool CiboPresenteNellaCella(ElementG elemento);
    }
    public class Rabbit : AnimalSett
    {
        public Rabbit(int X, int Y, Nets griglia) : base(X, Y, griglia) { }

        public override bool CiboPresenteNellaCella(ElementG elemento)
        {
            return elemento is Carrot;
        }

        public override async Task DiminuisciContatore()
        {
            await griglia.DiminuisciNumConigli();
        }

        public override async Task DiminuisciContatoreCibo()
        {
            await Task.Delay(0);
        }

    }

    public class Wolf : AnimalSett
    {
        public Wolf(int X, int Y, Nets griglia) : base(X, Y, griglia) { }

        public override bool CiboPresenteNellaCella(ElementG elemento)
        {
            return elemento is Rabbit;
        }

        public override async Task DiminuisciContatore()
        {
            await griglia.DiminuisciNumLupi();
        }

        public override async Task DiminuisciContatoreCibo()
        {
            await griglia.DiminuisciNumConigli();
        }
    }

    public class Carrot : ElementG
    {
        public Carrot(int X, int Y, Nets griglia) : base(X, Y, griglia) { }
    }
}
