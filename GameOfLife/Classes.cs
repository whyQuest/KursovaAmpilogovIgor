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

    //клас,що реалізує механізм черги для асинхронної обробки задач за допомогою семафора.
    public class Queue
    {
        //об'єкт який використовується для обмеження кількості одночасних доступів
        private SemaphoreSlim semaphore;
        //об'єкт черги, який використовується для зберігання завдань, що очікують на доступ до ресурсу.
        private ConcurrentQueue<TaskCompletionSource<bool>> queue =
            new ConcurrentQueue<TaskCompletionSource<bool>>();
        public Queue(int initCount)
        {
            semaphore = new SemaphoreSlim(initCount);
        }
        //Синхронний метод очікування на доступ до ресурсу.
        public Queue(int initCount, int maxCount)
        {
            semaphore = new SemaphoreSlim(initCount, maxCount);
        }
        public void Wait()
        {
            WaitAsync().Wait();
        }
        //Асинхронний метод очікування на доступ до ресурсу.
        //Створює об'єкт завдання,
        //додає його до черги та повертає завдання для асинхронного очікування.
        public Task WaitAsync()
        {
            //створюємо об'єкт наших завдань
            var tcs = new TaskCompletionSource<bool>();
            //додаємо наш об'єкт до черги
            queue.Enqueue(tcs);
            //викликаємо асинхронний метод. якщо все ок викликаємо ContinueWith, який виконується після завершення очікування.
            semaphore.WaitAsync().ContinueWith(t =>
            {
                //обираємо об'єкт з нашої черги
                TaskCompletionSource<bool> popped;
                //якщо все ок, даний об'єкт (завдання) отримує доступ до ресурсу
                if (queue.TryDequeue(out popped))
                    popped.SetResult(true);
            });
            return tcs.Task;
        }

        //вивільняємо ресурси черги
        public void Release()
        {
            semaphore.Release();
        }
    }

    //базовий класс для наших елементів на сітці
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


    //клас який містить інформацію про ячейку куди може переміститись об'єкт, та де знаходится їжа
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

        // створюємо сітку гри, де будуть додані кролики, вовки та морква
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
            //встановлюємо початкові значення кількості наших тварин
            NumRabbit = numbRabbit;
            NumWolf = numbWolf;
            simulateFinish = false;

            //встановлюємо інтервал спавну моркви на полі
            this.IntervallCarrot = intervallCarrot*1000;

            //створюємо координати нашої сітки
            int[,] Combi = new int[Lenght * Height, 2];
            int[] CombiINDEX = new int[Lenght * Height];
            
            //перемішуємо індекси масиву
            for (int x = 0; x < Lenght; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Combi[y + x * Height, 0] = x;
                    Combi[y + x * Height, 1] = y;
                    CombiINDEX[y + x * Height] = y + x * Height;
                }
            }

            //цикли, які рандомним чином визначають де будуть розташовуватись кролики, вовки, та морква
            rand = new Random();
            CombiINDEX = CombiINDEX.OrderBy(x => rand.Next()).ToArray();
            int i = 0;
            for (int j = 0; j < numbRabbit; j++)
            {
                //цикл, в якому ми передаємо індекси нашої сітки(рандомні), і в ці індекси додаємо наші об'єкти(вовк, кролик). Combi - є нашою сіткою,
                //CombiINDEX - містить індекси нашої сітки, яка представлена у вигляді двовимірного масиву
                //i++ потрібен для того, щоб в наступній ітерації використовувати інши координати сітки
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

            //циклом проходимо по всій сітці, і визначаємо які з координат містять тварин, а які ні. Наділяємо їх можливістю рухатись та вмирати
            foreach(CellsOK c in cells)
            {
                (c.Element as AnimalSett)?.minusLife();
                (c.Element as AnimalSett)?.Move();
            }

            //спавнимо моркву
            PushCarrot();
        }

        //якщо кролик вмирає
        public async Task CountRabbitMinus()
        {
            await numRabbitM.WaitAsync();

            //видалємо кролика
            NumRabbit--;

            //якщо кроликів залишилось 0, гра завершується
            if(NumRabbit == 0)
            {
                await modelFinish.WaitAsync();
                simulateFinish = true;
                FinSimulate(new SimulateUPEventArgs(NumRabbit, NumWolf));
                modelFinish.Release();
            }

            numRabbitM.Release();
        }

        //якщо вовк вмирає
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

        //спавн моркви
        public async void PushCarrot()
        {
            while (true)
            {
                //якщо гра закінчена, морква перестає спавнитись
                if (simulateFinish)
                {
                    return;
                }

                //якщо симуляція не на паузі
                if (!SimulatePause)
                {
                    int x, y;

                    bool zeroCells = false;

                    while (!zeroCells)
                    {
                        //обираємо рандомно де буде спавнитись морква
                        x = rand.Next(0, cells.GetLength(0));
                        y = rand.Next(0, cells.GetLength(1));

                        await cells[x, y].giveAsync();
                        //перевірка, чи немає у ячейці об'єкта
                        if (cells[x, y].Element == null)
                        {
                            zeroCells = true;
                            //створення нового об'єкту морква, переміщення її на сітку гри
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

                //затримуємо виконная циклу, на наш введений інтервал часу
                await Task.Delay(IntervallCarrot);
            }
        }

        public bool SimulatePause { get; set; }

        //якщо гра продовжена, знімаємо її з паузи
        public void Play()
        {
            SimulatePause = false;
        }

        //гра на паузі
        public void Pause()
        {
            SimulatePause = true;
        }

        //якщо гра завершується передаємо кількість вовків і кроликів для виводу
        public void Stop()
        {
            simulateFinish = true;
            FinSimulate(new SimulateUPEventArgs(NumRabbit, NumWolf));
        }


        //завершуємо гру, викликаємо делегат
        private void FinSimulate(SimulateUPEventArgs e)
        {
            Simulate?.Invoke(this, e);
        }

        //делегат
        public event EventHandler<SimulateUPEventArgs> Simulate;

    }

    public class CellsOK
    { 
        private Queue check = new Queue(1, 1);
        public ElementG Element { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        //ініціалізуємо наші ячейки
        public CellsOK(int X, int Y)
        {
            this.X = X; this.Y = Y;
        }

        public event EventHandler<CellsUPEventArgs> CellsUP;

        //за допомогою цих двух методів повідомляємо, про зміну стану або вмісту ячейки
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


        //видаляємо елемент з ячейки, і оновлюємо вміст
        public void Delete()
        {
            Element = null;
            cellsUP();
        }
    }

    //клас з налаштуваннями об'єктів
    public abstract class AnimalSett : ElementG
    {
        public Queue liveAnim = new Queue(1, 1);
        public int life { get; set; }
        public AnimalSett(int X, int Y, Nets net) : base(X, Y, net) { life = 10; }

        public void Analiz(int x, int y, ref int newX, ref int newY, ref bool newPlaceTRUE, ref bool eatTRUE1)
        {
            // перевіряємо,чи є їжа в ячейці, яку ми аналізуємо
            bool eatTRUE = eatrtue(net.cells[x, y].Element);

            // якщо сусідня ячейка уже знайдена и ячейка містить їжу
            // є шанс в 50%, що об'єкт переміститься на цю ячейку
            if (eatTRUE1 && eatTRUE && net.rand.Next(0, 100) < 50)
            {
                // забуваємо про попередню ячейку
                net.cells[newX, newY].ReleaseAcc();

                //оновлюємо координати
                newX = x;
                newY = y;
            }

            // если ближайшая ячейка с едой еще не найдена и анализируемая ячейка содержит еду
            // делаем анализируемую ячейку новой ячейкой для перемещения
            else if (!eatTRUE1 && eatTRUE)
            {
                // если в качестве следующей ячейки уже выбрана другая ячейка, открываем доступ к єтой ячейке
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
                // если в качестве следующей ячейки уже выбрана другая ячейка, открываем доступ к єтой ячейке
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

                //якщо гра закінчена, цикл завершується
                if (net.simulateFinish)
                    return;



                if (!net.SimulatePause)
                {

                    //відкриваємо доступ до ячейок
                    if (Y > 0)
                        await net.cells[X, Y - 1].giveAsync();
                    if (X > 0)
                        await net.cells[X - 1, Y].giveAsync();


                    //даємо доступ до поточної ячейки
                    await net.cells[X, Y].giveAsync();

                    //отримуємо об'єкт, що знаходиться в поточній ячейці
                    var animale = net.cells[X, Y].Element as AnimalSett;

                    //перевірка життєвого циклу об'єкта
                    if (animale != null)
                    {
                        if(animale.life == 0)
                        {
                            net.cells[X, Y].Delete();
                            await minusC4ET();
                        }
                    }
                    // Перевірка, чи елемент в поточній клітині співпадає з поточним об'єктом.
                    // Якщо ні, то відбувається вивільнення ресурсів та повертається з методу.
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


                    //аналізуємо можливі позіції
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

                    

                    //якщо переміщення об'єкту відбулось
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
                    //якщо не відбулось
                    else
                    {
                        net.cells[X, Y].ReleaseAcc();
                    }
                }   


                //затримка перед наступним мувом
                await Task.Delay(net.rand.Next(500, 1000));
            }
        }

        //зменшення їжі
        public abstract Task minusC4ETeat();


        //метод, що викликається коли хтось вмирає
        public async void minusLife()
        {
            while (true)
            {
                if (net.simulateFinish)
                    return;

                if (!net.SimulatePause)
                {
                    //блокуємо доступ
                    await liveAnim.WaitAsync();
                    life--;
                    //звільнення блокування
                    liveAnim.Release();
                    int a = X, b = Y;
                    //коли об'єкт вмирає оновлюємо інфу про ячейку
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


        //кролик з'їв моркву
        public override bool eatrtue(ElementG element)
        {
            return element is Carrot;
        }

        //кролик вмер
        public override async Task minusC4ET()
        {
            await net.CountRabbitMinus();
        }

        //кролика з'їв вовк
        public override async Task minusC4ETeat()
        {
            await Task.Delay(0);
        }

    }

    public class Wolf : AnimalSett
    {
        public Wolf(int X, int Y, Nets net) : base(X, Y, net) { }


        //вовк з'їв кролика
        public override bool eatrtue(ElementG element)
        {
            return element is Rabbit;
        }

        //вовк вмер
        public override async Task minusC4ET()
        {
            await net.CountWolfMinus();
        }

        //вовк з'їв кролика, оновлюємо к-ть кроликів
        public override async Task minusC4ETeat()
        {
            await net.CountRabbitMinus();
        }
    }

    //морква
    public class Carrot : ElementG
    {
        public Carrot(int X, int Y, Nets net) : base(X, Y, net) { }
    }

    //клас що зберігає інфу скільки живих кролів і вовків
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

    //клас що зберігає інфу про зміни в конкретній клітині на сітці
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
}
