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
                (c.Element as AnimalSett)?.minusLife();
                (c.Element as AnimalSett)?.Move();
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


        public void Delete()
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

        public Information(bool accsses, bool newplace, bool eatsearch, int x, int y, bool trueok1)
        {
            Accsses = accsses;
            newPlace = newplace;
            eatSearch = eatsearch;
            newX = x;
            newY = y;
            trueok = trueok1;
        }
    }
    public abstract class AnimalSett : ElementG
    {
        public Queue liveAnim = new Queue(1, 1);
        public int life { get; set; }
        public AnimalSett(int X, int Y, Nets net) : base(X, Y, net) { life = 10; }

        public void Analiz(int x, int y, ref int newX, ref int newY, ref bool newPlaceTRUE, ref bool eatTRUE1)
        {
            // проверяем, есть ли еда в анализируемой ячейке
            bool eatTRUE = eatrtue(net.cells[x, y].Element);

            // если соседняя ячейка с едой уже найдена и анализируемая ячейка содержит еду
            // имеет 50% шанс сделать анализируемую ячейку новой ячейкой для перемещения
            if (eatTRUE1 && eatTRUE && net.rand.Next(0, 100) < 50)
            {
                // Вам больше не нужен доступ к предыдущей ячейке
                net.cells[newX, newY].ReleaseAcc();

                newX = x;
                newY = y;
            }

            // если ближайшая ячейка с едой еще не найдена и анализируемая ячейка содержит еду
            // делаем анализируемую ячейку новой ячейкой для перемещения
            else if (!eatTRUE1 && eatTRUE)
            {
                // если в качестве следующей ячейки уже выбрана другая ячейка, открываем доступ
                if (newPlaceTRUE)
                    net.cells[newX, newY].ReleaseAcc();

                newX = x;
                newY = y;
                eatTRUE1 = true;
                newPlaceTRUE = true;
            }

            // если еда еще не найдена, анализируемая ячейка ее не содержит и пуста,
            // помечаем анализируемую ячейку как следующую, если новая позиция не найдена
            // или сообщить с вероятностью 50%, если уже найдена другая ячейка, в которую можно перейти
            else if (!eatTRUE1 && (!newPlaceTRUE || net.rand.Next(0, 100) < 50) && net.cells[x, y].Element == null)
            {
                // если в качестве следующей ячейки уже выбрана другая ячейка, открываем доступ
                if (newPlaceTRUE)
                    net.cells[newX, newY].ReleaseAcc();

                newX = x;
                newY = y;
                newPlaceTRUE = true;
            }

            // если анализируемая ячейка не помечена как следующая, доступ к ней можно разблокировать
            if (newX != x || newY != y)
                net.cells[x, y].ReleaseAcc();
        }
        public async void Move()
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
                        if(animale.life == 0)
                        {
                            net.cells[X, Y].Delete();
                            await minusC4ET();
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
                    bool eatTRUE = false, newPlaceOK = false;


                    if (Y > 0)
                    {
                        Analiz(X, Y - 1, ref newX, ref newY, ref newPlaceOK, ref eatTRUE);
                    }

                    if (X > 0)
                    {
                        Analiz(X - 1, Y, ref newX, ref newY, ref newPlaceOK, ref eatTRUE);
                    }

                    if (X < net.cells.GetLength(0) - 1)
                    {
                        Analiz(X + 1, Y, ref newX, ref newY, ref newPlaceOK, ref eatTRUE);
                    }

                    if (Y < net.cells.GetLength(1) - 1)
                    {
                        Analiz(X, Y + 1, ref newX, ref newY, ref newPlaceOK, ref eatTRUE);
                    }

                    

                    if (newPlaceOK)
                    {
                        int oldX, oldY;
                        oldX = X;
                        oldY = Y;
                        X = newX;
                        Y = newY;
                        if (eatTRUE)
                        {
                            await liveAnim.WaitAsync();
                            life = 10;
                            liveAnim.Release();
                            await minusC4ETeat();
                        }
                        net.cells[X, Y].Element = this;
                        net.cells[X, Y].cellsUP();
                        net.cells[oldX, oldY].Delete();
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

        public abstract Task minusC4ETeat();

        public async void minusLife()
        {
            while (true)
            {
                if (net.simulateFinish)
                    return;

                if (!net.SimulatePause)
                {
                    await liveAnim.WaitAsync();
                    life--;
                    liveAnim.Release();
                    int a = X, b = Y;
                    net.cells[X, Y].cellsUP();

                    if (life == 0)
                    {
                        return;
                    }
                }
                
                await Task.Delay(1000);
            }
        }

        public abstract Task minusC4ET();

        public abstract bool eatrtue(ElementG element);
    }
    public class Rabbit : AnimalSett
    {
        public Rabbit(int X, int Y, Nets net) : base(X, Y, net) { }

        public override bool eatrtue(ElementG element)
        {
            return element is Carrot;
        }

        public override async Task minusC4ET()
        {
            await net.CountRabbitMinus();
        }

        public override async Task minusC4ETeat()
        {
            await Task.Delay(0);
        }

    }

    public class Wolf : AnimalSett
    {
        public Wolf(int X, int Y, Nets net) : base(X, Y, net) { }

        public override bool eatrtue(ElementG element)
        {
            return element is Rabbit;
        }

        public override async Task minusC4ET()
        {
            await net.CountWolfMinus();
        }

        public override async Task minusC4ETeat()
        {
            await net.CountRabbitMinus();
        }
    }

    public class Carrot : ElementG
    {
        public Carrot(int X, int Y, Nets net) : base(X, Y, net) { }
    }
}
