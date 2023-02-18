using System.Collections.Generic;

namespace DefaultNamespace
{
    public static class BingoPayTableManger
    {

        public static PayTableEvent[] PayTableEvent =
        {
            new PayTableEvent()
                { Line = new[] { 0, 1, 2, 3, 4 } },
            new PayTableEvent()
                { Line = new[] { 5, 6, 7, 8, 9 } },
            new PayTableEvent()
                { Line = new[] { 10, 11, 12, 13, 14 } },
            new PayTableEvent()
                { Line = new[] { 15, 16, 17, 18, 19 } },
            new PayTableEvent()
                { Line = new[] { 20, 21, 22, 23, 24 } },
            new PayTableEvent()
                { Line = new[] { 0, 5, 10, 15, 20 } },
            new PayTableEvent()
                { Line = new[] { 1, 6, 11, 16, 21 } },
            new PayTableEvent()
                { Line = new[] { 2, 7, 12, 17, 22 } },
            new PayTableEvent()
                { Line = new[] { 3, 8, 13, 18, 23 } },
            new PayTableEvent()
                { Line = new[] { 4, 9, 14, 19, 24 } },
            new PayTableEvent()
                { Line = new[] { 0, 6, 12, 18, 24 } },
            new PayTableEvent()
                { Line = new[] { 20, 16, 12, 8, 4 } },
        };
    }

    public class PayTableEvent
    {
        public int[] Line;
        public bool Check;
    }
}