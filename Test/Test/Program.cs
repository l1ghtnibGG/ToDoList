using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SimpleWifi;

public class Test
{
    public Test()
    {

    }
    private class A
    {
        public int x = 1;
    }
    private class B : A
    {
        public string pos = "!";
    }
    static ReaderWriterLockSlim readerWriterLock = new ReaderWriterLockSlim();
    static void Main()
    {
        Wifi wifi = new Wifi();

        List<AccessPoint> accessPointsList = wifi.GetAccessPoints().
            OrderByDescending(x => x.SignalStrength).ToList();

        foreach (AccessPoint accessPoint in accessPointsList)
        {
            Console.WriteLine(accessPoint.Name);
            Console.WriteLine(accessPoint.SignalStrength);
            Console.WriteLine(accessPoint.IsSecure);
            Console.WriteLine(accessPoint.IsConnected);
            Console.WriteLine();
        }
    }

    public static int RemoveElement(int[] nums, int val)
    {
        var last = nums.Length - 1;
        var i = 0;

        while (i < last)
        {
            if (nums[i] == val)
            {
                if (nums[last] != val)
                {
                    (nums[i], nums[last]) = (nums[last], nums[i]);
                }
                else
                {
                    while (nums[last] == val)
                    {
                        if (last == i) break;

                        last--;
                    }

                    (nums[i], nums[last]) = (nums[last], nums[i]);
                    last--;
                }
            }

            i++;
        }

        nums = nums.Where(x => x != val).ToArray();

        return i;
    }

    private static string TaskWithResult()
    {
        Task.Delay(2000);
        //Thread.Sleep(2000);
        return "Hello";
    }

    private static void Hello()
    {
        Console.WriteLine("Hello");
    }

    static int outer()
    {
        inner();
        dynamic x = 'o';
        inner();
        return x;
        void inner() => x = "1";
    }

    public struct Q : IDisposable
    {
        const string @answer = "42";
        const string @const = $"6 * 7 = {answer}";
        public Q(int a, int b)
        {

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public record struct W { public int P { get; init; } }
}

struct IIn
{
    private void Move()
    {

    }
}

public class Person<T>
{
    void Move<T>()
    {

    }
}
