using System;

using MartianMike.Objects;

namespace MartianMike.Core;

internal static class GlobalEvents
{
    public static Action TrapTriggered;
    public static Action<ExitArea> ExitAreaReached;
}
