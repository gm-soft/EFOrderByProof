# Benchmarks for EF Core

## Benchmark results

### PostgreSQL

```
BenchmarkDotNet=v0.13.1, OS=macOS Monterey 12.2 (21D49) [Darwin 21.3.0]
Apple M1 2.40GHz, 1 CPU, 8 logical and 8 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  DefaultJob : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT


|                  Method |      Mean |     Error |    StdDev |    Gen 0 |    Gen 1 | Allocated |
|------------------------ |----------:|----------:|----------:|---------:|---------:|----------:|
|         RunDefaultAsync |  6.225 ms | 0.1171 ms | 0.1202 ms | 609.3750 |        - |      1 MB |
| RunWithoutOrderingAsync | 14.521 ms | 0.2030 ms | 0.1695 ms | 687.5000 |        - |      2 MB |
|         RunAsSplitAsync |  9.854 ms | 0.1887 ms | 0.3153 ms | 640.6250 | 218.7500 |      2 MB |
```

### Azure SQL Server



## What do you need to run the benchmarks

- Run Docker app
- Have .NET 6 SDK installed

## To run the benchmarks

```bash
# SQL Server
dotnet run --project SqlServerApp/SqlServerApp.csproj --configuration=Release

# PostgreSQL
dotnet run --project PostgreApp/PostgreApp.csproj --configuration=Release
```
