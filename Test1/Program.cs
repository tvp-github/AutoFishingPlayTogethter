using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KAutoHelper;
namespace Test1
{
    class Program
    {

        static void Main(string[] args)
        {

            Task t = null;
            bool fl = true;
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;
            int min;
            int f = 1;
            try
            {
                bool ok = Int32.TryParse(args[0], out min);
                if (!ok)
                    min = 10000;

                ok = Int32.TryParse(args[1], out f);
                if (!ok)
                    f = 1;
            }
            catch (Exception e)
            {
                min = 10000;
                f = 1;
            }
            if (f <= 0 || f > 6)
            {
                f = 1;
            }
            Run(ct, min, f);
        }

        private static async void Run(CancellationToken ct, int min, int f)
        {
            ct.ThrowIfCancellationRequested();
            float[] MIN = { 0, 0, 0.9f };
            float[] MAX = { 180, 1, 1 };

            /*
            Console.WriteLine("Chi nut keo");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(5 - i);
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            }
            Rectangle bounder1 = ScreenCapture.GetActiveWindowRect();

            Console.WriteLine(bounder1);
            Point keo1 = ScreenCapture.GetCursorPosition();
            Console.WriteLine(keo1);
            Console.WriteLine("Chi nut tha");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(5 - i);
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            }

            Point tha1 = ScreenCapture.GetCursorPosition();
            Console.WriteLine(tha1);

            Console.WriteLine("Chi nut bao quan");

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(5 - i);
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            }
            Point next1 = ScreenCapture.GetCursorPosition();
            Console.WriteLine(next1);
            return;
            */
            int countKeo = 0;

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(5 - i);
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            }
            IntPtr ld = ScreenCapture.GetForegroundWindow();
            Rectangle bounder = ScreenCapture.GetActiveWindowRect(ld);
            //AutoControl.SendClickOnPosition(ld, 300, 300);

            Point keo = new Point((int)(bounder.X + 0.847 * bounder.Width), (int)(bounder.Y + 0.746 * bounder.Height));
            Point tha = new Point((int)(bounder.X + 0.771 * bounder.Width), (int)(bounder.Y + 0.580 * bounder.Height));
            Point next = new Point((int)(bounder.X + 0.741 * bounder.Width), (int)(bounder.Y + 0.713 * bounder.Height));
            Point middle = new Point((int)(bounder.X + 0.48 * bounder.Width), (int)(bounder.Y + 0.72 * bounder.Height));
            Point balo = new Point((int)(bounder.X + 0.931 * bounder.Width), (int)(bounder.Y + 0.518 * bounder.Height));
            Point tool = new Point((int)(bounder.X + 0.684 * bounder.Width), (int)(bounder.Y + 0.081 * bounder.Height));
            Point f2 = new Point((int)(bounder.X + (0.729 - 0.149 * 2 + 0.149 * ((f - 1) % 3 + 1)) * bounder.Width), (int)(bounder.Y + (0.442 + 0.33 * (int)((f - 1) / 3)) * bounder.Height));
            Console.WriteLine("Run");
            bool needToRepair = true;
            while (true)
            {
                if (needToRepair)
                {
                    Random rand = new Random();
                    if (rand.Next(5) == 0)
                    {
                        click(ld, tha, bounder);
                        Task.Delay(TimeSpan.FromSeconds(2)).Wait();
                    }
                    click(ld, middle, bounder);
                    Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                    click(ld, next, bounder);
                    Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                    //click(ld, balo, bounder);
                    click(ld, balo, bounder);
                    Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                    click(ld, tool, bounder);
                    Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                    click(ld, f2, bounder);
                    Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                    click(ld, middle, bounder);
                    Task.Delay(TimeSpan.FromSeconds(2)).Wait();
                    click(ld, middle, bounder);
                    Task.Delay(TimeSpan.FromSeconds(2)).Wait();
                    click(ld, middle, bounder);
                    Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                    //Chon lai can cau
                    click(ld, next, bounder);
                    Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                    click(ld, balo, bounder);
                    Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                    click(ld, tool, bounder);
                    Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                    click(ld, f2, bounder);
                    Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                    click(ld, middle, bounder);
                    Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                    needToRepair = false;
                }
                Bitmap b1 = ScreenCapture.CaptureWindow(ld);
                int cCount = Count(MIN, MAX, b1);
                click(ld, tha, bounder);
                Task.Delay(TimeSpan.FromSeconds(3)).Wait();
                Bitmap b = ScreenCapture.CaptureWindow(ld);
                int fCount = Count(MIN, MAX, b);
                if (fCount - cCount > 7000)
                {
                    needToRepair = true;
                }
                else
                    while (true)
                    {
                        Bitmap sc = ScreenCapture.CaptureWindow(ld);
                        int count = Count(MIN, MAX, sc);
                        Console.WriteLine(count - fCount);
                        if (count - fCount > 2 * min)
                        {
                            Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                            needToRepair = true;
                            break;
                        }
                        if (count - fCount > min)
                        {
                            click(ld, keo, bounder);
                            //Task.Delay(TimeSpan.FromSeconds(5)).Wait();
                            countKeo++;
                            Console.WriteLine("So lan keo:" + countKeo);
                            int t = 0;
                            while (t++ < 20)
                            {
                                Task.Delay(TimeSpan.FromMilliseconds(500)).Wait();
                                if (isPanelOpen(MIN, MAX, ld))
                                {

                                    click(ld, next, bounder);
                                    Task.Delay(TimeSpan.FromSeconds(2)).Wait();
                                    break;
                                }
                            }
                            break;
                        }

                        Task.Delay(TimeSpan.FromMilliseconds(100)).Wait();
                    }

            }
        }

        private static int Count(float[] MIN, float[] MAX, Bitmap b)
        {
            int h = b.Size.Height;
            int w = b.Size.Width;
            int size = 3 * h / 5;
            int count = 0;
            for (int i = w / 2 - size / 2; i < w / 2 + size / 2; i++)
            {
                for (int j = h / 2 - size / 2; j < h / 2 + size / 2; j++)
                {
                    Color c = b.GetPixel(i, j);
                    float H = c.GetHue();
                    float S = c.GetSaturation();
                    float L = c.GetBrightness();
                    if (H <= MAX[0] && S <= MAX[1] && L <= MAX[2] && H >= MIN[0] && S >= MIN[1] && L >= MIN[2])
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        private static bool isPanelOpen(float[] MIN, float[] MAX, IntPtr ld)
        {
            Bitmap b = ScreenCapture.CaptureWindow(ld);
            int h = b.Size.Height;
            int w = b.Size.Width;
            int count = 0;
            for (int i = w * 3 / 5; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    Color c = b.GetPixel(i, j);
                    float H = c.GetHue();
                    float S = c.GetSaturation();
                    float L = c.GetBrightness();
                    if (H <= MAX[0] && S <= MAX[1] && L <= MAX[2] && H >= MIN[0] && S >= MIN[1] && L >= MIN[2])
                    {
                        count++;
                    }
                }
            }
            return count > (w * h / 10);
        }
        private static void click(IntPtr ld, Point point, Rectangle bounder)
        {
            List<IntPtr> children = AutoControl.GetChildHandle(ld);
            foreach (var child in children)
            {
                AutoControl.SendClickOnPosition(child, point.X - bounder.X, point.Y - bounder.Y);
            }
        }
    }
}
