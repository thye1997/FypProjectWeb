``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1237 (21H1/May2021Update)
AMD Ryzen 5 3550H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=5.0.302
  [Host]     : .NET Core 3.1.17 (CoreCLR 4.700.21.31506, CoreFX 4.700.21.31502), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 3.1.17 (CoreCLR 4.700.21.31506, CoreFX 4.700.21.31502), X64 RyuJIT


```
|               Method |     Mean |     Error |    StdDev |   Median | Allocated |
|--------------------- |---------:|----------:|----------:|---------:|----------:|
|   GetAppointmentList | 2.332 ms | 0.1780 ms | 0.5108 ms | 2.104 ms |    149 KB |
| GetAppointmentList_2 | 5.394 ms | 1.6113 ms | 4.7256 ms | 2.523 ms |    169 KB |
