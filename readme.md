# Benchmarks for EF Core

## Benchmark results

### PostgreSQL

```
BenchmarkDotNet=v0.13.1, OS=macOS Monterey 12.2 (21D49) [Darwin 21.3.0]
Apple M1 2.40GHz, 1 CPU, 8 logical and 8 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  DefaultJob : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT


|                  Method |      Mean |     Error |    StdDev |
|------------------------ |----------:|----------:|----------:|
|         RunDefaultAsync |  6.304 ms | 0.1257 ms | 0.1345 ms |
| RunWithoutOrderingAsync | 14.967 ms | 0.2877 ms | 0.5877 ms |
|         RunAsSplitAsync |  9.448 ms | 0.0882 ms | 0.0825 ms |
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
