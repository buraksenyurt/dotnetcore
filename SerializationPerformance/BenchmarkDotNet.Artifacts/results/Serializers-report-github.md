``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.15063.1266 (1703/CreatorsUpdate/Redstone2)
Intel Core i7-6600U CPU 2.60GHz (Max: 2.61GHz) (Skylake), 1 CPU, 4 logical and 2 physical cores
Frequency=2742187 Hz, Resolution=364.6724 ns, Timer=TSC
.NET Core SDK=2.1.401
  [Host]     : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT
  Job-ZLGOUL : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT

Runtime=Core  MaxIterationCount=5  MaxWarmupIterationCount=8  
MinIterationCount=3  

```
|            Method | BookCount |         Mean |        Error |       StdDev |
|------------------ |---------- |-------------:|-------------:|-------------:|
|            **ToJson** |         **1** |     **416.1 us** |     **12.94 us** |     **3.360 us** |
|     ToMessagePack |         1 |     375.6 us |     43.21 us |    11.224 us |
| ToMessagePackJson |         1 |     391.1 us |     82.22 us |    21.356 us |
|        ToProtobuf |         1 |     385.8 us |     61.31 us |    15.925 us |
|            **ToJson** |        **10** |     **415.4 us** |     **38.41 us** |     **9.976 us** |
|     ToMessagePack |        10 |     401.4 us |     78.34 us |    20.348 us |
| ToMessagePackJson |        10 |     398.0 us |     71.87 us |    18.668 us |
|        ToProtobuf |        10 |     394.4 us |     83.55 us |    21.702 us |
|            **ToJson** |      **1000** |   **2,492.2 us** |    **716.95 us** |   **186.224 us** |
|     ToMessagePack |      1000 |     555.3 us |     90.84 us |    23.596 us |
| ToMessagePackJson |      1000 |   1,446.8 us |    374.64 us |    97.311 us |
|        ToProtobuf |      1000 |   1,264.8 us |     55.97 us |    14.538 us |
|            **ToJson** |     **10000** |  **19,112.7 us** |  **5,468.11 us** | **1,420.322 us** |
|     ToMessagePack |     10000 |   2,043.3 us |    537.94 us |   139.727 us |
| ToMessagePackJson |     10000 |   9,906.5 us |  1,993.67 us |   517.847 us |
|        ToProtobuf |     10000 |   7,217.9 us |    461.00 us |   119.742 us |
|            **ToJson** |    **100000** | **180,636.8 us** | **21,877.57 us** | **5,682.617 us** |
|     ToMessagePack |    100000 |  17,483.2 us |  1,275.54 us |   331.317 us |
| ToMessagePackJson |    100000 | 104,183.6 us | 29,461.76 us | 7,652.583 us |
|        ToProtobuf |    100000 |  77,098.1 us | 33,598.03 us | 8,726.963 us |
