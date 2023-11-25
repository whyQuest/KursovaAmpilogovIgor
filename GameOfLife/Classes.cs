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
    public class Queue
    {
        private SemaphoreSlim semaphore;
        private ConcurrentQueue<TaskCompletionSource<bool>> queue =
            new ConcurrentQueue<TaskCompletionSource<bool>>();
        public Queue(int initCount)
        {
            semaphore = new SemaphoreSlim(initCount);
        }
        public Queue(int initCount, int maxCount)
        {
            semaphore = new SemaphoreSlim(initCount, maxCount);
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

        public SimulateUPEventArgs(int rabbitLive, int WolfLive)
        {
            this.rabbitLive = rabbitLive;
            wolfLive = WolfLive;
        }
    }

    public class CellsUPEventArgs
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ElementG Elements { get; set; }

        public CellsUPEventArgs(int X, int Y, ElementG Element)
        {
            this.X = X; this.Y = Y; this.Elements = Element;
        }
    }

    public class Nets
    {
        private int IntervallCarrot;
        public Random rand { get; set; }
        public CellsOK[,] cells { get; set; }
        public int Lenght { get; set; }
        public int Height { get; set; }
        private Queue modelFinish = new Queue(1, 1);
        public bool simulateFinish { get; set; }
        private Queue numRabbitM = new Queue(1, 1);
        private Queue numWolfM = new Queue(1, 1);
        public int NumRabbit { get; set; }
        public int NumWolf { get; set; }
        public Nets(int lenght, int height)
        {
            cells = new CellsOK[lenght, height];
            Lenght = lenght;
            Height = height;
            
            for(int i=0; i<lenght; i++)
            {
                for (int j = 0; j < height; j++)
                    cells[i, j] = new CellsOK(i, j);
            }

            SimulatePause = true;
        }

        public void Init(int numbRabbit, int numbWolf, int numCarrot, int intervallCarrot)
        {
            NumRabbit = numbRabbit;
            NumWolf = numbWolf;
            simulateFinish = false;
            this.IntervallCarrot = intervallCarrot*1000;
            int[,] Combi = new int[Lenght * Height, 2];
            int[] CombiINDEX = new int[Lenght * Height];
            for (int x = 0; x < Lenght; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Combi[y + x * Height, 0] = x;
                    Combi[y + x * Height, 1] = y;
                    CombiINDEX[y + x * Height] = y + x * Height;
                }
            }
            rand = new Random();
            CombiINDEX = CombiINDEX.OrderBy(x => rand.Next()).ToArray();
            int i = 0;
            for (int j = 0; j < numbRabbit; j++)
            {
                cells[Combi[CombiINDEX[i], 0], Combi[CombiINDEX[i], 1]].Element = new Rabbit(Combi[CombiINDEX[i], 0], Combi[CombiINDEX[i], 1], this);
                i++;
            }
            for (int j = 0; j < numbWolf; j++)
            {
                cells[Combi[CombiINDEX[i], 0], Combi[CombiINDEX[i], 1]].Element = new Wolf(Combi[CombiINDEX[i], 0], Combi[CombiINDEX[i], 1], this);
                i++;
            }
            for (int j = 0; j < numCarrot; j++)
            {
                cells[Combi[CombiINDEX[i], 0], Combi[CombiINDEX[i], 1]].Element = new Carrot(Combi[CombiINDEX[i], 0], Combi[CombiINDEX[i], 1], this);
                i++;
            }

            foreach(CellsOK c in cells)
            {
                (c.Element as AnimalSett)?.DiminuisciVita();
                (c.Element as AnimalSett)?.Muovi();
            }

            PushCarrot();
        }

        public async Task CountRabbitMinus()
        {
            await numRabbitM.WaitAsync();

            NumRabbit--;

            if(NumRabbit == 0)
            {
                await modelFinish.WaitAsync();
                simulateFinish = true;
                FinSimulate(new SimulateUPEventArgs(NumRabbit, NumWolf));
                modelFinish.Release();
            }

            numRabbitM.Release();
        }

        public async Task CountWolfMinus()
        {
            await numWolfM.WaitAsync();

            NumWolf--;

            if (NumWolf == 0)
            {
                await modelFinish.WaitAsync();
                simulateFinish = true;
                FinSimulate(new SimulateUPEventArgs(NumRabbit, NumWolf));
                modelFinish.Release();
            }

            numWolfM.Release();
        }

        public async void PushCarrot()
        {
            while (true)
            {
                if (simulateFinish)
                {
                    return;
                }

                if (!SimulatePause)
                {
                    int x, y;

                    bool zeroCells = false;
                    while (!zeroCells)
                    {
                        x = rand.Next(0, cells.GetLength(0));
                        y = rand.Next(0, cells.GetLength(1));

                        await cells[x, y].giveAsync();
                        if (cells[x, y].Element == null)
                        {
                            zeroCells = true;
                            cells[x, y].Element = new Carrot(x, y, this);
                            cells[x, y].cellsUP();
                        }
                        try
                        {
                            cells[x, y].ReleaseAcc();
                        }
                        catch(Exception e)
                        {

                        }
                    }
                }

                await Task.Delay(IntervallCarrot);
            }
        }

        public bool SimulatePause { get; set; }
        public void Play()
        {
            SimulatePause = false;
        }

        public void Pause()
        {
            SimulatePause = true;
        }

        public void Stop()
        {
            simulateFinish = true;
            FinSimulate(new SimulateUPEventArgs(NumRabbit, NumWolf));
        }

        private void FinSimulate(SimulateUPEventArgs e)
        {
            Simulate?.Invoke(this, e);
        }

        public event EventHandler<SimulateUPEventArgs> Simulate;

    }

    public class CellsOK
    { 
        private Queue check = new Queue(1, 1);
        public ElementG Element { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public CellsOK(int X, int Y)
        {
            this.X = X; this.Y = Y;
        }

        public event EventHandler<CellsUPEventArgs> CellsUP;

        protected void cellsUP2(CellsUPEventArgs e)
        {
            CellsUP?.Invoke(this, e);
        }
        public void cellsUP()
        {
            CellsUPEventArgs e = new CellsUPEventArgs(X, Y, Element);
            cellsUP2(e);
        }

        public async Task giveAsync()
        {
            await check.WaitAsync();
        }

        public void ReleaseAcc()
        {
            check.Release();
        }


        public void Elimina()
        {
            Element = null;
            cellsUP();
        }
    }

    public abstract class ElementG
    {
        protected Nets net;
        public ElementG(int X, int Y, Nets Net)
        {
            this.X = X;
            this.Y = Y;
            this.net = Net;
        }
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Information
    {
        public bool Accsses { get; set; }
        public bool newPlace { get; set; }
        public bool eatSearch { get; set; }
        public int newX { get; set; }
        public int newY { get; set; }
        public bool trueok { get; set; }

        public Information(bool accessoOttenuto, bool nuovaPosizioneTrovata, bool ciboTrovato,
            int x, int y, bool eliminato)
        {
            Accsses = accessoOttenuto;
            newPlace = nuovaPosizioneTrovata;
            eatSearch = ciboTrovato;
            newX = x;
            newY = y;
            trueok = eliminato;
        }
    }
    public abstract class AnimalSett : ElementG
    {
        public Queue vitaMutex = new Queue(1, 1);
        public int Vita { get; set; }
        public AnimalSett(int X, int Y, Nets griglia) : base(X, Y, griglia) { Vita = 10; }

        public void AnalizzaCella(int xCella, int yCella, ref int newX, ref int newY, ref bool NuovaPosizioneTrovata, ref bool CiboTrovato)
        {
            // controlla se nella cella che si sta analizzando c'è del cibo
            bool ciboPresenteNellaCella = CiboPresenteNellaCella(net.cells[xCella, yCella].Element);

            // se è già stata trovata una cella vicina con del cibo e la cella in analisi contiene del cibo
            // ha una possibilità del 50% di rendere la cella in analisi la nuova cella verso cui spostarsi
            if (CiboTrovato && ciboPresenteNellaCella && net.rand.Next(0, 100) < 50)
            {
                // non occorre più avere l'accesso della cella precedente
                net.cells[newX, newY].ReleaseAcc();

                newX = xCella;
                newY = yCella;
            }

            // se non è ancora stata trovata una cella vicina con del cibo e la cella in analisi contiene del cibo
            // rendi la cella in analisi la nuova cella verso cui spostarsi
            else if (!CiboTrovato && ciboPresenteNellaCella)
            {
                // se era già stata selezionata un'altra cella come prossima cella, rilascia l'accesso
                if (NuovaPosizioneTrovata)
                    net.cells[newX, newY].ReleaseAcc();

                newX = xCella;
                newY = yCella;
                CiboTrovato = true;
                NuovaPosizioneTrovata = true;
            }

            // se non è stato trovato ancora cibo, la cella in analisi non ne contiene ed è vuota, 
            // segna la cella in analisi come prossima cella se non è stata trovata nessuna nuova posizione
            // oppure segnala con il 50% di probabilità se è già stata trovata un'altra cella verso cui muoversi
            else if (!CiboTrovato && (!NuovaPosizioneTrovata || net.rand.Next(0, 100) < 50) && net.cells[xCella, yCella].Element == null)
            {
                // se era già stata selezionata un'altra cella come prossima cella, rilascia l'accesso
                if (NuovaPosizioneTrovata)
                    net.cells[newX, newY].ReleaseAcc();

                newX = xCella;
                newY = yCella;
                NuovaPosizioneTrovata = true;
            }

            // nel caso la cella in analisi non sia stata segnata come prossima cella, il suo accesso si può rilasciare
            if (newX != xCella || newY != yCella)
                net.cells[xCella, yCella].ReleaseAcc();
        }
        public async void Muovi()
        {
            while (true)
            {
                if (net.simulateFinish)
                    return;

                if (!net.SimulatePause)
                {
                    if (Y > 0)
                        await net.cells[X, Y - 1].giveAsync();
                    if (X > 0)
                        await net.cells[X - 1, Y].giveAsync();


                    await net.cells[X, Y].giveAsync();

                    var animale = net.cells[X, Y].Element as AnimalSett;
                    if (animale != null)
                    {
                        if(animale.Vita == 0)
                        {
                            net.cells[X, Y].Elimina();
                            await DiminuisciContatore();
                        }
                    }

                    if (net.cells[X, Y].Element != this)
                    {
                        net.cells[X, Y].ReleaseAcc();
                        
                        if (Y > 0)
                            net.cells[X, Y - 1].ReleaseAcc();
                        if (X > 0)
                            net.cells[X - 1, Y].ReleaseAcc();

                        return;
                    }

                    if (X < net.cells.GetLength(0) - 1)
                        await net.cells[X + 1, Y].giveAsync();
                    if (Y < net.cells.GetLength(1) - 1)
                        await net.cells[X, Y + 1].giveAsync();


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

                    if (X < net.cells.GetLength(0) - 1)
                    {
                        AnalizzaCella(X + 1, Y, ref newX, ref newY, ref NuovaPosizioneTrovata, ref CiboTrovato);
                    }

                    if (Y < net.cells.GetLength(1) - 1)
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
                        net.cells[X, Y].Element = this;
                        net.cells[X, Y].cellsUP();
                        net.cells[oldX, oldY].Elimina();
                        net.cells[oldX, oldY].ReleaseAcc();
                        net.cells[X, Y].ReleaseAcc();
                    }
                    else
                    {
                        net.cells[X, Y].ReleaseAcc();
                    }
                }   

                await Task.Delay(net.rand.Next(500, 1000));
            }
        }

        public abstract Task DiminuisciContatoreCibo();

        public async void DiminuisciVita()
        {
            while (true)
            {
                if (net.simulateFinish)
                    return;

                if (!net.SimulatePause)
                {
                    await vitaMutex.WaitAsync();
                    Vita--;
                    vitaMutex.Release();
                    int a = X, b = Y;
                    net.cells[X, Y].cellsUP();

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
            await net.CountRabbitMinus();
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
            await net.CountWolfMinus();
        }

        public override async Task DiminuisciContatoreCibo()
        {
            await net.CountRabbitMinus();
        }
    }

    public class Carrot : ElementG
    {
        public Carrot(int X, int Y, Nets griglia) : base(X, Y, griglia) { }
    }
}
